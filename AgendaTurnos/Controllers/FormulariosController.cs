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
    public class FormulariosController : Controller
    {
        private readonly AgendaTurnosContext _context;

        static List<Formulario> formularios = new List<Formulario>()
        {
            new Formulario()
            {

                Id = Guid.NewGuid(),
                Fecha = new DateTime(2020,10,01),
                Nombre = "Joel",
                Apellido = "AAAA",
                Email = "kskpluss32@gmail.com",
                Leido = false,
                Titulo = " ",
                Mensaje = " ",

            },

            new Formulario()
            {

                Id = Guid.NewGuid(),
                Fecha = new DateTime(2020,10,01),
                Nombre = "MAtias",
                Apellido = "AAAA",
                Email = "gow.mt@hotmail.com",
                Leido = false,
                Titulo = " ",
                Mensaje = " ",

            }


        };

        public FormulariosController(AgendaTurnosContext context)
        {
            _context = context;
        }

        // GET: Formularios
        public async Task<IActionResult> Index()
        {
            return View(formularios);
           // return View(await _context.Formulario.ToListAsync());
        }

        // GET: Formularios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var formulario = await _context.Formulario.FirstOrDefaultAsync(m => m.Id == id);


            var formulario = formularios.FirstOrDefault(m => m.Id == id);

            if (formulario == null)
            {
                return NotFound();
            }

            return View(formulario);
        }

        // GET: Formularios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Formularios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Email,Nombre,Apellido,Leido,Titulo,Mensaje")] Formulario formulario)
        {
            if (ModelState.IsValid)
            {
                formulario.Id = Guid.NewGuid();
                // _context.Add(formulario);
                //await _context.SaveChangesAsync();
                formularios.Add(formulario);
                return RedirectToAction(nameof(Index));
            }
            return View(formulario);
        }

        // GET: Formularios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario = formularios.FirstOrDefault(e => e.Id == id);
            if (formulario == null)
            {
                return NotFound();
            }
            return View(formulario);

            /* var formulario = await _context.Formulario.FindAsync(id);
             if (formulario == null)
             {
                 return NotFound();
             }
             return View(formulario);*/
        }

        // POST: Formularios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Fecha,Email,Nombre,Apellido,Leido,Titulo,Mensaje")] Formulario formulario)
        {
            if (id != formulario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(formulario);
                    //await _context.SaveChangesAsync();

                    var formularioExistente = formularios.FirstOrDefault(e => e.Id == id);
                    formularioExistente.Fecha = formulario.Fecha;
                    formularioExistente.Email = formulario.Email;
                    formularioExistente.Nombre = formulario.Nombre;
                    formularioExistente.Apellido = formulario.Apellido;
                    formularioExistente.Leido = formulario.Leido;
                    formularioExistente.Titulo = formulario.Titulo;
                    formularioExistente.Mensaje = formulario.Mensaje;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormularioExists(formulario.Id))
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
            return View(formulario);
        }

        // GET: Formularios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var formulario = await _context.Formulario.FirstOrDefaultAsync(m => m.Id == id);
            var formulario = formularios.FirstOrDefault(e => e.Id == id);
            if (formulario == null)
            {
                return NotFound();
            }

            return View(formulario);
        }

        // POST: Formularios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //var formulario = await _context.Formulario.FindAsync(id);
            //_context.Formulario.Remove(formulario);
            //await _context.SaveChangesAsync();
            var formulario = formularios.FirstOrDefault(e => e.Id == id);
            formularios.Remove(formulario);
            return RedirectToAction(nameof(Index));
        }

        private bool FormularioExists(Guid id)
        {
            return formularios.Exists(e => e.Id == id);
            //return _context.Formulario.Any(e => e.Id == id);
        }
    }
}
