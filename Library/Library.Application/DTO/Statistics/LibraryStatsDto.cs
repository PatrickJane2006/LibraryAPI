namespace Library.Application.DTO.Statistics;

public class LibraryStatsDto
{
    public int TotalOrders { get; set; }
    public int ActiveOrders { get; set; }
    public int ReturnedBooks { get; set; }
    public List<TopBookDto> TopBooks { get; set; } = new();
    public int TotalUsers { get; set; }
    public int Admins { get; set; }
}
