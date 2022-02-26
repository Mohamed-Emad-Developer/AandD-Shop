using ECommerceMS.Models;
using ECommerceMS.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using ECommerceMS.services.repository;

namespace ECommerceMS.Controllers
{
    public class ProductsController : Controller
    {
        readonly IProductRepository ProductRepository;
        readonly ICategoryRepository CategoryRepository;
        private readonly UserManager<ApplicationUser> userManager;
        public ProductsController(IProductRepository _ProddRepo, UserManager<ApplicationUser> userManager, ICategoryRepository _CatRepo)
        {
            ProductRepository = _ProddRepo;
            CategoryRepository = _CatRepo;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            List<Product> product = ProductRepository.GetAll();
            return View(product);
        }

        public IActionResult Details(int id)
        {
            Product product = ProductRepository.GetById(id);
            List<Category> category = CategoryRepository.GetAll();
            ViewData["Categories"] = category;
            return View(product);
        }

        public IActionResult Create()
        {
            List<Category> category = CategoryRepository.GetAll();
            ViewData["Categories"] = category;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product prod)
        {
            if(ModelState.IsValid == true)
            {
                ProductRepository.Create(prod);
                return RedirectToAction("Index");
            }
            List<Category> category = CategoryRepository.GetAll();
            ViewData["Categories"] = category;
            return View(prod);
        }

        public IActionResult Edit(int id)
        {
            Product product = ProductRepository.GetById(id);
            List<Category> category = CategoryRepository.GetAll();
            ViewData["Categories"] = category;
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
            List<Category> category = CategoryRepository.GetAll();
            ViewData["Categories"] = category;
            return View(prod);
        }

        public IActionResult Delete(int id)
        {
            Product product = ProductRepository.GetById(id);
            List<Category> category = CategoryRepository.GetAll();
            ViewData["Categories"] = category;
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmeDelete(int id)
        {
            ProductRepository.Delete(id);
            List<Category> category = CategoryRepository.GetAll();
            ViewData["Categories"] = category;
            return RedirectToAction("Index");
        }

        public IActionResult FavouriteProducts()
        {

            var products = ProductRepository.GetFavouriteProducts(userManager.GetUserId(User));
            return View(products);
         
        }




    }
}
