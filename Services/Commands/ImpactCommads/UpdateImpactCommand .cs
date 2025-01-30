using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;


namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.ImpactCommads;

public class UpdateImpactCommand : IRequest<ImpactDTO>
{
    public int ImpactId { get; set; }
    public int? OrganizationId { get; set; }
    public int? EventId { get; set; }
    public string? MetricName { get; set; }
    public decimal? MetricValue { get; set; }
    public DateTime? MeasurementDate { get; set; }

    public class UpdateImpactCommandHandler : IRequestHandler<UpdateImpactCommand, ImpactDTO>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateImpactCommandHandler> _logger;

        public UpdateImpactCommandHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<UpdateImpactCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ImpactDTO> Handle(UpdateImpactCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateImpactCommand for ImpactId: {ImpactId}", request.ImpactId);

            var impact = await _context.Impacts.FindAsync(request.ImpactId, cancellationToken);

            if (impact == null)
            {
                _logger.LogWarning("Impact not found for ImpactId: {ImpactId}", request.ImpactId);
                throw new Exception("Impact not found");
            }

            impact.OrganizationId = request.OrganizationId;
            impact.EventId = request.EventId;
            impact.MetricName = request.MetricName;
            impact.MetricValue = request.MetricValue;
            impact.MeasurementDate = request.MeasurementDate;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Impact updated with ID: {ImpactId}", impact.ImpactId);

            return _mapper.Map<ImpactDTO>(impact);
        }
    }
}

