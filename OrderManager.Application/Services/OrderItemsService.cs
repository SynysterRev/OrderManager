using OrderManager.Application.DTO;
using OrderManager.Application.Exceptions;
using OrderManager.Application.Helpers;
using OrderManager.Application.Interfaces;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Repositories;
using OrderManager.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Services
{
    public class OrderItemsService : IOrderItemsService
    {
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly IOrdersRepository _ordersRepository;

        public OrderItemsService(IOrderItemsRepository repository, IOrdersRepository ordersRepository)
        {
            _orderItemsRepository = repository;
            _ordersRepository = ordersRepository;
        }

        public async Task<OrderItemResponse> AddOrderItem(OrderItemAddRequest orderItem)
        {
            ValidationHelper.ModelValidation(orderItem);

            OrderItem newOrderItem = orderItem.ToOrderItem();
            newOrderItem.OrderItemID = new Guid();

            await _orderItemsRepository.AddOrderItem(newOrderItem);

            return newOrderItem.ToOrderItemResponse();
        }

        public async Task<OrderItemResponse> PartiallyUpdateOrderItem(OrderItemPartialUpdateRequest orderItemRequest)
        {
            ValidationHelper.ModelValidation(orderItemRequest);

            OrderItem? existingOrderItem = await _orderItemsRepository.GetOrderItemByGuid(orderItemRequest.OrderItemID);

            if (existingOrderItem == null)
            {
                throw new NotFoundException(nameof(OrderItem), orderItemRequest.OrderItemID);
            }

            if (orderItemRequest.OrderID.HasValue)
                existingOrderItem.OrderID = orderItemRequest.OrderID.Value;

            if (orderItemRequest.UnitPrice.HasValue)
                existingOrderItem.UnitPrice = orderItemRequest.UnitPrice.Value;

            if (orderItemRequest.TotalPrice.HasValue)
                existingOrderItem.TotalPrice = orderItemRequest.TotalPrice.Value;

            if (orderItemRequest.Quantity.HasValue)
                existingOrderItem.Quantity = orderItemRequest.Quantity.Value;

            if (orderItemRequest.ProductName != null)
                existingOrderItem.ProductName = orderItemRequest.ProductName;

            await _orderItemsRepository.UpdateOrderItem(existingOrderItem);

            return existingOrderItem.ToOrderItemResponse();
        }

        public async Task<OrderItemResponse> UpdateOrderItem(OrderItemUpdateRequest orderItemRequest)
        {
            ValidationHelper.ModelValidation(orderItemRequest);

            OrderItem? existingOrderItem = await _orderItemsRepository.GetOrderItemByGuid(orderItemRequest.OrderItemID);

            if (existingOrderItem == null)
            {
                throw new NotFoundException(nameof(OrderItem), orderItemRequest.OrderItemID);
            }
            existingOrderItem.OrderID = orderItemRequest.OrderID;
            existingOrderItem.UnitPrice = orderItemRequest.UnitPrice;
            existingOrderItem.TotalPrice = orderItemRequest.TotalPrice;
            existingOrderItem.Quantity = orderItemRequest.Quantity;
            existingOrderItem.ProductName = orderItemRequest.ProductName;

            await _orderItemsRepository.UpdateOrderItem(existingOrderItem);

            return existingOrderItem.ToOrderItemResponse();
        }

        public async Task<List<OrderItemResponse>> GetAllOrderItems()
        {
            var orderItems = await _orderItemsRepository.GetAllOrderItems();
            return orderItems.Select(x => x.ToOrderItemResponse()).ToList();
        }

        public async Task<List<OrderItemResponse>> GetAllOrderItemsByOrderID(Guid orderID)
        {
            Order? order = await _ordersRepository.GetOrderByGuid(orderID);
            if (order == null)
            {
                throw new NotFoundException(nameof(Order), orderID);
            }

            return order.OrderItems.Select(x => x.ToOrderItemResponse()).ToList();
        }

        public async Task<OrderItemResponse?> GetOrderItemByGuid(Guid orderItemID)
        {
            OrderItem? orderItem = await _orderItemsRepository.GetOrderItemByGuid(orderItemID);
            return orderItem?.ToOrderItemResponse();
        }

        public async Task<bool> DeleteOrderItem(Guid orderItemID)
        {
            OrderItem? orderItem = await _orderItemsRepository.GetOrderItemByGuid(orderItemID);
            if (orderItem == null)
            {
                throw new NotFoundException(nameof(OrderItem), orderItemID);
            }

            return await _orderItemsRepository.DeleteOrderItem(orderItemID);
        }
    }
}
