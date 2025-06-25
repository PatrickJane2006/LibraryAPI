namespace Library.Application.DTO.Statistics;

public class TopBookDto
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int OrdersCount { get; set; }
}
