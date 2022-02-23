using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceMS.Models
{
    public class ProductCarts
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}
