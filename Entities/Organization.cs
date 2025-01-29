namespace DONACIONES_VOLUNTARIAS.API.Entities;

public partial class Organization
{
    public int OrganizationId { get; set; }

    public int? UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Website { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Donation> Donations { get; set; } = [];

    public virtual ICollection<Event> Events { get; set; } = [];

    public virtual ICollection<Impact> Impacts { get; set; } = [];

    public virtual User? User { get; set; }
}
