using ECommerceMS.Models;
using System.Collections.Generic;

namespace ECommerceMS.ViewModel
{
    public class OrderDetailsViewModel
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public List<Product> products { get; set; }
        public int CartID { get; set; }
        
        public decimal TotalCost { get; set; }
    }
}
