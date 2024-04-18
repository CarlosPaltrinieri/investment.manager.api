using investiment.manager.api.Models.Investment;

namespace investiment.manager.api.Interfaces.Investment
{
    public interface IInvestmentRepository
    {
        public Task<List<InvestmentModel>> GetInvestmentAsync(Dictionary<string, string> filters = null);
        public Task CreateInvestiment(InvestmentModel model);
        public Task UpdateInvestmentAsync(InvestmentModel model, Dictionary<string, string> filters);
    }
}
