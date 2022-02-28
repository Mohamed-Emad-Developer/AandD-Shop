using ECommerceMS.Models;
using ECommerceMS.services;
using ECommerceMS.services.repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
 
namespace ECommerceMS.Controllers
{
    public class ProductsController : Controller
    {
        readonly IProductRepository ProductRepository;
        readonly ICategoryRepository CategoryRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        public ProductsController(IProductRepository _ProddRepo, UserManager<ApplicationUser> userManager,
            ICategoryRepository _CatRepo, IWebHostEnvironment hostEnvironment)
        {
            ProductRepository = _ProddRepo;
            CategoryRepository = _CatRepo;
            _hostEnvironment = hostEnvironment;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            List<Product> product = ProductRepository.GetAll();
            return View(product);
        }
        public IActionResult GetAllProductsForCustomer()
        {
            List<Product> product = ProductRepository.GetAll();
            return View("AllProducts", product);
        }

        public IActionResult Details(int id)
        {
            Product product = ProductRepository.GetById(id);
            List<Category> category = CategoryRepository.GetAll();
            ViewData["Categories"] = category;
            return View(product);
        }

        public IActionResult CustomerDetails(int id)
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
        public async Task<IActionResult> Create(Product prod)
        {
            if (ModelState.IsValid == true)
            {
                if (prod.ImageFile != null)
                {

                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(prod.ImageFile.FileName);
                    string extension = Path.GetExtension(prod.ImageFile.FileName);
                    prod.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Images/products/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await prod.ImageFile.CopyToAsync(fileStream);
                    }
                }
                ProductRepository.Create(prod);
                return RedirectToAction("Index");
            }
            List<Category> category = CategoryRepository.GetAll();
            ViewData["Categories"] = category;
            return View(prod);
        }

        public  IActionResult Edit(int id)
        {
            Product product = ProductRepository.GetById(id);
            //string wwwRootPath = _hostEnvironment.WebRootPath;
            //string path = Path.Combine(wwwRootPath + "/Images/products/", product.Image);
            //using (var fileStream = new FileStream(path, FileMode.Open))
            //{
            //    await prod.ImageFile.CopyToAsync(fileStream);
            //product.ImageFile = (IFormFile)fileStream;
            //}
            List<Category> category = CategoryRepository.GetAll();
            ViewData["Categories"] = category;
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, Product prod)
        {
            if (ModelState.IsValid == true)
            {
                var productInDB = ProductRepository.GetById(id);
                if (prod.ImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string OldPath =  productInDB.Image;
                    if(OldPath != prod.Image)
                    {
                        if (System.IO.File.Exists(OldPath))
                        {
                            System.IO.File.Delete(OldPath);

                        }
                        string fileName = Path.GetFileNameWithoutExtension(prod.ImageFile.FileName);
                        string extension = Path.GetExtension(prod.ImageFile.FileName);
                        prod.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Images/products/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await prod.ImageFile.CopyToAsync(fileStream);
                        }
                    }
                }
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
        public  IActionResult ConfirmeDelete(int id)
        {
            var prod = ProductRepository.GetById(id);
            if (prod.Image != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string path = Path.Combine(wwwRootPath + "/Images/products/", prod.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);

                }
            }
            ProductRepository.Delete(id);
            List<Category> category = CategoryRepository.GetAll();
            ViewData["Categories"] = category;
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Customer")]
        public IActionResult FavouriteProducts()
        {
            var products = ProductRepository.GetFavouriteProducts(userManager.GetUserId(User));
            return View(products);
        }
        public IActionResult AddToFavouriteList(int id)
        {
            ProductRepository.AddToFavouriteList(userManager.GetUserId(User), id);
            return RedirectToAction("FavouriteProducts");
        }
        public IActionResult RemoveFromFavouriteList(int id)
        {
            ProductRepository.RemoveFromFavouriteList(userManager.GetUserId(User), id);
            return RedirectToAction("FavouriteProducts");
        }
   
    }
}
