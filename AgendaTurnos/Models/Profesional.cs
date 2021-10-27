using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AgendaTurnos.Models

{
    public class Profesional : Usuario
    {
        [Required]
        public String Matricula { get; set; }
        
        [Required]
        public Prestacion Prestacion { get; set; }
        
        [Required]
        public DateTime HoraInicio { get; set; }
        
        [Required]
        public DateTime HoraFin { get; set; }

        public List<Turno> Turnos { get; set; }


    }
}