using Microsoft.EntityFrameworkCore;
using OrderManager.Domain.Entities;
using OrderManager.Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Infrastructure.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrdersRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> AddOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrder(Guid orderID)
        {
            Order? order = await _dbContext.Orders.FindAsync(orderID);
            if (order == null)
            {
                return false;
            }
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _dbContext.Orders.Include("OrderItems").OrderByDescending(x => x.OrderDate).ToListAsync();
        }

        public async Task<Order?> GetOrderByGuid(Guid orderID)
        {
            return await _dbContext.Orders.Include("OrderItems").FirstOrDefaultAsync(x => x.OrderID == orderID);
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            Order? foundOrder = await _dbContext.Orders.FindAsync(order.OrderID);
            if (foundOrder == null)
            {
                return order;
            }

            _dbContext.Entry(foundOrder).CurrentValues.SetValues(order);

            await _dbContext.SaveChangesAsync();

            return foundOrder;
        }
    }
}
