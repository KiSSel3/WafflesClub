using Waffles_Club.Data.Entity;

namespace Waffles_Club.DataManagment.Interfaces;

public interface IFillingTypeRepository:IBaseRepository<FillingType>
{
    Task<FillingType> GetByNormalizedName(string normalizedName);
}