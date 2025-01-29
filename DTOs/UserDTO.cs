namespace DONACIONES_VOLUNTARIAS.API.DTOs;

public class UserDTO
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Role { get; set; }

    public DateTime? CreatedAt { get; set; }
}
