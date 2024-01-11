using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.ObjectModel;

namespace LibraryManagement.Entity
{
    public class Borrower
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public Collection<Book>? listBookBorrowed { get; set; } = null; 
    }
}
