namespace Library.Domain.Entities;

public enum OrderStatus
{
    Active,    // Заказ активен и в работе
    Returned,  // Книги возвращены, заказ закрыт
    Overdue,   // Просроченный заказ
    Canceled   // Заказ отменён
}
public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Active; 

    public ICollection<OrderBook> OrderBooks { get; set; } = new List<OrderBook>();
}
