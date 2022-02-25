using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceMS.Models
{
    public class FavouriteList
    {
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
