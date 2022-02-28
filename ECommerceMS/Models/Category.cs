using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceMS.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Remote(action: "TitleExist", controller: "Category",
            ErrorMessage = "Title Already Exist", AdditionalFields = "Id")]
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Image is required")]
        [Display(Name = "Image")]
        public string Image { get; set; }
        [Display(Name = "Upload File")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public ICollection<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }
}
