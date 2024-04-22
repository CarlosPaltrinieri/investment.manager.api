using investiment.manager.api.Interfaces.User;
using investiment.manager.api.Models.User;

namespace investiment.manager.api.Utils
{
    public class UserInvestorExtension(IUserInvestorRepository repository)
    {
        private readonly IUserInvestorRepository _repository = repository;
        
    }
}