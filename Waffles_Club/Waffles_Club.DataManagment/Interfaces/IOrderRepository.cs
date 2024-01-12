using Waffles_Club.Data.Entity;
using Waffles_Club.Data.Enum;
using Waffles_Club.Data.Models;

namespace Waffles_Club.DataManagment.Interfaces;

public interface IOrderRepository:IBaseRepository<Order>
{
    Task<List<Order>> GetByUserId(Guid userId);
    Task<List<Order>> GetByStatus(OrderStatus status);
}