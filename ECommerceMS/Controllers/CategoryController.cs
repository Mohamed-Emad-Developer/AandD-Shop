using ECommerceMS.Models;
using ECommerceMS.services.repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMS.Controllers
{
    public class CategoryController : Controller
    {
        readonly ICategoryRepository categoryRepository;
        private readonly IWebHostEnvironment _hostEnv;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CategoryController(ICategoryRepository _ctgRepo, IWebHostEnvironment hostEnv, IWebHostEnvironment hostEnvironment)
        {
            categoryRepository = _ctgRepo;
            this._hostEnv = hostEnv;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult GetAllCategoriesForCustomer()
        {
            List<Category> CtgModel = categoryRepository.GetAll();
            return View("AllCategories", CtgModel);
        }

        //Get All Catgories
        public IActionResult Index()
        {
            List<Category> CtgModel = categoryRepository.GetAll();
            return View(CtgModel);
        }
        //Get All Catgories
        public IActionResult Search(string searchString)
        {
            List<Category> CtgModel = categoryRepository.GetAllSearch(searchString);
            return View("Index" ,CtgModel);
        }

        //Get Catgory Details
        public IActionResult Details(int id)
        {
            Category CtgDetailsModel = categoryRepository.GetById(id);
            return View(CtgDetailsModel);
        }

        // Add New Catgory
        [HttpGet]
        public IActionResult Create()
        {
            Category ctg = new();
            return View(ctg);
        }

        // Save Catgory
        [HttpPost]
        public async Task<IActionResult> Create(Category newCtg)
        {
            if (ModelState.IsValid == true)
            {
                if (newCtg.ImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(newCtg.ImageFile.FileName);
                    string extension = Path.GetExtension(newCtg.ImageFile.FileName);
                    newCtg.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Images/Categories/"
                        , fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await newCtg.ImageFile.CopyToAsync(fileStream);
                    }
                }
                categoryRepository.Create(newCtg);
                return RedirectToAction("Index");
            }

            return View(newCtg);
        }

        //Edit Catgory
        public IActionResult Edit(int id)
        {
            Category Ctg = categoryRepository.GetById(id);
            return View(Ctg);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Category newCtg)
        {
            if (ModelState.IsValid == true)
            {
                categoryRepository.Update(id, newCtg);
                return RedirectToAction("Index");
            }

            return View(newCtg);
        }

        //Delete Catgory
        public IActionResult Delete(int id)
        {
            Category ctg = categoryRepository.GetById(id);
            return View(ctg);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmeDelete(int id)
        {
            var ctg = categoryRepository.GetById(id);
            if (ctg.Image != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string path = Path.Combine(wwwRootPath + "/Images/Categories/", ctg.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);

                }
            }
            categoryRepository.Delete(id);
            return RedirectToAction("Index");
        }

        //Title Exist
        public IActionResult TitleExist(string Title, int id)
        {
            if (id == 0)
            {
                Category Ctg = categoryRepository.GetByName(Title);
                if (Ctg == null)
                    return Json(true);
                else
                    return Json(false);
            }
            else
            {
                Category Ctg = categoryRepository.GetByName(Title);
                if (Ctg == null)
                    return Json(true);
                else
                {
                    if (Ctg.Id == id)
                        return Json(true);
                    else
                        return Json(false);
                }

            }
        }


    }
}
