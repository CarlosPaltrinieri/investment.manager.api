using investiment.manager.api.Interfaces;
using investiment.manager.api.Interfaces.Investment;
using investiment.manager.api.Models.Investment;

namespace investiment.manager.api.Repositories.Investment
{
    public class InvestmentRepository(IRepositoryBase<InvestmentModel> repositoryBase) : IInvestmentRepository
    {
        private readonly IRepositoryBase<InvestmentModel> _repositoryBase = repositoryBase;

        public async Task<List<InvestmentModel>> GetInvestmentAsync(string collectionName, Dictionary<string, string> filters)
        {
            return await _repositoryBase.GetAsync(collectionName, filters);
        }

        public async Task CreateInvestiment(InvestmentModel model, string collectionName)
        {
            await _repositoryBase.AddAsync(model, collectionName);
        }

        public async Task UpdateInvestmentAsync(InvestmentModel model, Dictionary<string, string> filters, string collectionName)
        {
            await _repositoryBase.UpdateAsync(filters, model, collectionName);
        }


    }
}
