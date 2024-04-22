using investiment.manager.api.Models.User;
using investiment.manager.api.Services.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace investiment.manager.api.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController(ILogger<InvestimentController> logger, UserInvestorService service) : ControllerBase
    {
        private readonly ILogger<InvestimentController> _logger = logger;
        private readonly UserInvestorService _userInvestorService = service;

        /// <summary>
        /// Criação de usuário.
        /// É necessário para compra de um investimento.
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "Email": "teste@teste.com",
        ///         "Name": "NomeInvestimento",
        ///         "DocumentId": "Texto de descricao",
        ///         "Birthday": "DateTime de tempo de expiracao"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserInvestor userModel)
        {
            try
            {
                var result = await _userInvestorService.CreateUser(userModel);

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
        /// Consulta de usuário por e-mail.
        /// </summary>
        /// <param name="email"></param>
        [HttpGet]
        public async Task<IActionResult> GetUser([FromHeader] string email)
        {
            try
            {
                var result = await _userInvestorService.GetUserByEmail(email);

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

    }
}
