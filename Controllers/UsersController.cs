using Microsoft.AspNetCore.Mvc;
using MediatR;
using DONACIONES_VOLUNTARIAS.API.Services.Commands.UserCommads;
using DONACIONES_VOLUNTARIAS.API.Services.Queries.UserQuerys;

namespace DONACIONES_VOLUNTARIAS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            _logger.LogInformation("GetUserById called with id: {Id}", id);

            var query = new GetUserByIdQuery { UserId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogWarning("User not found for id: {Id}", id);
                return NotFound("User not found");
            }

            _logger.LogInformation("User found: {UserId}", result.UserId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogInformation("GetAllUsers called");

            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query);

            _logger.LogInformation("Retrieved {Count} users", result.Count);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            _logger.LogInformation("CreateUser called");

            var result = await _mediator.Send(command);

            _logger.LogInformation("User created with ID: {UserId}", result.UserId);
            return CreatedAtAction(nameof(GetUserById), new { id = result.UserId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            _logger.LogInformation("UpdateUser called with id: {Id}", id);

            if (id != command.UserId)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);

            _logger.LogInformation("User updated with ID: {UserId}", result.UserId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("DeleteUser called with id: {Id}", id);

            var command = new DeleteUserCommand { UserId = id };
            await _mediator.Send(command);

            _logger.LogInformation("User deleted with ID: {UserId}", id);
            return NoContent();
        }
    }
}
