using OrderManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Infrastructure.Repositories
{
    public interface IOrdersRepository
    {
        /// <summary>
        /// Get all orders stocked in the database
        /// </summary>
        /// <returns>An list of all orders</returns>
        public Task<List<Order>> GetAllOrders();

        /// <summary>
        /// Get order matching with ID if one 
        /// </summary>
        /// <param name="orderID">The Guid of the wanted order</param>
        /// <returns>An order if any matching</returns>
        public Task<Order?> GetOrderByGuid(Guid orderID);

        /// <summary>
        /// Add a new order in the database
        /// </summary>
        /// <param name="order">Order to add</param>
        /// <returns>The added order</returns>
        public Task<Order> AddOrder(Order order);

        /// <summary>
        /// Update the order with same ID
        /// </summary>
        /// <param name="order">The order to update</param>
        /// <returns>The updated order</returns>
        public Task<Order> UpdateOrder(Order order);

        /// <summary>
        /// Delete the order matching with the ID
        /// </summary>
        /// <param name="orderID">The ID of the order to delete</param>
        /// <returns>True if deletion complete, otherwise false</returns>
        public Task<bool> DeleteOrder(Guid orderID);
    }
}
