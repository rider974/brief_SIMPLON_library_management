using LibraryManagement.Entity;

namespace LibraryManagement.Services.Contract
{
    public interface IBorrowerService
    {
        Task<Borrower> GetBorrowerByIdAsync(string id);

        Task<Borrower> CreateBorrower(Borrower borrower);

        // Task<Book> UpdateBookAsync(string id, Book book);

        Task<Boolean> BorrowBook(string idBook, string idBorrower);

        Task<Borrower> ReturnBook(string idBook, string idBorrower);

        Task<Dictionary<string, int>> GetBooksByTotalBorrowing();

    }
}
