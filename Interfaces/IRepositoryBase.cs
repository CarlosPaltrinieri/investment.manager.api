namespace investiment.manager.api.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        public Task<List<T>> GetAsync(Dictionary<string, string> filters = null);

        public Task AddAsync(object item);

        public Task UpdateAsync(Dictionary<string, string> filters, object item);
    }
}
