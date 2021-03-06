using ECommerceMS.enums;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceMS.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Image { get; set; }
        [Display(Name ="Upload File")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Description { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Admin")]
        public string AdminId { get; set; }
        public Admin Admin { get; set; }
        public ICollection<ProductOrders> ProductOrders { get; set; }
        public ICollection<ProductCarts> ProductCarts { get; set; }
        public ICollection<FavouriteList> FavouriteList { get; set; }
        public Product()
        {
            ProductOrders = new List<ProductOrders>();
            ProductCarts = new List<ProductCarts>();
            FavouriteList = new List<FavouriteList>();
        }
    }
}
