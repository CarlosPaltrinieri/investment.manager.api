using investiment.manager.api.Models.Investment;
using investiment.manager.api.Services.Investment;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace investiment.manager.api.Controllers
{
    [ApiController]
    [Route("investiment")]
    public class InvestimentController(ILogger<InvestimentController> logger, InvestmentService service) : ControllerBase
    {
        private readonly ILogger<InvestimentController> _logger = logger;
        private readonly InvestmentService _service = service;

        /// <summary>
        /// Consulta investimento pelo Id e/ou pelo Tipo de investimento.
        /// </summary>
        /// <param name="idInvestment"></param>
        /// <param name="typeInvestment"></param>
        [HttpGet]
        public async Task<IActionResult> GetInvestment([FromHeader] string? idInvestment = null, string? typeInvestment = null)
        {
            try
            {
                var investment = new InvestmentModel();
                var result = await _service.GetInvestmentAsync(idInvestment, typeInvestment);

                if (result != null && result?.StatusCode == (int)HttpStatusCode.OK)
                    return Ok(result);
                else
                    return BadRequest(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Investment Controller - GET - Error when retrieve information {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }

        /// <summary>
        /// Criar investimento para compra.
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///     "Type": "CDB",
        ///     "Value": {
        ///         "Name": "NomeInvestimento",
        ///         "Description": "Texto de descricao",
        ///         "ExpiryDate": "DateTime de tempo de expiracao",
        ///         "Price": 0.5
        ///         }
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> CreateInvestment([FromBody] InvestmentModel investment)
        {
            try
            {
                var result = await _service.CreateInvestiment(investment);

                if (result != null && result?.StatusCode == (int)HttpStatusCode.OK)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Investment Controller - POST - Error when try to insert information {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }

        /// <summary>
        /// Atualiza tipo de investimento.
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///     "Id": "IdInvestimento",
        ///     "Type": "CDB",
        ///     "Value": {
        ///         "Name": "NomeInvestimento",
        ///         "Description": "Texto de descricao",
        ///         "ExpiryDate": "DateTime de tempo de expiracao",
        ///         "Price": 0.5
        ///         }
        ///     }
        ///
        /// </remarks>

        [HttpPut]
        public async Task<IActionResult> UpdateInvestment([FromBody] InvestmentModel investment)
        {
            try
            {
                var result = await _service.UpdateInvestmentAsync(investment);

                if (result != null && result?.StatusCode == (int)HttpStatusCode.OK)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Investment Controller - PUT - Error when try to update information {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }
    }
}
