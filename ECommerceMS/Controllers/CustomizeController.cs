using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMS.Controllers
{
    public class CustomizeController : Controller
    {   
        [Authorize(Roles ="Customer")]
        public IActionResult Style()
        {
            return View();
        } 
        [Authorize(Roles ="Customer")]
        [HttpPost]
        public IActionResult Style(IFormFile imgResult)
        {
            return View();
        }
    }
}
