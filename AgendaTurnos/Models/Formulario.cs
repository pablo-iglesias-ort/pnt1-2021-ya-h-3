﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AgendaTurnos.Models
{
    public class Formulario
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Por favor, ingresar un email valido.")]
        public string Email { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        public bool Leido { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Mensaje { get; set; }

        [Required]
        public Usuario Usuario { get; set; }


    }
}