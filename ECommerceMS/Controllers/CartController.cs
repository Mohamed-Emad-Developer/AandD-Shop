using ECommerceMS.Models;
using ECommerceMS.services;
using ECommerceMS.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
            var shopingCartVM = new ShoppingCartViewModel()
            {
                Cart = cartInDb,
                TotalCost = cartInDb.ProductCarts.Sum(pc => pc.Quantity * pc.Product.Price),
            };
            return View(shopingCartVM);
        }
        public IActionResult IncrementQuantity(int cartId, int productId)
        {
           _cartRepository.IncrementQuantity( cartId,  productId);
            return RedirectToAction("CartDetails");
        }
        public IActionResult DecrementQuantity(int cartId, int productId)
        {
           _cartRepository.DecrementQuantity(cartId, productId);
            return RedirectToAction("CartDetails");
        }

        public IActionResult RemoveProductFromCart(int cartId, int productId)
        {
            _cartRepository.RemoveProductFromCart(cartId, productId);
            return RedirectToAction("CartDetails");
        }
    }
}
