namespace Library.Application.DTO.Books;

public class CreateBookDto
{
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public string Genre { get; set; } = "";
    public int Year { get; set; }
    public string? Description { get; set; } = null;
    public int AvailableCount { get; set; }
}
