using Microsoft.AspNetCore.Mvc;
using MediatR;
using global::DONACIONES_VOLUNTARIAS.API.Services.Commands.VolunteerCommands;
using global::DONACIONES_VOLUNTARIAS.API.Services.Queries.VolunteerQuerys;

namespace DONACIONES_VOLUNTARIAS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VolunteersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<VolunteersController> _logger;

    public VolunteersController(IMediator mediator, ILogger<VolunteersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVolunteerById(int id)
    {
        _logger.LogInformation("GetVolunteerById called with id: {Id}", id);

        var query = new GetVolunteerByIdQuery { VolunteerId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            _logger.LogWarning("Volunteer not found for id: {Id}", id);
            return NotFound("Volunteer not found");
        }

        _logger.LogInformation("Volunteer found: {VolunteerId}", result.VolunteerId);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllVolunteers()
    {
        _logger.LogInformation("GetAllVolunteers called");

        var query = new GetAllVolunteersQuery();
        var result = await _mediator.Send(query);

        _logger.LogInformation("Retrieved {Count} volunteers", result.Count);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateVolunteer([FromBody] CreateVolunteerCommand command)
    {
        _logger.LogInformation("CreateVolunteer called");

        var result = await _mediator.Send(command);

        _logger.LogInformation("Volunteer created with ID: {VolunteerId}", result.VolunteerId);
        return CreatedAtAction(nameof(GetVolunteerById), new { id = result.VolunteerId }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVolunteer(int id, [FromBody] UpdateVolunteerCommand command)
    {
        _logger.LogInformation("UpdateVolunteer called with id: {Id}", id);

        if (id != command.VolunteerId)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);

        _logger.LogInformation("Volunteer updated with ID: {VolunteerId}", result.VolunteerId);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVolunteer(int id)
    {
        _logger.LogInformation("DeleteVolunteer called with id: {Id}", id);

        var command = new DeleteVolunteerCommand { VolunteerId = id };
        await _mediator.Send(command);

        _logger.LogInformation("Volunteer deleted with ID: {VolunteerId}", id);
        return NoContent();
    }
}
