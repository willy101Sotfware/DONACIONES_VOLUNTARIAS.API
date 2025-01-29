namespace DONACIONES_VOLUNTARIAS.API.Entities;

public partial class Event
{
    public int EventId { get; set; }

    public int? OrganizationId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? EventDate { get; set; }

    public string? Location { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<EventParticipation> EventParticipations { get; set; } = new List<EventParticipation>();

    public virtual ICollection<Impact> Impacts { get; set; } = new List<Impact>();

    public virtual Organization? Organization { get; set; }
}
