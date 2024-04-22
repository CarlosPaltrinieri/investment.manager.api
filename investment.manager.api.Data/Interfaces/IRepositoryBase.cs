namespace investiment.manager.api.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        public Task<List<T>> GetAsync(string collectionName, Dictionary<string, string> filters = null);

        public Task AddAsync(object item, string collectionName);

        public Task UpdateAsync(Dictionary<string, string> filters, object item, string collectionName);
        public Task DeleteAsync(Dictionary<string, string> filters, string collectionName);
    }
}
