using AgendaTurnos.Data;
using AgendaTurnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Entity_Framework.Data
{
	public static class InicializacionDeDatos
	{
		public static void Inicializar(AgendaTurnosContext context)
		{
			context.Database.EnsureCreated();

			using (var transaccion = context.Database.BeginTransaction())
			{
				try
				{
					if (!context.Paciente.Any())
					{
						var nuevoPaciente = new Paciente();
						nuevoPaciente.Id = Guid.NewGuid();
						nuevoPaciente.Apellido = "AAAA";
						nuevoPaciente.Nombre = "Joel";
						nuevoPaciente.Email = "kskpluss32@gmail.com";
						nuevoPaciente.FechaAlta = DateTime.Now.Date;
						nuevoPaciente.Dni = "1234567";
						nuevoPaciente.Password = "123456789";
						nuevoPaciente.Telefono = "11111111";
						nuevoPaciente.Direccion = "sarasa 456";
						nuevoPaciente.ObraSocial = "swiss medical";
						nuevoPaciente.Turnos = new List<Turno>();
						context.Paciente.Add(nuevoPaciente);
						context.SaveChanges();

					}
					if (!context.Profesional.Any())
					{
						var nuevoProfesional = new Profesional();
						nuevoProfesional.Id = Guid.NewGuid();
						nuevoProfesional.Apellido = "Salum";
						nuevoProfesional.Nombre = "Joel";
						nuevoProfesional.Email = "kskplusst@hotmail.com@gmail.com";
						nuevoProfesional.FechaAlta = DateTime.Now.Date;
						nuevoProfesional.Dni = "1234567";
						nuevoProfesional.Password = "123456789";
						nuevoProfesional.Telefono = "11111111";
						nuevoProfesional.Direccion = "sarasa 456";
						nuevoProfesional.Matricula = "123456789";
						nuevoProfesional.HoraInicio = new DateTime(2021, 02, 07, 08, 00, 00);
						nuevoProfesional.Turnos = new List<Turno>();
						nuevoProfesional.HoraFin = new DateTime(2021, 02, 07, 21, 00, 00);
						context.Profesional.Add(nuevoProfesional);
						context.SaveChanges();

					}



					transaccion.Commit();
				}
				catch
				{
					transaccion.Rollback();
				}
			}

		}
	}
}