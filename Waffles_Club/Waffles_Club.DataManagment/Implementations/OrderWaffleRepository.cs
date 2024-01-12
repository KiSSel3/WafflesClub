using Microsoft.EntityFrameworkCore;
using Waffles_Club.Data.Models;
using Waffles_Club.DataManagment.Interfaces;

namespace Waffles_Club.DataManagment.Implementations;

public class OrderWaffleRepository : IOrderWaffleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderWaffleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(OrderWaffle? entity)
    {
        await _dbContext.OrderWaffles.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task<List<OrderWaffle>> GetAll()
    {
        return _dbContext.OrderWaffles.ToListAsync();
    }

    public async Task Delete(OrderWaffle? entity)
    {
        _dbContext.OrderWaffles.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<OrderWaffle> Update(OrderWaffle entity)
    {
        _dbContext.OrderWaffles.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderWaffle> GetById(Guid id)
    {
        return await _dbContext.OrderWaffles.FindAsync(id);
    }

    public async Task<List<OrderWaffle>> GetByOrderId(Guid orderId)
    {
        return await _dbContext.OrderWaffles.Where(ow => ow.OrderId == orderId).ToListAsync();
    }
}
