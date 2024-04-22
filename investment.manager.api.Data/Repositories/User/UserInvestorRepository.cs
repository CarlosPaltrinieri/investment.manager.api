using investiment.manager.api.Interfaces;
using investiment.manager.api.Interfaces.User;
using investiment.manager.api.Models.User;

namespace investiment.manager.api.Repositories.User
{
    public class UserInvestorRepository(IRepositoryBase<UserInvestor> repositoryBase) : IUserInvestorRepository
    {
        private readonly IRepositoryBase<UserInvestor> _repositoryBase = repositoryBase;

        public async Task CreateUser(UserInvestor user, string collectionName)
        {
            await _repositoryBase.AddAsync(user, collectionName);
        }

        public async Task<List<UserInvestor>> GetUser(Dictionary<string, string> filter, string collectionName)
        {
            return await _repositoryBase.GetAsync(collectionName, filter);
        }
    }
}
