using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.DTO
{
    public class OrderItemPartialUpdateRequest
    {
        [Required]
        public Guid OrderItemID { get; set; }
        public Guid? OrderID { get; set; }

        [StringLength(50)]
        public string? ProductName { get; set; }

        [Range(0, int.MaxValue)]
        public int? Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public double? UnitPrice { get; set; }
        public double? TotalPrice { get; set; }
    }
}
