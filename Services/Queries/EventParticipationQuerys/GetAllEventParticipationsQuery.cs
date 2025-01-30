using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.EventParticipationQuerys;

public class GetAllEventParticipationsQuery : IRequest<List<EventParticipationDTO>>
{
    public class GetAllEventParticipationsQueryHandler : IRequestHandler<GetAllEventParticipationsQuery, List<EventParticipationDTO>>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllEventParticipationsQueryHandler> _logger;

        public GetAllEventParticipationsQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetAllEventParticipationsQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<EventParticipationDTO>> Handle(GetAllEventParticipationsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllEventParticipationsQuery");

            var participations = await _context.EventParticipations
                .Include(p => p.Event)
                .Include(p => p.Volunteer)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Retrieved {Count} event participations", participations.Count);

            return _mapper.Map<List<EventParticipationDTO>>(participations);
        }
    }
}
