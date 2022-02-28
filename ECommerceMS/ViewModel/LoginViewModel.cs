﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceMS.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember me?")]
        public bool RememberMe { get; set;}
    }
}
