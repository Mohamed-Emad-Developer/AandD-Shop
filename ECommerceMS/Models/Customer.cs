using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceMS.Models
{
    public class Customer
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<FavouriteList> FavouriteList { get; set; }
        public Customer()
        {
            FavouriteList = new List<FavouriteList>();
        }
    }
}
