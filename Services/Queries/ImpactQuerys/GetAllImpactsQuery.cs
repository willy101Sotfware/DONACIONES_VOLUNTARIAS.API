using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.ImpactQuerys;

public class GetAllImpactsQuery : IRequest<List<ImpactDTO>>
{
    public class GetAllImpactsQueryHandler : IRequestHandler<GetAllImpactsQuery, List<ImpactDTO>>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllImpactsQueryHandler> _logger;

        public GetAllImpactsQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetAllImpactsQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ImpactDTO>> Handle(GetAllImpactsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllImpactsQuery");

            var impacts = await _context.Impacts
                .Include(i => i.Organization)
                .Include(i => i.Event)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Retrieved {Count} impacts", impacts.Count);

            return _mapper.Map<List<ImpactDTO>>(impacts);
        }
    }
}

