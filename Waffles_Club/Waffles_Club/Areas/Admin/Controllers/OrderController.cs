using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.Enum;
using Waffles_Club.Service.Services.Implementations;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangeOrderStatus(OrderStatus newStatus,Guid orderId)
        {
            try
            {
                var order =await _orderService.ChangeStatusByOrderIdAsync(orderId,newStatus);
                var orders=await _orderService.GetAllOrdersViewModelsAsync();
                return View("Orders",orders);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
            
        }
        [Authorize]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersViewModelsAsync();
                return View("Orders", orders);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }
        [Authorize]
        public async Task<IActionResult> Delete(Guid orderId)
        {
            try
            {
                await _orderService.DeleteOrderByIdAsync(orderId);
                var orders = await _orderService.GetAllOrdersViewModelsAsync();
                return View("Orders", orders);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }
    }
}
