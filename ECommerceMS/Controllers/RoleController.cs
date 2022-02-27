using ECommerceMS.Data;
using ECommerceMS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMS.Controllers
{
    //[Authorize(Roles="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ECommerceDB context;
        public RoleController(RoleManager<IdentityRole> _roleManager, ECommerceDB _context)
        {
            roleManager = _roleManager;
            context = _context;
        }
        #region Create
        //Create 
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel newRole)
        {
            if (ModelState.IsValid == true)
            {
                IdentityRole role = new IdentityRole() { Name = newRole.RoleName };
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            };
            return View(newRole);
        }
        #endregion

        #region ShowAll
        public IActionResult Index()
        {
            List<RoleViewModel> roleViews = new List<RoleViewModel>();

            var roles = context.Roles.ToList();
            foreach (var role in roles)
            {
                RoleViewModel roleViewModel = new RoleViewModel() { RoleName = role.Name, ID = role.Id };
                roleViews.Add(roleViewModel);
            }

            return View(roleViews);
        }
        #endregion

        #region EditRole
        public async Task<IActionResult> EditRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await roleManager.FindByIdAsync(id);
            var roleViewModel = new RoleViewModel()
            {
                ID = role.Id,
                RoleName = role.Name,
            };

            return View(roleViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleViewModel roleViewModel, [FromRoute] string id)
        {
            var roleInDataBase = await roleManager.FindByIdAsync(id);
            roleInDataBase.Name = roleViewModel.RoleName;
            var result = await roleManager.UpdateAsync(roleInDataBase);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(roleViewModel);
            }
        }
        #endregion
        #region Delete
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return RedirectToAction("Index");
            }
        }
        #endregion

    }
}
