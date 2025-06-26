using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class SeatService : ISeatService
{
    private readonly AppDbContext _context;

    public SeatService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(CreateSeatDto dto)
    {
        var room = await _context.ReadingRooms.FindAsync(dto.ReadingRoomId);
        if (room == null)
            throw new Exception("Читальный зал не найден");

        var seat = new Seat
        {
            Number = dto.Number,
            ReadingRoomId = dto.ReadingRoomId
        };

        _context.Seats.Add(seat);
        await _context.SaveChangesAsync();
        return seat.Id;
    }

    public async Task<IEnumerable<SeatDto>> GetByRoomIdAsync(int roomId)
    {
        return await _context.Seats
            .Where(s => s.ReadingRoomId == roomId)
            .Select(s => new SeatDto
            {
                Id = s.Id,
                Number = s.Number,
                ReadingRoomId = s.ReadingRoomId
            })
            .ToListAsync();
    }
}