using MediatR;
using DONACIONES_VOLUNTARIAS.API.Interface;


namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.UserCommads;

public class DeleteUserCommand : IRequest
{
    public int UserId { get; set; }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IGestionVoluntariadoDonacionesContext context, ILogger<DeleteUserCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling DeleteUserCommand for UserId: {UserId}", request.UserId);

            var user = await _context.Users.FindAsync(request.UserId, cancellationToken);

            if (user == null)
            {
                _logger.LogWarning("User not found for UserId: {UserId}", request.UserId);
                throw new Exception("User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("User deleted with ID: {UserId}", request.UserId);

            return Unit.Value;
        }
    }
}

