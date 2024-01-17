using DwellEase.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Waffles_Club.Data.Entity;
using Waffles_Club.Service.Services.Implementations;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.ViewModels;
using Waffles_Club.Validators;

namespace Waffles_Club.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public OrderController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }
        private void ValidateViewModel(OrderViewModel orderViewModel)
        {
            var validateResult = GeneralValidator.Validate(new OrderViewModelValidator(), orderViewModel);
            if (validateResult.Length != 0)
            {
                throw new Exception(validateResult.ToString());
            }
        }
        [Authorize]
        public async Task<IActionResult> GetOrdersByUserId()
        {
            try
            {
                var userId = User.FindFirst("UserId")?.Value;
                var orders=await _orderService.GetOrdersByUserIdAsync(userId);
                return View(orders);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }
        [Authorize]
        public async Task<IActionResult> CreateOrder()
        {
            try
            {
                var userId = User.FindFirst("UserId")?.Value;

                var carts = await _cartService.GetByUserIdAsync(userId);

                List<OrderViewModel> orderViewModels = new List<OrderViewModel>();
                foreach(var cart in carts)
                {
                    var orderViewModel = new OrderViewModel
                    {
                        Count = cart.Count,
                        WaffleId = cart.WaffleId
                    };

                    orderViewModels.Add(orderViewModel);
                }

                await _orderService.CreateOrder(userId, orderViewModels);
                return Redirect("/");

            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }
    }
}
