using Waffles_Club.Data.Models;

namespace Waffles_Club.DataManagment.Interfaces;

public interface IRoleUserRepository:IBaseRepository<RoleUser>
{
    Task<List<RoleUser>> GetByUserId(Guid userId);
    Task<List<RoleUser>> GetByRoleId(Guid roleId);
}