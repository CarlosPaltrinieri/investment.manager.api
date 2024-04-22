using investiment.manager.api.Models.User;
using investment.manager.api.Data.Services.Wallet;
using investment.manager.api.Domain.Models.Wallet;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace investiment.manager.api.Controllers
{
    [ApiController]
    [Route("wallet")]
    public class WalletController(ILogger<InvestimentController> logger, WalletService service) : ControllerBase
    {
        private readonly ILogger<InvestimentController> _logger = logger;
        private readonly WalletService _walletService = service;

        /// <summary>
        /// Comprar investimento.
        /// É necessário que seja feita a criação do investimento (/POST investment) previamente para a compra.
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "IdUser": "662588920d116446e440821d",
        ///         "IdInvestment": "66258f7baa3e1e55ae09c0c8",
        ///         "Email": "teste@teste.com"
        ///     }
        ///
        /// </remarks>
        [Route("buy-investment")]
        [HttpPost]
        public async Task<IActionResult> BuyInvestment([FromBody] WalletUser walletUser)
        {
            try
            {
                var result = await _walletService.BuyInvestment(walletUser);

                if (result != null && result?.StatusCode == (int)HttpStatusCode.OK)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Wallet Controller - POST - Error when try to buy investment {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }

        /// <summary>
        /// Vender investimento.
        /// É necessário que seja feita a compra do investimento previamente para a venda.
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "IdUser": "662588920d116446e440821d",
        ///         "IdInvestment": "66258f7baa3e1e55ae09c0c8",
        ///         "Email": "teste@teste.com"
        ///     }
        ///
        /// </remarks>
        [Route("sell-investment")]
        [HttpPost]
        public async Task<IActionResult> SellInvestment([FromBody] WalletUser walletUser)
        {
            try
            {
                var result = await _walletService.SellInvestment(walletUser);

                if (result != null && result?.StatusCode == (int)HttpStatusCode.OK)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Wallet Controller - POST - Error when try to sell invesment {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }

        /// <summary>
        /// Consulta carteira do usuário, visualizando seus investimentos.
        /// </summary>
        /// <param name="idUser"></param>
        [Route("user-wallet")]
        [HttpGet]
        public async Task<IActionResult> GetUserWallet([FromHeader] string idUser)
        {
            try
            {
                var result = await _walletService.GetUserWaller(idUser);

                if (result != null && result?.StatusCode == (int)HttpStatusCode.OK)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Wallet Controller - POST - Error when try to get user wallet/investments {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }
    }
}
