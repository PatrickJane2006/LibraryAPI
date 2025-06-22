using Library.Application.DTO.User;
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
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) throw new Exception("Пользователь не найден");

        return new UserProfileDto
        {
            FullName = user.FullName,
            Email = user.Email,
            Phone = user.Phone,
            Address = user.Address,
            BirthDate = user.BirthDate,
            Role = user.Role.ToString()
        };
    }
}
