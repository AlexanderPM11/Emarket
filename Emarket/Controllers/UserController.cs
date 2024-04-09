using Emarket.Core.Aplication.Services;
using Emarket.Core.Aplication.Viewmodels.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emarket.Core.Aplication.Helpers;
using WebApp.Emarket.Middlewares;

namespace WebApp.Emarket.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _service;
        private readonly ValidateUserSession _validateUserSession;
        public UserController(IUserServices service, ValidateUserSession validateUserSession)
        {
            _service = service;
            _validateUserSession = validateUserSession;
        }

        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View("Login");
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginVm)
        {
          
            if (!ModelState.IsValid)
            {
                return View("Login",loginVm);
            }
            UserViewModel userVm = await _service.Login(loginVm);

            if (userVm != null)
            {
                HttpContext.Session.Set<UserViewModel>("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("UserValidation", "Datos de acceso incorrecto");
            }
            //Validar que haya un usuario registrado
            
            return View("Login",loginVm);
        }
        public IActionResult Register()
        {
            
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel userVm)
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View(userVm);
            }
            await _service.Add(userVm);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
        public IActionResult LogOut()
        {

            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

    }
}
