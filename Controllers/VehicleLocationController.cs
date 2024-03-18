using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MilesCarRental.Controllers
{
    public class VehicleLocationController : Controller
    {
        // GET: VehicleLocationController
        public ActionResult Index()
        {
            return View();
        }

        // POST: VehicleLocationController/Create
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
    }
}
