using Microsoft.EntityFrameworkCore;
using Waffles_Club.Data.Models;
using Waffles_Club.DataManagment.Interfaces;

namespace Waffles_Club.DataManagment.Implementations;

public class RoleUserRepository : IRoleUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RoleUserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(RoleUser? entity)
    {
        await _dbContext.RoleUsers.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task<List<RoleUser>> GetAll()
    {
        return _dbContext.RoleUsers.ToListAsync();
    }

    public async Task Delete(RoleUser? entity)
    {
        _dbContext.RoleUsers.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<RoleUser> Update(RoleUser entity)
    {
        _dbContext.RoleUsers.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<RoleUser> GetById(Guid id)
    {
        return await _dbContext.RoleUsers.FindAsync(id);
    }

    public async Task<List<RoleUser>> GetByUserId(Guid userId)
    {
        return await _dbContext.RoleUsers.Where(ru => ru.UserId == userId).ToListAsync();
    }

    public async Task<List<RoleUser>> GetByRoleId(Guid roleId)
    {
        return await _dbContext.RoleUsers.Where(ru => ru.RoleId == roleId).ToListAsync();
    }
}
