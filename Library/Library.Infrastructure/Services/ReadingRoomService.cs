using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ReadingRoomService : IReadingRoomService
{
    private readonly AppDbContext _context;

    public ReadingRoomService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(CreateReadingRoomDto dto)
    {
        var room = new ReadingRoom
        {
            Name = dto.Name,
            Location = dto.Location,
            Capacity = dto.Capacity,
            IsAvailable = dto.IsAvailable
        };

        _context.ReadingRooms.Add(room);
        await _context.SaveChangesAsync();
        return room.Id;
    }

    public async Task<IEnumerable<ReadingRoomDto>> GetAllAsync()
    {
        return await _context.ReadingRooms
            .Select(r => new ReadingRoomDto
            {
                Id = r.Id,
                Name = r.Name,
                Location = r.Location,
                Capacity = r.Capacity,
                IsAvailable = r.IsAvailable
            })
            .ToListAsync();
    }
}