using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.UserQuerys;

public class GetUserByIdQuery : IRequest<UserDTO?>
{
    public int UserId { get; set; }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO?>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;

        public GetUserByIdQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetUserByIdQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDTO?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetUserByIdQuery for UserId: {UserId}", request.UserId);

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

            if (user == null)
            {
                _logger.LogWarning("User not found for UserId: {UserId}", request.UserId);
                return null;
            }

            _logger.LogInformation("User found: {UserId}", user.UserId);
            return _mapper.Map<UserDTO>(user);
        }
    }
}

