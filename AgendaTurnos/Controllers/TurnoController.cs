using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaTurnos.Controllers
{
    public class TurnoController : Controller
    {
        // GET: TurnoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TurnoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TurnoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TurnoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TurnoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TurnoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TurnoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TurnoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
