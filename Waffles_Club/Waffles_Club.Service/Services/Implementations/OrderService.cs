﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.Enum;
using Waffles_Club.Data.Models;
using Waffles_Club.DataManagment.Interfaces;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.Mappers;

namespace Waffles_Club.Service.Services.Implementations
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderWaffleRepository _orderWaffleRepository;
		private readonly IWaffleRepository _waffleRepository;
		private readonly StringToGuidMapper _guidMapper;

		public OrderService(IOrderRepository orderRepository, IOrderWaffleRepository orderWaffleRepository, IWaffleRepository waffleRepository, StringToGuidMapper guidMapper) =>
			(_orderRepository, _orderWaffleRepository, _waffleRepository, _guidMapper) = (orderRepository, orderWaffleRepository, waffleRepository, guidMapper);

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

			await _orderRepository.Update(orderById);

			return orderById;
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

		public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
		{
			var guidUserId = _guidMapper.MapTo(userId);

			var ordersByUser = await _orderRepository.GetByUserId(guidUserId);

			return ordersByUser;
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
	}
}