using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Entities;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;


namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.EventParticipationCommands;

public class CreateEventParticipationCommand : IRequest<EventParticipationDTO>
{
    public int? EventId { get; set; }
    public int? VolunteerId { get; set; }
    public DateTime? ParticipationDate { get; set; }
    public string? Notes { get; set; }

    public class CreateEventParticipationCommandHandler : IRequestHandler<CreateEventParticipationCommand, EventParticipationDTO>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateEventParticipationCommandHandler> _logger;

        public CreateEventParticipationCommandHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<CreateEventParticipationCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<EventParticipationDTO> Handle(CreateEventParticipationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateEventParticipationCommand");

            var participation = new EventParticipation
            {
                EventId = request.EventId,
                VolunteerId = request.VolunteerId,
                ParticipationDate = request.ParticipationDate,
                Notes = request.Notes
            };

            _context.EventParticipations.Add(participation);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("EventParticipation created with ID: {ParticipationId}", participation.ParticipationId);

            return _mapper.Map<EventParticipationDTO>(participation);
        }
    }
}
