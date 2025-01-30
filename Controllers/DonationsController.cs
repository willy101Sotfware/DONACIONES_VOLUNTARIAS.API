using Microsoft.AspNetCore.Mvc;
using MediatR;
using DONACIONES_VOLUNTARIAS.API.Services.Commands.DonationCommands;
using DONACIONES_VOLUNTARIAS.API.Services.Queries.DonationQuerys;
using Microsoft.AspNetCore.Authorization;

namespace DONACIONES_VOLUNTARIAS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DonationsController> _logger;

        public DonationsController(IMediator mediator, ILogger<DonationsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllDonations()
        {
            _logger.LogInformation("GetAllDonations called");

            var query = new GetAllDonationsQuery();
            var result = await _mediator.Send(query);

            _logger.LogInformation("Retrieved {Count} donations", result.Count);
            return Ok(result);
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetDonationById(int id)
        {
            _logger.LogInformation("GetDonationById called with id: {Id}", id);

            var query = new GetDonationByIdQuery { DonationId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogWarning("Donación no encontrada para  id: {Id}", id);
                return NotFound("Donación no encontrada");
            }

            _logger.LogInformation("Donación encontrada: {DonationId}", result.DonationId);
            return Ok(result);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateDonation([FromBody] CreateDonationCommand command)
        {
            _logger.LogInformation("CreateDonation called");

            var result = await _mediator.Send(command);

            _logger.LogInformation("Donation created with ID: {DonationId}", result.DonationId);
            return CreatedAtAction(nameof(GetDonationById), new { id = result.DonationId }, result);
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateDonation(int id, [FromBody] UpdateDonationCommand command)
        {
            _logger.LogInformation("UpdateDonation called with id: {Id}", id);

            if (id != command.DonationId)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);

            _logger.LogInformation("Donation updated with ID: {DonationId}", result.DonationId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteDonation(int id)
        {
            _logger.LogInformation("DeleteDonation called with id: {Id}", id);

            var command = new DeleteDonationCommand { DonationId = id };
            await _mediator.Send(command);

            _logger.LogInformation("Donation deleted with ID: {DonationId}", id);
            return NoContent();
        }
    }
}
