using Microsoft.AspNetCore.Mvc;
using MediatR;
using DONACIONES_VOLUNTARIAS.API.Services.Commands.ImpactCommads;
using DONACIONES_VOLUNTARIAS.API.Services.Queries.ImpactQuerys;

namespace DONACIONES_VOLUNTARIAS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImpactsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ImpactsController> _logger;

        public ImpactsController(IMediator mediator, ILogger<ImpactsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImpactById(int id)
        {
            _logger.LogInformation("GetImpactById called with id: {Id}", id);

            var query = new GetImpactByIdQuery { ImpactId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogWarning("Impact not found for id: {Id}", id);
                return NotFound("Impact not found");
            }

            _logger.LogInformation("Impact found: {ImpactId}", result.ImpactId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImpacts()
        {
            _logger.LogInformation("GetAllImpacts called");

            var query = new GetAllImpactsQuery();
            var result = await _mediator.Send(query);

            _logger.LogInformation("Retrieved {Count} impacts", result.Count);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateImpact([FromBody] CreateImpactCommand command)
        {
            _logger.LogInformation("CreateImpact called");

            var result = await _mediator.Send(command);

            _logger.LogInformation("Impact created with ID: {ImpactId}", result.ImpactId);
            return CreatedAtAction(nameof(GetImpactById), new { id = result.ImpactId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImpact(int id, [FromBody] UpdateImpactCommand command)
        {
            _logger.LogInformation("UpdateImpact called with id: {Id}", id);

            if (id != command.ImpactId)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);

            _logger.LogInformation("Impact updated with ID: {ImpactId}", result.ImpactId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImpact(int id)
        {
            _logger.LogInformation("DeleteImpact called with id: {Id}", id);

            var command = new DeleteImpactCommand { ImpactId = id };
            await _mediator.Send(command);

            _logger.LogInformation("Impact deleted with ID: {ImpactId}", id);
            return NoContent();
        }
    }
}
