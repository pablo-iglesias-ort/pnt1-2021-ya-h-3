using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaTurnos.Models

{
    public class Profesional : Usuario
    {
        [Required]
        public String Matricula { get; set; }
        
        [Required]
        public DateTime HoraInicio { get; set; }
        
        [Required]
        public DateTime HoraFin { get; set; }


        //Relacion de Entidades
        [ForeignKey(nameof(Prestacion))]
        public Guid PrestacionId { get; set; }
        public Prestacion Prestacion { get; set; }
        public List<Turno> Turnos { get; set; }
    }
}