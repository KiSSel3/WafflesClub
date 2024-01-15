using DwellEase.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.ViewModels;
using Waffles_Club.Validators;

namespace Waffles_Club.Controllers
{
    public class OrderController : Controller
    {
      private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        private void ValidateViewModel(CartViewModel orderViewModel)
        {
            var validateResult = GeneralValidator.Validate(new OrderViewModelValidator(), orderViewModel);
            if (validateResult.Length != 0)
            {
                throw new Exception(validateResult.ToString());
            }
        }
        [Authorize]
        public async Task<IActionResult> GetOrdersByUserId(string userId)
        {
            try
            {
                var orders=await _orderService.GetOrdersByUserIdAsync(userId);
                return View(orders);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }
        [Authorize]
        public async Task<IActionResult> CreateOrder(List<OrderViewModel> orderViewModels)
        {
            try
            {

            }
        }
    }
}
