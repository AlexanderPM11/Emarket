using Emarket.Core.Aplication.Interface.Repositories;
using Emarket.Core.Domain.Entities;
using Emarket.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Infrastructure.Persistence.Repositories
{
   public class CategoriesRepository: GenericRepository<Categories>, ICategoryRepository
    {
        
        private readonly EmarketContext _dbContext;

        public CategoriesRepository(EmarketContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    
    }
}
