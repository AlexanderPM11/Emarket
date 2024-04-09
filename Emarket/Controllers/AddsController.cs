using Emarket.Core.Aplication.Interface.Services;
using Emarket.Core.Aplication.Services;
using Emarket.Core.Aplication.Viewmodels.Adds;
using Emarket.Core.Aplication.Viewmodels.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Emarket.Middlewares;

namespace WebApp.Emarket.Controllers
{
    public class AddsController : Controller
    {
        private readonly IAddsServices _addsServices;
        private readonly ICategoriesServices _categoryServices;
        private readonly ValidateUserSession _validateUserSession;
        public AddsController(IAddsServices addsServices, ICategoriesServices categoryServices, ValidateUserSession validateUserSession)
        {
            _addsServices = addsServices;
            _categoryServices = categoryServices;
            _validateUserSession = validateUserSession;
        }


        public async Task< IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _addsServices.GetAllViewModelByUserRegiste());


        }
        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            SaveAddViewModel vm = new();
            vm.Categories = await _categoryServices.GetAllViewModelByUserRegiste();

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaveAddViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryServices.GetAllViewModelByUserRegiste();
                return View( vm);
               
            }
            if(vm.File1==null && vm.File2 == null && vm.File3 == null &&vm.File4 == null)
            {
                vm.Categories = await _categoryServices.GetAllViewModelByUserRegiste();
                ModelState.AddModelError("UserValidation", "Debe haber almenos una imagen seleccionada: ");
                return View(vm);
                
            }
            SaveAddViewModel productVm = await _addsServices.Add(vm);
            if (productVm != null && productVm.Id != 0)
            {
                productVm.Image = UploadFile(vm.File1, productVm.Id);
                productVm.Image1 = UploadFile(vm.File2, productVm.Id);
                productVm.Image2 = UploadFile(vm.File3, productVm.Id);
                productVm.Image3 = UploadFile(vm.File4, productVm.Id);
                await _addsServices.Update(productVm);
            }           
            
            return RedirectToRoute(new { controller = "Adds", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SaveAddViewModel vm = await _addsServices.GetByIdSaveViewModel(id);
            vm.Categories = await _categoryServices.GetAllViewModel();

            return View("Create",vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SaveAddViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                SaveAddViewModel vmAdds = await _addsServices.GetByIdSaveViewModel(vm.Id);
                vm.Categories = await _categoryServices.GetAllViewModel();
                vm.Image = vmAdds.Image;
                vm.Image1 = vmAdds.Image1;
                vm.Image2 = vmAdds.Image2;
                vm.Image3 = vmAdds.Image3;
                return View("Create", vm);
            }
            SaveAddViewModel productVm = await _addsServices.GetByIdSaveViewModel(vm.Id);
            vm.Image = UploadFile(vm.File1, productVm.Id, true, productVm.Image);
            vm.Image1 = UploadFile(vm.File2, productVm.Id, true, productVm.Image1);
            vm.Image2 = UploadFile(vm.File3, productVm.Id, true, productVm.Image2);
            vm.Image3 = UploadFile(vm.File4, productVm.Id, true, productVm.Image3);
            await _addsServices.Update(vm);
            return RedirectToRoute(new { controller = "Adds", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _addsServices.GetByIdSaveViewModel(id));
        }
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            await _addsServices.Delete(id);
            string basePath = $"/Image/Adds/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }
                Directory.Delete(path);
            }
            return RedirectToRoute(new { controller = "Adds", action = "Index" });
        }

        public string UploadFile(IFormFile file, int id, bool isEditMode = false, string imageUrl = "")
        {
            if (isEditMode && file == null)
            {
                return imageUrl;
            }
            // get directory path
            string basePath = $"/Image/Adds/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");
            // create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //get file path
            Guid guid = Guid.NewGuid();
            if ( file != null)
            {
                FileInfo fileInfo = new(file.FileName);
                string fileName = guid + fileInfo.Extension;
                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                if (isEditMode)
                {
                    if (imageUrl != null)
                    {
                        string[] oldImgePart = imageUrl.Split("/");
                        string oldImageName = oldImgePart[^1];
                        string completeImageOldPath = Path.Combine(path, oldImageName);
                        if (System.IO.File.Exists(completeImageOldPath))
                        {
                            System.IO.File.Delete(completeImageOldPath);
                        }
                    }
                    
                }
                //return Path.Combine(basePath,fileName);
                return $"{basePath}/{fileName}";
            }
            else
            {
                return "";
            }

          

        }

        public async Task<IActionResult> DetailsAdd(int id)
        {           

            return View(await _addsServices.GetByIdAddsViewModel(id));
        }

    



    }
}
