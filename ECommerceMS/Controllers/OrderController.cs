using ECommerceMS.Models;
using ECommerceMS.services;
using ECommerceMS.services.repository;
using ECommerceMS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceMS.Controllers
{
    public class OrderController : Controller
    {
        readonly IOrderRepository orderRepository;
        readonly ICustomerRepository customerRepository;
        readonly ICartRepository cartRepository;
        readonly IProductOrdersRepository productOrdersRepository;
        readonly IProductRepository productRepository;
        public OrderController(IOrderRepository _orderRepo, ICustomerRepository _customerRepo, ICartRepository _cartRepo, IProductOrdersRepository _OrderProductRepo,IProductRepository __productRepo)
        {
            orderRepository = _orderRepo;
            customerRepository = _customerRepo;
            cartRepository = _cartRepo;
            productOrdersRepository = _OrderProductRepo;
            productRepository = __productRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Customer")]
        public IActionResult CheckOut(int id) // id refers to cart_id 
        {
            decimal TotalPrice = 0;
            OrderDetailsViewModel ODVM = new OrderDetailsViewModel();
            Cart cart = cartRepository.Get(id);
            Customer customer = customerRepository.Get(cart.CustomerId);
            List<ProductCarts> productCart=cart.ProductCarts.ToList();
            ViewData["ProductCart"]= productCart;
            foreach (var item in productCart)
            {  
                TotalPrice += (item.Product.Price*item.Quantity);              
             //   ODVM.products.Add(item.Product);
            }
            //  orderRepository.CreateByCost(TotalPrice);
            ODVM.Name=customer.User.Name;
            ODVM.Adress = customer.Address;
            ODVM.Phone = customer.Phone;
            ODVM.TotalCost = TotalPrice;
            ODVM.CartID = id; 
            return View(ODVM);
        }
        [Authorize(Roles = "Customer")]
        public IActionResult ConfirmOrder(OrderDetailsViewModel newOrder)
        {
            if (ModelState.IsValid)
            {
                string CustomerId = cartRepository.Get(newOrder.CartID).CustomerId;
                //customerRepository.UpdateNAP(CustomerId, newOrder);
                //int ordernum=orderRepository.Create(newOrder.TotalCost,CustomerId);
                int ordernum = orderRepository.Create(newOrder, CustomerId);
                Cart cart = cartRepository.Get(newOrder.CartID);
                cart.OrderNum = ordernum;
                List<ProductCarts> productCart = cart.ProductCarts.ToList();
                
                foreach (var item in productCart)
                {
                    productOrdersRepository.Create(ordernum,item.Product.Id,item.Quantity);
                    productRepository.DecrementStockQuantity(item.ProductId,item.Quantity);
                    cartRepository.RemoveProductFromCart(item.CartId,item.ProductId);
                }
                ViewData["success"] = "Order Saved successfully";
                ViewData["CustomerID"] = CustomerId;
                return View();

            }
            else
            {
                ViewData["ProductCart"] = cartRepository.Get(newOrder.CartID).ProductCarts.ToList();
                return View("CheckOut",newOrder);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ShowOrders() // Show All orders for admin
        {
            List<Order> orders = orderRepository.Get();
            return View(orders);
        }
        [Authorize(Roles = "Customer")]
        public IActionResult GetProductsOfOrder(int id) // Show products for each order
        {
            Order order = orderRepository.Get(id);
            return PartialView("_GetProducts", order);
        }           
        [Authorize(Roles = "Customer")]
        public IActionResult Show_CusOrders(string id) // show orders for a specified customer
        {
            List<Order> orders = orderRepository.GetByCusID(id);
            return View(orders);
        }
    }
}
