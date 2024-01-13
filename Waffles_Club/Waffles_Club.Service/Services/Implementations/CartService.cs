using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Waffles_Club.Data.Entity;
using Waffles_Club.Shared.ViewModels;
using Waffles_Club.DataManagment.Interfaces;
using Waffles_Club.Shared.Mappers;

namespace Waffles_Club.Service.Services.Implementations;

public class CartService
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

    public async Task<Cart> GetByUserId(string userId)
    {
        var guidUserId = _guidMapper.MapTo(userId);
        var cart= await _cartRepository.GetByUserId(guidUserId);
        if (cart==null)
        {
            var message = $"Cart with user id: {guidUserId} not found";
            _logger.LogError(message);
            throw new Exception(message);
        }
        
        return cart;
    }

    public async Task<Cart> GetById(string id)
    {
        var guidId = _guidMapper.MapTo(id);
        var cart= await _cartRepository.GetById(guidId);
        if (cart==null)
        {
            var message = $"Cart with id: {guidId} not found";
            _logger.LogError(message);
            throw new Exception(message);
        }
        
        return cart;
    }

    public async Task Create(CartViewModel cartViewModel)
    {
        var cart = new Cart()
        {
            UserId = _guidMapper.MapTo(cartViewModel.UserId),
            WaffleId = cartViewModel.WaffleId
        };
        await _cartRepository.Create(cart);
    }
}