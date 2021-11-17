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
        public async Task<IActionResult> Create(DateTime fechaYhora, Guid profesionalId)
        {
            Turno turno = new Turno();
            turno.Id = Guid.NewGuid();
            //var turnoLibre = _context.Turno.FirstOrDefault(t => t.FechaSolicitud == fechaYhora);
            turno.Fecha = DateTime.Now;
            turno.Confirmado = false;
            turno.Activo = true;
            turno.FechaSolicitud = fechaYhora;
            turno.PacienteId = Guid.Parse(User.FindFirst(ClaimTypes.Name).Value);
            turno.ProfesionalId = profesionalId;
            
            _context.Add(turno);
                await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Paciente.Turnos), nameof(Paciente), new { id = turno.PacienteId });
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
        [Authorize(Roles = "Profesional,Administrador,Paciente")]
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

                    return RedirectToAction(nameof(Profesional.Turnos), nameof(Profesional), new { id = id });
                }
                else if (User.IsInRole(Rol.Paciente.ToString()))
                {
                    Guid id = Guid.Parse(User.FindFirst(ClaimTypes.Name).Value);
                    return RedirectToAction(nameof(Paciente.Turnos), nameof(Paciente), new { id = id });
                    
                }
                else {
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
            Guid id = Guid.Parse(User.FindFirst(ClaimTypes.Name).Value);

            if (_context.Turno.Where(p => p.PacienteId == id).Where(p => p.Activo == true).Any())
            {
                return RedirectToAction(nameof(Paciente.Turnos), nameof(Paciente), new { id = id });
            }
            return View(_context.Prestacion.ToList());

        }

        public IActionResult SeleccionarProfesional(Guid id)
        {
            var profesionales = _context.Profesional.
                                Include(profesional => profesional.Prestacion).
                ToList().Where(p => p.PrestacionId == id);
                TempData["PrestacionId"] = id;
            
            return View(profesionales);
        }

        public IActionResult SeleccionarFecha(Profesional profesional)
        {
            ViewBag.profesional = profesional.Nombre + " " + profesional.Apellido;
            ViewBag.profesionalId= profesional.Id;
            ViewBag.horaInicio = profesional.HoraInicio.Hour +" : " +  profesional.HoraInicio.Minute;
            ViewBag.horaFin = profesional.HoraFin.Hour +" : "+ profesional.HoraFin.Minute;
            
         return View();
        }

        public IActionResult ConfirmarTurno(Turno turno, DateTime fecha, DateTime hora)
        {
            var horaTurno = new DateTime(1900, 01, 01, hora.Hour, hora.Minute, 0);
            var fechaTurno = new DateTime(fecha.Year, fecha.Month, fecha.Day, 0, 0, 0, 1);
            var fechaHoy = DateTime.Now;
            var fechaMas7 = fechaHoy.AddDays(7);
            turno.Profesional = _context.Profesional.Include(p => p.Prestacion)
                            .FirstOrDefault(p => p.Id == turno.ProfesionalId);

            if ((horaTurno >= turno.Profesional.HoraInicio) && (horaTurno <= turno.Profesional.HoraFin)) {
                if ((fechaTurno >= fechaHoy) && (fechaTurno <= fechaMas7)) {
                    var fechaYhora = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora.Hour, hora.Minute, 0);
                    if (!(_context.Turno.Any(t => t.FechaSolicitud == fechaYhora))) {
                        var pacienteId = turno.PacienteId;
                        turno.FechaSolicitud = horaTurno;
                        return View(turno);
                    }else
                    {
                        ViewData["ErrorMessage"] = "Ya hay un turno tomado en ese dia y horario.";
                        return RedirectToAction(nameof(SeleccionarFecha));
                    }
                } else
                {
                    ViewData["ErrorMessage"] = "El dia del turno tiene que estar entre hoy y 7 dias mas";
                    return RedirectToAction(nameof(SeleccionarFecha));
                }
            } else
            {
                ViewData["ErrorMessage"] = "Tiene que elegir un horario en que el profesional trabaje";
                return RedirectToAction(nameof(SeleccionarFecha));
            }
            return RedirectToAction(nameof(SeleccionarFecha));
            //return View(SeleccionarFecha(turno.Profesional));
        }

    }
}
