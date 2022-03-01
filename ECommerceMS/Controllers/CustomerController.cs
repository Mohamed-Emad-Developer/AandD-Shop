using ECommerceMS.services.repository;
using ECommerceMS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMS.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public CustomerController(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public IActionResult Account(string id)
        {
            var customer = _customerRepository.Get(id);
            var customerDetailsVM = new CustomerAccountViewModel()
            {
                Cusomer = customer,
                Orders = _orderRepository.GetByCusID(id)
            };
            return View(customerDetailsVM);
        }
    }
}
