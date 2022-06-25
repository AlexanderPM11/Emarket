using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Aplication.Viewmodels.Users
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [DataType(DataType.Text)]
        public string Username { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
