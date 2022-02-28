using ECommerceMS.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceMS.ViewModel
{
    public class OrderDetailsViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Name contains char, spaces only ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Adress is required.")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Address must be characters")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "You must provide a phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "10 Numbers Only Accepted")]
        public string Phone { get; set; }
        public List<Product> products { get; set; }
        public int CartID { get; set; }

        public decimal TotalCost { get; set; }
    }
}
