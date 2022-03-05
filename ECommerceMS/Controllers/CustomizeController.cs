using Microsoft.AspNetCore.Mvc;

namespace ECommerceMS.Controllers
{
    public class CustomizeController : Controller
    {
        public IActionResult Style()
        {
            return View();
        }
    }
}
