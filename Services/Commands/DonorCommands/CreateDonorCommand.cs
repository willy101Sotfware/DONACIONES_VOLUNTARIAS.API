
using MediatR;
using AutoMapper;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using DONACIONES_VOLUNTARIAS.API.Entities;


namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.DonorCommands;

    public class CreateDonorCommand : IRequest<DonorDTO>
    {
        public int? UserId { get; set; }
        public string? PreferredCauses { get; set; }

        public class CreateDonorCommandHandler : IRequestHandler<CreateDonorCommand, DonorDTO>
        {
            private readonly IGestionVoluntariadoDonacionesContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<CreateDonorCommandHandler> _logger;

            public CreateDonorCommandHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<CreateDonorCommandHandler> logger)
            {
                _context = context;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<DonorDTO> Handle(CreateDonorCommand request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Handling CreateDonorCommand");

                var donor = new Donor
                {
                    UserId = request.UserId,
                    PreferredCauses = request.PreferredCauses,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Donors.Add(donor);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Donor created with ID: {DonorId}", donor.DonorId);

                return _mapper.Map<DonorDTO>(donor);
            }
        }
    }


