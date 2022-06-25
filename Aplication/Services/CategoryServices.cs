using Emarket.Core.Aplication.Helpers;
using Emarket.Core.Aplication.Interface.Repositories;
using Emarket.Core.Aplication.Viewmodels.Categories;
using Emarket.Core.Aplication.Viewmodels.Users;
using Emarket.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Aplication.Services
{
    class CategoryServices:ICategoriesServices
    {
        private readonly ICategoryRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryServices(ICategoryRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<SaveCategoryViewModel> Add(SaveCategoryViewModel vm)
        {
            UserViewModel userVm = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            Categories product = new();
            product.Name = vm.Name;
            product.Descripcion = vm.Description;
            product.usId = userVm.Id;

            product = await _repository.AddAsync(product);
            SaveCategoryViewModel ProductVm = new();

            ProductVm.Name = product.Name;
            ProductVm.Description = product.Descripcion;
            ProductVm.Id = product.Id;
            return ProductVm;
        }

        public async Task Update(SaveCategoryViewModel vm)
        {
            Categories product = await _repository.GetByIdAsync(vm.Id);
            product.Id = vm.Id;
            product.Name = vm.Name;

            product.Descripcion = vm.Description;


            await _repository.UpdateAsync(product);
        }

        public async Task Delete(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(product);
        }

        public async Task<SaveCategoryViewModel> GetByIdSaveViewModel(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            SaveCategoryViewModel vm = new();
            vm.Id = product.Id;
            vm.Name = product.Name;
            vm.Description = product.Descripcion;
            return vm;
        }

        public async Task<List<CategoryViewModel>> GetAllViewModel()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(s => new CategoryViewModel
            {
                Name = s.Name,
                Description = s.Descripcion,
                Id = s.Id
            }).ToList();
        }
        public async Task<List<CategoryViewModel>> GetAllViewModelByUserRegiste()
        {
            
            UserViewModel userVm = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            var list = await _repository.GetAllWithIncludeAsync(new List<string> { "Adds" });
            return list.Select(s => new CategoryViewModel
            {
                Id = s.Id,
                Description = s.Descripcion,               
                Name = s.Name,
                userId=s.usId,
                CountLinkedAdds=s.Adds.Count

            }).Where(s => s.userId==userVm.Id ).ToList();
        }
    }
}
