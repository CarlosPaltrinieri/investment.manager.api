using investiment.manager.api.Interfaces.User;
using investiment.manager.api.Models.Investment;
using investiment.manager.api.Models.User;

namespace investiment.manager.api.Services.User
{
    public class UserInvestorService(IUserInvestorRepository repository) : UserInvestorExtension(repository)
    {
        private readonly IUserInvestorRepository _repository = repository;

        public async Task CreateUser(UserInvestor user)
        {
            if (user == null)
            {
                if (!await ValidateIfUserExists(user))
                {
                    await _repository.CreateUser(user);
                }
            }
        }
        public async Task<UserInvestor> GetUserByEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var dic = new Dictionary<string, string>
                {
                    { "Email", email }
                };

                var response = await _repository.GetUser(dic);

                return response.FirstOrDefault();
            }
            return null;
        }

        public async Task BuyInvestment(InvestmentModel investment, UserInvestor user)
        {

        }
    }
}
