using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Entities;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;

namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.ImpactCommads;

public class CreateImpactCommand : IRequest<ImpactDTO>
{
    public int? OrganizationId { get; set; }
    public int? EventId { get; set; }
    public string? MetricName { get; set; }
    public decimal? MetricValue { get; set; }
    public DateTime? MeasurementDate { get; set; }

    public class CreateImpactCommandHandler : IRequestHandler<CreateImpactCommand, ImpactDTO>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateImpactCommandHandler> _logger;

        public CreateImpactCommandHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<CreateImpactCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ImpactDTO> Handle(CreateImpactCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateImpactCommand");

            var impact = new Impact
            {
                OrganizationId = request.OrganizationId,
                EventId = request.EventId,
                MetricName = request.MetricName,
                MetricValue = request.MetricValue,
                MeasurementDate = request.MeasurementDate ?? DateTime.UtcNow
            };

            _context.Impacts.Add(impact);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Impact created with ID: {ImpactId}", impact.ImpactId);

            return _mapper.Map<ImpactDTO>(impact);
        }
    }
}
