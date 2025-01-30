using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.VolunteerQuerys;

public class GetAllVolunteersQuery : IRequest<List<VolunteerDTO>>
{
    public class GetAllVolunteersQueryHandler : IRequestHandler<GetAllVolunteersQuery, List<VolunteerDTO>>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllVolunteersQueryHandler> _logger;

        public GetAllVolunteersQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetAllVolunteersQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<VolunteerDTO>> Handle(GetAllVolunteersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllVolunteersQuery");

            var volunteers = await _context.Volunteers
                .Include(v => v.User)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Retrieved {Count} volunteers", volunteers.Count);

            return _mapper.Map<List<VolunteerDTO>>(volunteers);
        }
    }
}
