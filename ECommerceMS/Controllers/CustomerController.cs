using ECommerceMS.services.repository;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMS.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IActionResult Account(string id)
        {
            var customer = _customerRepository.Get(id);
            return View(customer);
        }
    }
}
