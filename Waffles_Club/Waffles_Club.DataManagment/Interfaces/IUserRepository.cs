using Waffles_Club.Data.Entity;

namespace Waffles_Club.DataManagment.Interfaces;

public interface IUserRepository:IBaseRepository<User>
{
    Task<User> GetByName(string name);
    Task<User> GetByLogin(string login);
}