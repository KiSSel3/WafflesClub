using Waffles_Club.Data.Entity;

namespace Waffles_Club.DataManagment.Interfaces;

public interface IFillingTypeRepository:IBaseRepository<FillingType>
{
    FillingType GetByNormalizedName(string normalizedName);
}