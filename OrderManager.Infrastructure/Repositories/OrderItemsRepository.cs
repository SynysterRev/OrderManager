using Microsoft.EntityFrameworkCore;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Repositories;
using OrderManager.Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Infrastructure.Repositories
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderItemsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderItem> AddOrderItem(OrderItem orderItem)
        {
            _dbContext.OrderItems.Add(orderItem);
            await _dbContext.SaveChangesAsync();
            return orderItem;
        }

        public async Task<bool> DeleteOrderItem(Guid orderItemID)
        {
            OrderItem? orderItem = await _dbContext.OrderItems.FindAsync(orderItemID);
            if (orderItem == null)
            {
                return false;
            }
            _dbContext.OrderItems.Remove(orderItem);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<OrderItem>> GetAllOrderItems()
        {
            return await _dbContext.OrderItems.Include("Order").OrderByDescending(x => x.ProductName).ToListAsync();
        }

        public async Task<List<OrderItem>> GetAllOrderItemsByOrderID(Guid orderID)
        {
            return await _dbContext.OrderItems.Include("Order").Where(x => x.OrderID == orderID).ToListAsync();
        }

        public async Task<OrderItem?> GetOrderItemByGuid(Guid orderItemID)
        {
            return await _dbContext.OrderItems.Include("Order").FirstOrDefaultAsync(x => x.OrderID == orderItemID);
        }

        public async Task<OrderItem> UpdateOrderItem(OrderItem orderItem)
        {
            OrderItem? foundOrderItem = await _dbContext.OrderItems.FindAsync(orderItem.OrderID);
            if (foundOrderItem == null)
            {
                return orderItem;
            }
            foundOrderItem.UnitPrice = orderItem.UnitPrice;
            foundOrderItem.OrderID = orderItem.OrderID;
            foundOrderItem.ProductName = orderItem.ProductName;
            foundOrderItem.Quantity = orderItem.Quantity;
            foundOrderItem.TotalPrice = orderItem.TotalPrice;

            await _dbContext.SaveChangesAsync();

            return foundOrderItem;
        }
    }
}
