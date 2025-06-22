namespace Library.Domain.Entities;

public class OrderBook
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public bool IsReturned { get; set; } = false;
    public DateTime? ReturnDue { get; set; }
}
