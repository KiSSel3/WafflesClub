using Waffles_Club.Data.Entity;

namespace Waffles_Club.DataManagment.Interfaces;

public interface IUserRepository:IBaseRepository<User>
{
    User GetByName(string name);
    User GetByEmail(string email);
}