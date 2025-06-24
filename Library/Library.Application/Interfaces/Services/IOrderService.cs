public interface IOrderService
{
    Task<int> CreateOrderAsync(int userId, CreateOrderDto dto);
    Task<IEnumerable<OrderDto>> GetUserOrdersAsync(int userId);
    Task CancelOrderAsync(int orderId, int userId, bool isAdmin);
}
