using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;

namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.VolunteerCommands;

public class UpdateVolunteerCommand : IRequest<VolunteerDTO>
{
    public int VolunteerId { get; set; }
    public int? UserId { get; set; }
    public string? Skills { get; set; }
    public string? Interests { get; set; }
    public string? Availability { get; set; }

    public class UpdateVolunteerCommandHandler : IRequestHandler<UpdateVolunteerCommand, VolunteerDTO>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateVolunteerCommandHandler> _logger;

        public UpdateVolunteerCommandHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<UpdateVolunteerCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<VolunteerDTO> Handle(UpdateVolunteerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateVolunteerCommand for VolunteerId: {VolunteerId}", request.VolunteerId);

            var volunteer = await _context.Volunteers.FindAsync(request.VolunteerId, cancellationToken);

            if (volunteer == null)
            {
                _logger.LogWarning("Volunteer not found for VolunteerId: {VolunteerId}", request.VolunteerId);
                throw new Exception("Volunteer not found");
            }

            volunteer.UserId = request.UserId;
            volunteer.Skills = request.Skills;
            volunteer.Interests = request.Interests;
            volunteer.Availability = request.Availability;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Volunteer updated with ID: {VolunteerId}", volunteer.VolunteerId);

            return _mapper.Map<VolunteerDTO>(volunteer);
        }
    }
}

