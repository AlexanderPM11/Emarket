using Emarket.Core.Aplication.Interface.Services;
using Emarket.Core.Aplication.Services;
using Emarket.Core.Aplication.Viewmodels.Adds;
using Emarket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Emarket.Middlewares;

namespace Emarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAddsServices _addsServices;
        private readonly ICategoriesServices _categoryServices;
        private readonly ValidateUserSession _validateUserSession;
        public HomeController(IAddsServices addsServices, ValidateUserSession validateUserSession,  ICategoriesServices categoryServices) 
        {
            _addsServices = addsServices;
            _validateUserSession = validateUserSession;
            _categoryServices = categoryServices;
        }
        public async Task< IActionResult> Index(IFormCollection form)

        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            ViewBag.Categories = await _categoryServices.GetAllViewModel();
            ViewBag.Adds = await _addsServices.GetAllViewModel();
            string filterName = form["AddsName"];
            if(filterName== "Todas")
            {
                filterName = null;
            }
            string filterId = form["CategoriesId"];
            if (filterId == "0")
            {
                filterId = null;
            }
            

            if (filterName != null)
            {
                var listByAdss = await _addsServices.GetAllViewModelWithFilterByName(filterName);
                return View(listByAdss);
            }
            else if (filterId != null)
            {
                var listByCate = await _addsServices.GetAllViewModelWithFilter(filterId);
                return View(listByCate);
            }
            
            return View(await _addsServices.GetAllViewModel());
        }
        
        
    }
}
