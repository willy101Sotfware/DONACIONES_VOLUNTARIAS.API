namespace DONACIONES_VOLUNTARIAS.API.DTOs;

public class EventParticipationDTO
{
    public int ParticipationId { get; set; }

    public int? EventId { get; set; }

    public int? VolunteerId { get; set; }

    public DateTime? ParticipationDate { get; set; }

    public string? Notes { get; set; }

}
