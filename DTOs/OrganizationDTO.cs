namespace DONACIONES_VOLUNTARIAS.API.DTOs;

public class OrganizationDTO
{
    public int OrganizationId { get; set; }

    public int? UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Website { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public DateTime? CreatedAt { get; set; }
}
