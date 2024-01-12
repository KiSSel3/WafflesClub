using Microsoft.EntityFrameworkCore;
using Waffles_Club.Data.Entity;
using Waffles_Club.DataManagment.Interfaces;

namespace Waffles_Club.DataManagment.Implementations;

public class FillingTypeRepository : IFillingTypeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FillingTypeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(FillingType? entity)
    {
        await _dbContext.FillingTypes.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task<List<FillingType>> GetAll()
    {
        return _dbContext.FillingTypes.ToListAsync();
    }

    public async Task Delete(FillingType? entity)
    {
        _dbContext.FillingTypes.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<FillingType> Update(FillingType entity)
    {
        _dbContext.FillingTypes.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<FillingType?> GetById(Guid id)
    {
        return await _dbContext.FillingTypes.FindAsync(id);
    }

    public async Task<FillingType> GetByNormalizedName(string normalizedName)
    {
        return await _dbContext.FillingTypes.FirstOrDefaultAsync(ft => ft.NormalizedName == normalizedName);
    }
}
