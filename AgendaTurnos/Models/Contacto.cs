using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AgendaTurnos.Models
{
    public class Contacto
    {
        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Por favor, ingresar un email valido.")]
        public string Email { get; set; } // si esta logueado usa el que tiene 

        [Required]
        public string Nombre { get; set; }// si esta logueado usa el que tiene 

        [Required]
        public string Apellido { get; set; }// si esta logueado usa el que tiene 
        public Boolean Leido { get; set; } // false defecto

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Mensaje { get; set; }
    }
}
