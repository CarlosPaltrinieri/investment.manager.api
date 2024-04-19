using investiment.manager.api.Interfaces.User;
using investiment.manager.api.Models.User;

namespace investiment.manager.api.Utils
{
    public class UserInvestorExtension(IUserInvestorRepository repository)
    {
        private readonly IUserInvestorRepository _repository = repository;
        protected async Task<bool> ValidateIfUserExists(UserInvestor? user)
        {
            var dic = new Dictionary<string, string>
            {
                { "Email", user.Email },
                { "DocumentId", user.DocumentId }
            };

            var responseUser = await _repository.GetUser(dic);

            if (responseUser == null || responseUser.Count == 0)
                return false;
            else return true;
        }
    }
}