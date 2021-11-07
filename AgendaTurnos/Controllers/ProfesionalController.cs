using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaTurnos.Data;
using AgendaTurnos.Models;
using Microsoft.AspNetCore.Authorization;

namespace AgendaTurnos.Controllers
{
    [Authorize(Roles = "Profesional")]
    public class ProfesionalController : Controller
    {
        private readonly AgendaTurnosContext _context;

        public ProfesionalController(AgendaTurnosContext context)
        {
            _context = context;
        }

        // GET: Profesional
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profesional.ToListAsync());
        }

        // GET: Profesional/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesional
              .FirstOrDefaultAsync(m => m.Id == id);
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
                //profesional.FechaAlta = DateTime.Now.Date;
                _context.Add(profesional);
                await _context.SaveChangesAsync();
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

            var profesional = await _context.Profesional.FindAsync(id);

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
                    _context.Update(profesional);
                    await _context.SaveChangesAsync();
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

            var profesional = await _context.Profesional.FirstOrDefaultAsync(m => m.Id == id);

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
            var profesional = await _context.Profesional.FindAsync(id);
            _context.Profesional.Remove(profesional);
            await _context.SaveChangesAsync();
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
        public IActionResult Turnos(Guid id)
        {
            if (!ProfesionalExists(id))
            {
                return NotFound();
            }

            var turnos = _context.Profesional
                                .Include(profesional => profesional.Turnos)
                                    .ThenInclude(turno => turno.Paciente)
                                .FirstOrDefault(e => e.Id == id)
                                .Turnos;
            ViewData["ProfesionalId"] = id;
            return View(turnos);
        }
    }
}
