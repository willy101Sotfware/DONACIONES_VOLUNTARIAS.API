namespace DONACIONES_VOLUNTARIAS.API.Entities;

public partial class Volunteer
{
    public int VolunteerId { get; set; }

    public int? UserId { get; set; }

    public string? Skills { get; set; }

    public string? Interests { get; set; }

    public string? Availability { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<EventParticipation> EventParticipations { get; set; } = new List<EventParticipation>();

    public virtual User? User { get; set; }
}
