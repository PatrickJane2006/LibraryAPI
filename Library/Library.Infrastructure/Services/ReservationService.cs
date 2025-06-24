using Library.Application.DTO.Reservations;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ReservationService : IReservationService
{
    private readonly AppDbContext _context;

    public ReservationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(int userId, CreateReservationDto dto)
    {
        var overlapping = await _context.Reservations.AnyAsync(r =>
            r.SeatId == dto.SeatId &&
            r.EndTime > dto.StartTime &&
            r.StartTime < dto.EndTime);

        if (overlapping)
            throw new Exception("Место уже занято в это время");

        var reservation = new Reservation
        {
            UserId = userId,
            SeatId = dto.SeatId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime
        };

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
        return reservation.Id;
    }

    public async Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(int userId)
    {
        return await _context.Reservations
            .Where(r => r.UserId == userId)
            .Include(r => r.Seat)
            .ThenInclude(s => s.ReadingRoom)
            .Select(r => new ReservationDto
            {
                Id = r.Id,
                SeatId = r.SeatId,
                SeatNumber = r.Seat.Number,
                RoomId = r.Seat.ReadingRoomId,
                RoomName = r.Seat.ReadingRoom.Name,
                StartTime = r.StartTime,
                EndTime = r.EndTime
            })
            .ToListAsync();
    }

    public async Task DeleteAsync(int reservationId, int userId, bool isAdmin)
    {
        var reservation = await _context.Reservations.FindAsync(reservationId);
        if (reservation == null) return;

        if (!isAdmin && reservation.UserId != userId)
            throw new Exception("Нет доступа");

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
    }
}
