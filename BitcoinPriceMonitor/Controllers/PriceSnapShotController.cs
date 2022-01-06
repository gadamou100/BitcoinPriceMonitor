using BitcoinPriceMonitor.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BitcoinPriceMonitor.Controllers
{
    public class PriceSnapShotController : Controller
    {
        private readonly IPriceSnapshotService _priceSnapshotService;

        public PriceSnapShotController(IPriceSnapshotService priceSnapshotService)
        {
            _priceSnapshotService = priceSnapshotService;
        }

        // GET: PriceSnapShotController
        public async Task<ActionResult> Index()
        {
            var model  = await _priceSnapshotService.GetAllPriceSnapshots();
            return View(model);
        }

        // GET: PriceSnapShotController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PriceSnapShotController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PriceSnapShotController/Create
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

        // GET: PriceSnapShotController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PriceSnapShotController/Edit/5
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

        // GET: PriceSnapShotController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PriceSnapShotController/Delete/5
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
