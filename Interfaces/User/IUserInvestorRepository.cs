using investiment.manager.api.Models.User;

namespace investiment.manager.api.Interfaces.User
{
    public interface IUserInvestorRepository
    {
        public Task CreateUser(UserInvestor user);
        public Task<List<UserInvestor>> GetUser(Dictionary<string, string> filter);
        public Task BuyAction(UserInvestor user, Dictionary<string, string> filter);
        public Task SellAction(UserInvestor user, Dictionary<string, string> filter);
    }
}
