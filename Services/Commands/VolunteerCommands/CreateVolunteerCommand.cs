using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Entities;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;

namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.VolunteerCommands;

public class CreateVolunteerCommand : IRequest<VolunteerDTO>
{
    public int? UserId { get; set; }
    public string? Skills { get; set; }
    public string? Interests { get; set; }
    public string? Availability { get; set; }

    public class CreateVolunteerCommandHandler : IRequestHandler<CreateVolunteerCommand, VolunteerDTO>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateVolunteerCommandHandler> _logger;

        public CreateVolunteerCommandHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<CreateVolunteerCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<VolunteerDTO> Handle(CreateVolunteerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateVolunteerCommand");

            var volunteer = new Volunteer
            {
                UserId = request.UserId,
                Skills = request.Skills,
                Interests = request.Interests,
                Availability = request.Availability,
                CreatedAt = DateTime.UtcNow
            };

            _context.Volunteers.Add(volunteer);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Volunteer created with ID: {VolunteerId}", volunteer.VolunteerId);

            return _mapper.Map<VolunteerDTO>(volunteer);
        }
    }
}
