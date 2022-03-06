using ECommerceMS.Data;
using ECommerceMS.Models;
using ECommerceMS.services.repository;
using ECommerceMS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMS.Controllers
{

    public class CustomizeController : Controller
    {
        private readonly ECommerceDB _context;
        private readonly UserManager<ApplicationUser> _userManager;
        readonly ICustomerRepository customerRepository;
        readonly IOrderRepository orderRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CustomizeController(ICustomerRepository _customerRepo, UserManager<ApplicationUser> userManager, IOrderRepository _orderRepo, ECommerceDB context, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            customerRepository = _customerRepo;
            orderRepository = _orderRepo;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [Authorize(Roles ="Customer")]
        public IActionResult Style()
        {
            var customProduct = new CustomProduct() { Cost = 200 };
            return View(customProduct);
        } 
        [Authorize(Roles ="Customer")]
        [HttpPost]
        public async Task<IActionResult>  Style(CustomProduct NewCustomProduct)
        {
            if (ModelState.IsValid == true)
            {
                if (NewCustomProduct.ImageFile != null)
                {

                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(NewCustomProduct.ImageFile.FileName);
                    //string extension = Path.GetExtension(NewCustomProduct.ImageFile.FileName);
                    NewCustomProduct.ImagePath = fileName = fileName + DateTime.Now.ToString("yymmssfff") + ".png";
                    string path = Path.Combine(wwwRootPath + "/Images/customize/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await NewCustomProduct.ImageFile.CopyToAsync(fileStream);
                    }
                }

                _context.CustomProducts.Add(NewCustomProduct);
                _context.SaveChanges();
                return RedirectToAction("SubmitStyle","Customize",new {id= NewCustomProduct.Id} );
            }
            return View();
        }
        public IActionResult SubmitStyle(int id)
        {
            Customer customer = customerRepository.Get(_userManager.GetUserId(User));
            
            CustomProductOrderViewModel CPOVM=new CustomProductOrderViewModel();
            CPOVM.Name=customer.User.Name;
            CPOVM.Adress = customer.Address;
            CPOVM.Phone = customer.Phone;
            CPOVM.CustomProductID = id;
            CPOVM.CustomerID = customer.Id;
            CustomProduct customproduct=_context.CustomProducts.FirstOrDefault(ww => ww.Id == id);
            CPOVM.Cost=customproduct.Cost;
            ViewData["customProduct"] = customproduct;
            return View(CPOVM);
        }
        public IActionResult ConfirmOrder(CustomProductOrderViewModel newOrder)
        {
            if (ModelState.IsValid)
            {

                int ordernum = orderRepository.CreateCustomProductOrder(newOrder);
                var customProduct = _context.CustomProducts.FirstOrDefault(ww => ww.Id == newOrder.CustomProductID);
                customProduct.OrderNum = ordernum;
                ViewData["success"] = "Order Saved successfully";
                ViewData["CustomerID"] = newOrder.CustomerID;
                return RedirectToAction("Show_CusOrders", "Order",new {id =_userManager.GetUserId(User) });

            }
            else
            {
                CustomProduct customproduct = _context.CustomProducts.FirstOrDefault(ww => ww.Id == newOrder.CustomProductID);
                ViewData["customProduct"] = customproduct;
                return View("SubmitStyle",newOrder);
            }
        }
    }
}
