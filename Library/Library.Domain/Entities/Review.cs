namespace Library.Domain.Entities;

public class Review
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public int Rating { get; set; } // от 1 до 5
    public string Text { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
