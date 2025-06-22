namespace Library.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsAvailable { get; set; } = true;
    public int AvailableCount { get; set; } = 0;

    public ICollection<OrderBook> OrderBooks { get; set; } = new List<OrderBook>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

}
