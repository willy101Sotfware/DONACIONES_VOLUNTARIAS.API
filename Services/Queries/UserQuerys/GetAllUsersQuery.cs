using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.UserQuerys;

public class GetAllUsersQuery : IRequest<List<UserDTO>>
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDTO>>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllUsersQueryHandler> _logger;

        public GetAllUsersQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetAllUsersQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllUsersQuery");

            var users = await _context.Users.ToListAsync(cancellationToken);

            _logger.LogInformation("Retrieved {Count} users", users.Count);

            return _mapper.Map<List<UserDTO>>(users);
        }
    }
}

