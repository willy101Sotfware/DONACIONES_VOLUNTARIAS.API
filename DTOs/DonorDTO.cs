namespace DONACIONES_VOLUNTARIAS.API.DTOs;

public class DonorDTO
{
    public int DonorId { get; set; }

    public int? UserId { get; set; }

    public string? PreferredCauses { get; set; }

    public DateTime? CreatedAt { get; set; }
}
