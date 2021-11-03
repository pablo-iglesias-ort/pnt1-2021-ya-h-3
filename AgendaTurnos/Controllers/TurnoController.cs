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
    public class TurnoController : Controller
    {
        private readonly AgendaTurnosContext _context;

        static List<Turno> turnos = new List<Turno>()
        {
         

        };

        public TurnoController(AgendaTurnosContext context)
        {
            _context = context;
        }

        // GET: Turno
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Turno.ToListAsync());
            return View(turnos);
        }

        // GET: Turno/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var turno = await _context.Turno.FirstOrDefaultAsync(m => m.Id == id);
            var turno = turnos.FirstOrDefault(m => m.Id == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turnos);
        }

        // GET: Turno/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Turno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Fecha, Confirmado, Activo, FechaSolicitud, DescripcionCancelacion")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                turno.Id = Guid.NewGuid();
                _context.Add(turno);
                await _context.SaveChangesAsync();
                //turnos.Add(turno);
                return RedirectToAction(nameof(Index));
            }
            return View(turno);
        }

        // GET: Turno/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var turno = await _context.Turno.FindAsync(id);
            var turno = turnos.FirstOrDefault(m => m.Id == id);

            if (turno == null)
            {
                return NotFound();
            }
            return View(turno);
        }

        // POST: Turno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Fecha, Confirmado, Activo, FechaSolicitud, DescripcionCancelacion")] Turno turno)
        {
            if (id != turno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(turno);
                    //await _context.SaveChangesAsync();

                    var turnoExistente = turnos.FirstOrDefault(e => e.Id == id);
                    turnoExistente.Fecha = turno.Fecha;
                    turnoExistente.Confirmado = turno.Confirmado;
                    turnoExistente.Activo = turno.Activo;
                    turnoExistente.FechaSolicitud = turno.FechaSolicitud;
                    turnoExistente.DescripcionCancelacion = turno.DescripcionCancelacion;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.Id))
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
            return View(turno);
        }

        // GET: Turno/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var turno = await _context.Turno.FirstOrDefaultAsync(m => m.Id == id);
            var turno = turnos.FirstOrDefault(e => e.Id == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // POST: Turno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            /*var turno = await _context.Turno.FindAsync(id);
            _context.Turno.Remove(turno);
            await _context.SaveChangesAsync();*/
            var turno = turnos.FirstOrDefault(e => e.Id == id);
            turnos.Remove(turno);
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(Guid id)
        {
            return _context.Turno.Any(e => e.Id == id);
        }
   
    
    }
}
