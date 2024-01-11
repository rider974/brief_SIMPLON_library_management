using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryManagement.Entity
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 

        public string? ISBN { get; set; }

        public required string Title { get; set; } = null!;

        public  DateTime PublicationDate { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? Author { get; set; }

        public string? Categorie { get; set; } = null; 

        public Boolean isAvailable { get; set; }


    }
}
