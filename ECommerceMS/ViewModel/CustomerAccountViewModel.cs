using ECommerceMS.Models;
using System.Collections.Generic;

namespace ECommerceMS.ViewModel
{
    public class CustomerAccountViewModel
    {
        public Customer Cusomer { get; set; }
        public List<Order> Orders { get; set; }
    }
}
