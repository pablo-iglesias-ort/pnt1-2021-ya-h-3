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
    public class UsuarioController : Controller
    {
        private readonly AgendaTurnosContext _context;
        static List<Usuario> usuarios = new List<Usuario>()
        {
            new Usuario()
            {
                Id = Guid.NewGuid(),
                Nombre = "Joel",
                Email = "kskpluss32@gmail.com",
                FechaAlta = new DateTime(1991, 7, 8),
                Password = "123456789",
            },
            new Usuario()
            {
                Id = Guid.NewGuid(),
                Nombre = "Matias",
                Email = "gow.mt@hotmail.com",
                FechaAlta = new DateTime(1993, 12, 18),
                Password = "987654321",
            },
              new Usuario()
            {
                Id = Guid.NewGuid(),
                Nombre = "Sudak",
                Email = "gonzalezsudak@yahoo.com.ar",
                FechaAlta = new DateTime(1989, 2, 15),
                Password = "asadddadas",
            }
        };

        public UsuarioController(AgendaTurnosContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            return View(usuarios);
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Email,FechaAlta,Password")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Id = Guid.NewGuid();
                //_context.Add(usuario);
                //await _context.SaveChangesAsync();
                usuarios.Add(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ////var usuarios = await _context.Usuario.FindAsync(id);
            var usuario = usuarios.FirstOrDefault(e => e.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Email,FechaAlta,Password")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   //_context.Update(usuario);
                   // await _context.SaveChangesAsync();

                    var usuarioExistente = usuarios.FirstOrDefault(e => e.Id == id);
                    usuarioExistente.Nombre = usuario.Nombre;
                    usuarioExistente.Email = usuario.Email;
                    usuarioExistente.FechaAlta = usuario.FechaAlta;
                    usuarioExistente.Password = usuario.Password;
                }
                catch (Exception)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == id);
            var usuario = usuarios.FirstOrDefault(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //var usuario = await _context.Usuario.FindAsync(id);
            //_context.Usuario.Remove(usuario);
            // await _context.SaveChangesAsync();
            var usuario = usuarios.FirstOrDefault(m => m.Id == id);
            usuarios.Remove(usuario);
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(Guid id)
        {
            return usuarios.Exists(e => e.Id == id);
            //return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
