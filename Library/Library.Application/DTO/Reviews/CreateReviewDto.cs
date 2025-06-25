namespace Library.Application.DTO.Reviews;

public class CreateReviewDto
{
    public int BookId { get; set; }
    public int Rating { get; set; } // от 1 до 5
    public string Comment { get; set; } = string.Empty;
}