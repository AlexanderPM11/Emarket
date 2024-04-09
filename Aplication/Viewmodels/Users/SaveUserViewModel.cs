using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Aplication.Viewmodels.Users
{
  public  class SaveUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [DataType(DataType.Text)]
        public string Username { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "La contraseña deben coincidir")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Debe colocar el Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Emial incorrecto!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El numero es requerido")]
        [Phone( ErrorMessage = "Numero de telefono incorrecto!")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }
    }
}
