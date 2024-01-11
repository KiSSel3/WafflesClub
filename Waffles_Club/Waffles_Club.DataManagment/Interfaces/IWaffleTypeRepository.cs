using Waffles_Club.Data.Entity;

namespace Waffles_Club.DataManagment.Interfaces;

public interface IWaffleTypeRepository
{
    WaffleType GetByNormalizedName(string normalizedName);
}