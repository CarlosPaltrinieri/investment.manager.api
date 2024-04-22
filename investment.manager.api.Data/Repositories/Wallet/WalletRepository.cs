using investiment.manager.api.Interfaces;
using investment.manager.api.Data.Interfaces.Wallet;
using investment.manager.api.Domain.Models.Wallet;

namespace investment.manager.api.Data.Repositories.Wallet
{
    public class WalletRepository(IRepositoryBase<WalletUser> repositoryBase) : IWalletRepository
    {
        private readonly IRepositoryBase<WalletUser> _repositoryBase = repositoryBase;

        public async Task BuyAction(WalletUser wallet, string collectionName)
        {
            await _repositoryBase.AddAsync(wallet, collectionName);
        }

        public async Task<List<WalletUser>> GetWalletByUserId(Dictionary<string,string> filter, string collectionName)
        {
            return await _repositoryBase.GetAsync(collectionName, filter);
        }

        public async Task SellAction(Dictionary<string, string> filter, string collectionName)
        {
            await _repositoryBase.DeleteAsync(filter, collectionName);
        }
    }
}
