using OrderManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.DTO
{
    public class OrderAddRequest
    {
        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        public DateTime OrderDate { get; set; }

        [Range(0, double.MaxValue)]
        public double TotalAmount { get; set; }

        public Order ToOrder()
        {
            return new Order
            {
                CustomerName = CustomerName,
                OrderDate = OrderDate,
                TotalAmount = TotalAmount
            };
        }
    }
}
