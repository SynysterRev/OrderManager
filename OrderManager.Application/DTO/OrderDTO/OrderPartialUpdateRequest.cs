using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.DTO
{
    public class OrderPartialUpdateRequest
    {
        [Required]
        public Guid OrderID { get; set; }

        [StringLength(50)]
        public string? CustomerName { get; set; }
        public DateTime? OrderDate { get; set; }

        [Range(0, double.MaxValue)]
        public double? TotalAmount { get; set; }
    }
}
