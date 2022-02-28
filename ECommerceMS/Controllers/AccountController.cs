using ECommerceMS.Models;
using ECommerceMS.services;
using ECommerceMS.services.repository;
using ECommerceMS.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICustomerRepository _customerRepository;
        //private readonly IAdminRepository _adminRepository;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ICustomerRepository customerRepository, SignInManager<ApplicationUser> signInManager, IAdminRepository adminRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _customerRepository = customerRepository;
            _signInManager = signInManager;
            //_adminRepository = adminRepository;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(CustomerRegisterViewModel customer)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser customerUser = new ApplicationUser()
                {
                    Email = customer.Email,
                    UserName = customer.Email,
                    Name = customer.Name,
                };

                var result = await _userManager.CreateAsync(customerUser, customer.Password);
                if (result.Succeeded)
                {
                    var assignCustomerToRole = await _userManager.AddToRoleAsync(customerUser, "Customer");
                    if (assignCustomerToRole.Succeeded)
                    {
                        Customer customerData = new Customer()
                        {
                            Id = customerUser.Id,
                            Address = customer.Address,
                            Phone = customer.Phone,
                        };
                        _customerRepository.Create(customerData);
                        await _signInManager.SignInAsync(customerUser,false);
                        return RedirectToAction("Index","Home");

                    }
                    else
                    {
                        foreach (var error in assignCustomerToRole.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(customer);
        }

        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if(user != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result =
                       await _signInManager.PasswordSignInAsync
                       (user, login.Password, login.RememberMe, false);
                    if (result.Succeeded)
                    {
                        if (await _userManager.IsInRoleAsync(user, "Customer"))
                        {
                            returnUrl = returnUrl == null ? $"/Home/Index" : returnUrl;
                            return LocalRedirect(returnUrl);

                        }
                        else if (await _userManager.IsInRoleAsync(user, "Admin"))
                        {
                            returnUrl = returnUrl == null ? "/Products/Index" : returnUrl;
                            return LocalRedirect(returnUrl);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email and Password are not valid");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email and Password are not valid");
                }
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(login);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

     
    }
}
