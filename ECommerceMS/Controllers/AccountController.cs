﻿using ECommerceMS.Models;
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
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICartRepository _cartRepository;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ICustomerRepository customerRepository, SignInManager<ApplicationUser> signInManager, IAdminRepository adminRepository, IAccountRepository accountRepository, ICartRepository cartRepository)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _cartRepository = cartRepository;
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
                ApplicationUser user = new ApplicationUser()
                {
                    Email = customer.Email,
                    UserName = customer.Email,
                    Name = customer.Name,
                };

                var result = await _accountRepository.Register(user, customer.Password);
                if (result.Succeeded)
                {
                    var assignCustomerToRole = await _accountRepository.AddToRole(user, "Customer");
                    if (assignCustomerToRole.Succeeded)
                    {
                         
                        _customerRepository.Create(user.Id,customer.Address, customer.Phone);
                        _cartRepository.Create(user.Id);
                        await _accountRepository.SignInAfterRegister(user);
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
                var user = await _accountRepository.GetUser(login.Email);
                if(user != null)
                {
                    var result =
                       await _accountRepository.SignIn(user, login.Password, login.RememberMe);
                    if (result.Succeeded)
                    {
                        if (await _accountRepository.IsUserInRole(user, "Customer"))
                        {
                            returnUrl = returnUrl == null ? $"/Home/Index" : returnUrl;
                            return LocalRedirect(returnUrl);

                        }
                        else if (await _accountRepository.IsUserInRole(user, "Admin"))
                        {
                            returnUrl = returnUrl == null ? "/Products/GetAllProductsForAdmin" : returnUrl;
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
            await _accountRepository.Signout();
            return RedirectToAction("Index", "Home");
        }

     
    }
}
