using Waffles_Club.Data.Models;

namespace Waffles_Club.DataManagment.Interfaces;

public interface IOrderWaffleRepository:IBaseRepository<OrderWaffle>
{
    List<OrderWaffle> GetByOrderId(Guid orderId);
}