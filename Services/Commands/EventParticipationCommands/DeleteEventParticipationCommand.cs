using MediatR;
using DONACIONES_VOLUNTARIAS.API.Interface;


namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.EventParticipationCommands;

public class DeleteEventParticipationCommand : IRequest
{
    public int ParticipationId { get; set; }

    public class DeleteEventParticipationCommandHandler : IRequestHandler<DeleteEventParticipationCommand>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly ILogger<DeleteEventParticipationCommandHandler> _logger;

        public DeleteEventParticipationCommandHandler(IGestionVoluntariadoDonacionesContext context, ILogger<DeleteEventParticipationCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteEventParticipationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling DeleteEventParticipationCommand for ParticipationId: {ParticipationId}", request.ParticipationId);

            var participation = await _context.EventParticipations.FindAsync(request.ParticipationId, cancellationToken);

            if (participation == null)
            {
                _logger.LogWarning("EventParticipation not found for ParticipationId: {ParticipationId}", request.ParticipationId);
                throw new Exception("EventParticipation not found");
            }

            _context.EventParticipations.Remove(participation);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("EventParticipation deleted with ID: {ParticipationId}", request.ParticipationId);

            return Unit.Value;
        }
    }
}
