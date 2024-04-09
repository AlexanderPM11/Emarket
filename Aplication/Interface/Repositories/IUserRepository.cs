using Emarket.Core.Aplication.Viewmodels.Users;
using Emarket.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Aplication.Interface
{
  public  interface IUserRepository : IGenericReposistory<User>
    {
        Task<User> LoginAsync(LoginViewModel loginVm);
    }
}
