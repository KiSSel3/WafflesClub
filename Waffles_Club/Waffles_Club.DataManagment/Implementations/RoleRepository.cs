using Microsoft.EntityFrameworkCore;
using Waffles_Club.Data.Entity;
using Waffles_Club.DataManagment.Interfaces;

namespace Waffles_Club.DataManagment.Implementations;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RoleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Role? entity)
    {
        await _dbContext.Roles.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task<List<Role>> GetAll()
    {
        return _dbContext.Roles.ToListAsync();
    }

    public async Task Delete(Role? entity)
    {
        _dbContext.Roles.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Role> Update(Role entity)
    {
        _dbContext.Roles.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Role> GetById(Guid id)
    {
        return await _dbContext.Roles.FindAsync(id);
    }

    public async Task<Role> GetByName(string name)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(role => role.Name == name);
    }
}