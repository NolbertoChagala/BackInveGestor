using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_gestorinv.Servicio.Services
{
    public class CategoriaServices : Controller
    {
        // GET: CategoriaServices
        public ActionResult Index()
        {
            return View();
        }

        // GET: CategoriaServices/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriaServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaServices/Create
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

        // GET: CategoriaServices/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoriaServices/Edit/5
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

        // GET: CategoriaServices/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoriaServices/Delete/5
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
