using Library.Application.DTO;  
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateOrderAsync(int userId, CreateOrderDto dto)
    {
        var order = new Order
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Active, 
            OrderBooks = dto.BookIds.Select(bookId => new OrderBook
            {
                BookId = bookId
            }).ToList()
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order.Id;
    }

    public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(int userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderBooks)
            .ThenInclude(ob => ob.Book)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                BookTitles = o.OrderBooks.Select(ob => ob.Book.Title).ToList(),
                Status = o.Status.ToString(),
                CreatedAt = o.CreatedAt
            })
            .ToListAsync();
    }

    public async Task CancelOrderAsync(int orderId, int userId, bool isAdmin)
    {
        var order = await _context.Orders.Include(o => o.OrderBooks).FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null) return;

        if (!isAdmin && order.UserId != userId)
            throw new Exception("Нет доступа");

        order.Status = OrderStatus.Canceled; 
        await _context.SaveChangesAsync();
    }
}
