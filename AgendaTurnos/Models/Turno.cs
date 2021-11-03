using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaTurnos.Models
{
    public class Turno
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public Boolean Confirmado { get; set; } //defecto false

        [Required]
        public Boolean Activo { get; set; }

        [Required]
        public DateTime FechaSolicitud { get; set; }

        public String DescripcionCancelacion { get; set; }

        //Relacion con otras entidades

        [ForeignKey(nameof(Paciente))]
        public Guid PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        

        [ForeignKey(nameof(Profesional))]
        public Guid ProfesionalId { get; set; }
        public Profesional Profesional { get; set; }

    }

}
