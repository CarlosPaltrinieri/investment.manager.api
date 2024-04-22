using investiment.manager.api.Interfaces.Investment;
using investiment.manager.api.Utils;
using investment.manager.api.Data.Interfaces.Wallet;
using investment.manager.api.Domain.Models.Wallet;
using Microsoft.Extensions.Options;
using System.Net;

namespace investment.manager.api.Data.Services.Wallet
{
    public class WalletService(IWalletRepository repository, IInvestmentRepository investmentRepository, IOptions<DbContext> dbContext)
    {
        private readonly IWalletRepository _walletRepository = repository;
        private readonly IInvestmentRepository _investmentRepository = investmentRepository;
        private readonly ResponseExtension _response = new();
        private readonly IOptions<DbContext> _dbContext = dbContext;
        public async Task<ResponseExtension> SellInvestment(WalletUser walletUser)
        {
            var filterUser = new Dictionary<string, string>
            {
                { "IdUser", walletUser.IdUser },
                { "Email", walletUser.Email }
            };

            var userHasInvestment = await ValidateUserInvesment(walletUser, filterUser);

            if (userHasInvestment)
            {
                await _walletRepository.SellAction(filterUser, _dbContext.Value.Collection.Wallet);

                return _response.Response(HttpStatusCode.OK, "Investment selled successfully");
            }
            else
                return _response.Response(HttpStatusCode.NoContent, $"User provided haven't the invesment selected");
        }
        public async Task<ResponseExtension> BuyInvestment(WalletUser walletUser)
        {
            var filterInvestment = new Dictionary<string, string>
            {
                {"Id", walletUser.IdInvestment}
            };

            var listInvestment = await _investmentRepository.GetInvestmentAsync(_dbContext.Value.Collection.Investment, filterInvestment);
            var investment = listInvestment?.FirstOrDefault();

            if (investment != null)
            {
                var filterUser = new Dictionary<string, string>
                {
                    {"IdUser", walletUser.IdUser },
                    {"Email", walletUser.Email }
                };

                var userHasSameInvestment = await ValidateUserInvesment(walletUser, filterUser);

                if (userHasSameInvestment)
                    return _response.Response(HttpStatusCode.NoContent, "User already has the investment selected.");
                else
                {
                    await _walletRepository.BuyAction(walletUser, _dbContext.Value.Collection.Wallet);

                    return _response.Response(HttpStatusCode.OK, "Investment bought successfully.");
                }
            }

            return _response.Response(HttpStatusCode.BadGateway, "Investment selected doesn't exists.");
        }

        public async Task<ResponseExtension> GetUserWaller(string userId)
        {
            var filterUser = new Dictionary<string, string>
                {
                    {"IdUser", userId},
                };

            var wallet = await _walletRepository.GetWalletByUserId(filterUser, _dbContext.Value.Collection.Wallet);

            return _response.Response(HttpStatusCode.OK, wallet);
        }

        private async Task<bool> ValidateUserInvesment(WalletUser walletUser, Dictionary<string, string> filterUser)
        {
            var listWalletUser = await _walletRepository.GetWalletByUserId(filterUser, _dbContext.Value.Collection.Wallet);

            var userHasInvestment = listWalletUser?.Where(x => x.IdInvestment == walletUser.IdInvestment).Any();

            if (userHasInvestment != null)
                return (bool)userHasInvestment;
            else
                return false;
        }
    }
}
