using investiment.manager.api.Interfaces;
using investiment.manager.api.Interfaces.User;
using investiment.manager.api.Models.User;

namespace investiment.manager.api.Repositories.User
{
    public class UserInvestorRepository(IRepositoryBase<UserInvestor> repositoryBase) : IUserInvestorRepository
    {
        private readonly IRepositoryBase<UserInvestor> _repositoryBase = repositoryBase;

        public async Task BuyAction(UserInvestor user, Dictionary<string, string> filter)
        {
            await _repositoryBase.UpdateAsync(filter, user);
        }

        public async Task CreateUser(UserInvestor user)
        {
            await _repositoryBase.AddAsync(user);
        }

        public async Task<List<UserInvestor>> GetUser(Dictionary<string, string> filter)
        {
            return await _repositoryBase.GetAsync(filter);
        }


        public async Task SellAction(UserInvestor user, Dictionary<string, string> filter)
        {
            await _repositoryBase.UpdateAsync(filter, user);
        }
    }
}
