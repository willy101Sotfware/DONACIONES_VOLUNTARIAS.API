using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Entities;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;


namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.OrganizationCommands;

public class CreateOrganizationCommand : IRequest<OrganizationDTO>
{
    public int? UserId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Website { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }

    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, OrganizationDTO>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateOrganizationCommandHandler> _logger;

        public CreateOrganizationCommandHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<CreateOrganizationCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrganizationDTO> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateOrganizationCommand");

            var organization = new Organization
            {
                UserId = request.UserId,
                Name = request.Name,
                Description = request.Description,
                Website = request.Website,
                ContactEmail = request.ContactEmail,
                ContactPhone = request.ContactPhone,
                CreatedAt = DateTime.UtcNow
            };

            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Organization created with ID: {OrganizationId}", organization.OrganizationId);

            return _mapper.Map<OrganizationDTO>(organization);
        }
    }
}
