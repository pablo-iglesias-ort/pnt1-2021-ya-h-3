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
    public class ProfesionalController : Controller
    {
        private readonly AgendaTurnosContext _context;

        static List<Profesional> profesionales = new List<Profesional>()
        {
            new Profesional()
            {
                Id = Guid.NewGuid(),
                Nombre = "Joel",
                Apellido = "Salum",
                Email = "kskplusst@hotmail.com",
                FechaAlta = new DateTime(2021, 02, 07),
                Password = "45345",
                Telefono = "2222222",
                Direccion = "falsa 123",
                Dni = "235789",                
                Matricula = "123456789",
                HoraInicio = new DateTime(2021, 02, 07, 08, 00, 00),
                Turnos = new List<Turno> (),
            },
                        new Profesional()
            {
                Id = Guid.NewGuid(),
                Nombre = "Matias",
                Apellido = "Pintow",
                Email = "gow.mt@hotmail.com",
                FechaAlta = new DateTime(2021, 01, 07),
                Password = "987654321",
                Telefono = "2222222",
                Direccion = "verdadero 123",
                Dni = "1325789",
                Matricula = "987643211",
                HoraInicio = new DateTime(2021, 02, 07, 08, 30, 00),
                Turnos = new List<Turno> (),
            }
        };

        public ProfesionalController(AgendaTurnosContext context)
        {
            _context = context;
        }

        // GET: Profesional
        public async Task<IActionResult> Index()
        {
            return View(profesionales);
        //    return View(await _context.Profesional.ToListAsync());
        }

        // GET: Profesional/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var profesional = await _context.Profesional
            //  .FirstOrDefaultAsync(m => m.Id == id);
            var profesional = profesionales.FirstOrDefault(m => m.Id == id);
            if (profesional == null)
            {
                return NotFound();
            }

            return View(profesional);
        }

        // GET: Profesional/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesional/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matricula,HoraInicio,HoraFin,Id,Nombre,Apellido,Email,FechaAlta,Password,Telefono,Direccion,Dni,Rol")] Profesional profesional)
        {
            if (ModelState.IsValid)
            {
                profesional.Id = Guid.NewGuid();
                //_context.Add(profesional);
                //await _context.SaveChangesAsync();
                profesionales.Add(profesional);
                return RedirectToAction(nameof(Index));
            }
            return View(profesional);
        }

        // GET: Profesional/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var profesional = await _context.Profesional.FindAsync(id);
            var profesional = profesionales.FirstOrDefault(m => m.Id == id);

            if (profesional == null)
            {
                return NotFound();
            }
            return View(profesional);
        }

        // POST: Profesional/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Matricula,HoraInicio,HoraFin,Id,Nombre,Apellido,Email,FechaAlta,Password,Telefono,Direccion,Dni")] Profesional profesional)
        {
            if (id != profesional.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(profesional);
                    //await _context.SaveChangesAsync();
                    var profesionalExistente = profesionales.FirstOrDefault(m => m.Id == id);
                    profesionalExistente.Matricula = profesional.Matricula;
                    profesionalExistente.HoraInicio = profesional.HoraInicio;
                    profesionalExistente.HoraFin = profesional.HoraFin;
                    profesionalExistente.Nombre = profesional.Nombre;
                    profesionalExistente.Apellido = profesional.Apellido;
                    profesionalExistente.Email = profesional.Email;
                    profesionalExistente.FechaAlta = profesional.FechaAlta;
                    profesionalExistente.Password = profesional.Password;
                    profesionalExistente.Telefono = profesional.Telefono;
                    profesionalExistente.Direccion = profesional.Direccion;
                    profesionalExistente.Dni = profesional.Dni;


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesionalExists(profesional.Id))
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
            return View(profesional);
        }

        // GET: Profesional/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var profesional = await _context.Profesional

            //.FirstOrDefaultAsync(m => m.Id == id);
            var profesional = profesionales.FirstOrDefault(m => m.Id == id);

            if (profesional == null)
            {
                return NotFound();
            }

            return View(profesional);
        }

        // POST: Profesional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //var profesional = await _context.Profesional.FindAsync(id);
            //_context.Profesional.Remove(profesional);
            //await _context.SaveChangesAsync();
            var profesional = profesionales.FirstOrDefault(m => m.Id == id);
            profesionales.Remove(profesional);
            return RedirectToAction(nameof(Index));
        }
        // metodos del profesional
        private bool ProfesionalExists(Guid id)
        {
            return _context.Profesional.Any(e => e.Id == id);
        }
        public void confirmarTurnos(DateTime dia)
        {

        }
        public void listarTurnos(DateTime dia)
        {

        }
        public void cantTurnosAtendidos(DateTime mes)
        {

        }
    }
}
