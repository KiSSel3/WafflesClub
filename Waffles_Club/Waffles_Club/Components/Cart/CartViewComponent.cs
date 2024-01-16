using Microsoft.AspNetCore.Mvc;

using Waffles_Club.Service.Services.Interfaces;

namespace Waffles_Club.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;
        
        public CartViewComponent(ICartService cartService) =>
            _cartService = cartService;

        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            try
            {
                var cart = await _cartService.GetByUserIdAsync(userId);
                return View("CartView", cart.Count());
            }
            catch
            {
                return View("CartView", 0);
            }
        }
    }
}
