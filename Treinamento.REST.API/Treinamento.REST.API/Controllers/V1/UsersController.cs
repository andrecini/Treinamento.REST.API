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
        /// Retrieve a user by their unique ID.
        /// </summary>
        /// <remarks>
        /// This endpoint allows you to retrieve a user's information by providing their unique ID.
        /// </remarks>
        /// <param name="id">The unique ID of the user to retrieve.</param>
        /// <returns>Returns the user information based on the provided ID.</returns>
        /// <response code="200">Returns the user information if found.</response>
        /// <response code="400">Returns an error message if no user with the specified ID is found.</response>
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetUserById([Required] int id)
        {
            var user = _service.GetUserById(id);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new GetByIdResponse<User>()
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
        /// Create a new user.
        /// </summary>
        /// <remarks>
        /// This endpoint allows you to create a new user by providing their basic information.
        /// </remarks>
        /// <param name="user">The basic information of the user to be created.</param>
        /// <returns>Returns the result of user creation.</returns>
        /// <response code="201">Returns a successful user creation result.</response>
        /// <response code="400">Returns an error message if the data provided is invalid.</response>
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
        /// Update an existing user.
        /// </summary>
        /// <remarks>
        /// This endpoint allows you to update an existing user's information by providing their ID and new data.
        /// </remarks>
        /// <param name="id">The ID of the user to be updated.</param>
        /// <param name="user">The updated user information.</param>
        /// <returns>Returns the result of user update.</returns>
        /// <response code="200">Returns a successful user update result.</response>
        /// <response code="400">Returns an error message if the data provided is invalid.</response>
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
        /// Update the role of an existing user.
        /// </summary>
        /// <remarks>
        /// This endpoint allows you to update the role of an existing user by providing their ID and a new role.
        /// </remarks>
        /// <param name="id">The ID of the user whose role will be updated.</param>
        /// <param name="role">The new role for the user.</param>
        /// <returns>Returns the result of the user's role update.</returns>
        /// <response code="200">Returns a successful user role update result.</response>
        /// <response code="400">Returns an error message if the data provided is invalid.</response>
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
        /// Update the status of an existing user.
        /// </summary>
        /// <remarks>
        /// This endpoint allows you to update the status of an existing user by providing their ID and a new status.
        /// </remarks>
        /// <param name="id">The ID of the user whose status will be updated.</param>
        /// <param name="status">The new status for the user.</param>
        /// <returns>Returns the result of the user's status update.</returns>
        /// <response code="200">Returns a successful user status update result.</response>
        /// <response code="400">Returns an error message if the data provided is invalid.</response>
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
        /// Delete an existing user by their ID.
        /// </summary>
        /// <remarks>
        /// This endpoint allows you to delete an existing user by providing their ID. If the user is found and successfully deleted, it returns a success message.
        /// </remarks>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <returns>Returns the result of the user deletion.</returns>
        /// <response code="200">Returns a success message if the user is deleted.</response>
        /// <response code="404">Returns an error message if no user with the specified ID is found.</response>
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