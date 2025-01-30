using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using global::DONACIONES_VOLUNTARIAS.API.DTOs;
using global::DONACIONES_VOLUNTARIAS.API.Interface;

namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.OrganizationQuerys;

    public class GetOrganizationByIdQuery : IRequest<OrganizationDTO?>
    {
        public int OrganizationId { get; set; }

        public class GetOrganizationByIdQueryHandler : IRequestHandler<GetOrganizationByIdQuery, OrganizationDTO?>
        {
            private readonly IGestionVoluntariadoDonacionesContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<GetOrganizationByIdQueryHandler> _logger;

            public GetOrganizationByIdQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetOrganizationByIdQueryHandler> logger)
            {
                _context = context;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<OrganizationDTO?> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Handling GetOrganizationByIdQuery for OrganizationId: {OrganizationId}", request.OrganizationId);

                var organization = await _context.Organizations
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.OrganizationId == request.OrganizationId, cancellationToken);

                if (organization == null)
                {
                    _logger.LogWarning("Organization not found for OrganizationId: {OrganizationId}", request.OrganizationId);
                    return null;
                }

                _logger.LogInformation("Organization found: {OrganizationId}", organization.OrganizationId);
                return _mapper.Map<OrganizationDTO>(organization);
            }
        }
    }

