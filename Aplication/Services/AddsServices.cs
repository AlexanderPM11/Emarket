using Emarket.Core.Aplication.Helpers;
using Emarket.Core.Aplication.Interface.Repositories;
using Emarket.Core.Aplication.Interface.Services;
using Emarket.Core.Aplication.Viewmodels.Adds;
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
   public class AddsServices:IAddsServices
    {
        private readonly IAddsRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userVm;
        private readonly IUserServices _serviceUser;


        public AddsServices(IAddsRepository repository, IHttpContextAccessor httpContextAccessor, IUserServices service)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            userVm = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _serviceUser = service;
        }

        public async Task<SaveAddViewModel> Add(SaveAddViewModel vm)
        {
            Adds adds = new();
            adds.NameArticul = vm.NameArticul;
            adds.Image = vm.Image;
            adds.Image2 = vm.Image1;
            adds.Image3 = vm.Image2;
            adds.Image4 = vm.Image3;
            adds.Price = vm.Price;
            adds.CategoryId = vm.CategoryId;
            adds.UserId = userVm.Id;

            adds = await _repository.AddAsync(adds);
            SaveAddViewModel ProductVm = new();

            ProductVm.NameArticul = adds.NameArticul;
            ProductVm.Image = adds.Image;
            ProductVm.Image1 = adds.Image2;
            ProductVm.Image2 = adds.Image3;
            ProductVm.Image3 = adds.Image4;
            ProductVm.Price = adds.Price;
            ProductVm.CategoryId = adds.CategoryId;
            ProductVm.UserId = userVm.Id;
            ProductVm.Id = adds.Id;

            return ProductVm;
        }

        public async Task Update(SaveAddViewModel adds)
        {
            Adds ProductVm = await _repository.GetByIdAsync(adds.Id);
            ProductVm.NameArticul = adds.NameArticul;
            ProductVm.Image = adds.Image;
            ProductVm.Image2 = adds.Image1;
            ProductVm.Image3 = adds.Image2;
            ProductVm.Image4 = adds.Image3;
            ProductVm.Price = adds.Price;
            ProductVm.CategoryId = adds.CategoryId;
            ProductVm.Id = adds.Id;


            await _repository.UpdateAsync(ProductVm);
        }

        public async Task Delete(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(product);
        }

        public async Task<SaveAddViewModel> GetByIdSaveViewModel(int id)
        {
            var adds = await _repository.GetByIdAsync(id);
            SaveAddViewModel vm = new();
            vm.NameArticul = adds.NameArticul;
            vm.Image = adds.Image;
            vm.Image1 = adds.Image2;
            vm.Image2 = adds.Image3;
            vm.Image3 = adds.Image4;
            vm.Price = adds.Price;
            vm.CategoryId = adds.CategoryId;
            vm.UserId = adds.UserId;
            vm.Id = adds.Id;
            return vm;
        }
        public async Task<List<AddsViewModel>> GetByIdAddsViewModel(int id)
        {
            int idUserCreatedAdds = 0;
            var list = await _repository.GetAllWithIncludeAsync(new List<string> { "Categories" });

           
            foreach(Adds adds in list.Where(s => s.Id ==id))
            {
                idUserCreatedAdds = adds.UserId;
            }

            SaveUserViewModel userCreate = await _serviceUser.GetByIdSaveViewModel(idUserCreatedAdds);

            return list.Select(s => new AddsViewModel
            {
               
                NameArticul = s.NameArticul,
                Image = s.Image,
                Image2 = s.Image2,
                Image3 = s.Image3,
                Image4 = s.Image4,
                Price = s.Price,
                Category = s.Categories.Name,
                DescripCategory = s.Categories.Descripcion,
                UserId = s.UserId,
                Id = s.Id,
                fechaPubli=s.Created,
                NameAnunciante= userCreate.Name,
                Phone= userCreate.Phone,
                Email= userCreate.Email
            }).Where(s=>s.Id==id).ToList();
         
        }
        public async Task<List<AddsViewModel>> GetAllViewModel()
        {
            var list = await _repository.GetAllWithIncludeAsync(new List<string> { "Categories" });
            return list.Select(s => new AddsViewModel
            {
                Id=s.Id,
                NameArticul = s.NameArticul,
                Image = s.Image,
                Image2=s.Image2,
                Image3=s.Image3,
                Image4=s.Image4,
                Price = s.Price,
                Category = s.Categories.Name,
                DescripCategory=s.Categories.Descripcion,
                UserId = s.UserId

            }).Where(s=>s.UserId!=userVm.Id).ToList();
        }
        public async Task<List<AddsViewModel>> GetAllViewModelByUserRegiste()
        {
            UserViewModel userVm = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            var list = await _repository.GetAllWithIncludeAsync(new List<string> { "Categories" });
            return list.Select(s => new AddsViewModel
            {
                Id = s.Id,
                NameArticul = s.NameArticul,
                Image = s.Image,
            Image2 = s.Image2,
            Image3 = s.Image3,
            Image4 = s.Image4,
            Price = s.Price,
                Category = s.Categories.Name,
                DescripCategory = s.Categories.Descripcion,
                UserId = s.UserId

            }).Where(s => s.UserId == userVm.Id).ToList();
        }
        public async Task<List<AddsViewModel>> GetAllViewModelWithFilter(string id)
        {
            var list = await _repository.GetAllWithIncludeAsync(new List<string>() { "Categories" });
            var ListViewModels = list.Where(pro => pro.UserId != userVm.Id).Select(s => new AddsViewModel
            {
                Id = s.Id,
                NameArticul = s.NameArticul,
                Image = s.Image,
                Image2 = s.Image2,
                Image3 = s.Image3,
                Image4 = s.Image4,
                Price = s.Price,
                Category = s.Categories.Name,
                CategoryId=s.CategoryId,
                DescripCategory = s.Categories.Descripcion,
                UserId = s.UserId
            }).ToList();
            if (id != null)
            {
                ListViewModels = ListViewModels.Where(adds => adds.CategoryId == int.Parse(id)).ToList();
            }
            return ListViewModels;
        }
        public async Task<List<AddsViewModel>> GetAllViewModelWithFilterByName(string filter)
        {
            var list = await _repository.GetAllWithIncludeAsync(new List<string>() { "Categories" });
            var ListViewModels = list.Where(pro => pro.UserId != userVm.Id).Select(s => new AddsViewModel
            {
                Id = s.Id,
                NameArticul = s.NameArticul,
                Image = s.Image,
                Image2 = s.Image2,
                Image3 = s.Image3,
                Image4 = s.Image4,
                Price = s.Price,
                Category = s.Categories.Name,
                CategoryId = s.CategoryId,
                DescripCategory = s.Categories.Descripcion,
                UserId = s.UserId
            }).ToList();
            if (filter!= null)
            {
                ListViewModels = ListViewModels.Where(adds => adds.NameArticul == filter).ToList();
            }
            return ListViewModels;
        }

    }
}
