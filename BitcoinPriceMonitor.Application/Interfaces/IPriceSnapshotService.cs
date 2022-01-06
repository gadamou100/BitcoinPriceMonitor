using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using BitCoinPriceMonitor.Domain.Data.Entities;

namespace BitcoinPriceMonitor.Application.Interfaces
{
    public interface IPriceSnapshotService
    {
        Task<IPagedList<PriceSnapshot>> GetAllPriceSnapshots(DateTime? dateFilter = null, string? sourceFilter = null, int pageNo = 0, int pageSize = 20, bool orderByDate = false, bool orderByDateDesc = false, bool orderByPrice = false, bool orderByPriceDesc = false);
    }
}