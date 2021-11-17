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

namespace AgendaTurnos.Controllers
{
    [Authorize(Roles = "Profesional, Administrador")]
    public class ProfesionalController : Controller
    {
        private readonly AgendaTurnosContext _context;
        private readonly ISeguridad seguridad = new SeguridadBasica();

        public ProfesionalController(AgendaTurnosContext context)
        {
            _context = context;
        }

        // GET: Profesional
        [Authorize(Roles = "Administrador")]
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
        public async Task<IActionResult> Create(Profesional profesional, string pass, string prestacion)
        {
            Guid id = Guid.Parse(User.FindFirst(ClaimTypes.Name).Value);
            var prest = _context.Prestacion.FirstOrDefault(p => p.Nombre == prestacion);
            if (prestacion == null)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                if (seguridad.ValidarPass(pass))
                {
                    profesional.Password = seguridad.EncriptarPass(pass);
                    profesional.Id = Guid.NewGuid();
                    profesional.FechaAlta = DateTime.Now.Date;
                    //hora de inicio con fecha fija
                    var horaInicio = profesional.HoraInicio;
                    var aux = new DateTime(1900, 01, 01, horaInicio.Hour, horaInicio.Minute, 0);
                    profesional.HoraInicio = aux;
                    //hora fin con fecha fija
                    var horaFin = profesional.HoraFin;
                    aux = new DateTime(1900, 01, 01, horaFin.Hour, horaFin.Minute, 0);
                    profesional.HoraFin = aux;
                    profesional.PrestacionId = prest.Id;
                    //agrego para subir a la base
                    _context.Add(profesional);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(nameof(Usuario.Password), "La contraseña es incorrecta");
                }

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
        public async Task<IActionResult> Edit(Guid id, Profesional profesional, string pass)
        {
            if (id != profesional.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    profesional.Password = seguridad.EncriptarPass(pass);
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
            var existe = _context.Profesional.Any(e => e.Id == id);
            return existe;
        }

        //Lista los turnos que tiene el profesional
        [Authorize(Roles = "Profesional")]
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
                                .Turnos.Where(t => t.Activo);
            ViewData["ProfesionalId"] = id;
            return View(turnos);
        }
    }
}
