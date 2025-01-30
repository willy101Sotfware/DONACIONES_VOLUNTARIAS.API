using Microsoft.AspNetCore.Mvc;
using MediatR;
using DONACIONES_VOLUNTARIAS.API.Services.Queries.DonorQuerys;
using DONACIONES_VOLUNTARIAS.API.Services.Commands.DonorCommands;


namespace DONACIONES_VOLUNTARIAS.API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class DonorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DonorsController> _logger;

        public DonorsController(IMediator mediator, ILogger<DonorsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDonorById(int id)
        {
            _logger.LogInformation("GetDonorById called with id: {Id}", id);

            var query = new GetDonorByIdQuery { DonorId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogWarning("Donor not found for id: {Id}", id);
                return NotFound("Donor not found");
            }

            _logger.LogInformation("Donor found: {DonorId}", result.DonorId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDonors()
        {
            _logger.LogInformation("GetAllDonors called");

            var query = new GetAllDonorsQuery();
            var result = await _mediator.Send(query);

            _logger.LogInformation("Retrieved {Count} donors", result.Count);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDonor([FromBody] CreateDonorCommand command)
        {
            _logger.LogInformation("CreateDonor called");

            var result = await _mediator.Send(command);

            _logger.LogInformation("Donor created with ID: {DonorId}", result.DonorId);
            return CreatedAtAction(nameof(GetDonorById), new { id = result.DonorId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDonor(int id, [FromBody] UpdateDonorCommand command)
        {
            _logger.LogInformation("UpdateDonor called with id: {Id}", id);

            if (id != command.DonorId)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);

            _logger.LogInformation("Donor updated with ID: {DonorId}", result.DonorId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonor(int id)
        {
            _logger.LogInformation("DeleteDonor called with id: {Id}", id);

            var command = new DeleteDonorCommand { DonorId = id };
            await _mediator.Send(command);

            _logger.LogInformation("Donor deleted with ID: {DonorId}", id);
            return NoContent();
        }
    }

