using ECommerceMS.Data;
using ECommerceMS.Models;
using ECommerceMS.services.repository;
using ECommerceMS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace ECommerceMS.Controllers
{

    public class CustomizeController : Controller
    {
        ECommerceDB context = new ECommerceDB();
        private readonly UserManager<ApplicationUser> _userManager;
        readonly ICustomerRepository customerRepository;
        readonly IOrderRepository orderRepository;
        public CustomizeController(ICustomerRepository _customerRepo,UserManager<ApplicationUser> userManager, IOrderRepository _orderRepo)
        {
            _userManager = userManager;
            customerRepository = _customerRepo;
            orderRepository = _orderRepo;
            
        }

        [Authorize(Roles ="Customer")]
        public IActionResult Style()
        {
            return View();
        } 
        [Authorize(Roles ="Customer")]
        [HttpPost]
        public IActionResult Style(CustomProduct NewCustomProduct)
        {
            if (ModelState.IsValid == true)
            {
                ECommerceDB context = new ECommerceDB();
                context.CustomProducts.Add(NewCustomProduct);
                context.SaveChanges();
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
            CustomProduct customproduct=context.CustomProducts.FirstOrDefault(ww => ww.Id == id);
            CPOVM.Cost=customproduct.Cost;
            ViewData["customProduct"] = customproduct;
            return View(CPOVM);
        }
        public IActionResult ConfirmOrder(CustomProductOrderViewModel newOrder)
        {
            if (ModelState.IsValid)
            {

                int ordernum = orderRepository.CreateCustomProductOrder(newOrder);
                var customProduct = context.CustomProducts.FirstOrDefault(ww => ww.Id == newOrder.CustomProductID);
                customProduct.OrderNum = ordernum;
                ViewData["success"] = "Order Saved successfully";
                ViewData["CustomerID"] = newOrder.CustomerID;
                return View();

            }
            else
            {
                CustomProduct customproduct = context.CustomProducts.FirstOrDefault(ww => ww.Id == newOrder.CustomProductID);
                ViewData["customProduct"] = customproduct;
                return View("SubmitStyle",newOrder);
            }
        }
    }
}
