using investiment.manager.api.Interfaces.User;
using investiment.manager.api.Models.User;
using investiment.manager.api.Utils;
using Microsoft.Extensions.Options;
using System.Net;

namespace investiment.manager.api.Services.User
{
    public class UserInvestorService(IUserInvestorRepository repository, IOptions<DbContext> dbContext)
    {
        private readonly IUserInvestorRepository _userInvestorRepository = repository;
        private readonly ResponseExtension _response = new();
        private readonly IOptions<DbContext> _dbContext = dbContext;

        public async Task<ResponseExtension> CreateUser(UserInvestor user)
        {
            if (user != null)
            {
                if (!await ValidateIfUserExists(user, _dbContext.Value.Collection.User))
                {
                    await _userInvestorRepository.CreateUser(user, _dbContext.Value.Collection.User);

                    return _response.Response(HttpStatusCode.OK, "User created successfully.");
                }
                return _response.Response(HttpStatusCode.NoContent, "User already exists.");
            }

            return _response.Response(HttpStatusCode.BadRequest, "Invalid parameters.");
        }
        public async Task<ResponseExtension> GetUserByEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var dic = new Dictionary<string, string>
                {
                    { "Email", email }
                };

                var response = await _userInvestorRepository.GetUser(dic, _dbContext.Value.Collection.User);

                return _response.Response(HttpStatusCode.OK, response);
            }
            return _response.Response(HttpStatusCode.BadRequest, "Invalid parameters.");
        }

        protected async Task<bool> ValidateIfUserExists(UserInvestor? user, string collectionName)
        {
            var dic = new Dictionary<string, string>
            {
                { "Email", user.Email },
                { "DocumentId", user.DocumentId }
            };

            var responseUser = await _userInvestorRepository.GetUser(dic, collectionName);

            if (responseUser == null || responseUser.Count == 0)
                return false;
            else return true;
        }


    }
}
