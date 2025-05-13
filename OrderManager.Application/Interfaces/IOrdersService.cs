using OrderManager.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Interfaces
{
    public interface IOrdersService
    {
        /// <summary>
        /// Add a new order to the database
        /// </summary>
        /// <param name="order">The order to add</param>
        /// <returns>The added order</returns>
        Task<OrderResponse> AddOrder(OrderAddRequest orderRequest);

        /// <summary>
        /// Update the specified order
        /// </summary>
        /// <param name="orderUpdate">The order to update</param>
        /// <returns>The updated orderResponse</returns>
        Task<OrderResponse> UpdateOrder(OrderUpdateRequest orderUpdate);

        /// <summary>
        /// Update partially the specified order
        /// </summary>
        /// <param name="orderUpdate">The order to update</param>
        /// <returns>The updated orderResponse</returns>
        Task<OrderResponse> PartiallyUpdateOrder(OrderPartialUpdateRequest orderPartialUpdate);

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns>A list of all orders</returns>
        public Task<List<OrderResponse>> GetAllOrders();

        /// <summary>
        /// Get order matching with ID if one 
        /// </summary>
        /// <param name="orderID">The Guid of the wanted order</param>
        /// <returns>An order if any matching</returns>
        public Task<OrderResponse?> GetOrderByGuid(Guid orderID);

        /// <summary>
        /// Delete the order based on the given guid
        /// </summary>
        /// <param name="orderId">The guid of the order to delete</param>
        /// <returns>True if deletion is successful, false otherwise</returns>
        Task<bool> DeleteOrder(Guid orderId);
    }
}
