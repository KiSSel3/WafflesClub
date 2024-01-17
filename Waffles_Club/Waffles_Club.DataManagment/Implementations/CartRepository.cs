using Microsoft.EntityFrameworkCore;
using Waffles_Club.Data.Entity;
using Waffles_Club.DataManagment.Interfaces;

namespace Waffles_Club.DataManagment.Implementations;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CartRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Cart? entity)
    {
        await _dbContext.Carts.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task<List<Cart>> GetAll()
    {
        return _dbContext.Carts.ToListAsync();
    }

    public async Task Delete(Cart? entity)
    {
        _dbContext.Carts.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Cart> Update(Cart entity)
    {
        _dbContext.Carts.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Cart> GetById(Guid id)
    {
        return await _dbContext.Carts.FindAsync(id);
    }

    public async Task<List<Cart>> GetByUserId(Guid userId)
    {
        return await _dbContext.Carts.Where(a=>a.UserId== userId).ToListAsync();
    }

    public async Task DeleteByUserId(Guid userId)
    {
        var cartsToDelete = await _dbContext.Carts.Where(c => c.UserId.Equals(userId)).ToListAsync();

        _dbContext.Carts.RemoveRange(cartsToDelete);

        await _dbContext.SaveChangesAsync();
    }
}
