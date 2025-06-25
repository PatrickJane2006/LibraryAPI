using Library.Application.DTO.Reviews;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ReviewService : IReviewService
{
    private readonly AppDbContext _context;

    public ReviewService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddReviewAsync(int userId, CreateReviewDto dto)
    {
        var review = new Review
        {
            BookId = dto.BookId,
            Comment = dto.Comment,
            Rating = dto.Rating,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return review.Id;
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsByBookIdAsync(int bookId)
    {
        return await _context.Reviews
            .Where(r => r.BookId == bookId)
            .Include(r => r.User)
            .Select(r => new ReviewDto
            {
                Id = r.Id,
                BookId = r.BookId,
                Comment = r.Comment,
                Rating = r.Rating,
                CreatedAt = r.CreatedAt,
                UserName = r.User.FullName
            })
            .ToListAsync();
    }
}
