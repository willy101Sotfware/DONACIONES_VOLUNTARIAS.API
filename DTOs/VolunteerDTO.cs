namespace DONACIONES_VOLUNTARIAS.API.DTOs;

public class VolunteerDTO
{
    public int VolunteerId { get; set; }

    public int? UserId { get; set; }

    public string? Skills { get; set; }

    public string? Interests { get; set; }

    public string? Availability { get; set; }

    public DateTime? CreatedAt { get; set; }
}
