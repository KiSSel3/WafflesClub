using Waffles_Club.Data.Models;

namespace Waffles_Club.DataManagment.Interfaces;

public interface IRoleUserRepository:IBaseRepository<RoleUser>
{
    List<RoleUser> GetByUserId(Guid userId);
    List<RoleUser> GetByRoleId(Guid roleId);
}