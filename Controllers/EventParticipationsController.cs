using Microsoft.AspNetCore.Mvc;
using MediatR;
using DONACIONES_VOLUNTARIAS.API.Services.Commands.EventParticipationCommands;
using DONACIONES_VOLUNTARIAS.API.Services.Queries.EventParticipationQuerys;

namespace DONACIONES_VOLUNTARIAS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventParticipationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<EventParticipationsController> _logger;

    public EventParticipationsController(IMediator mediator, ILogger<EventParticipationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventParticipationById(int id)
    {
        _logger.LogInformation("GetEventParticipationById called with id: {Id}", id);

        var query = new GetEventParticipationByIdQuery { ParticipationId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            _logger.LogWarning("EventParticipation not found for id: {Id}", id);
            return NotFound("EventParticipation not found");
        }

        _logger.LogInformation("EventParticipation found: {ParticipationId}", result.ParticipationId);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEventParticipations()
    {
        _logger.LogInformation("GetAllEventParticipations called");

        var query = new GetAllEventParticipationsQuery();
        var result = await _mediator.Send(query);

        _logger.LogInformation("Retrieved {Count} event participations", result.Count);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEventParticipation([FromBody] CreateEventParticipationCommand command)
    {
        _logger.LogInformation("CreateEventParticipation called");

        var result = await _mediator.Send(command);

        _logger.LogInformation("EventParticipation created with ID: {ParticipationId}", result.ParticipationId);
        return CreatedAtAction(nameof(GetEventParticipationById), new { id = result.ParticipationId }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEventParticipation(int id, [FromBody] UpdateEventParticipationCommand command)
    {
        _logger.LogInformation("UpdateEventParticipation called with id: {Id}", id);

        if (id != command.ParticipationId)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);

        _logger.LogInformation("EventParticipation updated with ID: {ParticipationId}", result.ParticipationId);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEventParticipation(int id)
    {
        _logger.LogInformation("DeleteEventParticipation called with id: {Id}", id);

        var command = new DeleteEventParticipationCommand { ParticipationId = id };
        await _mediator.Send(command);

        _logger.LogInformation("EventParticipation deleted with ID: {ParticipationId}", id);
        return NoContent();
    }
}
