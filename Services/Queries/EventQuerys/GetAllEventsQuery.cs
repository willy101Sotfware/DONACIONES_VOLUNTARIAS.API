
using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.EventQuerys;

public class GetAllEventsQuery : IRequest<List<EventDTO>>
{
    public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, List<EventDTO>>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllEventsQueryHandler> _logger;

        public GetAllEventsQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetAllEventsQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<EventDTO>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllEventsQuery");

            var events = await _context.Events
                .Include(e => e.Organization)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Retrieved {Count} events", events.Count);

            return _mapper.Map<List<EventDTO>>(events);
        }
    }
}

