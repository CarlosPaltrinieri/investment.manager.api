using investiment.manager.api.Interfaces.Investment;
using investiment.manager.api.Models.Investment;
using investiment.manager.api.Utils;
using Microsoft.Extensions.Options;
using System.Net;

namespace investiment.manager.api.Services.Investment
{
    public class InvestmentService(IInvestmentRepository repository, IOptions<DbContext> dbContext)
    {
        private readonly IInvestmentRepository _repository = repository;
        private readonly ResponseExtension _response = new();
        private readonly IOptions<DbContext> _dbContext = dbContext;


        public async Task<ResponseExtension> CreateInvestiment(InvestmentModel model)
        {
            await _repository.CreateInvestiment(model, _dbContext.Value.Collection.Investment);

            return _response.Response(HttpStatusCode.OK, "Investment successfully created");
        }

        public async Task<ResponseExtension> GetInvestmentAsync(string? idInvestment, string? typeInvestment)
        {
            if (idInvestment == null && typeInvestment == null)
            {
                var listInvestment = await _repository.GetInvestmentAsync(_dbContext.Value.Collection.Investment);

                return _response.Response(HttpStatusCode.OK, listInvestment);
            }
            else
            {
                var filters = new Dictionary<string, string>
                {
                    { "Id", idInvestment },
                    { "Type", typeInvestment }
                };

                var listInvestment = await _repository.GetInvestmentAsync(_dbContext.Value.Collection.Investment, filters);

                return _response.Response(HttpStatusCode.OK, listInvestment);
            }
        }

        public async Task<ResponseExtension> UpdateInvestmentAsync(InvestmentModel model)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(model));

            var filters = new Dictionary<string, string>
                {
                    { "Id", model.Id }
                };

            model.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateInvestmentAsync(model, filters, _dbContext.Value.Collection.Investment);

            return _response.Response(HttpStatusCode.OK, "Investment successfully updated");
        }
    }
}
