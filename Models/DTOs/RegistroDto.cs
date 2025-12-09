using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class RegistroDto
    {
        [Required(ErrorMessage = "UserName requerido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password requerido")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "El password deber der minimo 3 maximo 10 caracteres")]
        public string Password{ get; set; }

        [Required(ErrorMessage = "Apellidos requeridos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Nombres requeridos")]
        public string Names { get; set; }

        [Required(ErrorMessage = "Email requeridos")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Rol requeridos")]
        public string Role { get; set; }
    }
}
