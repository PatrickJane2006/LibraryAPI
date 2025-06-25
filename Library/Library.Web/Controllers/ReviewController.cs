using Library.Application.DTO.Reviews;
using Library.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddReview(CreateReviewDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized();

        var id = await _reviewService.AddReviewAsync(userId, dto);
        return Ok(new { id });
    }

    [HttpGet("book/{bookId}")]
    public async Task<IActionResult> GetReviews(int bookId)
    {
        var reviews = await _reviewService.GetReviewsByBookIdAsync(bookId);
        return Ok(reviews);
    }
}