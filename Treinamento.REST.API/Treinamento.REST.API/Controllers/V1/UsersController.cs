using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Treinamento.REST.API.Responses;
using Treinamento.REST.Domain.Entities;
using Treinamento.REST.Domain.Enums;
using Treinamento.REST.Domain.Interfaces.Services;

namespace Treinamento.REST.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _service;

        public UsersController(ILogger<UsersController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of users.
        /// </summary>
        /// <param name="page">Page number, greater than or equal to 1.</param>
        /// <param name="pageSize">Page size, greater than or equal to 5.</param>
        /// <returns>Returns a list of users.</returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetUsers([Required] int page, [Required] int pageSize)
        {
            if (page <= 0) return BadRequest("The page value must be greater than 0.");
            if (pageSize < 5) return BadRequest("The page size value must be grater or equal than 0.");

            var users = _service.GetUsers(page, pageSize);

            if (users == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while communicating with the database.");
            }

            return StatusCode(StatusCodes.Status200OK, new GetResponse<User>()
            {
                Page = page,
                PageSize = pageSize,
                TotalAmount = _service.GetTotalAmountOfUsers(),
                Success = true,
                Message = $"{users.Count()} users found",
                Users = users
            });
        }

        /// <summary>
        /// Retrives a user by id
        /// </summary>
        /// <param name="id">Id of User</param>
        /// <returns>Returns a users by id.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetUserById([Required] int id)
        {
            var user = _service.GetUserById(id);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status200OK, new GetByIdResponse<User>()
                {
                    Success = false,
                    Message = $"No user with id '{id}' was found",
                    User = user
                });
            }

            return StatusCode(StatusCodes.Status200OK, new GetByIdResponse<User>()
            {
                Success = true,
                Message = $"A user with id '{id}' was found",
                User = user
            });
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">Basic information of the user to be added.</param>
        /// <returns>Returns the result of the user addition.</returns>
        [HttpPost]
        public IActionResult AddUser([FromBody] UserInput user)
        {
            var newUser = _service.AddUser(user);

            if (newUser == null)
            {
                return BadRequest("Unable to Add user. Check the data entered and try again.");
            }

            return StatusCode(StatusCodes.Status201Created, new PostResponse<User>()
            {
                Success = true,
                Message = "User successfully created.",
                URI = @$"{Request.Scheme}://{Request.Host.Value}/v1/users/{newUser.Id}",
                CreatedUser = newUser
            });
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">ID of the user to be updated.</param>
        /// <param name="user">Updated user information.</param>
        /// <returns>Returns the result of the user update.</returns>
        [HttpPut]
        [Authorize]
        public IActionResult UpdateUser([Required] int id, [FromBody] UserInput user)
        {
            var userUpdated = _service.UpdateUser(id, user);

            if (userUpdated == null)
            {
                return BadRequest("Unable to Update user. Check the data entered and try again.");
            }

            return StatusCode(StatusCodes.Status200OK, new PutResponse<User>()
            {
                Success = true,
                Message = "User updated successfully.",
                URI = @$"{Request.Scheme}://{Request.Host.Value}/v1/users/{id}",
                UpdatedUser = userUpdated
            });
        }

        /// <summary>
        /// Updates the role of an existing user.
        /// </summary>
        /// <param name="id">ID of the user whose role will be updated.</param>
        /// <param name="role">New role for the user.</param>
        /// <returns>Returns the result of the user's role update.</returns>
        [HttpPut("{id}/roles")]
        [Authorize]
        public IActionResult UpdateUserRoles([Required] int id, [Required] Roles role)
        {
            var userUpdated = _service.UpdateUserRole(id, role);

            if (userUpdated == null)
            {
                return BadRequest("Unable to Update user's role. Check the data entered and try again.");
            }

            return StatusCode(StatusCodes.Status200OK, new PutResponse<User>()
            {
                Success = true,
                Message = "User's role updated successfully.",
                URI = @$"{Request.Scheme}://{Request.Host.Value}/v1/users/{id}",
                UpdatedUser = userUpdated
            });
        }

        /// <summary>
        /// Updates the status of an existing user.
        /// </summary>
        /// <param name="id">ID of the user whose status will be updated.</param>
        /// <param name="status">New status for the user.</param>
        /// <returns>Returns the result of the user's status update.</returns>

        [HttpPut("{id}/status")]
        [Authorize]
        public IActionResult UpdateUserStatus([Required] int id, [Required] Status status)
        {
            var userUpdated = _service.UpdateUserStatus(id, status);

            if (userUpdated == null)
            {
                return BadRequest("Unable to Update user's status. Check the data entered and try again.");
            }

            return StatusCode(StatusCodes.Status200OK, new PutResponse<User>()
            {
                Success = true,
                Message = "User's status updated successfully.",
                URI = @$"{Request.Scheme}://{Request.Host.Value}/v1/users/{id}",
                UpdatedUser = userUpdated
            });
        }

        /// <summary>
        /// Deletes an existing user by their ID.
        /// </summary>
        /// <param name="id">ID of the user to be deleted.</param>
        /// <returns>Returns the result of the user deletion.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteUser([Required] int id)
        {
            var result = _service.DeleteUserById(id);

            if (!result)
            {
                return StatusCode(StatusCodes.Status404NotFound, new GetResponse<User>()
                {
                    Success = false,
                    Message = $"No User with id '{id}' was found"
                });
            }

            return StatusCode(StatusCodes.Status200OK, new DeleteResponse()
            {
                Success = true,
                Message = $"User with id '{id}' was deleted"
            });
        }

    }
}