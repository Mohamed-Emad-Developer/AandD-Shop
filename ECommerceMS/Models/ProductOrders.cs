using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceMS.Models
{
    public class ProductOrders
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("Order")]
        public int OrderNum { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
