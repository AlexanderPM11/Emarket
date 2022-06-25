using Emarket.Core.Aplication.Viewmodels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Aplication.Services
{
   public interface IUserServices: IGenericServices<SaveUserViewModel, UserViewModel>
    {
        Task<UserViewModel> Login(LoginViewModel vm);
        Task<bool> ValidateNameUser(string nameUser);
    }
}
