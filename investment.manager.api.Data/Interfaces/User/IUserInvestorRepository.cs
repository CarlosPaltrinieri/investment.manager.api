
using investiment.manager.api.Models.User;

namespace investiment.manager.api.Interfaces.User
{
    public interface IUserInvestorRepository
    {
        public Task CreateUser(UserInvestor user, string collectionName);
        public Task<List<UserInvestor>> GetUser(Dictionary<string, string> filter, string collectionName);
        
    }
}
