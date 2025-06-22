using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities;

public enum UserRole { User, Admin }

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Address { get; set; } = "";

    [Column(TypeName = "timestamp without time zone")]
    public DateTime BirthDate { get; set; }


    public string Email { get; set; } = ""; // <--- добавляем
    public string PasswordHash { get; set; } = "";
    public UserRole Role { get; set; } = UserRole.User;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
