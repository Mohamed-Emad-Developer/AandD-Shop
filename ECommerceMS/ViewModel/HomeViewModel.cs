using ECommerceMS.Models;
using System.Collections.Generic;

namespace ECommerceMS.ViewModel
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}
