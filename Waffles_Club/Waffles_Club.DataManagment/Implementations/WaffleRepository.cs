using Microsoft.EntityFrameworkCore;
using Waffles_Club.Data.Entity;
using Waffles_Club.DataManagment.Interfaces;

namespace Waffles_Club.DataManagment.Implementations;

public class WaffleRepository : IWaffleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public WaffleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Waffle? entity)
    {
        await _dbContext.Waffles.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task<List<Waffle>> GetAll()
    {
        return _dbContext.Waffles.ToListAsync();
    }

    public async Task Delete(Waffle? entity)
    {
        _dbContext.Waffles.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Waffle> Update(Waffle entity)
    {
        _dbContext.Waffles.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Waffle> GetById(Guid id)
    {
        return await _dbContext.Waffles.FirstOrDefaultAsync(x=>x.Id==id);
    }

    public async Task<Waffle> GetByName(string name)
    {
        return await _dbContext.Waffles.FirstOrDefaultAsync(x => x.Name == name);
    }
}