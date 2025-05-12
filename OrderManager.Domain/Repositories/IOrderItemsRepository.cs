using OrderManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Repositories
{
    public interface IOrderItemsRepository
    {
        /// <summary>
        /// Get all order items stocked in the database
        /// </summary>
        /// <returns>An list of all order items</returns>
        public Task<List<OrderItem>> GetAllOrderItems();

        /// <summary>
        /// Get all order items for a giving order
        /// </summary>
        /// <returns>An list of all order items</returns>
        public Task<List<OrderItem>> GetAllOrderItemsByOrderID(Guid orderID);

        /// <summary>
        /// Get order item matching with ID if one 
        /// </summary>
        /// <param name="orderItemID">The Guid of the wanted order item</param>
        /// <returns>An order item if any matching</returns>
        public Task<OrderItem?> GetOrderItemByGuid(Guid orderItemID);

        /// <summary>
        /// Add a new order item in the database
        /// </summary>
        /// <param name="orderItem">Order item to add</param>
        /// <returns>The added order item</returns>
        public Task<OrderItem> AddOrderItem(OrderItem orderItem);

        /// <summary>
        /// Update the order item with same ID
        /// </summary>
        /// <param name="orderItem">The order item to update</param>
        /// <returns>The updated order item</returns>
        public Task<OrderItem> UpdateOrderItem(OrderItem orderItem);

        /// <summary>
        /// Delete the order item matching with the ID
        /// </summary>
        /// <param name="orderItemID">The ID of the order item to delete</param>
        /// <returns>True if deletion complete, otherwise false</returns>
        public Task<bool> DeleteOrderItem(Guid orderItemID);
    }
}
