using MediatR;
using DONACIONES_VOLUNTARIAS.API.Interface;

namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.OrganizationCommands;

public class DeleteOrganizationCommand : IRequest
{
    public int OrganizationId { get; set; }

    public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommand>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly ILogger<DeleteOrganizationCommandHandler> _logger;

        public DeleteOrganizationCommandHandler(IGestionVoluntariadoDonacionesContext context, ILogger<DeleteOrganizationCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling DeleteOrganizationCommand for OrganizationId: {OrganizationId}", request.OrganizationId);

            var organization = await _context.Organizations.FindAsync(request.OrganizationId, cancellationToken);

            if (organization == null)
            {
                _logger.LogWarning("Organization not found for OrganizationId: {OrganizationId}", request.OrganizationId);
                throw new Exception("Organization not found");
            }

            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Organization deleted with ID: {OrganizationId}", request.OrganizationId);

            return Unit.Value;
        }
    }
}

