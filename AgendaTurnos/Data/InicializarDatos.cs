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
		public static void Inicializar(AgendaTurnosContext context)
		{
			context.Database.EnsureCreated();

			using (var transaccion = context.Database.BeginTransaction())
			{
				try
				{
					if (!context.Paciente.Any())
					{
						// 1er paciente
						var nuevoPaciente = new Paciente()
						{
							Id = Guid.NewGuid(),
							Apellido = "Salta",
							Nombre = "Paloma",
							Email = "psalta@gmail.com",
							FechaAlta = DateTime.Now.Date,
							Dni = "25689741",
							Password = "paloma1234",
							Telefono = "1144889966",
							Direccion = "av directorio 580 piso 4b",
							ObraSocial = "Particular",
							Turnos = new List<Turno>(),
						};
						context.Paciente.Add(nuevoPaciente);
						context.SaveChanges();

						// 2do paciente
						nuevoPaciente = new Paciente()
						{
							Id = Guid.NewGuid(),
							Apellido = "Argento",
							Nombre = "Pepe",
							Email = "pepeargento@yahoo.com.ar",
							FechaAlta = DateTime.Now.Date,
							Dni = "20145698",
							Password = "racingCampeon",
							Telefono = "1158963214",
							Direccion = "varela 649",
							ObraSocial = "OSPICAL",
							Turnos = new List<Turno>(),
						};
						context.Paciente.Add(nuevoPaciente);
						context.SaveChanges();

					}


					if (!context.Profesional.Any())
					{
						// 1er profesional
						var nuevoProfesional = new Profesional()
						{
							Id = Guid.NewGuid(),
							Nombre = "Joel",
							Apellido = "Salum",
							Email = "kskplusst@hotmail.com",
							FechaAlta = DateTime.Now.Date,
							Password = "87aB2365",
							Telefono = "11111111",
							Direccion = "sarasa 456",
							Dni = "1234567",
							Matricula = "123456789",
							HoraInicio = new DateTime(2021, 02, 07, 08, 30, 00),
							HoraFin = new DateTime(2021, 02, 07, 21, 00, 00),
							Turnos = new List<Turno>(),
						};
						context.Profesional.Add(nuevoProfesional);
						context.SaveChanges();

						// 2do profesional
						nuevoProfesional = new Profesional()
						{
							Id = Guid.NewGuid(),
							Nombre = "Matias",
							Apellido = "Pintow",
							Email = "gow.mt@hotmail.com",
							FechaAlta = DateTime.Now.Date,
							Password = "987654321",
							Telefono = "2222222",
							Direccion = "verdadero 456",
							Dni = "1325789",
							Matricula = "987643211",
							HoraInicio = new DateTime(2021, 02, 07, 08, 30, 00),
							HoraFin = new DateTime(2021, 02, 07, 13, 30, 00),
							Turnos = new List<Turno>(),
						};
						context.Profesional.Add(nuevoProfesional);
						context.SaveChanges();
					}

					if (!context.Administrador.Any())
                    {
						//1er Admin
						var nuevoAdmin = new Administrador()
						{
							Nombre = "Matias",
							Apellido = "Gonzalez",
							Email = "msgonzalezsudak@yaoo.com.ar",
							FechaAlta = DateTime.Now.Date,
							Password = "ABC123qwerty",
							Telefono = "1125897463",
							Direccion = "calle falsa 123",
							Dni = "35789645",
						};
						context.Administrador.Add(nuevoAdmin);
						context.SaveChanges();

						//2do Admin
						nuevoAdmin = new Administrador()
						{
							Nombre = "Homero",
							Apellido = "Simpsons",
							Email = "amantedelacomida53@aol.com",
							FechaAlta = DateTime.Now.Date,
							Password = "marchIlove1",
							Telefono = "1169874523",
							Direccion = "Siempreviva 742",
							Dni = "15236478",
						};
						context.Administrador.Add(nuevoAdmin);
						context.SaveChanges();
					}

					if (!context.Prestacion.Any())
					{
						//1er Prestacion
						var nuevaPrestacion = new Prestacion()
						{
							Nombre = "Oftalmologia",
							Descripcion = "Oculista",
							DuracionMinutos = 15,
							Precio = 800f,
						};
						context.Prestacion.Add(nuevaPrestacion);
						context.SaveChanges();

						//2da Prestacion
						nuevaPrestacion = new Prestacion()
						{
							Nombre = "Odontologia",
							Descripcion = "Dentista",
							DuracionMinutos = 30,
							Precio = 900f,
						};
						context.Prestacion.Add(nuevaPrestacion);
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