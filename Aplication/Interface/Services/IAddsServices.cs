using Emarket.Core.Aplication.Services;
using Emarket.Core.Aplication.Viewmodels.Adds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Aplication.Interface.Services
{
   public interface IAddsServices: IGenericServices<SaveAddViewModel, AddsViewModel>
    {
        Task<List<AddsViewModel>> GetAllViewModelByUserRegiste();
        Task<List<AddsViewModel>> GetAllViewModelWithFilter(string filter);
        Task<List<AddsViewModel>> GetByIdAddsViewModel(int id);
        Task<List<AddsViewModel>> GetAllViewModelWithFilterByName(string filter);
    }
}
