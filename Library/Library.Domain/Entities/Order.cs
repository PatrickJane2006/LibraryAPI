namespace Library.Domain.Entities;

public enum OrderStatus { Active, Returned, Overdue }

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; }

    public ICollection<OrderBook> OrderBooks { get; set; } = new List<OrderBook>();
}
