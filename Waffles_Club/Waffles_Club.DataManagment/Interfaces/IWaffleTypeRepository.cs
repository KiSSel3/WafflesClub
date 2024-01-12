using Waffles_Club.Data.Entity;

namespace Waffles_Club.DataManagment.Interfaces;

public interface IWaffleTypeRepository:IBaseRepository<WaffleType>
{
    Task<WaffleType> GetByNormalizedName(string normalizedName);
}