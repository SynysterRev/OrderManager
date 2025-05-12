using OrderManager.Application.DTO;
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
    }
}
