using Emarket.Core.Aplication.Helpers;
using Emarket.Core.Aplication.Interface;
using Emarket.Core.Aplication.Viewmodels.Users;
using Emarket.Core.Domain.Entities;
using Emarket.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Infrastructure.Persistence.Repositories
{
   public class UserRepository: GenericRepository<User>, IUserRepository
    {
        private readonly EmarketContext _dbContext;

        public UserRepository(EmarketContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override  async Task<User> AddAsync(User entity)
        {
            entity.Password = PasswordEncrypted.ComputeSah256Hash(entity.Password);

          return  await base.AddAsync(entity);
        }
        public async Task<User> LoginAsync(LoginViewModel loginVm)
        {
            string passwordEncrypt = PasswordEncrypted.ComputeSah256Hash(loginVm.Password);
            User user = await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.Username == loginVm.Username &&
            user.Password == passwordEncrypt);
            return user;
        }
    }
}
