using BitCoinPriceMonitor.Domain.Data.Entities;

namespace BitcoinPriceMonitor.Application.Interfaces
{
    public interface IPriceSourceServices
    {
        Task<decimal> GetLatestPriceFromSource(string sourceId);
        Task<IEnumerable<PriceSource>> GetPriceSources();
    }
}