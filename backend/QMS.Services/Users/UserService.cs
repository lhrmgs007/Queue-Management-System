using QMS.Core.Entities;
using QMS.Core.Utilities;
using QMS.Data.Repositories;

namespace QMS.Services.Users;

public interface IUserService
{
    Task<User?> AuthenticateAsync(string username, string password);
    Task<User> CreateUserAsync(string username, string password, int roleId);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null)
            return null;

        if (!PasswordHasher.VerifyPassword(password, user.PasswordHash))
            return null;

        user.LastLoginDate = DateTime.UtcNow;
        await _userRepository.UpdateUserAsync(user);

        return user;
    }

    public async Task<User> CreateUserAsync(string username, string password, int roleId)
    {
        var hashedPassword = PasswordHasher.HashPassword(password);
        var user = new User
        {
            Username = username,
            PasswordHash = hashedPassword,
            RoleId = roleId,
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        return await _userRepository.CreateUserAsync(user);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        return await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteUserAsync(id);
    }
}
