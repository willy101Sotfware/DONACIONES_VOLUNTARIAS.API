namespace DONACIONES_VOLUNTARIAS.API.Entities;

public partial class EventParticipation
{
    public int ParticipationId { get; set; }

    public int? EventId { get; set; }

    public int? VolunteerId { get; set; }

    public DateTime? ParticipationDate { get; set; }

    public string? Notes { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Volunteer? Volunteer { get; set; }
}
