using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace AgendaTurnos.Models
{
    public class Turno
    {

        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public Boolean Confirmado { get; set; }

        [Required]
        public Boolean Activo { get; set; }

        [Required]
        public DateTime FechaSolicitud { get; set; }

        [Required]
        public Paciente Paciente { get; set; }

        [Required]
        public Profesional Profesional { get; set; }

        public String DescripcionCancelacion { get; set; }

    }

}
