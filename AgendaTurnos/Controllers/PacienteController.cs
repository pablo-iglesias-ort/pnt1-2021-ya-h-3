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
    [Authorize(Roles = "Paciente")]
    public class PacienteController : Controller
    {
        private readonly AgendaTurnosContext _context;
        private readonly ISeguridad seguridad = new SeguridadBasica();
        public PacienteController(AgendaTurnosContext context)
        {
            _context = context;
        }

        // GET: Paciente
        public async Task<IActionResult> Index()
        {
            return View(await _context.Paciente.ToListAsync());
        }

        // GET: Paciente/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.FirstOrDefaultAsync(m => m.Id == id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Paciente/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paciente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente, string pass)
        {
            if (ModelState.IsValid)
            {
                if (!PacienteExists(paciente.Email))
                {
                    if (seguridad.ValidarPass(pass))
                    {
                        paciente.Id = Guid.NewGuid();
                        paciente.FechaAlta = DateTime.Now.Date;
                        paciente.Password = seguridad.EncriptarPass(pass);
                        _context.Add(paciente);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(Usuario.Password), "La contraseña es incorrecta");
                    }
                }
                else {
                    ModelState.AddModelError(nameof(Usuario.Email), "El Email se encuentra registrado");
                }
            }
            return View(paciente);
        }

        // GET: Paciente/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.FindAsync(id);

            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Paciente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ObraSocial,Id,Nombre,Apellido,Email,FechaAlta,Password,Telefono,Direccion,Dni,Rol")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            return View(paciente);
        }

        private bool PacienteExists(Guid id)
        {
            return _context.Paciente.Any(e => e.Id == id);
        }

        private bool PacienteExists(string email)
        {
            return _context.Paciente.Any(e => e.Email == email);
        }

        public IActionResult Turnos(Guid id)
        {
            if (!PacienteExists(id))
            {
                return NotFound();
            }

            var turnos = _context.Paciente
                                .Include(paciente => paciente.Turnos)
                                   .ThenInclude(turno => turno.Profesional)
                                        .ThenInclude(prof => prof.Prestacion)
                                .FirstOrDefault(e => e.Id == id)
                                .Turnos;
            ViewData["PacienteId"] = id;
            return View(turnos);
        }

        [Authorize(Roles = "Paciente")]
        public async Task<IActionResult> EditarPerfil()
        {
            Guid id = Guid.Parse(User.Identity.Name);
            var paciente = await _context.Paciente.FindAsync(id);

            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPerfil(Paciente paciente)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            return View(paciente);
        }
    }
}
