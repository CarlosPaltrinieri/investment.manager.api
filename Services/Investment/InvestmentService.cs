using investiment.manager.api.Interfaces.Investment;
using investiment.manager.api.Models.Investment;

namespace investiment.manager.api.Services.Investment
{
    public class InvestmentService(IInvestmentRepository repository)
    {
        private readonly IInvestmentRepository _repository = repository;

        public async Task CreateInvestiment(InvestmentModel model)
        {
            await _repository.CreateInvestiment(model);
        }

        public async Task<List<InvestmentModel>> GetInvestmentAsync(string? idInvestment, string? typeInvestment)
        {
            if (idInvestment == null && typeInvestment == null)
                return await _repository.GetInvestmentAsync();
            else
            {
                var filters = new Dictionary<string, string>
                {
                    { "Id", idInvestment },
                    { "Type", typeInvestment }
                };

                return await _repository.GetInvestmentAsync(filters);
            }
        }

        public async Task UpdateInvestmentAsync(InvestmentModel model)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(model));

            var filters = new Dictionary<string, string>
                {
                    { "Id", model.Id }
                };

            await _repository.UpdateInvestmentAsync(model, filters);
        }
    }
}
