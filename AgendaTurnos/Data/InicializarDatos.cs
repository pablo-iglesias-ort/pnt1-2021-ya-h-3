using AgendaTurnos.Controllers;
using AgendaTurnos.Data;
using AgendaTurnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaTurnos.Data
{
	public static class InicializacionDeDatos
	{
		public static readonly ISeguridad seguridad = new SeguridadBasica();
		public static void Inicializar(AgendaTurnosContext context)
		{

			context.Database.EnsureCreated();


			using (var transaccion = context.Database.BeginTransaction())
			{
				try
				{
					if (context.Usuario.Any())
					{
						// Si ya hay datos aqui, significa que ya los hemos creado previamente
						return;
					}


					// 1er paciente
					var nuevoPaciente1 = new Paciente()
					{
							Id = Guid.NewGuid(),
							Apellido = "Salta",
							Nombre = "Paloma",
							Email = "paciente@paciente.com",
							Dni = "25689741",
							FechaAlta = DateTime.Now,
							Password = seguridad.EncriptarPass("paciente"),
							Telefono = "1144889966",
							Direccion = "av directorio 580 piso 4b",
							ObraSocial = "Particular",
							Turnos = new List<Turno>(),
						};
						context.Paciente.Add(nuevoPaciente1);
						context.SaveChanges();

						// 2do paciente
						var nuevoPaciente2 = new Paciente()
						{
							Id = Guid.NewGuid(),
							Apellido = "Argento",
							Nombre = "Pepe",
							Email = "paciente2@paciente.com",
							Dni = "20145698",
							FechaAlta = DateTime.Now,
							Password = seguridad.EncriptarPass("paciente2"),
							Telefono = "1158963214",
							Direccion = "varela 649",
							ObraSocial = "OSPICAL",
							Turnos = new List<Turno>(),
						};
						context.Paciente.Add(nuevoPaciente2);
						context.SaveChanges();

					//1er Prestacion
					var nuevaPrestacion1 = new Prestacion()
					{
						Id = Guid.NewGuid(),
						Nombre = "Oftalmologia",
						Descripcion = "Oculista",
						DuracionMinutos = 15,
						Precio = 800f,
					};
					context.Prestacion.Add(nuevaPrestacion1);
					context.SaveChanges();

					//2da Prestacion
					var nuevaPrestacion2 = new Prestacion()
					{
						Id = Guid.NewGuid(),
						Nombre = "Odontologia",
						Descripcion = "Dentista",
						DuracionMinutos = 30,
						Precio = 900f,
					};
					context.Prestacion.Add(nuevaPrestacion2);
					context.SaveChanges();

					// 1er profesional
					var nuevoProfesional1 = new Profesional()
						{
							Id = Guid.NewGuid(),
							Nombre = "Joel",
							Apellido = "Salum",
							Email = "profesional@profesional.com",
							FechaAlta = DateTime.Now,
							Password = seguridad.EncriptarPass("profesional"),
							Telefono = "11111111",
							Direccion = "sarasa 456",
							Dni = "1234567",
							Matricula = "123456789",
							HoraInicio = new DateTime(1900, 01, 01, 08, 30, 00),
							HoraFin = new DateTime(1900, 01, 01, 21, 00, 00),
							Turnos = new List<Turno>(),
							PrestacionId = nuevaPrestacion1.Id,
						};
						context.Profesional.Add(nuevoProfesional1);
						context.SaveChanges();

						// 2do profesional
						var nuevoProfesional2 = new Profesional()
						{
							Id = Guid.NewGuid(),
							Nombre = "Matias",
							Apellido = "Pintow",
							Email = "profesional2@profesional.com",
							FechaAlta = DateTime.Now,
							Password = seguridad.EncriptarPass("profesional2"),
							Telefono = "2222222",
							Direccion = "verdadero 456",
							Dni = "1325789",
							Matricula = "987643211",
							HoraInicio = new DateTime(1900, 01, 01, 08, 30, 00),
							HoraFin = new DateTime(1900, 01, 01, 13, 30, 00),
							Turnos = new List<Turno>(),
							PrestacionId = nuevaPrestacion2.Id,
						};
						context.Profesional.Add(nuevoProfesional2);
						context.SaveChanges();
				
						//1er Admin
						var nuevoAdmin1 = new Administrador()
						{
							Id = Guid.NewGuid(),
							Nombre = "Matias",
							Apellido = "Gonzalez",
							Email = "administrador@administrador.com",
							FechaAlta = DateTime.Now,
							Password = seguridad.EncriptarPass("administrador"),
							Telefono = "1125897463",
							Direccion = "calle falsa 123",
							Dni = "35789645",
						};
						context.Administrador.Add(nuevoAdmin1);
						context.SaveChanges();

						//2do Admin
						var nuevoAdmin2 = new Administrador()
						{
							Id = Guid.NewGuid(),
							Nombre = "Homero",
							Apellido = "Simpson",
							Email = "administrador2@administrador.com",
							FechaAlta = DateTime.Now,
							Password = seguridad.EncriptarPass("administrador2"),
							Telefono = "1169874523",
							Direccion = "Siempreviva 742",
							Dni = "15236478",
						};
						context.Administrador.Add(nuevoAdmin2);
						context.SaveChanges();


					//Creamos un turno para Pepe
					var nuevoTurno = new Turno()
					{
						Id = Guid.NewGuid(),
						Fecha = DateTime.Now.Date,
						Activo = true,
						Confirmado = true,
						ProfesionalId = nuevoProfesional2.Id,
						PacienteId = nuevoPaciente2.Id,
						Profesional = nuevoProfesional2,
						Paciente = nuevoPaciente2,
					};

						context.Turno.Add(nuevoTurno);
						context.SaveChanges();

					var nuevoTurno2 = new Turno()
					{
						Id = Guid.NewGuid(),
						Fecha = DateTime.Now.Date,
						Activo = true,
						Confirmado = false,
						ProfesionalId = nuevoProfesional1.Id,
						PacienteId = nuevoPaciente1.Id,
						Profesional = nuevoProfesional1,
						Paciente = nuevoPaciente1,
					};

					context.Turno.Add(nuevoTurno2);
					context.SaveChanges();

					var nuevoTurno3 = new Turno()
					{
						Id = Guid.NewGuid(),
						Fecha = DateTime.Now.Date,
						Activo = true,
						Confirmado = false,
						ProfesionalId = nuevoProfesional1.Id,
						PacienteId = nuevoPaciente1.Id,
						Profesional = nuevoProfesional1,
						Paciente = nuevoPaciente1,
					};

					context.Turno.Add(nuevoTurno3);
					context.SaveChanges();

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