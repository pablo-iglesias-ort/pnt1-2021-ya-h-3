using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AgendaTurnos.Models
{
    public class Prestacion
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public String Nombre { get; set; }

        [Required]
        public String Descripcion { get; set; }

        [Required]
        public int DuracionMinutos { get; set; }

        [Required]
        public float Precio { get; set; }

        [Required]
        public List<Profesional> Profesionales { get; set; }

    }
}