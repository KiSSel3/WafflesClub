using DwellEase.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;
using Waffles_Club.Data.Entity;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.ViewModels;
using Waffles_Club.Validators;

namespace Waffles_Club.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IWaffleService _waffleService;

        public CartController(ICartService cartService, IWaffleService waffleService)
        {
            _cartService = cartService;
            _waffleService = waffleService;
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
                var cartsViewModelList= new List<CartsListViewModel>();
                foreach (var cart in carts) 
                {
                    var waffle=await _waffleService.GetWaffleByIdAsync(cart.WaffleId);
                    var cartViewModel = new CartsListViewModel { Count = cart.Count, Waffle = waffle };
                    cartsViewModelList.Add(cartViewModel);
                }
                return View(cartsViewModelList);
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
                var cartsViewModelList = new List<CartsListViewModel>();
                foreach (var cart in newCarts)
                {
                    var waffle = await _waffleService.GetWaffleByIdAsync(cart.WaffleId);
                    var cartsViewModel = new CartsListViewModel { Count = cart.Count, Waffle = waffle };
                    cartsViewModelList.Add(cartsViewModel);
                }
                return View(cartsViewModelList);

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
                var cartsViewModelList = new List<CartsListViewModel>();
                foreach (var cart in newCarts)
                {
                    var waffle = await _waffleService.GetWaffleByIdAsync(cart.WaffleId);
                    var cartsViewModel = new CartsListViewModel { Count = cart.Count, Waffle = waffle };
                    cartsViewModelList.Add(cartsViewModel);
                }
                return View(cartsViewModelList);

            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }
    }
}
