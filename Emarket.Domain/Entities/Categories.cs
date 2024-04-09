using Emarket.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Domain.Entities
{
   public class Categories: AuditableBase
    {
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public int usId { get; set; }
        public ICollection<Adds> Adds { get; set; }
        public User Users { get; set; }


    }
}
