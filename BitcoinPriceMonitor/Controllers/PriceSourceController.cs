using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Interfaces;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BitcoinPriceMonitor.Controllers
{
    [Authorize]
    public class PriceSourceController : Controller
    {
        private readonly IPriceSourceService _priceSourceService;
        private readonly ILogger<PriceSourceController> _logger;
        public PriceSourceController(IPriceSourceService priceSourceService, ILogger<PriceSourceController> logger)
        {
            _priceSourceService = priceSourceService;
            _logger = logger;
        }

        // GET: PriceSourceController
        public async Task <ActionResult> Index()
        {
            var model =await _priceSourceService.GetPriceSources();
            return View(model);
        }

        public async Task<ActionResult> GetLatestPriceFromSource(string source)
        {
            try
            {
                var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
                var result = await _priceSourceService.GetLatestPriceFromSource(source, userId);
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                return new JsonResult(e.Message) { StatusCode = 500};
            }
        }

        // GET: PriceSourceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PriceSourceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PriceSourceController/Create
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

        // GET: PriceSourceController/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        // POST: PriceSourceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
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

        // GET: PriceSourceController/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: PriceSourceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
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
