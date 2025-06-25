using Library.Application.DTO.Statistics;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore; 

public class StatisticsService : IStatisticsService
{
    private readonly AppDbContext _context;

    public StatisticsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LibraryStatsDto> GetLibraryStatisticsAsync()
    {
        var totalOrders = await _context.Orders.CountAsync();
        var activeOrders = await _context.Orders.CountAsync(o => o.Status == OrderStatus.Active);
        var returnedBooks = await _context.OrderBooks.CountAsync(ob => ob.IsReturned);

        var topBooks = await _context.OrderBooks
            .Include(ob => ob.Book) // Нужно для доступа к Title
            .GroupBy(ob => ob.BookId)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new TopBookDto
            {
                BookId = g.Key,
                Title = g.First().Book.Title,
                OrdersCount = g.Count()
            })
            .ToListAsync(); // ❗ теперь без ошибок

        var totalUsers = await _context.Users.CountAsync();
        var admins = await _context.Users.CountAsync(u => u.Role == UserRole.Admin);


        return new LibraryStatsDto
        {
            TotalOrders = totalOrders,
            ActiveOrders = activeOrders,
            ReturnedBooks = returnedBooks,
            TopBooks = topBooks,
            TotalUsers = totalUsers,
            Admins = admins
        };
    }
}
