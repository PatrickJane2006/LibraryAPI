using Library.Application.DTO.Users;

namespace Library.Application.Interfaces.Services;

public interface IUserService
{
    Task<UserProfileDto> GetCurrentUserAsync(int userId);
}
