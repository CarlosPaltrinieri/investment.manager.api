using investiment.manager.api.Interfaces;
using investiment.manager.api.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace investiment.manager.api.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public RepositoryBase(IOptions<DbContext> dbContext)
        {
            var mongoClient = new MongoClient(
                dbContext.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                dbContext.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<T>(
                dbContext.Value.BooksCollectionName);
        }

        public async Task AddAsync(object item)
        {
            await _collection.InsertOneAsync((T)item);
        }

        public async Task<List<T>> GetAsync(Dictionary<string, string> filters)
        {
            if (filters == null || filters.Count == 0)
                return await _collection.FindAsync(_ => true).Result.ToListAsync();
            else
            {
                BsonDocument filter = GetFilters(filters);

                return await _collection.FindAsync(filter).Result.ToListAsync();
            }
        }

        public async Task UpdateAsync(Dictionary<string, string> filters, object item)
        {
            BsonDocument filter = GetFilters(filters);

            await _collection.ReplaceOneAsync(filter, (T)item);
        }

        private static BsonDocument GetFilters(Dictionary<string, string> filters)
        {
            var filter = new BsonDocument();
            foreach (var item in filters)
                filter.Add(item.Key, item.Value);
            return filter;
        }
    }
}
