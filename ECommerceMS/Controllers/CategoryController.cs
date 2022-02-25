using ECommerceMS.Models;
using ECommerceMS.services.repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECommerceMS.Controllers
{
    public class CategoryController : Controller
    {
        readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository _ctgRepo)
        {
            categoryRepository = _ctgRepo;
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
