using Waffles_Club.Data.Entity;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Service.Services.Interfaces;

public interface IUserService
{
    Task CreateAsync(RegisterViewModel viewModel);
    Task<User> GetById(string userId);
    Task<List<User>> GetAll();
    Task AddToRoleAsync(string userId, string roleName);
    Task DeleteAsync(string userId);
    Task UpdateAsync(string userId, UpdateUserViewModel viewModel);
    Task UpdatePassword(string userId, UpdatePasswordViewModel viewModel);
    Task<User> GetByLogin(string login);
}