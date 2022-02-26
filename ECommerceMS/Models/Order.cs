using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceMS.Models
{
    public class Order
    {
        [Key]
        public int OrderNum { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }

        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<ProductOrders> ProductOrders { get; set; }
        public Order()
        {
            ProductOrders = new List<ProductOrders>();
        }
    }
}
