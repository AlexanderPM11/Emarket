using Emarket.Core.Aplication.Helpers;
using Emarket.Core.Aplication.Viewmodels.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Emarket.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UserViewModel userVm = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            if(userVm == null)
            {
                return false;
            }
            return true;
        }
    }
}
