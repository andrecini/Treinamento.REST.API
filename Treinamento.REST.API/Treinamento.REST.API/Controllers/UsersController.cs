using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Treinamento.REST.Domain.Entities;
using Treinamento.REST.Domain.Enums;
using Treinamento.REST.Domain.Interfaces.Services;

namespace Treinamento.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _service;


        public UsersController(ILogger<UsersController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("get")]
        [Authorize]
        public IActionResult GetUsers()
        {
            var users = _service.GetUsers();

            if (users == null)
            {
                return BadRequest("Internal Server Error.");
            }
            else if (users.Count() == 0)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Users Found.");
            }

            return StatusCode(StatusCodes.Status200OK, users);
        }

        [HttpGet("get/id")]
        [Authorize]
        public IActionResult GetUserById([Required] int id)
        {
            var user = _service.GetUserById(id);

            if (user == null)
            {
                return NotFound("No User Found with this ID.");
            }

            return Ok(user);
        }

        [HttpPost("add")]
        [Authorize]
        public IActionResult AddUser([FromBody] User user)
        {
            var result = _service.AddUser(user);

            if (!result)
            {
                return BadRequest("Unable to Add user. Check the data entered and try again..");
            }

            return StatusCode(StatusCodes.Status201Created, "User added successfully.");
        }

        [HttpPut("update")]
        [Authorize]
        public IActionResult UpdateUser([FromBody] User user)
        {
            var result = _service.UpdateUser(user);

            if (!result)
            {
                return BadRequest("Unable to Update user. Check the data entered and try again.");
            }

            return Ok("User updated successfully.");
        }

        [HttpPut("update/roles")]
        [Authorize]
        public IActionResult UpdateUserRoles([Required] int id, [Required] Roles role)
        {
            var result = _service.UpdateUserRole(id, role);

            if (!result)
            {
                return BadRequest("Unable to Update user's role. Check the data entered and try again.");
            }

            return Ok("User's role updated successfully.");
        }

        [HttpDelete("delete")]
        [Authorize]
        public IActionResult DeleteUser([Required] int id)
        {
            var result = _service.DeleteUserById(id);

            if (!result)
            {
                return BadRequest("Unable to Delete user. Check the entered ID and try again..");
            }

            return Ok("User's role updated successfully.");
        }

    }
}