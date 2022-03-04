using System.ComponentModel.DataAnnotations;

namespace ECommerceMS.ViewModel
{
    public class EditCustomerViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }

        [Required]
        [Display(Name ="Old Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="New Password")]
        public string NewPassword{ get; set; }
    }
}
