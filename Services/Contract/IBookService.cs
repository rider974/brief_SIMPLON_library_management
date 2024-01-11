using LibraryManagement.Entity;

namespace LibraryManagement.Services.Contract
{
    public interface IBookService
    {
        Task<Book> GetBookByIdAsync(string id);

        Task<Book> CreateBook(Book book);

        // Task<Book> UpdateBookAsync(string id, Book book);

        Task<List<Book>> GetBooksAsync(); 
        Task<List<Book>> GetBooksByCategoryAsync(string category);


        Task<List<Book>> GetBooksByTitleAsync(string title);

        Task<List<Book>> GetBooksByAuthorAsync(string authorName);

    }
}
