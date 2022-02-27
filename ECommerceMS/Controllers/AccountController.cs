using ECommerceMS.Models;
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
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ICustomerRepository customerRepository, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _customerRepository = customerRepository;
            _signInManager = signInManager;
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

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
