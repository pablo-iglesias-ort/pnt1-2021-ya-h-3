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
    [Authorize]
    public class TurnoController : Controller
    {
        private readonly AgendaTurnosContext _context;

        public TurnoController(AgendaTurnosContext context)
        {
            _context = context;
        }

        // GET: Turno
        public async Task<IActionResult> Index()
        {
            return View(await _context.Turno.ToListAsync());
        }

        // GET: Turno/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno.FirstOrDefaultAsync(m => m.Id == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
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

            var turno = await _context.Turno.FindAsync(id);

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
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
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

            var turno = await _context.Turno.FirstOrDefaultAsync(m => m.Id == id);
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
            var turno = await _context.Turno.FindAsync(id);
            _context.Turno.Remove(turno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(Guid id)
        {
            return _context.Turno.Any(e => e.Id == id);
        }
   
    
    }
}
