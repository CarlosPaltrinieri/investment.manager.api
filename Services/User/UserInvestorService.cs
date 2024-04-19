using investiment.manager.api.Interfaces.Investment;
using investiment.manager.api.Interfaces.User;
using investiment.manager.api.Models.Investment;
using investiment.manager.api.Models.User;
using investiment.manager.api.Utils;
using System.Net;

namespace investiment.manager.api.Services.User
{
    public class UserInvestorService(IUserInvestorRepository repository, IInvestmentRepository investmentRepository) : UserInvestorExtension(repository)
    {
        private readonly IUserInvestorRepository _userInvestorRepository = repository;
        private readonly IInvestmentRepository _investmentRepository = investmentRepository;
        private readonly ResponseExtension _response = new();

        public async Task<ResponseExtension> CreateUser(UserInvestor user)
        {
            if (user == null)
            {
                if (!await ValidateIfUserExists(user))
                {
                    await _userInvestorRepository.CreateUser(user);

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

                var response = await _userInvestorRepository.GetUser(dic);

                return _response.Response(HttpStatusCode.OK, response);
            }
            return _response.Response(HttpStatusCode.BadRequest, "Invalid parameters.");
        }

        public async Task<ResponseExtension> BuyInvestment(InvestmentModel investmentModel, UserInvestor user)
        {
            // Validar se investimento existe
            var filterInvestment = new Dictionary<string, string>
            {
                {"Id", investmentModel.Id}
            };

            var listInvestment = await _investmentRepository.GetInvestmentAsync(filterInvestment);
            var investment = listInvestment?.FirstOrDefault();

            if (investment != null)
            {
                var userHasSameInvestment = user.ListInvestments.Contains(investment.Id);

                if (userHasSameInvestment)
                {
                    return _response.Response(HttpStatusCode.NoContent, "User already has this investment selected.");
                }
                else
                {
                    var filterUser = new Dictionary<string, string>
                    {
                        { "Id", user.Id },
                        { "DocumentId", user.DocumentId },
                        { "Email", user.Email }
                    };

                    user.ListInvestments?.Add(investment.Id);
                    await _userInvestorRepository.BuyAction(user, filterUser);

                    return _response.Response(HttpStatusCode.OK, "Investment bought successfully.");
                }
            }

            return _response.Response(HttpStatusCode.BadGateway, "Investment selected doesn't exists.");
        }

        public async Task<ResponseExtension> SellInvestment(UserInvestor user, string idInvestment)
        {
            var filterUser = new Dictionary<string, string>
            {
                { "Id", user.Id },
                { "DocumentId", user.DocumentId },
                { "Email", user.Email }
            };

            var response = await _userInvestorRepository.GetUser(filterUser);
            var userInvestment = response?.FirstOrDefault();

            var userHasInvestment = userInvestment?.ListInvestments?.Contains(idInvestment);

            if (userHasInvestment != null && userHasInvestment == true)
            {
                userInvestment?.ListInvestments.Remove(idInvestment);

                await _userInvestorRepository.SellAction(user, filterUser);

                return _response.Response(HttpStatusCode.OK, "Investment selled successfully");
            }
            else
                return _response.Response(HttpStatusCode.NoContent, $"User provided haven't the invesment selected");
        }
    }
}
