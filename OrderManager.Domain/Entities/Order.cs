using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderSequenceNumber { get; set; }

        public string? OrderNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        public DateTime OrderDate { get; set; }

        [Range(0, double.MaxValue)]
        public double TotalAmount { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
