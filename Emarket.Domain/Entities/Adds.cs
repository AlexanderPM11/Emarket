using Emarket.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Domain.Entities
{
  public  class Adds: AuditableBase
    {
        public string NameArticul { get; set; }
        public string Image { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public Categories Categories { get; set; }
        public User Users { get; set; }
    }
}
