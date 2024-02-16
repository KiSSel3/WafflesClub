using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.Enum;
using Waffles_Club.Data.Models;
using Waffles_Club.DataManagment.Implementations;
using Waffles_Club.DataManagment.Interfaces;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.Mappers;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Service.Services.Implementations
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderWaffleRepository _orderWaffleRepository;
		private readonly IWaffleRepository _waffleRepository;
		private readonly ICartRepository _cartRepository;
		private readonly StringToGuidMapper _guidMapper;
		private readonly IUserService _userService;

		public OrderService(IOrderRepository orderRepository, IOrderWaffleRepository orderWaffleRepository, IWaffleRepository waffleRepository, ICartRepository cartRepository, StringToGuidMapper guidMapper, IUserService userService)
		{
			_userService = userService;
			(_orderRepository, _orderWaffleRepository, _waffleRepository, _cartRepository, _guidMapper) = (
				orderRepository, orderWaffleRepository, waffleRepository, cartRepository, guidMapper);
		}

		public async Task<OrderWaffle> AddWaffleToOrder(Guid orderId, Guid waffleId)
		{
			var orderById = await _orderRepository.GetById(orderId);
			if (orderById == null)
			{
				throw new Exception("No order");
			}

			var waffleById = await _waffleRepository.GetById(waffleId);
			if (waffleById == null)
			{
				throw new Exception("No waffle");
			}

			var newOrderWaffle = new OrderWaffle()
			{
				OrderId = orderId,
				WaffleId = waffleId
			};

			await _orderWaffleRepository.Create(newOrderWaffle);

			return newOrderWaffle;
		}

		public async Task<Order> ChangeStatusByOrderIdAsync(Guid orderId, OrderStatus newStatus)
		{
			var orderById = await _orderRepository.GetById(orderId);
			if (orderById == null)
			{
				throw new Exception("No order");
			}

			orderById.OrderStatus = newStatus;

			var newOrder=await _orderRepository.Update(orderById);

			return newOrder;
		}

		public async Task<Order> DeleteOrderByIdAsync(Guid orderId)
		{
			var orderById = await _orderRepository.GetById(orderId);
			if (orderById == null)
			{
				throw new Exception("No order");
			}

			await _orderRepository.Delete(orderById);

			return orderById;
		}

		public async Task<List<Order>> GetAllOrdersAsync()
		{
			var orders = await _orderRepository.GetAll();

			return orders;
		}

		public async Task<Order> GetOrderByIdAsync(Guid orderId)
		{
			var orderById = await _orderRepository.GetById(orderId);
			if (orderById == null)
			{
				throw new Exception("No order");
			}

			return orderById;
		}

		public async Task<List<Order>> GetOrdersByStatusAsync(OrderStatus status)
		{
			var allOrders = await _orderRepository.GetAll();

			var statusOrders = allOrders.Where(o => o.OrderStatus.Equals(status)).ToList();

			return statusOrders;
		}

        public async Task<List<OrderListViewModel>> GetOrdersByUserIdAsync(string userId)
        {
            var guidUserId = _guidMapper.MapTo(userId);

            var ordersByUser = await _orderRepository.GetByUserId(guidUserId);
            List<OrderListViewModel> orderListViewModels = new List<OrderListViewModel>();

            foreach (var order in ordersByUser)
            {
                var orderWaffles = await _orderWaffleRepository.GetByOrderId(order.Id);

                var cartsListViewModels = new List<CartsListViewModel>();
                foreach (var orderWaffle in orderWaffles)
                {
                    var waffle = await _waffleRepository.GetById(orderWaffle.WaffleId);
                    var cartsListViewModel = new CartsListViewModel
                    {
                        Waffle = waffle,
                        Count = orderWaffle.Count
                    };
                    cartsListViewModels.Add(cartsListViewModel);
                }

                var orderListViewModel = new OrderListViewModel
                {
					Id= order.Id,
                    Date = order.Date,
                    Carts = cartsListViewModels.ToList(),
                    Status = order.OrderStatus
                };

                orderListViewModels.Add(orderListViewModel);
            }

            return orderListViewModels;
        }
        public async Task<List<OrderListViewModel>> GetOrderViewModelsAsync()
        {

            var orders=await _orderRepository.GetAll();
            List<OrderListViewModel> orderListViewModels = new List<OrderListViewModel>();

            foreach (var order in orders)
            {
                var orderWaffles = await _orderWaffleRepository.GetByOrderId(order.Id);

                var cartsListViewModels = new List<CartsListViewModel>();
                foreach (var orderWaffle in orderWaffles)
                {
                    var waffle = await _waffleRepository.GetById(orderWaffle.WaffleId);
                    var cartsListViewModel = new CartsListViewModel
                    {
                        Waffle = waffle,
                        Count = orderWaffle.Count
                    };
                    cartsListViewModels.Add(cartsListViewModel);
                }

                var orderListViewModel = new OrderListViewModel
                {
                    Id = order.Id,
                    UserName=(await _userService.GetById(order.UserId.ToString())).Name,
                    Date = order.Date,
                    Carts = cartsListViewModels.ToList(),
                    Status = order.OrderStatus
                };

                orderListViewModels.Add(orderListViewModel);
            }

            return orderListViewModels;
        }
        public async Task<OrderListViewModel> GetOrderViewModelByIdAsync(Guid orderId)
		{
			var order = await _orderRepository.GetById(orderId);
			var orderWaffles = await _orderWaffleRepository.GetByOrderId(orderId);

			var cartsListViewModels = new List<CartsListViewModel>();
			foreach (var orderWaffle in orderWaffles)
			{
				var waffle = await _waffleRepository.GetById(orderWaffle.WaffleId);
				var cartsListViewModel = new CartsListViewModel
				{
					Waffle = waffle,
					Count = orderWaffle.Count
				};
				cartsListViewModels.Add(cartsListViewModel);
			}

			var orderListViewModel = new OrderListViewModel
			{
				Id = orderId,
				Date = order.Date,
				Carts = cartsListViewModels.ToList(),
				Status = order.OrderStatus
			};

			return orderListViewModel;
		}


        public async Task<List<Waffle>> GetWafflesByOrderIdAsync(Guid orderId)
		{
			var waffles = new List<Waffle>();

			var orderWaffles = await _orderWaffleRepository.GetByOrderId(orderId);

			foreach(var item in orderWaffles)
			{
				var waffleById = await _waffleRepository.GetById(item.WaffleId);
				if(waffleById == null)
				{
					throw new Exception("Error when receiving goods from an order");
				}


				waffles.Add(waffleById);
			}

			return waffles;
		}
		public async Task CreateOrder(string userId, List<OrderViewModel> orderViewModels)
		{
            var guidUserId = _guidMapper.MapTo(userId);
			var currentDate= DateTime.Now;
			var order = new Order
			{
				UserId = guidUserId,
				Date = currentDate,
				OrderStatus = OrderStatus.Processing
			};
			await _orderRepository.Create(order);
			var ordersByUser=await _orderRepository.GetByUserId(guidUserId);
			var newOrder=ordersByUser.FirstOrDefault(order=>order.Date==currentDate);
            foreach (var orderViewModel in orderViewModels)
            {
                var orderWaffle = new OrderWaffle
                {
                    OrderId = newOrder.Id,
                    WaffleId = orderViewModel.WaffleId,
                    Count = orderViewModel.Count
				};

                await _orderWaffleRepository.Create(orderWaffle);
            }

			await _cartRepository.DeleteByUserId(guidUserId);
        }
        public async Task<List<OrderListViewModel>> GetAllOrdersViewModelsAsync()
        {
            var orders = await _orderRepository.GetAll();
            List<OrderListViewModel> orderListViewModels = new List<OrderListViewModel>();

            foreach (var order in orders)
            {
                var orderWaffles = await _orderWaffleRepository.GetByOrderId(order.Id);

                var cartsListViewModels = new List<CartsListViewModel>();
                foreach (var orderWaffle in orderWaffles)
                {
                    var waffle = await _waffleRepository.GetById(orderWaffle.WaffleId);
                    var cartsListViewModel = new CartsListViewModel
                    {
                        Waffle = waffle,
                        Count = orderWaffle.Count
                    };
                    cartsListViewModels.Add(cartsListViewModel);
                }

                var orderListViewModel = new OrderListViewModel
                {
                    Id = order.Id,
                    Date = order.Date,
                    Carts = cartsListViewModels.ToList(),
                    Status = order.OrderStatus
                };

                orderListViewModels.Add(orderListViewModel);
            }

            return orderListViewModels;
        }
    }
    
}
