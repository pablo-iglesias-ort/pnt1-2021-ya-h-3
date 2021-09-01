using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AgendaTurnos.Models
{
    public class Usuario
    {
        [Required]
        public Guid Id { get; set; }


        [Required]
        public string  Nombre { get; set; }


        [Required]
        [EmailAddress(ErrorMessage ="Por favor, ingresar un email valido.")]
        public string Email { get; set; }


        [Required]
        public DateTime FechaAlta { get; set; }


        [Required]
        [MinLength(8, ErrorMessage = "La contraseña debe tener como minimo 8(ocho) caracteres.")]
        public string Password { get; set; }
    }
}
