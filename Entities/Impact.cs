namespace DONACIONES_VOLUNTARIAS.API.Entities;

public partial class Impact
{
    public int ImpactId { get; set; }

    public int? OrganizationId { get; set; }

    public int? EventId { get; set; }

    public string? MetricName { get; set; }

    public decimal? MetricValue { get; set; }

    public DateTime? MeasurementDate { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Organization? Organization { get; set; }
}
