using LibraryManagement.Configurations;
using LibraryManagement.Entity;
using LibraryManagement.Services.Contract;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibraryManagement.Services.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly IMongoCollection<Author> _authorCollection;
        public AuthorService(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _authorCollection = database.GetCollection<Author>(settings.Value.AuthorsCollectionName);


        }

        public async Task<Author> GetAuthorByIdAsync(string id)
        {
            return await _authorCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Author> CreateAuthor(Author author)
        {
             await _authorCollection.InsertOneAsync(author);
            
            return author;
        }

        public async Task<Author> UpdateAuthorAsync(Author authorToUpdate)
        {
            var author = await _authorCollection.Find(x => x.Id == authorToUpdate.Id).FirstOrDefaultAsync();

            
            if (author == null) return authorToUpdate;

            var authorBefore = Builders<Author>.Filter.Eq(author => author.Id, authorToUpdate.Id);
            var authorBirthdate = Builders<Author>.Update.Set(author => author.Birthdate, authorToUpdate.Birthdate);
            var authorBiography = Builders<Author>.Update.Set(author => author.Biography, authorToUpdate.Biography);

            var authorName = Builders<Author>.Update.Set(author => author.Name, authorToUpdate.Name);

            await _authorCollection.UpdateOneAsync(authorBefore, authorBirthdate);
            await _authorCollection.UpdateOneAsync(authorBefore, authorBiography);
            await _authorCollection.UpdateOneAsync(authorBefore, authorName);


            return author;
        }


    }
}
