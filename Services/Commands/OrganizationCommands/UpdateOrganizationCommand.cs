using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;


namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.OrganizationCommands;

public class UpdateOrganizationCommand : IRequest<OrganizationDTO>
{
    public int OrganizationId { get; set; }
    public int? UserId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Website { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }

    public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, OrganizationDTO>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrganizationCommandHandler> _logger;

        public UpdateOrganizationCommandHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<UpdateOrganizationCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrganizationDTO> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateOrganizationCommand for OrganizationId: {OrganizationId}", request.OrganizationId);

            var organization = await _context.Organizations.FindAsync(request.OrganizationId, cancellationToken);

            if (organization == null)
            {
                _logger.LogWarning("Organization not found for OrganizationId: {OrganizationId}", request.OrganizationId);
                throw new Exception("Organization not found");
            }

            organization.UserId = request.UserId;
            organization.Name = request.Name;
            organization.Description = request.Description;
            organization.Website = request.Website;
            organization.ContactEmail = request.ContactEmail;
            organization.ContactPhone = request.ContactPhone;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Organization updated with ID: {OrganizationId}", organization.OrganizationId);

            return _mapper.Map<OrganizationDTO>(organization);
        }
    }
}

