using Microsoft.AspNetCore.Mvc;
using MediatR;
using DONACIONES_VOLUNTARIAS.API.Services.Queries;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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

        [HttpGet("{id}")]
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
    }
}
