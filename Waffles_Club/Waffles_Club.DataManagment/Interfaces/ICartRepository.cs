using Waffles_Club.Data.Entity;

namespace Waffles_Club.DataManagment.Interfaces;

public interface ICartRepository:IBaseRepository<Cart>
{
    Task<Cart> GetByUserId(Guid userId);
}