using LibraryManagement.Configurations;
using LibraryManagement.Entity;
using LibraryManagement.Services.Contract;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Buffers;
using System.Collections.ObjectModel;
using static MongoDB.Driver.WriteConcern;

namespace LibraryManagement.Services.Implementation
{
    public class BorrowerService : IBorrowerService
    {


        private readonly IMongoCollection<Book> _bookCollection;
        private readonly IMongoCollection<Borrower> _borrowerCollection;

        public BorrowerService(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _borrowerCollection = database.GetCollection<Borrower>(settings.Value.BorrowersCollectionName);
            _bookCollection = database.GetCollection<Book>(settings.Value.BooksCollectionName);
        }


        public async Task<Borrower> GetBorrowerByIdAsync(string id)
        {
            return await _borrowerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Borrower> CreateBorrower(Borrower borrowerToCreate)
        {
            await _borrowerCollection.InsertOneAsync(borrowerToCreate);

            return borrowerToCreate;
        }



        public async Task<Boolean> BorrowBook(string idBook, string idBorrower)
        {
            var borrower = await _borrowerCollection.Find(x => x.Id == idBorrower).FirstOrDefaultAsync();

            if (borrower == null) return false; 
            var bookBorrowed= await _bookCollection.Find(x => x.Id == idBook).FirstOrDefaultAsync();


            bookBorrowed.isAvailable = false;

            var bookFiltered = Builders<Book>.Filter.Eq(book => book.Id, bookBorrowed.Id);
            var bookUpdated = Builders<Book>.Update.Set(book => book.isAvailable, false);


            await _bookCollection.UpdateOneAsync(bookFiltered, bookUpdated);

            // books borrowed multiple times 
            // makes all the same book in the list unavailable 
            if (borrower.listBookBorrowed != null)
            {
                foreach (var oneBook in borrower.listBookBorrowed)
                {
                    if (oneBook.Id == bookBorrowed.Id)
                    {
                        oneBook.isAvailable = false;
                    }
                }
            }


            if (borrower.GetType().GetProperty("listBookBorrowed") == null) return false; 
            var borrowerBefore = Builders<Borrower>.Filter.Eq(oneBorrower => oneBorrower.Id, borrower.Id);

            
            borrower.listBookBorrowed.Add(bookBorrowed);

            var borrowerAfter = Builders<Borrower>.Update.Set(oneBorrower => oneBorrower.listBookBorrowed, borrower.listBookBorrowed);


            await _borrowerCollection.UpdateOneAsync(borrowerBefore, borrowerAfter);

            // update the book details inside the borrower's collection 


            return true;
        }

        /*private void setAllSameBookSameAvailability(Boolean isAvailable , Borrower borrower, )
        {
            foreach (var oneBook in arrayBooks)
            {
                if ()
            }
        }*/

        public async Task<Borrower>  ReturnBook(string idBook, string idBorrower)
        {
            var borrower = await _borrowerCollection.Find(x => x.Id == idBorrower).FirstOrDefaultAsync();

            var bookReturned = await _bookCollection.Find(x => x.Id == idBook).FirstOrDefaultAsync();

            bookReturned.isAvailable = true; 

            var bookFiltered = Builders<Book>.Filter.Eq(book => book.Id, bookReturned.Id);
            var bookUpdated = Builders<Book>.Update.Set(book => book.isAvailable, true);


            await _bookCollection.UpdateOneAsync(bookFiltered, bookUpdated);

            if (borrower.listBookBorrowed == null) return borrower; 
            foreach (var oneBook in borrower.listBookBorrowed)
            {
                if (oneBook.Id != idBook) continue;

                var borrowerBefore = Builders<Borrower>.Filter.Eq(oneBorrower => oneBorrower.Id, borrower.Id);


                oneBook.isAvailable = true;

                var borrowerAfter = Builders<Borrower>.Update.Set(oneBorrower => oneBorrower.listBookBorrowed, borrower.listBookBorrowed);

                await _borrowerCollection.UpdateOneAsync(borrowerBefore, borrowerAfter);

            }

            return borrower;
        }


        public async Task<Dictionary<string, int>> GetBooksByTotalBorrowing()
        {

            var allBooks = Builders<Book>.Filter.Empty;

            var allBooksBorrowed = Builders<Borrower>.Filter.Empty;

            Dictionary<string, int> allBooksWithBorrowedCount = new Dictionary<string, int>();

            foreach (var oneBook in await _bookCollection.Find(allBooks).ToListAsync())
            {
                var count = 0;
                var allBorrowers = await _borrowerCollection.Find(allBooksBorrowed).ToListAsync();

                if (allBorrowers == null) continue; 

                foreach (var oneBorrower in allBorrowers)
                {
                    if (oneBorrower.listBookBorrowed == null) continue; 

                    foreach (var oneBookBorrowed in oneBorrower.listBookBorrowed)
                    {
                        if (oneBook.Id != oneBookBorrowed.Id) continue;

                        count++;
                    }
                }
                allBooksWithBorrowedCount.Add(oneBook.Title, count);
                count = 0;
            }

            return allBooksWithBorrowedCount; 
        }


      /* TODO public async Task<Dictionary<string, Array>> GetAuthorsByBorrowedBook()
        {

        }*/
    }
}
