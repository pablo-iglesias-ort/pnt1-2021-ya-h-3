using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AgendaTurnos.Models
{
    public class Paciente : Usuario
    {
    
        [Required]
        public String ObraSocial { get; set; }


        public List<Turno> Turnos { get; set; }

        public void generarTurno() { 
        
        }
        public void cancelarTurno()
        {

        }
        public void verTurno()
        {

        }
        public void modDatosPersonales()
        {

        }

    }
}