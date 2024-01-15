using Waffles_Club.Data.Entity;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Service.Services.Interfaces;

public interface ICartService
{
    Task<List<Cart>> GetAllAsync();
    Task UpdateAsync(Guid cartId, CartViewModel viewModel);
    Task DeleteAsync(Guid cartId);
    Task<Cart> GetByIdAsync(Guid cartId);
    Task<List<Cart>> GetByUserIdAsync(string userId);
    Task<List<Cart>> AddToCartAsync(CartViewModel cartViewModel);
    Task<List<Cart>> DeleteFromCartAsync(CartViewModel cartViewModel);
}