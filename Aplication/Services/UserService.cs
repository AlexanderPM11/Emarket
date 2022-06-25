using Emarket.Core.Aplication.Interface;
using Emarket.Core.Aplication.Interface.Repositories;
using Emarket.Core.Aplication.Viewmodels.Categories;
using Emarket.Core.Aplication.Viewmodels.Users;
using Emarket.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Aplication.Services
{
    class UserService : IUserServices
    {
        private readonly IUserRepository _Userrepository;

        public UserService(IUserRepository repository)
        {
            _Userrepository = repository;
        }

        public async Task<SaveUserViewModel> Add(SaveUserViewModel vm)
        {
            User user = new();
            user.Name = vm.Name;
            user.Username = vm.Username;
            user.Id = vm.Id;
            user.Password = vm.Password;
            user.Phone = vm.Phone;
            user.Email = vm.Email;
            user = await _Userrepository.AddAsync(user);

            SaveUserViewModel ProductVm = new();

            ProductVm.Name = user.Name;
            ProductVm.Username = user.Username;
            ProductVm.Password = user.Password;
            ProductVm.Phone = user.Phone;
            ProductVm.Id = user.Id;
            return ProductVm;
        }

        public async Task Update(SaveUserViewModel vm)
        {
            User user = new();
            user.Name = vm.Name;
            user.Username = vm.Username;
            user.Id = vm.Id;
            user.Password = vm.Password;
            user.Phone = vm.Phone;
            user.Email = vm.Email;
            await _Userrepository.AddAsync(user);

            await _Userrepository.UpdateAsync(user);
        }

        public async Task Delete(int id)
        {
            var product = await _Userrepository.GetByIdAsync(id);
            await _Userrepository.DeleteAsync(product);
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await _Userrepository.GetByIdAsync(id);
            SaveUserViewModel vm = new();
            vm.Name = user.Name;
            vm.Username = user.Username;
            vm.Id = user.Id;
            vm.Password = user.Password;
            vm.Phone = user.Phone;
            vm.Email = user.Email;

            return vm;
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var list = await _Userrepository.GetAllWithIncludeAsync(new List<string>() { "Adds" });
            return list.Select(s => new UserViewModel
            {
                Name = s.Name,
                Phone = s.Phone,
                Id = s.Id,
                Email = s.Email,
                Username = s.Username,
                Password = s.Password
            }).ToList();
        }

        public async Task<UserViewModel> Login(LoginViewModel vm)
        {
            UserViewModel userVm = new();
            User user = await _Userrepository.LoginAsync(vm);
            if (user == null)
            {
                return null;
            }
            userVm.Name = user.Name;
            userVm.Phone = user.Phone;
            userVm.Id = user.Id;
            userVm.Email = user.Email;
            userVm.Username = user.Username;
            userVm.Password = user.Password;
            return userVm;
        }
        public async Task<bool> ValidateNameUser(string nameUser)
        {
            bool validateUser = false;
            var list = await _Userrepository.GetAllAsync();

            foreach (User adds in list.Where(s => s.Username == nameUser))
            {
                validateUser = true;
            }
            
            return validateUser;
        }
    }
}
