namespace DONACIONES_VOLUNTARIAS.API.DTOs;

public class DonationDTO
{
    public int DonationId { get; set; }

    public int? DonorId { get; set; }

    public int? OrganizationId { get; set; }

    public decimal Amount { get; set; }

    public DateTime? DonationDate { get; set; }

    public string? Notes { get; set; }
}
