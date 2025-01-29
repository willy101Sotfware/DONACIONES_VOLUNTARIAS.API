namespace DONACIONES_VOLUNTARIAS.API.DTOs;

public class ImpactDTO
{
    public int ImpactId { get; set; }

    public int? OrganizationId { get; set; }

    public int? EventId { get; set; }

    public string? MetricName { get; set; }

    public decimal? MetricValue { get; set; }

    public DateTime? MeasurementDate { get; set; }

}
