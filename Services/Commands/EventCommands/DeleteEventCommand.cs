using MediatR;
using global::DONACIONES_VOLUNTARIAS.API.Interface;
namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.EventCommands;

public class DeleteEventCommand : IRequest
{
    public int EventId { get; set; }

    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly ILogger<DeleteEventCommandHandler> _logger;

        public DeleteEventCommandHandler(IGestionVoluntariadoDonacionesContext context, ILogger<DeleteEventCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling DeleteEventCommand for EventId: {EventId}", request.EventId);

            var eventEntity = await _context.Events.FindAsync(request.EventId, cancellationToken);

            if (eventEntity == null)
            {
                _logger.LogWarning("Event not found for EventId: {EventId}", request.EventId);
                throw new Exception("Event not found");
            }

            _context.Events.Remove(eventEntity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Event deleted with ID: {EventId}", request.EventId);

            return Unit.Value;
        }
    }
}
