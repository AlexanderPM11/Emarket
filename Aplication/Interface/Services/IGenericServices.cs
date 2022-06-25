using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Aplication.Services
{
   public interface IGenericServices<Entity, ViewModel>
      where Entity : class
        where ViewModel : class
   {
        Task<Entity> Add(Entity vm);
        Task Update(Entity vm);
        Task Delete(int id);
        Task<Entity> GetByIdSaveViewModel(int id);
        Task<List<ViewModel>> GetAllViewModel();
   }

}
