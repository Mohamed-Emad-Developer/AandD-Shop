using ECommerceMS.Models;
using ECommerceMS.services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECommerceMS.Controllers
{
    public class ProductsController : Controller
    {
        readonly IProductRepository ProductRepository;

        public ProductsController(IProductRepository _ProddRepo)
        {
            ProductRepository = _ProddRepo;
        }

        public IActionResult Index()
        {
            List<Product> product = ProductRepository.GetAll();
            return View(product);
        }

        public IActionResult Details(int id)
        {
            Product product = ProductRepository.GetById(id);
            return View(product);
        }

        public IActionResult Create()
        {
            Product product = new();
            return View(product);
        }

        [HttpPost]
        public IActionResult Create(Product prod)
        {
            if(ModelState.IsValid == true)
            {
                ProductRepository.Create(prod);
                return RedirectToAction("Index");
            }
            return View(prod);
        }

        public IActionResult Edit(int id)
        {
            Product product = ProductRepository.GetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Product prod)
        {
            if (ModelState.IsValid == true)
            {
                ProductRepository.Update(id, prod);
                return RedirectToAction("Index");
            }
            return View(prod);
        }

        public IActionResult Delete(int id)
        {
            Product product = ProductRepository.GetById(id);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmeDelete(int id)
        {
            ProductRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
