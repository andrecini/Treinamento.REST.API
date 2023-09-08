using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Treinamento.REST.Domain.Enums;
using Treinamento.REST.Domain.Interfaces.Services;

namespace Treinamento.REST.API.Controllers
{
    [ApiController]
    [Route("api/authentications")]
    public class AuthenticateController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        private readonly IUserService _service;

        public AuthenticateController(ILogger<UsersController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("login")]
        public IActionResult Login([Required] string username, [Required] string password)
        {
            var auth = _service.VerifyUser(username, password);

            if (auth == null)
            {
                return Unauthorized("Unauthorized user. Check the username and password.");
            }

            return StatusCode(StatusCodes.Status200OK, auth);
        }
    }
}