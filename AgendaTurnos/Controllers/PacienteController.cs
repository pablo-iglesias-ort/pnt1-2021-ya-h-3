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
    public class PacienteController : Controller
    {
        private readonly AgendaTurnosContext _context;

        static List<Paciente> pacientes = new List<Paciente>()
        {


        };

        public PacienteController(AgendaTurnosContext context)
        {
            _context = context;
        }

        // GET: Paciente
        public async Task<IActionResult> Index()
        {
            return View(await _context.Paciente.ToListAsync());
            //return View(pacientes);
        }

        // GET: Paciente/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.FirstOrDefaultAsync(m => m.Id == id);
            //var paciente = pacientes.FirstOrDefault(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Paciente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paciente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObraSocial,Id,Nombre,Apellido,Email,FechaAlta,Password,Telefono,Direccion,Dni,Rol")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                paciente.Id = Guid.NewGuid();
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                //pacientes.Add(paciente);
                return RedirectToAction(nameof(Index));
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

            //var paciente = await _context.Paciente.FindAsync(id);
            var paciente = pacientes.FirstOrDefault(m => m.Id == id);

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

                    /*var pacienteExistente = pacientes.FirstOrDefault(e => e.Id == id);
                    pacienteExistente.ObraSocial = paciente.ObraSocial;
                    pacienteExistente.Nombre = paciente.Nombre;
                    pacienteExistente.Apellido = paciente.Apellido;
                    pacienteExistente.Email = paciente.Email;
                    pacienteExistente.FechaAlta = paciente.FechaAlta;
                    pacienteExistente.Password = paciente.Password;
                    pacienteExistente.Telefono = paciente.Telefono;
                    pacienteExistente.Direccion = paciente.Direccion;
                    pacienteExistente.Dni = paciente.Dni;*/
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

        // GET: Paciente/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var paciente = await _context.Paciente.FirstOrDefaultAsync(m => m.Id == id);
            var paciente = pacientes.FirstOrDefault(e => e.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Paciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            /*var paciente = await _context.Paciente.FindAsync(id);
            _context.Paciente.Remove(paciente);
            await _context.SaveChangesAsync();*/
            var paciente = pacientes.FirstOrDefault(e => e.Id == id);
            pacientes.Remove(paciente);
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(Guid id)
        {
            return _context.Paciente.Any(e => e.Id == id);
        }
        // metodos del paciente
        public void generarTurno()
        {

        }
        public void cancelarTurno()
        {

        }
        public void verTurno()
        {

        }
        public void modDatosPersonales()
        {

        }
    }
}
