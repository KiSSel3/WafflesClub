using Waffles_Club.Data.Entity;

namespace Waffles_Club.DataManagment.Interfaces;

public interface ICartRepository:IBaseRepository<Cart>
{
    Cart GetByUserId(Guid userId);
}