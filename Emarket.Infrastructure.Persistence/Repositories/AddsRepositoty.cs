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
   public class AddsRepositoty : GenericRepository<Adds>, IAddsRepository
    {
        private readonly EmarketContext _dbContext;

        public AddsRepositoty(EmarketContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
