using Waffles_Club.Data.Entity;

namespace Waffles_Club.DataManagment.Interfaces;

public interface IWaffleRepository:IBaseRepository<Waffle>
{
    Task<Waffle> GetByName(string name);
    Task<Waffle> GetById(Guid id);
}