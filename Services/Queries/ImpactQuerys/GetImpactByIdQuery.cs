using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.ImpactQuerys;

public class GetImpactByIdQuery : IRequest<ImpactDTO?>
{
    public int ImpactId { get; set; }

    public class GetImpactByIdQueryHandler : IRequestHandler<GetImpactByIdQuery, ImpactDTO?>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetImpactByIdQueryHandler> _logger;

        public GetImpactByIdQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetImpactByIdQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ImpactDTO?> Handle(GetImpactByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetImpactByIdQuery for ImpactId: {ImpactId}", request.ImpactId);

            var impact = await _context.Impacts
                .Include(i => i.Organization)
                .Include(i => i.Event)
                .FirstOrDefaultAsync(i => i.ImpactId == request.ImpactId, cancellationToken);

            if (impact == null)
            {
                _logger.LogWarning("Impact not found for ImpactId: {ImpactId}", request.ImpactId);
                return null;
            }

            _logger.LogInformation("Impact found: {ImpactId}", impact.ImpactId);
            return _mapper.Map<ImpactDTO>(impact);
        }
    }
}
