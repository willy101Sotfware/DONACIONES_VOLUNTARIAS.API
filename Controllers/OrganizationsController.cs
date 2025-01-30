using Microsoft.AspNetCore.Mvc;
using MediatR;
using global::DONACIONES_VOLUNTARIAS.API.Services.Commands.OrganizationCommands;
using global::DONACIONES_VOLUNTARIAS.API.Services.Queries.OrganizationQuery;
using global::DONACIONES_VOLUNTARIAS.API.Services.Queries.OrganizationQuerys;

namespace DONACIONES_VOLUNTARIAS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrganizationsController> _logger;

    public OrganizationsController(IMediator mediator, ILogger<OrganizationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrganizationById(int id)
    {
        _logger.LogInformation("GetOrganizationById called with id: {Id}", id);

        var query = new GetOrganizationByIdQuery { OrganizationId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            _logger.LogWarning("Organization not found for id: {Id}", id);
            return NotFound("Organization not found");
        }

        _logger.LogInformation("Organization found: {OrganizationId}", result.OrganizationId);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrganizations()
    {
        _logger.LogInformation("GetAllOrganizations called");

        var query = new GetAllOrganizationsQuery();
        var result = await _mediator.Send(query);

        _logger.LogInformation("Retrieved {Count} organizations", result.Count);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationCommand command)
    {
        _logger.LogInformation("CreateOrganization called");

        var result = await _mediator.Send(command);

        _logger.LogInformation("Organization created with ID: {OrganizationId}", result.OrganizationId);
        return CreatedAtAction(nameof(GetOrganizationById), new { id = result.OrganizationId }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrganization(int id, [FromBody] UpdateOrganizationCommand command)
    {
        _logger.LogInformation("UpdateOrganization called with id: {Id}", id);

        if (id != command.OrganizationId)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);

        _logger.LogInformation("Organization updated with ID: {OrganizationId}", result.OrganizationId);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganization(int id)
    {
        _logger.LogInformation("DeleteOrganization called with id: {Id}", id);

        var command = new DeleteOrganizationCommand { OrganizationId = id };
        await _mediator.Send(command);

        _logger.LogInformation("Organization deleted with ID: {OrganizationId}", id);
        return NoContent();
    }
}
