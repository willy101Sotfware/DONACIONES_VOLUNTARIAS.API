using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.OrganizationQuery;

public class GetAllOrganizationsQuery : IRequest<List<OrganizationDTO>>
{
    public class GetAllOrganizationsQueryHandler : IRequestHandler<GetAllOrganizationsQuery, List<OrganizationDTO>>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllOrganizationsQueryHandler> _logger;

        public GetAllOrganizationsQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetAllOrganizationsQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<OrganizationDTO>> Handle(GetAllOrganizationsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllOrganizationsQuery");

            var organizations = await _context.Organizations
                .Include(o => o.User)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Retrieved {Count} organizations", organizations.Count);

            return _mapper.Map<List<OrganizationDTO>>(organizations);
        }
    }
}

