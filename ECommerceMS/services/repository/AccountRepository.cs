using ECommerceMS.Models;
using ECommerceMS.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ECommerceMS.services.repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICustomerRepository _customerRepository;

        public AccountRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, 
            SignInManager<ApplicationUser> signInManager, ICustomerRepository customerRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _customerRepository = customerRepository;
        }

        public async Task<IdentityResult> AddToRole(ApplicationUser user, string roleName)
        {
            var result =await _userManager.AddToRoleAsync(user, roleName);
            return result;
        }

        public async Task<ApplicationUser> GetUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<bool> IsUserInRole(ApplicationUser user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> Register(ApplicationUser user, string password)
        {
           
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<SignInResult> SignIn(ApplicationUser user, string password, bool rememberMe)
        {
            Microsoft.AspNetCore.Identity.SignInResult result =
                        await _signInManager.PasswordSignInAsync(user,password, rememberMe, false);
            return result;

        }

        public async Task SignInAfterRegister(ApplicationUser user)
        {
            await _signInManager.SignInAsync(user, false);
        }

        public async Task Signout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
