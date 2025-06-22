using Library.Application.DTO.User;

namespace Library.Application.Interfaces.Services;

public interface IUserService
{
    Task<UserProfileDto> GetCurrentUserAsync(int userId);
}
