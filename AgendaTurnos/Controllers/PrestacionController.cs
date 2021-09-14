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
    public class PrestacionController : Controller
    {
       

        private readonly AgendaTurnosContext _context;

        static List<Prestacion> prestaciones = new List<Prestacion>()
        {
            new Prestacion()
        {
            Id =  Guid.NewGuid(),
            Nombre = "Odontologia",
            Descripcion = "Atención odontológica",
            DuracionMinutos = 5,
            Precio = 10f,
            Profesionales = new List<Profesional>()

            }
        };



        public PrestacionController(AgendaTurnosContext context)
        {
            _context = context;
        }

        // GET: Prestacion
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Prestacion.ToListAsync());
            return View(prestaciones);
        }

        // GET: Prestacion/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var prestacion = await _context.Prestacion
            //  .FirstOrDefaultAsync(m => m.Id == id);
            var prestacion = prestaciones.FirstOrDefault(m => m.Id == id);

            if (prestacion == null)
            {
                return NotFound();
            }

            return View(prestacion);
        }

        // GET: Prestacion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prestacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,DuracionMinutos,Precio")] Prestacion prestacion)
        {
            if (ModelState.IsValid)
            {
                prestacion.Id = Guid.NewGuid();
                // _context.Add(prestacion);
                //await _context.SaveChangesAsync();
                prestaciones.Add(prestacion);
                return RedirectToAction(nameof(Index));
            }
            return View(prestacion);
        }

        // GET: Prestacion/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var prestacion = await _context.Prestacion.FindAsync(id);
            var prestacion = prestaciones.FirstOrDefault(m => m.Id == id);
            if (prestacion == null)
            {
                return NotFound();
            }
            return View(prestacion);
        }

        // POST: Prestacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Descripcion,DuracionMinutos,Precio")] Prestacion prestacion)
        {
            if (id != prestacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(prestacion);
                    //await _context.SaveChangesAsync();
                    var prestacionExistentes = prestaciones.FirstOrDefault(m => m.Id == id);
                    prestacionExistentes.Nombre = prestacion.Nombre;
                    prestacionExistentes.Descripcion = prestacion.Descripcion;
                    prestacionExistentes.DuracionMinutos = prestacion.DuracionMinutos;
                    prestacionExistentes.Precio = prestacion.Precio;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestacionExists(prestacion.Id))
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
            return View(prestacion);
        }

        // GET: Prestacion/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var prestacion = await _context.Prestacion
            //  .FirstOrDefaultAsync(m => m.Id == id);
            var prestacion = prestaciones.FirstOrDefault(m => m.Id == id);

            if (prestacion == null)
            {
                return NotFound();
            }

            return View(prestacion);
        }

        // POST: Prestacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //var prestacion = await _context.Prestacion.FindAsync(id);
            // _context.Prestacion.Remove(prestacion);
            //await _context.SaveChangesAsync();
            var prestacion = prestaciones.FirstOrDefault(m => m.Id == id);
            prestaciones.Remove(prestacion);
            return RedirectToAction(nameof(Index));
        }

        private bool PrestacionExists(Guid id)
        {
            return _context.Prestacion.Any(e => e.Id == id);
        }
    }
}
