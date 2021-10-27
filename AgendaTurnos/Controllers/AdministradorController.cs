using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaTurnos.Data;
using AgendaTurnos.Models;

namespace AgendaTurnos.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly AgendaTurnosContext _context;

        //Datos estaticos para probar interface de admin
        static List<Administrador> admins = new List<Administrador>()
        {
            new Administrador()
            {
                //Nombre,Apellido,Email,FechaAlta,Password,Telefono,Direccion,Dni,Rol
                Id = Guid.NewGuid(),
                Nombre = "Joel",
                Apellido = "AAAA",
                Email = "kskpluss32@gmail.com",
                FechaAlta = new DateTime(2020,10,01),
                Password = "123456789",
                Telefono = "11111111",
                Direccion = "sarasa 456",
                Dni = "1234567",
                Rol = "Administrador"
            },
            new Administrador()

            {
                Id = Guid.NewGuid(),
                Nombre = "Matias",
                Apellido = "BBBBB",
                Email = "gow.mt@hotmail.com",
                FechaAlta = new DateTime(2021, 02, 07),
                Password = "987654321",
                Telefono = "2222222",
                Direccion = "falsa 123",
                Dni = "235789",
                Rol = "Administrador"
            },
            new Administrador()
            {
                Id = Guid.NewGuid(),
                Nombre = "Matias",
                Apellido = "Gonzalez",
                Email = "gonzalezsudak@yahoo.com.ar",
                FechaAlta = new DateTime(2020,11,15),
                Password = "asadddadas",
                Telefono = "33333333333",
                Direccion = "alfonsina 789",
                Dni = "78954621",
                Rol = "Administrador"
            }
        };

        public AdministradorController(AgendaTurnosContext context)
        {
            _context = context;
        }

        // GET: Administradors
        public async Task<IActionResult> Index()
        {
            return View(admins);
            //return View(await _context.Administrador.ToListAsync());
        }

        // GET: Administradors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var administrador = await _context.Administrador
                .FirstOrDefaultAsync(m => m.Id == id);*/
            var administrador = admins.FirstOrDefault(m => m.Id == id);

            if (administrador == null)
            {
                return NotFound();
            }

            return View(administrador);
        }

        // GET: Administradors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administradors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Email,FechaAlta,Password,Telefono,Direccion,Dni,Rol")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                administrador.Id = Guid.NewGuid();
                // _context.Add(administrador);
                // await _context.SaveChangesAsync();
                admins.Add(administrador);
                return RedirectToAction(nameof(Index));
            }
            return View(administrador);
        }

        // GET: Administradors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrador = admins.FirstOrDefault(e => e.Id == id);
            if (administrador == null)
            {
                return NotFound();
            }
            return View(administrador);
            /*
            var administrador = await _context.Administrador.FindAsync(id);
            if (administrador == null)
            {
                return NotFound();
            }
            return View(administrador);*/
        }

        // POST: Administradors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Apellido,Email,FechaAlta,Password,Telefono,Direccion,Dni,Rol")] Administrador administrador)
        {
            if (id != administrador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {   //Nombre,Apellido,Email,FechaAlta,Password,Telefono,Direccion,Dni,Rol

                    /* _context.Update(administrador);
                     await _context.SaveChangesAsync();*/
                    var usuarioExistente = admins.FirstOrDefault(e => e.Id == id);
                    usuarioExistente.Nombre = administrador.Nombre;
                    usuarioExistente.Apellido = administrador.Email;
                    usuarioExistente.Email = administrador.Email;
                    usuarioExistente.FechaAlta = administrador.FechaAlta;
                    usuarioExistente.Password = administrador.Password;
                    usuarioExistente.Telefono = administrador.Telefono;
                    usuarioExistente.Direccion = administrador.Direccion;
                    usuarioExistente.Dni = administrador.Dni;
                    usuarioExistente.Rol = administrador.Rol;


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministradorExists(administrador.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(administrador);
        }

        // GET: Administradors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /* var administrador = await _context.Administrador
                 .FirstOrDefaultAsync(m => m.Id == id);*/
            var administrador = admins.FirstOrDefault(m => m.Id == id);
            if (administrador == null)
            {
                return NotFound();
            }

            return View(administrador);
        }

        // POST: Administradors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            /*var administrador = await _context.Administrador.FindAsync(id);
            _context.Administrador.Remove(administrador);
            await _context.SaveChangesAsync();*/
            var administrador = admins.FirstOrDefault(m => m.Id == id);
            admins.Remove(administrador);
            return RedirectToAction(nameof(Index));
        }
        // aca van los metodos de Administrador:
        private bool AdministradorExists(Guid id)
        {
            return admins.Exists(e => e.Id == id);
            //return _context.Administrador.Any(e => e.Id == id);
        }
        public void altaProfesional()
        {

        }

        public void confirmarTurnos()
        {
            //por cada profesional y por dia

        }
        public void cancelarTurno()
        {
            //descripcion si o si

        }
        public void altaPrestacion()
        {

        }
        public void altaAdmin()
        {

        }
        public void deshabilitarAdmin()
        {

        }
        public void deshabilitarProfesional()
        {

        }
    }
}
