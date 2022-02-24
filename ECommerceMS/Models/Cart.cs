using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceMS.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime AddedAt { get; set; }

        [ForeignKey("Order")]
        public int? OrderNum { get; set; }
        public Order Order { get; set; }

        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<ProductCarts> ProductCarts { get; set; }
        public Cart()
        {
            ProductCarts = new List<ProductCarts>();
        }
    }
}
