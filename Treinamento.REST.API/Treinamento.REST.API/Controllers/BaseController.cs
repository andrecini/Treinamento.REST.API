using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Treinamento.REST.API.Controllers.V1;
using Treinamento.REST.Domain.Interfaces.Services;

namespace Treinamento.REST.API.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _service;
        private readonly IMemoryCache _cache;

        public BaseController(ILogger<UsersController> logger, IUserService service, IMemoryCache cache)
        {
            _logger = logger;
            _service = service;
            _cache = cache;
        }
    }
}
