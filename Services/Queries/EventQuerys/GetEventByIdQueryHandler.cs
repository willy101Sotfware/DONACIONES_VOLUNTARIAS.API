
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using global::DONACIONES_VOLUNTARIAS.API.DTOs;
using global::DONACIONES_VOLUNTARIAS.API.Interface;
namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.EventQuerys;

public class GetEventByIdQuery : IRequest<EventDTO?>
{
    public int EventId { get; set; }

    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventDTO?>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEventByIdQueryHandler> _logger;

        public GetEventByIdQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetEventByIdQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<EventDTO?> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetEventByIdQuery for EventId: {EventId}", request.EventId);

            var eventEntity = await _context.Events
                .Include(e => e.Organization)
                .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

            if (eventEntity == null)
            {
                _logger.LogWarning("Event not found for EventId: {EventId}", request.EventId);
                return null;
            }

            _logger.LogInformation("Event found: {EventId}", eventEntity.EventId);
            return _mapper.Map<EventDTO>(eventEntity);
        }
    }
}
