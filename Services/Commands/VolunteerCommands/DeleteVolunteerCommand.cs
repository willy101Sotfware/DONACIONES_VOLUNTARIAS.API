using MediatR;
using DONACIONES_VOLUNTARIAS.API.Interface;


namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.VolunteerCommands;

public class DeleteVolunteerCommand : IRequest
{
    public int VolunteerId { get; set; }

    public class DeleteVolunteerCommandHandler : IRequestHandler<DeleteVolunteerCommand>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly ILogger<DeleteVolunteerCommandHandler> _logger;

        public DeleteVolunteerCommandHandler(IGestionVoluntariadoDonacionesContext context, ILogger<DeleteVolunteerCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteVolunteerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling DeleteVolunteerCommand for VolunteerId: {VolunteerId}", request.VolunteerId);

            var volunteer = await _context.Volunteers.FindAsync(request.VolunteerId, cancellationToken);

            if (volunteer == null)
            {
                _logger.LogWarning("Volunteer not found for VolunteerId: {VolunteerId}", request.VolunteerId);
                throw new Exception("Volunteer not found");
            }

            _context.Volunteers.Remove(volunteer);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Volunteer deleted with ID: {VolunteerId}", request.VolunteerId);

            return Unit.Value;
        }
    }
}

