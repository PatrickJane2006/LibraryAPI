using Library.Application.DTO.Auth;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Library.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            throw new Exception("Пользователь с таким email уже существует.");

        var passwordHash = HashPassword(request.Password);

        var user = new User
        {
            FullName = request.FullName,
            Phone = request.Phone,
            Address = request.Address,
            BirthDate = request.BirthDate,
            Email = request.Email,
            PasswordHash = passwordHash,
            Role = UserRole.User
        };

        _context.Users.Add(user);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            // Возвращаем наиболее полезную ошибку
            var innerMessage = ex.InnerException?.Message ?? ex.Message;
            throw new Exception($"Ошибка при сохранении пользователя: {innerMessage}");
        }

        return GenerateToken(user);
    }


    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
            throw new Exception("Неверный email или пароль.");

        return GenerateToken(user);
    }

    private AuthResponse GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256 // Исправлено здесь
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AuthResponse
        {
            Token = tokenHandler.WriteToken(token),
            Role = user.Role.ToString(),
            Email = user.Email
        };
    }

    private string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        return HashPassword(password) == storedHash;
    }
}