using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaTurnos.Models
{
    public class ProfesionalPaciente
    {
        [Key]
        public Guid Id { get; set; }

        //relacion con otras entidades.

        [ForeignKey(nameof(Paciente))]

        public Guid PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        [ForeignKey(nameof(Profesional))]
        public Guid ProfesionaId { get; set; }
        public Profesional Profesional { get; set; }


    }
}
