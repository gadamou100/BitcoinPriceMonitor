using BitCoinPriceMonitor.Domain.Data.Entities;

namespace BitcoinPriceMonitor.Application.Interfaces
{
    public interface IPriceSourceService
    {
        Task<decimal> GetLatestPriceFromSource(string sourceId, string userId);
        Task<IEnumerable<PriceSource>> GetPriceSources();
    }
}