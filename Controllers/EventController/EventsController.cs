using Microsoft.AspNetCore.Mvc;
using MediatR;
using global::DONACIONES_VOLUNTARIAS.API.Services.Commands.EventCommands;
using global::DONACIONES_VOLUNTARIAS.API.Services.Queries.EventQuerys;
namespace DONACIONES_VOLUNTARIAS.API.Controllers.EventController;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<EventsController> _logger;

    public EventsController(IMediator mediator, ILogger<EventsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(int id)
    {
        _logger.LogInformation("GetEventById called with id: {Id}", id);

        var query = new GetEventByIdQuery { EventId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            _logger.LogWarning("Event not found for id: {Id}", id);
            return NotFound("Event not found");
        }

        _logger.LogInformation("Event found: {EventId}", result.EventId);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        _logger.LogInformation("GetAllEvents called");

        var query = new GetAllEventsQuery();
        var result = await _mediator.Send(query);

        _logger.LogInformation("Retrieved {Count} events", result.Count);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command)
    {
        _logger.LogInformation("CreateEvent called");

        var result = await _mediator.Send(command);

        _logger.LogInformation("Event created with ID: {EventId}", result.EventId);
        return CreatedAtAction(nameof(GetEventById), new { id = result.EventId }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventCommand command)
    {
        _logger.LogInformation("UpdateEvent called with id: {Id}", id);

        if (id != command.EventId)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);

        _logger.LogInformation("Event updated with ID: {EventId}", result.EventId);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        _logger.LogInformation("DeleteEvent called with id: {Id}", id);

        var command = new DeleteEventCommand { EventId = id };
        await _mediator.Send(command);

        _logger.LogInformation("Event deleted with ID: {EventId}", id);
        return NoContent();
    }
}

