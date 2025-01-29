namespace DONACIONES_VOLUNTARIAS.API.Entities;

public partial class Donor
{
    public int DonorId { get; set; }

    public int? UserId { get; set; }

    public string? PreferredCauses { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();

    public virtual User? User { get; set; }
}
