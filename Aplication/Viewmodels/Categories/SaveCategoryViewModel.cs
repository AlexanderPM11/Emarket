using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Aplication.Viewmodels.Categories
{
   public class SaveCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre del producto")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Debe colocar el nombre del producto")]
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
