using MediatR;
using DONACIONES_VOLUNTARIAS.API.Interface;


namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.DonationCommands;

public class DeleteDonationCommand : IRequest
{
    public int DonationId { get; set; }

    public class DeleteDonationCommandHandler : IRequestHandler<DeleteDonationCommand>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly ILogger<DeleteDonationCommandHandler> _logger;

        public DeleteDonationCommandHandler(IGestionVoluntariadoDonacionesContext context, ILogger<DeleteDonationCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteDonationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling DeleteDonationCommand for DonationId: {DonationId}", request.DonationId);

            var donation = await _context.Donations.FindAsync(request.DonationId, cancellationToken);

            if (donation == null)
            {
                _logger.LogWarning("Donation not found for DonationId: {DonationId}", request.DonationId);
                throw new Exception("Donation not found");
            }

            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Donation deleted with ID: {DonationId}", request.DonationId);

            return Unit.Value;
        }
    }
}
