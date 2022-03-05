using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceMS.Models
{
    public class CustomProduct
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public decimal Cost { get; set; }
        public int? OrderNum { get; set; }
        public Order Order { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
