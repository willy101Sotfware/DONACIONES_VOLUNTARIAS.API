using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;


namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.EventParticipationCommands;

public class UpdateEventParticipationCommand : IRequest<EventParticipationDTO>
{
    public int ParticipationId { get; set; }
    public int? EventId { get; set; }
    public int? VolunteerId { get; set; }
    public DateTime? ParticipationDate { get; set; }
    public string? Notes { get; set; }

    public class UpdateEventParticipationCommandHandler : IRequestHandler<UpdateEventParticipationCommand, EventParticipationDTO>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEventParticipationCommandHandler> _logger;

        public UpdateEventParticipationCommandHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<UpdateEventParticipationCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<EventParticipationDTO> Handle(UpdateEventParticipationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateEventParticipationCommand for ParticipationId: {ParticipationId}", request.ParticipationId);

            var participation = await _context.EventParticipations.FindAsync(request.ParticipationId, cancellationToken);

            if (participation == null)
            {
                _logger.LogWarning("EventParticipation not found for ParticipationId: {ParticipationId}", request.ParticipationId);
                throw new Exception("EventParticipation not found");
            }

            participation.EventId = request.EventId;
            participation.VolunteerId = request.VolunteerId;
            participation.ParticipationDate = request.ParticipationDate;
            participation.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("EventParticipation updated with ID: {ParticipationId}", participation.ParticipationId);

            return _mapper.Map<EventParticipationDTO>(participation);
        }
    }
}
