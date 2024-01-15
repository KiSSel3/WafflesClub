using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Waffles_Club.Data.Entity;
using Waffles_Club.Shared.ViewModels;
using Waffles_Club.DataManagment.Interfaces;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.Mappers;
using System.Web.Helpers;

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

    public async Task<List<Cart>> GetByUserIdAsync(string userId)
    {
        var guidUserId = _guidMapper.MapTo(userId);
        var carts= await _cartRepository.GetByUserId(guidUserId);
        if (carts.Count==0)
        {
            HandleError($"Cart with user id: {guidUserId} not found");
        }
        return carts;
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
    public async Task<List<Cart>> DeleteFromCartAsync(CartViewModel cartViewModel)
    {
        var guidUserId = _guidMapper.MapTo(cartViewModel.UserId);
        var cartsByUser = await _cartRepository.GetByUserId(guidUserId);
        bool isWaffleInCart = cartsByUser.Any(a => a.WaffleId == cartViewModel.WaffleId);
        if (!isWaffleInCart) 
        {
            HandleError("No waffle in cart");
        }
        var cart = cartsByUser.FirstOrDefault(cart => cart.WaffleId == cartViewModel.WaffleId && cart.UserId == guidUserId);
        cart.Count--;
        await _cartRepository.Update(cart);
        return cartsByUser;
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

    public async Task<List<Cart>> AddToCartAsync(CartViewModel cartViewModel)
    {
        var guidUserId = _guidMapper.MapTo(cartViewModel.UserId);
        var cartsByUser=await _cartRepository.GetByUserId(guidUserId);
        bool isWaffleInCart = cartsByUser.Any(a => a.WaffleId == cartViewModel.WaffleId);
        if (!isWaffleInCart) 
        {
            var newCart = new Cart()
            {
                UserId = guidUserId,
                WaffleId = cartViewModel.WaffleId,
                Count = 1
            };
            await _cartRepository.Create(newCart);
            return new List<Cart> { newCart };
        }
        var cart=cartsByUser.FirstOrDefault(cart=>cart.WaffleId==cartViewModel.WaffleId&&cart.UserId==guidUserId);
        cart.Count++;
        await _cartRepository.Update(cart);
        return cartsByUser;

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
        await _cartRepository.Update(cart);
    }
}