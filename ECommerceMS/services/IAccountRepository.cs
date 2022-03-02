using ECommerceMS.Models;
using ECommerceMS.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ECommerceMS.services
{
    public interface IAccountRepository
    {
        Task<ApplicationUser> GetUser(string email);
        Task<SignInResult> SignIn(ApplicationUser user, string password, bool rememberMe);
        Task SignInAfterRegister(ApplicationUser user);
        Task<bool> IsUserInRole(ApplicationUser user, string roleName);
        Task<IdentityResult> AddToRole(ApplicationUser user, string roleName);
        Task Signout();
        Task<IdentityResult> Register(ApplicationUser user, string password);
    }
}
