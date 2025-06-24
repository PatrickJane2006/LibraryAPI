public class OrderDto
{
    public int Id { get; set; }
    public List<string> BookTitles { get; set; } = new();
    public string Status { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}
