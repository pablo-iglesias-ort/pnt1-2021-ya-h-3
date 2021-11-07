using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AgendaTurnos.Models
{
    public abstract class Usuario
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Por favor, ingresar un email valido.")]
        public string Email { get; set; }

        public abstract DateTime FechaAlta { get; }

        [Required]
        [MinLength(8, ErrorMessage = "La contraseña debe tener como minimo 8(ocho) caracteres.")]
        [ScaffoldColumn(false)]
        public byte[] Password { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public string Dni { get; set; }

        public abstract Rol Rol { get; }

    }
}
