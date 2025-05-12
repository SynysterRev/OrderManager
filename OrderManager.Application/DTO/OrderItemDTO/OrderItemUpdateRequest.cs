using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.DTO
{
    public class OrderItemUpdateRequest
    {
        [Required]
        public Guid OrderItemID { get; set; }

        [Required]
        public Guid OrderID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double UnitPrice { get; set; }

        [Required]
        public double TotalPrice { get; set; }
    }
}
