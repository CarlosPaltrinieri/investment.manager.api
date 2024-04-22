using investment.manager.api.Domain.Models.Wallet;

namespace investment.manager.api.Data.Interfaces.Wallet
{
    public interface IWalletRepository
    {
        public Task BuyAction(WalletUser wallter, string collectionName);
        public Task SellAction(Dictionary<string, string> filter, string collectionName);
        public Task<List<WalletUser>> GetWalletByUserId(Dictionary<string,string> filter, string collectionName);
    }
}
