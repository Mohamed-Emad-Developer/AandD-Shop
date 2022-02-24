using ECommerceMS.Models;
using ECommerceMS.services.repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECommerceMS.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository _ctgRepo)
        {
            categoryRepository = _ctgRepo;
        }

        //Get All Catgories
        public IActionResult Index(string searchString)
        {
            List<Category> CtgModel = categoryRepository.getAll(searchString);
            return View(CtgModel);
        }

        //Get Catgory Details
        public IActionResult Details(int id)
        {
            Category CtgDetailsModel = categoryRepository.getById(id);
            return View(CtgDetailsModel);
        }

        // Add New Catgory
        [HttpGet]
        public IActionResult Create()
        {
            Category ctg = new Category();
            return View(ctg);
        }

        // Save Catgory
        [HttpPost]
        public IActionResult Create(Category newCtg)
        {
            if (ModelState.IsValid == true)
            {
                categoryRepository.create(newCtg);
                return RedirectToAction("Index");
            }

            return View(newCtg);
        }

        //Edit Catgory
        public IActionResult Edit(int id)
        {
            Category Ctg = categoryRepository.getById(id);
            return View(Ctg);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Category newCtg)
        {
            if (ModelState.IsValid == true)
            {
                categoryRepository.update(id, newCtg);
                return RedirectToAction("Index");
            }

            return View(newCtg);
        }

        //Delete Catgory
        public IActionResult Delete(int id)
        {
            Category ctg = categoryRepository.getById(id);
            return View(ctg);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmeDelete(int id)
        {
            categoryRepository.delete(id);
            return RedirectToAction("Index");
        }

        //Title Exist
        public IActionResult TitleExist(string Title, int id)
        {
            if (id == 0)
            {
                Category Ctg = categoryRepository.getByName(Title);
                if (Ctg == null)
                    return Json(true);
                else
                    return Json(false);
            }
            else
            {
                Category Ctg = categoryRepository.getByName(Title);
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
