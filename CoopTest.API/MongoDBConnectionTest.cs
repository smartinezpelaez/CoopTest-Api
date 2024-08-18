using MongoDB.Driver;

namespace CoopTest.API
{
    public class MongoDBConnectionTest
    {
        private readonly IMongoClient _client;

        public MongoDBConnectionTest(IMongoClient client)
        {
            _client = client;
        }

        public void CheckConnection()
        {
            try
            {
                var databases = _client.ListDatabaseNames().ToList();
                Console.WriteLine("Connected to MongoDB. Databases:");
                foreach (var db in databases)
                {
                    Console.WriteLine(db);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to MongoDB: {ex.Message}");
            }
        }
    }
}
