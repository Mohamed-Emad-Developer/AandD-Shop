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

        public CategoryController(ICategoryRepository _ctgRepo, IWebHostEnvironment hostEnv)
        {
            categoryRepository = _ctgRepo;
            this._hostEnv = hostEnv;
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
        public IActionResult Create(Category newCtg)
        {
            if (ModelState.IsValid == true)
            {
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
        public IActionResult Edit([FromRoute] int id, Category newCtg /* , IFormFile Image*/)
        {
            if (ModelState.IsValid == true)
            {
                //if (newCtg.Image != null)
                //{
                //    string imageName = new String(Path.GetFileNameWithoutExtension(Image.FileName).Take(10).ToArray()).Replace(' ', '-');
                //    imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(Image.FileName);
                //    var imagePath = Path.Combine(_hostEnv.ContentRootPath, "wwwroot/Images/Categories", imageName);
                //    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                //    {
                //        await Image.CopyToAsync(fileStream);
                //    }
                //}
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
