using Library.Application.DTO.Reviews;

public interface IReviewService
{
    Task<int> AddReviewAsync(int userId, CreateReviewDto dto);
    Task<IEnumerable<ReviewDto>> GetReviewsByBookIdAsync(int bookId);
}