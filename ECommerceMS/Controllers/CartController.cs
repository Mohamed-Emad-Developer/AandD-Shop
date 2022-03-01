using ECommerceMS.Models;
using ECommerceMS.services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMS.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ICartRepository cartRepository, UserManager<ApplicationUser> userManager)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
        }
        //int AddPrpductToCart(int prodcutId, string customerId);
        public IActionResult AddToCart(int id)
        {
            _cartRepository.AddPrpductToCart(id, _userManager.GetUserId(User));
            return RedirectToAction("CartDetails");

        }
        public IActionResult CartDetails()
        {
            var cartInDb = _cartRepository.GetByUserId(_userManager.GetUserId(User));
            return View(cartInDb);
        }
        public IActionResult IncrementQuantity(int id)
        {
           _cartRepository.IncrementQuantity(id);
            return RedirectToAction("CartDetails");
        }
        public IActionResult DecrementQuantity(int id)
        {
           _cartRepository.DecrementQuantity(id);
            return RedirectToAction("CartDetails");
        }
    }
}
