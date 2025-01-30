
using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;


namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.DonorCommands;

public class UpdateDonorCommand : IRequest<DonorDTO>
{
    public int DonorId { get; set; }
    public int? UserId { get; set; }
    public string? PreferredCauses { get; set; }

    public class UpdateDonorCommandHandler : IRequestHandler<UpdateDonorCommand, DonorDTO>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDonorCommandHandler> _logger;

        public UpdateDonorCommandHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<UpdateDonorCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DonorDTO> Handle(UpdateDonorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateDonorCommand for DonorId: {DonorId}", request.DonorId);

            var donor = await _context.Donors.FindAsync(request.DonorId, cancellationToken);

            if (donor == null)
            {
                _logger.LogWarning("Donor not found for DonorId: {DonorId}", request.DonorId);
                throw new Exception("Donor not found");
            }

            donor.UserId = request.UserId;
            donor.PreferredCauses = request.PreferredCauses;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Donor updated with ID: {DonorId}", donor.DonorId);

            return _mapper.Map<DonorDTO>(donor);
        }
    }
}
