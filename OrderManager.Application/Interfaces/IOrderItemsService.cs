using OrderManager.Application.DTO;
using OrderManager.Application.Exceptions;
using OrderManager.Application.Helpers;
using OrderManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Interfaces
{
    public interface IOrderItemsService
    {
        /// <summary>
        /// Add a new order item
        /// </summary>
        /// <param name="orderItem">Order item to add</param>
        /// <returns>The added order item</returns>
        public Task<OrderItemResponse> AddOrderItem(OrderItemAddRequest orderItem);

        /// <summary>
        /// Update partially the specified order item
        /// </summary>
        /// <param name="orderItemRequest">The order item to update</param>
        /// <returns>The updated orderItemResponse</returns>
        public Task<OrderItemResponse> PartiallyUpdateOrderItem(OrderItemPartialUpdateRequest orderItemRequest);

        /// <summary>
        /// Update the specified order item
        /// </summary>
        /// <param name="orderItemRequest">The order item to update</param>
        /// <returns>The updated orderItemResponse</returns>
        public Task<OrderItemResponse> UpdateOrderItem(OrderItemUpdateRequest orderItemRequest);

        /// <summary>
        /// Get all order items
        /// </summary>
        /// <returns>A list of all order items</returns>
        public Task<List<OrderItemResponse>> GetAllOrderItems();

        /// <summary>
        /// Get all order items belonging to the order with the given orderID
        /// </summary>
        /// <param name="orderID">The guid of the wanted order</param>
        /// <returns>A list of orderItemResponse</returns>
        public Task<List<OrderItemResponse>> GetAllOrderItemsByOrderID(Guid orderID);

        /// <summary>
        /// Get order item matching with ID, if one 
        /// </summary>
        /// <param name="orderItemID">The Guid of the wanted order item</param>
        /// <returns>An order item if any match</returns>
        public Task<OrderItemResponse?> GetOrderItemByGuid(Guid orderItemID);

        /// <summary>
        /// Delete the order item based on the given guid
        /// </summary>
        /// <param name="orderItemID">The guid of the order item to delete</param>
        /// <returns>True if deletion is successful, false otherwise</returns>
        public Task<bool> DeleteOrderItem(Guid orderItemID);
    }
}
