using ECommerceMS.Data;
using ECommerceMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace ECommerceMS.Controllers
{
    public class CustomizeController : Controller
    {   
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ECommerceDB _context;

        public CustomizeController(IWebHostEnvironment hostEnvironment, ECommerceDB context)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        [Authorize(Roles ="Customer")]
        public IActionResult Style()
        {
            var customProduct = new CustomProduct() { Cost = 200 };
            return View(customProduct);
        } 
        [Authorize(Roles ="Customer")]
        [HttpPost]
        public IActionResult Style([FromForm]CustomProduct customProduct)
        {
            if (ModelState.IsValid)
            {
                _context.CustomProducts.Add(customProduct);
                _context.SaveChanges();
            }
            return View();
        }
    }
}
