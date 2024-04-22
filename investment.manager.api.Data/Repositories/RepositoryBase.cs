using investiment.manager.api.Interfaces;
using investiment.manager.api.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace investiment.manager.api.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<T> _collection;

        public RepositoryBase(IOptions<DbContext> dbContext)
        {
            var mongoClient = new MongoClient(
                dbContext.Value.ConnectionString);

            _database = mongoClient.GetDatabase(
                dbContext.Value.DatabaseName);
        }

        public async Task AddAsync(object item, string collectionName)
        {
            _collection = _database.GetCollection<T>(collectionName);
            await _collection.InsertOneAsync((T)item);
        }

        public async Task<List<T>> GetAsync(string collectionName, Dictionary<string, string> filters)
        {
            _collection = _database.GetCollection<T>(collectionName);

            if (filters == null || filters.Count == 0)
                return await _collection.FindAsync(_ => true).Result.ToListAsync();
            else
            {
                BsonDocument filter = GetFilters(filters);

                return await _collection.FindAsync(filter).Result.ToListAsync();
            }
        }

        public async Task UpdateAsync(Dictionary<string, string> filters, object item, string collectionName)
        {
            _collection = _database.GetCollection<T>(collectionName);

            BsonDocument filter = GetFilters(filters);

            await _collection.ReplaceOneAsync(filter, (T)item);
        }

        public async Task DeleteAsync(Dictionary<string, string> filters, string collectionName)
        {
            _collection = _database.GetCollection<T>(collectionName);

            BsonDocument filter = GetFilters(filters);

            await _collection.DeleteOneAsync(filter);
        }

        private static BsonDocument GetFilters(Dictionary<string, string> filters)
        {
            var filter = new BsonDocument();
            foreach (var item in filters)
                if (item.Key.Equals("Id"))
                    filter.Add("_id", new ObjectId(item.Value));
                else
                    filter.Add(item.Key, item.Value);
            return filter;
        }
    }
}
