using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;

namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.UserCommads;

public class UpdateUserCommand : IRequest<UserDTO>
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Role { get; set; }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDTO>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<UpdateUserCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateUserCommand for UserId: {UserId}", request.UserId);

            var user = await _context.Users.FindAsync(request.UserId, cancellationToken);

            if (user == null)
            {
                _logger.LogWarning("User not found for UserId: {UserId}", request.UserId);
                throw new Exception("User not found");
            }

            user.Username = request.Username;
            user.PasswordHash = request.PasswordHash;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Role = request.Role;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("User updated with ID: {UserId}", user.UserId);

            return _mapper.Map<UserDTO>(user);
        }
    }
}
