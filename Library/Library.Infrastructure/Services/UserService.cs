using Library.Application.DTO.Reservations;
using Library.Application.DTO.Users;
using Library.Application.Interfaces.Services;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfileDto> GetCurrentUserAsync(int userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new Exception("Пользователь не найден");

        var reservations = await _context.Reservations
            .Where(r => r.UserId == userId)
            .Include(r => r.Seat)
                .ThenInclude(s => s.ReadingRoom)
            .Select(r => new ReservationDto
            {
                Id = r.Id,
                SeatId = r.Seat.Id,
                SeatNumber = r.Seat.Number,
                RoomId = r.Seat.ReadingRoom.Id,
                RoomName = r.Seat.ReadingRoom.Name,
                StartTime = r.StartTime,
                EndTime = r.EndTime
            })
            .ToListAsync();

        return new UserProfileDto
        {
            FullName = user.FullName,
            Email = user.Email,
            Phone = user.Phone,
            Address = user.Address,
            BirthDate = user.BirthDate,
            Role = user.Role.ToString(),
            Reservations = reservations
        };
    }
}
