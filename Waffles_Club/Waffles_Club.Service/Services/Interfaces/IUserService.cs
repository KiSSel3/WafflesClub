using Waffles_Club.Data.Entity;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Service.Services.Interfaces;

public interface IUserService
{
    Task CreateAsync(RegisterViewModel viewModel);
    Task<User> GetById(string id);
    Task<List<User>> GetAll();
    Task AddToRoleAsync(string userId, Guid roleId);
    Task DeleteAsync(string userId);
    Task UpdateAsync(string id, UpdateUserViewModel viewModel);
    Task UpdatePassword(string id, string newPassword);
    Task GetByLogin(string login);
}