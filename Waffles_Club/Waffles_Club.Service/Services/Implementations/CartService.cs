using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Waffles_Club.Data.Entity;
using Waffles_Club.Shared.ViewModels;
using Waffles_Club.DataManagment.Interfaces;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.Mappers;

namespace Waffles_Club.Service.Services.Implementations;

public class CartService:ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly ILogger<CartService> _logger;
    private readonly StringToGuidMapper _guidMapper;

    public CartService(ICartRepository cartRepository, ILogger<CartService> logger, StringToGuidMapper guidMapper)
    {
        _cartRepository = cartRepository;
        _logger = logger;
        _guidMapper = guidMapper;
    }

    private void HandleError(string message)
    {
        _logger.LogError(message);
        throw new Exception(message);
    }

    public async Task<Cart> GetByUserIdAsync(string userId)
    {
        var guidUserId = _guidMapper.MapTo(userId);
        var cart= await _cartRepository.GetByUserId(guidUserId);
        if (cart==null)
        {
            HandleError($"Cart with user id: {guidUserId} not found");
        }
        return cart;
    }

    public async Task DeleteAsync(Guid cartId)
    {
        var cart= await _cartRepository.GetById(cartId);
        if (cart==null)
        {
            HandleError($"Cart with id: {cartId} not found");
        }

        await _cartRepository.Delete(cart);
    }

    public async Task<Cart> GetByIdAsync(Guid id)
    {
        var cart= await _cartRepository.GetById(id);
        if (cart==null)
        {
            HandleError($"Cart with id: {id} not found");
        }
        return cart;
    }

    public async Task<List<Cart>> GetAllAsync()
    {
        var carts = await _cartRepository.GetAll();
        if (carts.Count==0)
        {
            HandleError("Carts are not found");
        }

        return carts;
    }

    public async Task CreateAsync(CartViewModel cartViewModel)
    {
        var cart = new Cart()
        {
            UserId = _guidMapper.MapTo(cartViewModel.UserId),
            WaffleId = cartViewModel.WaffleId,
            Count = cartViewModel.Count
        };
        await _cartRepository.Create(cart);
    }

    public async Task UpdateAsync(Guid cartId, CartViewModel viewModel)
    {
        var cart = await _cartRepository.GetById(cartId);
        if (cart==null)
        {
            HandleError($"Cart with id: {cartId} not found");
        }
        cart.WaffleId = viewModel.WaffleId;
        cart.UserId = _guidMapper.MapTo(viewModel.UserId);
        cart.Count = viewModel.Count;
        await _cartRepository.Update(cart);
    }
}