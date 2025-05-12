using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Entities
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemID { get; set; }

        [Required]
        [ForeignKey("Order")]
        public Guid OrderID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public double UnitPrice { get; set; }

        public double TotalPrice { get; set; }

        public Order? Order { get; set; }
    }
}
