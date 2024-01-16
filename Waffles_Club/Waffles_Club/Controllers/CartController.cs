using DwellEase.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Waffles_Club.Data.Entity;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.ViewModels;
using Waffles_Club.Validators;

namespace Waffles_Club.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        private void ValidateViewModel(CartViewModel cartViewModel)
        {
            var validateResult = GeneralValidator.Validate(new CartViewModelValidator(), cartViewModel);
            if (validateResult.Length != 0)
            {
                throw new Exception(validateResult.ToString());
            }
        }
        [Authorize]
        public async Task<IActionResult> GetCartByUserId()
        {
            try
            {
                string userId = User.FindFirst("UserId")?.Value;
                var carts = await _cartService.GetByUserIdAsync(userId);
                return View(carts);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
      
        }
        [Authorize]
        public async Task<IActionResult> AddWaffleToCart(Guid waffleId)
        {
            try
            {
                var cartViewModel = new CartViewModel() { UserId = User.FindFirst("UserId")?.Value, WaffleId = waffleId };
                ValidateViewModel(cartViewModel);
                var newCarts = await _cartService.AddToCartAsync(cartViewModel);
                return View(newCarts);

            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }
        [Authorize]
        public async Task<IActionResult> DeleteWaffleFromCart(Guid waffleId)
        {
            try
            {
                var cartViewModel = new CartViewModel() { UserId = User.FindFirst("UserId")?.Value, WaffleId = waffleId };
                ValidateViewModel(cartViewModel);
                var newCarts = await _cartService.DeleteFromCartAsync(cartViewModel);
                return View(newCarts);

            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }
    }
}
