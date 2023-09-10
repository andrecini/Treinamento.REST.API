using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Treinamento.REST.API.Responses;
using Treinamento.REST.Domain.Entities;
using Treinamento.REST.Domain.Enums;
using Treinamento.REST.Domain.Interfaces.Services;

namespace Treinamento.REST.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v1/authentications")]
    public class AuthenticateController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        private readonly IUserService _service;

        public AuthenticateController(ILogger<UsersController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Authenticate a user by verifying their username and password.
        /// </summary>
        /// <remarks>
        /// This endpoint allows users to log in by providing their username and password. 
        /// If the provided credentials are valid, it returns an authentication token.
        /// </remarks>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>Returns the authentication result.</returns>
        /// <response code="200">Returns a successful authentication result.</response>
        /// <response code="401">Returns an unauthorized error if the credentials are invalid.</response>
        [HttpGet("login")]
        public IActionResult Login([Required] string username, [Required] string password)
        {
            var auth = _service.VerifyUser(username, password);

            if (auth == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new GetAuthenticationResponse<Authentication>()
                {
                    Success = false,
                    Message = $"Unauthorized user. Check the entered username and password.",
                    Auth = auth
                });
            }

            return StatusCode(StatusCodes.Status200OK, new GetAuthenticationResponse<Authentication>()
            {
                Success = true,
                Message = $"Uauthorized user",
                Auth = auth
            });
        }
    }
}