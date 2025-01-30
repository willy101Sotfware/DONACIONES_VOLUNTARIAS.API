using MediatR;
using DONACIONES_VOLUNTARIAS.API.Interface;


namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.ImpactCommads;

public class DeleteImpactCommand : IRequest
{
    public int ImpactId { get; set; }

    public class DeleteImpactCommandHandler : IRequestHandler<DeleteImpactCommand>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly ILogger<DeleteImpactCommandHandler> _logger;

        public DeleteImpactCommandHandler(IGestionVoluntariadoDonacionesContext context, ILogger<DeleteImpactCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteImpactCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling DeleteImpactCommand for ImpactId: {ImpactId}", request.ImpactId);

            var impact = await _context.Impacts.FindAsync(request.ImpactId, cancellationToken);

            if (impact == null)
            {
                _logger.LogWarning("Impact not found for ImpactId: {ImpactId}", request.ImpactId);
                throw new Exception("Impact not found");
            }

            _context.Impacts.Remove(impact);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Impact deleted with ID: {ImpactId}", request.ImpactId);

            return Unit.Value;
        }
    }
}
