using Emarket.Core.Aplication.Services;
using Emarket.Core.Aplication.Viewmodels.Categories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Emarket.Middlewares;

namespace WebApp.Emarket.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoriesServices _categoryServices;
        private readonly ValidateUserSession _validateUserSession;
        public CategoryController(ICategoriesServices categoryServices, ValidateUserSession validateUserSession)
        {
            _categoryServices = categoryServices;
            _validateUserSession = validateUserSession;
        }
        public async Task< IActionResult >Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _categoryServices.GetAllViewModelByUserRegiste());
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            SaveCategoryViewModel vm = new();
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaveCategoryViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _categoryServices.Add(vm);
            
            return RedirectToRoute(new { controller = "Category", action = "Index" });
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            SaveCategoryViewModel vm = await _categoryServices.GetByIdSaveViewModel(id);

            return View("Create", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SaveCategoryViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
             
                return View("Create", vm);
            }
           
            await _categoryServices.Update(vm);
            return RedirectToRoute(new { controller = "Category", action = "Index" });
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _categoryServices.GetByIdSaveViewModel(id));
        }
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            await _categoryServices.Delete(id);            
            return RedirectToRoute(new { controller = "Category", action = "Index" });
        }


    }
}
