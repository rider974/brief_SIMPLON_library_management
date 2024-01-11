using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.ObjectModel;

namespace LibraryManagement.Entity
{
    public class Author
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 

        public string? Name { get; set; } = null;

        public string? Biography { get; set; } = null;

        public string? Birthdate { get; set; } = null;

        public Collection<Book>? bookList { get; set; } = null; 
    }
}
