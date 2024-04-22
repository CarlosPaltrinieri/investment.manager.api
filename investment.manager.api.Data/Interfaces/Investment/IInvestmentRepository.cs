using investiment.manager.api.Models.Investment;

namespace investiment.manager.api.Interfaces.Investment
{
    public interface IInvestmentRepository
    {
        public Task<List<InvestmentModel>> GetInvestmentAsync(string collectionName, Dictionary<string, string> filters = null);
        public Task CreateInvestiment(InvestmentModel model, string collectionName);
        public Task UpdateInvestmentAsync(InvestmentModel model, Dictionary<string, string> filters, string collectionName);
    }
}
