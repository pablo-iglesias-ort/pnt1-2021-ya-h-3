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
using System.Security.Claims;
using System.Collections;

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
         [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            var turno = _context.Turno.Where(t => t.Activo);
            return View(turno);
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
        [Authorize(Roles = "Profesional,Administrador")]
        public async Task<IActionResult> Edit(Turno turno, String accion)

        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(accion == "cancelacion")
                    {
                        turno.Activo = false;
                        turno.Confirmado = false;
                      
                    } else if( accion == "confirmacion")
                    {
                        turno.Confirmado = true;
                    }else if(accion == "atendido")
                    {
                        turno.Activo = false;
                    }

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

                if (User.IsInRole(Rol.Profesional.ToString()))
                {
                    Guid id = Guid.Parse(User.FindFirst(ClaimTypes.Name).Value);

                    return RedirectToAction(nameof(Profesional.Turnos), nameof(Profesional), new {id = id } );
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
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

        public IActionResult SolicitarTurno()
        {
            return View(_context.Prestacion.ToList());
        }

        [HttpPost, ActionName("SolicitarTurno")]
        [ValidateAntiForgeryToken]
        public IActionResult SolicitarTurno(Prestacion prestacion, Guid id)
        {
            var prest = prestacion.Id;
            return View(_context.Prestacion.ToList());
        }

        public IActionResult SeleccionarProfesional(Guid id)
        {
            var profesionales = _context.Profesional.
                                Include(profesional => profesional.Prestacion).
                ToList().Where(p => p.PrestacionId == id);
                                                       
            return View(profesionales);
        }

    }
}
