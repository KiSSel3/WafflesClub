using Microsoft.EntityFrameworkCore;
using Waffles_Club.Data.Entity;
using Waffles_Club.DataManagment.Interfaces;

namespace Waffles_Club.DataManagment.Implementations;

public class WaffleTypeRepository : IWaffleTypeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public WaffleTypeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WaffleType> GetByNormalizedName(string normalizedName)
    {
        return await _dbContext.WaffleTypes.FirstOrDefaultAsync(wt => wt.NormalizedName == normalizedName);
    }

    public async Task Create(WaffleType? entity)
    {
        await _dbContext.WaffleTypes.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task<List<WaffleType>> GetAll()
    {
        return _dbContext.WaffleTypes.ToListAsync();
    }

    public async Task Delete(WaffleType? entity)
    {
        _dbContext.WaffleTypes.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<WaffleType> Update(WaffleType entity)
    {
        _dbContext.WaffleTypes.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<WaffleType> GetById(Guid id)
    {
        return await _dbContext.WaffleTypes.FindAsync(id);
    }
}
