using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.ViewModels;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BitcoinPriceMonitor.Controllers
{
    [Authorize]
    public class PriceSnapShotController : Controller
    {
        private readonly IPriceSnapshotService _priceSnapshotService;

        public PriceSnapShotController(IPriceSnapshotService priceSnapshotService)
        {
            _priceSnapshotService = priceSnapshotService;
        }

        // GET: PriceSnapShotController
        public async Task<ActionResult> Index(DateTime? dateFilter = null, DateTime? endDateFilter = null, string? sourceFilter = null, int pageNo = 0, int pageSize = 10, bool orderByDate = false, bool orderByPrice = false, bool descending = true)
        {
            if (dateFilter != null && endDateFilter != null && dateFilter > endDateFilter)
                Swap(ref endDateFilter, ref dateFilter);
            var listItems  = await _priceSnapshotService.GetPriceSnapshots(dateFilter,endDateFilter,sourceFilter,pageNo,pageSize,orderByDate,orderByPrice,descending);
          
            var viewModel = new PricesIndexViewModel
            {
                ListItems = listItems,
                SortByDate = orderByDate,
                SortByPrice = orderByPrice,
                SortFilter  = sourceFilter,
                DateFilter = dateFilter,
                EndDateFilter = endDateFilter,
                IsDescending = descending,
            };
            return View(viewModel);
        }

        private void Swap(ref DateTime? endDateFilter, ref DateTime? dateFilter)
        {
            var temp = dateFilter;
            dateFilter = endDateFilter;
            endDateFilter = temp;
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
        public async Task<ActionResult> Edit(string id)
        {
            var model = await _priceSnapshotService.FindById(id);
            if(model.HasValue)
                return View(model.Value);
            return NotFound();
        }

        // POST: PriceSnapShotController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, IFormCollection collection)
        {
            try
            {
                if(collection.TryGetValue(nameof(PriceSnapshot.Comments), out var comments))
                {
                    var model = new PriceSnapshot { Id = id, Comments = comments };
                    var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
                    await _priceSnapshotService.Edit(model, userId);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PriceSnapShotController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var model = await _priceSnapshotService.FindById(id);
            if (model.HasValue)
                return View(model.Value);
            return NotFound();
        }

        // POST: PriceSnapShotController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                await _priceSnapshotService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
