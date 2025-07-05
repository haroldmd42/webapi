using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Services;

public class UserService : IUserService
{
    private readonly TareasContext _context;

    public UserService(TareasContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<User>> Get()
    {
        return await _context.users.ToListAsync();
    }

    public async Task Save(User user)
    {
        await _context.users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Guid id, User user)
    {
        var userActual = await _context.users.FindAsync(id);
        if (userActual != null)
        {
            userActual.Name = user.Name;
            userActual.Email = user.Email;
            userActual.Password = user.Password;
            userActual.LastName = user.LastName;
            userActual.Phone = user.Phone;

            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id)
    {
        var userActual = await _context.users.FindAsync(id);
        if (userActual != null)
        {
            _context.users.Remove(userActual);
            await _context.SaveChangesAsync();
        }
    }
}
public interface IUserService
{
    Task<IEnumerable<User>> Get();
    Task Save(User user);
    Task Update(Guid id, User user);
    Task Delete(Guid id);
}
