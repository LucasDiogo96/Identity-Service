using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Sample.Identity.Domain.Entities;
using Sample.Identity.Infra.Contracts;

namespace Sample.Identity.Infra.Contexts
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public MongoClient MongoClient { get; set; }

        private readonly List<Func<Task>> commands;

        public IClientSessionHandle Session { get; set; }

        public MongoContext(IConfiguration configuration)
        {
            commands = new List<Func<Task>>();

            string databaseName = configuration["DatabaseName"];

            MongoClient = new MongoClient(configuration.GetConnectionString(databaseName));

            Database = MongoClient.GetDatabase(databaseName);
        }

        public void Configure()
        {
            List<string> scope = new List<string>();

            // Add collection names
            scope.Add(nameof(User));

            // Add collection if not exists
            foreach (string collection in scope)
            {
                if (!CollectionExists(collection))
                    Database.CreateCollection(nameof(User));
            }
        }

        public async Task<int> SaveChanges()
        {
            using (Session = await MongoClient.StartSessionAsync())
            {
                IEnumerable<Task>? commandTasks = commands.Select(c => c());

                await Task.WhenAll(commandTasks);
            }

            return commands.Count;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func)
        {
            commands.Add(func);
        }

        public bool CollectionExists(string collectionName)
        {
            BsonDocument? filter = new BsonDocument("name", collectionName);
            //filter by collection name
            IAsyncCursor<string>? collections = Database.ListCollectionNames(new ListCollectionNamesOptions { Filter = filter });
            //check for existence
            return collections.Any();
        }
    }
}