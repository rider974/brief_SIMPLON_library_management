using LibraryManagement.Configurations;
using LibraryManagement.Entity;
using LibraryManagement.Services.Contract;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.ObjectModel;

namespace LibraryManagement.Services.Implementation
{
    public class BookService : IBookService
    {

        private readonly IMongoCollection<Book> _bookCollection;

        public BookService(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _bookCollection = database.GetCollection<Book>(settings.Value.BooksCollectionName);


        }


        public async Task<List<Book>> GetBooksAsync()
        {

            var filter = Builders<Book>.Filter.Empty;

            return await _bookCollection.Find(filter).ToListAsync();
        }

        public async Task<Book> CreateBook(Book bookToCreate)
        {
            
            await _bookCollection.InsertOneAsync(bookToCreate);

            return bookToCreate;
        }

        public async Task<Book> GetBookByIdAsync(string id)
        {
            return await _bookCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

       public async Task<List<Book>> GetBooksByCategoryAsync(string category)
        {
           List<Book> listBooks =  await _bookCollection.Find(book => book.Categorie == category).ToListAsync();  
            
            return listBooks;
        }

        public async Task<List<Book>> GetBooksByTitleAsync(string title)
        {
            List<Book> listBooks = await _bookCollection.Find(book => book.Title == title).ToListAsync();

            return listBooks;
        }


        public async Task<List<Book>> GetBooksByAuthorAsync(string authorName)
        {
            List<Book> listBooks =  await _bookCollection.Find(book => book.Author == authorName).ToListAsync();

            return listBooks;
        }
    }
}
