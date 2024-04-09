using Emarket.Core.Aplication.Interface.Services;
using Emarket.Core.Aplication.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Aplication
{
    public static class ServicesRegistrationContextAplication
    {
        public static void AddApplicationLayer(this IServiceCollection service, IConfiguration confi)
        {
            #region services
            service.AddTransient<IAddsServices, AddsServices>();
            service.AddTransient<ICategoriesServices, CategoryServices>();
            service.AddTransient<IUserServices, UserService>();
            //Método AddTransient: Se crear una nueva instancia de la clase cada que es requerido. 
            //    Es lo recomendado en casos donde se tienen clases livianas y sin estado.
            #endregion
        }
    }
}
