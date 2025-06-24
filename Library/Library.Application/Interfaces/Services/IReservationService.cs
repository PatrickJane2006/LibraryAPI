using Library.Application.DTO.Reservations;

namespace Library.Application.Interfaces.Services;

public interface IReservationService
{
    Task<int> CreateAsync(int userId, CreateReservationDto dto);
    Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(int userId);
    Task DeleteAsync(int reservationId, int userId, bool isAdmin);
}
