using Waffles_Club.Data.Entity;
using Waffles_Club.Data.Models;

namespace Waffles_Club.DataManagment.Interfaces;

public interface IRoleRepository:IBaseRepository<Role>
{
    Task<Role> GetByNormalizedName(string normalizedName);
}