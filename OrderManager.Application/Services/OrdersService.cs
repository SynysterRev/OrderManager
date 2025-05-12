using OrderManager.Application.DTO;
using OrderManager.Application.Exceptions;
using OrderManager.Application.Helpers;
using OrderManager.Application.Interfaces;
using OrderManager.Domain.Entities;
using OrderManager.Infrastructure.Repositories;

namespace OrderManager.Application.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<OrderResponse> AddOrder(OrderAddRequest orderRequest)
        {
            ValidationHelper.ModelValidation(orderRequest);

            Order newOrder = orderRequest.ToOrder();
            newOrder.OrderID = new Guid();

            await _ordersRepository.AddOrder(newOrder);

            newOrder.OrderNumber = GenerateOrderNumber(newOrder);

            await _ordersRepository.UpdateOrder(newOrder);

            return newOrder.ToOrderResponse();
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            Order? existingOrder = await _ordersRepository.GetOrderByGuid(orderId);

            if (existingOrder == null)
            {
                throw new NotFoundException(nameof(Order), orderId);
            }

            return await _ordersRepository.DeleteOrder(orderId);
        }

        public async Task<List<OrderResponse>> GetAllOrders()
        {
            var orders = await _ordersRepository.GetAllOrders();
            return orders.Select(x => x.ToOrderResponse()).ToList();
        }

        public async Task<OrderResponse?> GetOrderByGuid(Guid orderID)
        {
            Order? order = await _ordersRepository.GetOrderByGuid(orderID);
            return order?.ToOrderResponse();
        }

        public async Task<OrderResponse> PartiallyUpdateOrder(OrderPartialUpdateRequest orderPartialUpdate)
        {
            Order? existingOrder = await _ordersRepository.GetOrderByGuid(orderPartialUpdate.OrderID);

            if (existingOrder == null)
            {
                throw new NotFoundException(nameof(Order), orderPartialUpdate.OrderID);
            }

            if (orderPartialUpdate.OrderDate.HasValue)
                existingOrder.OrderDate = orderPartialUpdate.OrderDate.Value;

            if (orderPartialUpdate.CustomerName != null)
                existingOrder.CustomerName = orderPartialUpdate.CustomerName;

            if (orderPartialUpdate.TotalAmount.HasValue)
                existingOrder.TotalAmount = orderPartialUpdate.TotalAmount.Value;

            await _ordersRepository.UpdateOrder(existingOrder);

            return existingOrder.ToOrderResponse();
        }

        public async Task<OrderResponse> UpdateOrder(OrderUpdateRequest orderUpdate)
        {
            ValidationHelper.ModelValidation(orderUpdate);

            Order? existingOrder = await _ordersRepository.GetOrderByGuid(orderUpdate.OrderID);

            if (existingOrder == null)
            {
                throw new NotFoundException(nameof(Order), orderUpdate.OrderID);
            }

            existingOrder.OrderDate = orderUpdate.OrderDate;
            existingOrder.CustomerName = orderUpdate.CustomerName;
            existingOrder.TotalAmount = orderUpdate.TotalAmount;

            await _ordersRepository.UpdateOrder(existingOrder);

            return existingOrder.ToOrderResponse();
        }

        private string GenerateOrderNumber(Order order)
        {
            int year = order.OrderDate.Year;
            return $"ORD_{year}_{order.OrderSequenceNumber:D4}";
        }
    }
}
