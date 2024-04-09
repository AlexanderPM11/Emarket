using Emarket.Core.Aplication.Viewmodels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Aplication.Services
{
   public interface ICategoriesServices:IGenericServices<SaveCategoryViewModel, CategoryViewModel>
    {
        Task<List<CategoryViewModel>> GetAllViewModelByUserRegiste();
    }
}
