using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Entities;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;


namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.DonationCommands
{
    public class CreateDonationCommand : IRequest<DonationDTO>
    {
        public int DonorId { get; set; }
        public int OrganizationId { get; set; }
        public decimal Amount { get; set; }
        public string? Notes { get; set; }

        public class CreateDonationCommandHandler : IRequestHandler<CreateDonationCommand, DonationDTO>
        {
            private readonly IGestionVoluntariadoDonacionesContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<CreateDonationCommandHandler> _logger;

            public CreateDonationCommandHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<CreateDonationCommandHandler> logger)
            {
                _context = context;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<DonationDTO> Handle(CreateDonationCommand request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Handling CreateDonationCommand");

                var donation = new Donation
                {
                    DonorId = request.DonorId,
                    OrganizationId = request.OrganizationId,
                    Amount = request.Amount,
                    Notes = request.Notes,
                    DonationDate = DateTime.UtcNow // Asignar la fecha actual
                };

                _context.Donations.Add(donation);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Donation created with ID: {DonationId}", donation.DonationId);

                return _mapper.Map<DonationDTO>(donation);
            }
        }
    }
}
