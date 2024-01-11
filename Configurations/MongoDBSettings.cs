namespace LibraryManagement.Configurations
{
    public class MongoDBSettings
    {
        public string? ConnectionURI { get; set; } = null;

        public string? DatabaseName { get; set; } = null;

        public string? BooksCollectionName { get; set; } = null;

        public string? AuthorsCollectionName { get; set; } = null;


        public string? BorrowersCollectionName { get; set; } = null; 

    }
}
