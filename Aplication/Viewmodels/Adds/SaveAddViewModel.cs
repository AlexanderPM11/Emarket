using Emarket.Core.Aplication.Viewmodels.Categories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Aplication.Viewmodels.Adds
{
   public class SaveAddViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del articulo es requerido")]
        public string NameArticul { get; set; }
        public string Image { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        [Range(1,int.MaxValue,ErrorMessage = "Debe colocar el precio del articulo")]
        public double Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe colocar la categoria del articulo")]
        public int CategoryId { get; set; }
        public List<CategoryViewModel> Categories;
        public int UserId { get; set; }
        public IFormFile File1 { get; set; }
        public IFormFile File2 { get; set; }
        public IFormFile File3 { get; set; }
        public IFormFile File4 { get; set; }
    }
}
