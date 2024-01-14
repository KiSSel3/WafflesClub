using Microsoft.EntityFrameworkCore;
using Waffles_Club.Data.Entity;
using Waffles_Club.DataManagment.Interfaces;

namespace Waffles_Club.DataManagment.Implementations;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(User? entity)
    {
        await _dbContext.Users.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task<List<User>> GetAll()
    {
        return _dbContext.Users.ToListAsync();
    }

    public async Task Delete(User? entity)
    {
        _dbContext.Users.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User> Update(User entity)
    {
        _dbContext.Users.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<User> GetById(Guid id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User> GetByName(string name)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == name);
    }

    public async Task<User> GetByLogin(string login)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == login);
    }
}
