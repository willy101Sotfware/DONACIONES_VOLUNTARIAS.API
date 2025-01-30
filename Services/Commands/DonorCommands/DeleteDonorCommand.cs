
using MediatR;
using DONACIONES_VOLUNTARIAS.API.Interface;


namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.DonorCommands
{
    public class DeleteDonorCommand : IRequest
    {
        public int DonorId { get; set; }

        public class DeleteDonorCommandHandler : IRequestHandler<DeleteDonorCommand>
        {
            private readonly IGestionVoluntariadoDonacionesContext _context;
            private readonly ILogger<DeleteDonorCommandHandler> _logger;

            public DeleteDonorCommandHandler(IGestionVoluntariadoDonacionesContext context, ILogger<DeleteDonorCommandHandler> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<Unit> Handle(DeleteDonorCommand request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Handling DeleteDonorCommand for DonorId: {DonorId}", request.DonorId);

                var donor = await _context.Donors.FindAsync(request.DonorId, cancellationToken);

                if (donor == null)
                {
                    _logger.LogWarning("Donor not found for DonorId: {DonorId}", request.DonorId);
                    throw new Exception("Donor not found");
                }

                _context.Donors.Remove(donor);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Donor deleted with ID: {DonorId}", request.DonorId);

                return Unit.Value;
            }
        }
    }
}
