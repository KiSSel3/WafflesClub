using Waffles_Club.Data.Entity;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Service.Services.Interfaces;

public interface ICartService
{
    Task<List<Cart>> GetAllAsync();
    Task CreateAsync(CartViewModel viewModel);
    Task UpdateAsync(Guid cartId, CartViewModel viewModel);
    Task DeleteAsync(Guid cartId);
    Task<Cart> GetByIdAsync(Guid cartId);
    Task<Cart> GetByUserIdAsync(string userId);
}