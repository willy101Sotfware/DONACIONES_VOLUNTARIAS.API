using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.EventParticipationQuerys;

public class GetEventParticipationByIdQuery : IRequest<EventParticipationDTO?>
{
    public int ParticipationId { get; set; }

    public class GetEventParticipationByIdQueryHandler : IRequestHandler<GetEventParticipationByIdQuery, EventParticipationDTO?>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEventParticipationByIdQueryHandler> _logger;

        public GetEventParticipationByIdQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetEventParticipationByIdQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<EventParticipationDTO?> Handle(GetEventParticipationByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetEventParticipationByIdQuery for ParticipationId: {ParticipationId}", request.ParticipationId);

            var participation = await _context.EventParticipations
                .Include(p => p.Event)
                .Include(p => p.Volunteer)
                .FirstOrDefaultAsync(p => p.ParticipationId == request.ParticipationId, cancellationToken);

            if (participation == null)
            {
                _logger.LogWarning("EventParticipation not found for ParticipationId: {ParticipationId}", request.ParticipationId);
                return null;
            }

            _logger.LogInformation("EventParticipation found: {ParticipationId}", participation.ParticipationId);
            return _mapper.Map<EventParticipationDTO>(participation);
        }
    }
}

