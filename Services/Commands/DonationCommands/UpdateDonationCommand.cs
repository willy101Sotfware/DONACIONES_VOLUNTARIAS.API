using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;


namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.DonationCommands;

public class UpdateDonationCommand : IRequest<DonationDTO>
{
    public int DonationId { get; set; }
    public int? DonorId { get; set; }
    public int? OrganizationId { get; set; }
    public decimal Amount { get; set; }
    public string? Notes { get; set; }

    public class UpdateDonationCommandHandler : IRequestHandler<UpdateDonationCommand, DonationDTO>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDonationCommandHandler> _logger;

        public UpdateDonationCommandHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<UpdateDonationCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DonationDTO> Handle(UpdateDonationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateDonationCommand for DonationId: {DonationId}", request.DonationId);

            var donation = await _context.Donations.FindAsync(request.DonationId, cancellationToken);

            if (donation == null)
            {
                _logger.LogWarning("Donation not found for DonationId: {DonationId}", request.DonationId);
                throw new Exception("Donation not found");
            }

            donation.DonorId = request.DonorId;
            donation.OrganizationId = request.OrganizationId;
            donation.Amount = request.Amount;
            donation.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Donation updated with ID: {DonationId}", donation.DonationId);

            return _mapper.Map<DonationDTO>(donation);
        }
    }
}
