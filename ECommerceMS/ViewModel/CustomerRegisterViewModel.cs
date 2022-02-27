using System.ComponentModel.DataAnnotations;

namespace ECommerceMS.ViewModel
{
    public class CustomerRegisterViewModel
    {
        [Display(Name ="Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name ="Name")]
        [Required]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Address must be characters")]
        public string Name { get; set; }

        [Display(Name ="Address (optional)")]
        [RegularExpression("^[A-Za-z ]+$",ErrorMessage ="Address must be characters")]
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage ="Phone Number must be numbers")]
        public string Phone { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
