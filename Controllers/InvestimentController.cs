using investiment.manager.api.Interfaces;
using investiment.manager.api.Models.Investment;
using investiment.manager.api.Services.Investment;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace investiment.manager.api.Controllers
{
    [ApiController]
    [Route("investiment")]
    public class InvestimentController(ILogger<InvestimentController> logger, InvestmentService service) : ControllerBase
    {
        private readonly ILogger<InvestimentController> _logger = logger;
        private readonly InvestmentService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetInvestment([FromHeader] string? idInvestment = null, string? typeInvestment = null)
        {
            try
            {
                var investment = new InvestmentModel();
                var result = await _service.GetInvestmentAsync(idInvestment, typeInvestment);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Investment Controller - GET - Error when retrieve information {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateInvestment([FromBody] InvestmentModel investment)
        {
            try
            {
                
                await _service.CreateInvestiment(investment);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Investment Controller - POST - Error when try to insert information {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInvestment([FromBody] InvestmentModel investment)
        {
            try
            {

                await _service.UpdateInvestmentAsync(investment);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Investment Controller - PUT - Error when try to update information {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }
    }
}
