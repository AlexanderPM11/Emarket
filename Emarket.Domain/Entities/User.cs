using Emarket.Core.Domain.Common;
using EntityFrameworkCore.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Domain.Entities
{
    public class User : AuditableBase
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<Adds> Adds { get; set; }
        public ICollection<Categories> Categories { get; set; }



    }
}