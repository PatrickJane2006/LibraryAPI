namespace Library.Application.DTO.User;

public class UserProfileDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string Role { get; set; } = null!;
}
