using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace AgendaTurnos.Models
{
    public class Persona
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public DateTime FechaAlta { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Dni { get; set; }

        public List <Turno> Turnos  { get; set; }
    }
}
