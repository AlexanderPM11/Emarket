using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Aplication.Viewmodels.Adds
{
   public class AddsViewModel
    {
        public int Id { get; set; }
        public string NameArticul { get; set; }
        public string Image { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string DescripCategory { get; set; }
        public int UserId { get; set; }
        public DateTime fechaPubli { get; set; }
        public string NameAnunciante { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
