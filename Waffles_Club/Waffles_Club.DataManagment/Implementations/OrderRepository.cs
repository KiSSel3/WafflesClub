using Microsoft.EntityFrameworkCore;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.Enum;
using Waffles_Club.DataManagment.Interfaces;

namespace Waffles_Club.DataManagment.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Order? entity)
    {
        await _dbContext.Orders.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task<List<Order>> GetAll()
    {
        return _dbContext.Orders.ToListAsync();
    }

    public async Task Delete(Order? entity)
    {
        _dbContext.Orders.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Order> Update(Order entity)
    {
        _dbContext.Orders.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Order> GetById(Guid id)
    {
        return await _dbContext.Orders.FindAsync(id);
    }

    public Task<List<Order>> GetByUserId(Guid userId)
    {
        return _dbContext.Orders.Where(o => o.UserId == userId).ToListAsync();
    }

    public Task<List<Order>> GetByStatus(OrderStatus status)
    {
        return _dbContext.Orders.Where(o => o.OrderStatus == status).ToListAsync();
    }
}