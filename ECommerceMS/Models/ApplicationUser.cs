using Microsoft.AspNetCore.Identity;

namespace ECommerceMS.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }

    }
}
