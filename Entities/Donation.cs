namespace DONACIONES_VOLUNTARIAS.API.Entities;

public partial class Donation
{
    public int DonationId { get; set; }

    public int? DonorId { get; set; }

    public int? OrganizationId { get; set; }

    public decimal Amount { get; set; }

    public DateTime? DonationDate { get; set; }

    public string? Notes { get; set; }

    public virtual Donor? Donor { get; set; }

    public virtual Organization? Organization { get; set; }
}
