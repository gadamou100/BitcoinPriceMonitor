using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using BitCoinPriceMonitor.Domain.Data.Entities;

namespace BitcoinPriceMonitor.ViewModels
{
    public class PricesIndexViewModel
    {
        public IPagedList<PriceSnapshot> ListItems { get; set; }    
        public DateTime? DateFilter { get; set; }
        public DateTime? EndDateFilter { get; set; }

        public string? SortFilter { get; set; }
        public bool SortByDate { get; set; }
        public bool SortByPrice { get; set; }
        public bool IsDescending { get; set; }
    }
}
