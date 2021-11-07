using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaTurnos.Data;
using AgendaTurnos.Models;


namespace AgendaTurnos.Controllers
{
    public class LoginController : Controller
    {
        private readonly AgendaTurnosContext _context;
        private readonly ISeguridad seguridad = new SeguridadBasica();

        public LoginController(AgendaTurnosContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public IActionResult Ingresar(string returnUrl)
        {
            TempData["UrlIngreso"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ingresar(string usuario, string pass)
        {
            // Guardamos la URL a la que debemos redirigir al usuario
            var urlIngreso = TempData["UrlIngreso"] as string;

            // Verificamos que ambos esten informados
            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(pass))
            {

                // Verificamos que exista el usuario
                var user = await _context.Usuario.FirstOrDefaultAsync(u => u.User == usuario);
                if (user != null)
                {

                    // Verificamos que coincida la contraseña
                    var contraseña = seguridad.EncriptarPass(pass);
                    if (contraseña.SequenceEqual(user.Password))
                    {

                        // Creamos los Claims (credencial de acceso con informacion del usuario)
                        ClaimsIdentity identidad = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                        // Agregamos a la credencial el nombre de usuario
                        identidad.AddClaim(new Claim(ClaimTypes.Name, user.User));
                        // Agregamos a la credencial el nombre del estudiante/administrador
                        identidad.AddClaim(new Claim(ClaimTypes.GivenName, user.Nombre));
                        // Agregamos a la credencial el Rol
                        identidad.AddClaim(new Claim(ClaimTypes.Role, user.Rol.ToString()));

                        ClaimsPrincipal principal = new ClaimsPrincipal(identidad);

                        // Ejecutamos el Login
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        if (!string.IsNullOrEmpty(urlIngreso))
                        {
                            return Redirect(urlIngreso);
                        }
                        else
                        {
                            // Redirigimos a la pagina principal
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }

            ViewBag.ErrorEnLogin = "Verifique el usuario y contraseña";
            TempData["UrlIngreso"] = urlIngreso; // Volvemos a enviarla en el TempData para no perderla
            return View();

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult AccesoDenegado()
        {
            return View();
        }


        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrarse(Usuario user, string pass)
        {
            if (ModelState.IsValid)
            {
               // email.Id = Guid.NewGuid();
                if (seguridad.ValidarPass(pass))
                {
               //     email.Password = seguridad.EncriptarPass(pass);
                    //email.Rol = Rol.Paciente;
                    _context.Add(user);
               //     await _context.SaveChangesAsync();
               //     return RedirectToAction(nameof(Ingresar));
                }
                else
                {
                    ModelState.AddModelError(nameof(Usuario.Password), "La contraseña no cumple con los requisitos");
                }
            }
            return View(user);
        }
    }
}
