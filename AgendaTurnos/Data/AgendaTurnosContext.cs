using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AgendaTurnos.Models;

namespace AgendaTurnos.Data
{
    public class AgendaTurnosContext : DbContext
    {
        public AgendaTurnosContext (DbContextOptions<AgendaTurnosContext> options)
            : base(options)
        {
        }

        public DbSet<AgendaTurnos.Models.Usuario> Usuarios { get; set; }

        public DbSet<AgendaTurnos.Models.Administrador> Administradores { get; set; }

        public DbSet<AgendaTurnos.Models.Formulario> Formularios { get; set; }

        public DbSet<AgendaTurnos.Models.Paciente> Pacientes { get; set; }

        public DbSet<AgendaTurnos.Models.Profesional> Profesionales { get; set; }

        public DbSet<AgendaTurnos.Models.Prestacion> Prestaciones { get; set; }

        public DbSet<AgendaTurnos.Models.Turno> Turnos { get; set; }

        public DbSet<AgendaTurnos.Models.ProfesionalPaciente> ProfesionalesPacientes { get; set; }


    }
}
