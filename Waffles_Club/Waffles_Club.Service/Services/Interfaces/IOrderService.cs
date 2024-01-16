using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.Enum;
using Waffles_Club.Data.Models;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Service.Services.Interfaces
{
	public interface IOrderService
	{
		public Task<List<Order>> GetAllOrdersAsync();
		Task<List<OrderListViewModel>> GetOrdersByUserIdAsync(string userId);
        public Task<List<Order>> GetOrdersByStatusAsync(OrderStatus status);
		public Task<List<Waffle>> GetWafflesByOrderIdAsync(Guid orderId);
		public Task<Order> GetOrderByIdAsync(Guid orderId);
		public Task<Order> DeleteOrderByIdAsync(Guid orderId);
		public Task<Order> ChangeStatusByOrderIdAsync(Guid orderId, OrderStatus newStatus);
		public Task<OrderWaffle> AddWaffleToOrder(Guid orderId, Guid waffleId);
		public Task CreateOrder(string userId, List<OrderViewModel> orderViewModels);

    }
}
