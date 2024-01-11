using LibraryManagement.Entity;

namespace LibraryManagement.Services.Contract
{
    public interface IAuthorService
    {
        Task<Author> GetAuthorByIdAsync(string id);


        Task<Author> CreateAuthor(Author author);


        Task<Author> UpdateAuthorAsync(Author author);
    }
}
