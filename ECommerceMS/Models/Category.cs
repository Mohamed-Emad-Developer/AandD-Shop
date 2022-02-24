using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
    }
}
