using Microsoft.EntityFrameworkCore;
using QMS.Core.Entities;
using QMS.Data.Context;

namespace QMS.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly QmsDbContext _context;

    public UserRepository(QmsDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == id);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users
            .Include(u => u.Role)
            .Where(u => u.IsActive)
            .ToListAsync();
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            user.IsActive = false;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(int roleId)
    {
        return await _context.Users
            .Where(u => u.RoleId == roleId && u.IsActive)
            .ToListAsync();
    }
}
