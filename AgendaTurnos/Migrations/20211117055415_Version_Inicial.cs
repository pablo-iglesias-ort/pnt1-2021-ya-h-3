using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgendaTurnos.Migrations
{
    public partial class Version_Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prestacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Descripcion = table.Column<string>(nullable: false),
                    DuracionMinutos = table.Column<int>(nullable: false),
                    Precio = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Apellido = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    Password = table.Column<byte[]>(nullable: true),
                    Telefono = table.Column<string>(nullable: false),
                    Direccion = table.Column<string>(nullable: false),
                    Dni = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ObraSocial = table.Column<string>(nullable: true),
                    Matricula = table.Column<string>(nullable: true),
                    HoraInicio = table.Column<DateTime>(nullable: true),
                    HoraFin = table.Column<DateTime>(nullable: true),
                    PrestacionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Prestacion_PrestacionId",
                        column: x => x.PrestacionId,
                        principalTable: "Prestacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turno",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Confirmado = table.Column<bool>(nullable: false),
                    Activo = table.Column<bool>(nullable: false),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    DescripcionCancelacion = table.Column<string>(nullable: true),
                    PacienteId = table.Column<Guid>(nullable: false),
                    ProfesionalId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turno_Usuario_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turno_Usuario_ProfesionalId",
                        column: x => x.ProfesionalId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turno_PacienteId",
                table: "Turno",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Turno_ProfesionalId",
                table: "Turno",
                column: "ProfesionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PrestacionId",
                table: "Usuario",
                column: "PrestacionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Prestacion");
        }
    }
}
