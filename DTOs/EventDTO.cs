namespace DONACIONES_VOLUNTARIAS.API.DTOs;

public class EventDTO
{
    public int EventId { get; set; }

    public int? OrganizationId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? EventDate { get; set; }

    public string? Location { get; set; }

    public DateTime? CreatedAt { get; set; }
}
