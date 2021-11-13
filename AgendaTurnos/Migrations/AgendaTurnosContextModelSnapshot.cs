﻿// <auto-generated />
using System;
using AgendaTurnos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AgendaTurnos.Migrations
{
    [DbContext(typeof(AgendaTurnosContext))]
    partial class AgendaTurnosContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.20");

            modelBuilder.Entity("AgendaTurnos.Models.Prestacion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("DuracionMinutos")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Precio")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Prestacion");
                });

            modelBuilder.Entity("AgendaTurnos.Models.Turno", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Confirmado")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescripcionCancelacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaSolicitud")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PacienteId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProfesionalId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.HasIndex("ProfesionalId");

                    b.ToTable("Turno");
                });

            modelBuilder.Entity("AgendaTurnos.Models.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Password")
                        .HasColumnType("BLOB");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuario");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Usuario");
                });

            modelBuilder.Entity("AgendaTurnos.Models.Administrador", b =>
                {
                    b.HasBaseType("AgendaTurnos.Models.Usuario");

                    b.HasDiscriminator().HasValue("Administrador");
                });

            modelBuilder.Entity("AgendaTurnos.Models.Paciente", b =>
                {
                    b.HasBaseType("AgendaTurnos.Models.Usuario");

                    b.Property<string>("ObraSocial")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Paciente");
                });

            modelBuilder.Entity("AgendaTurnos.Models.Profesional", b =>
                {
                    b.HasBaseType("AgendaTurnos.Models.Usuario");

                    b.Property<DateTime>("HoraFin")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("HoraInicio")
                        .HasColumnType("TEXT");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PrestacionId")
                        .HasColumnType("TEXT");

                    b.HasIndex("PrestacionId");

                    b.HasDiscriminator().HasValue("Profesional");
                });

            modelBuilder.Entity("AgendaTurnos.Models.Turno", b =>
                {
                    b.HasOne("AgendaTurnos.Models.Paciente", "Paciente")
                        .WithMany("Turnos")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgendaTurnos.Models.Profesional", "Profesional")
                        .WithMany("Turnos")
                        .HasForeignKey("ProfesionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AgendaTurnos.Models.Profesional", b =>
                {
                    b.HasOne("AgendaTurnos.Models.Prestacion", "Prestacion")
                        .WithMany("Profesionales")
                        .HasForeignKey("PrestacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
