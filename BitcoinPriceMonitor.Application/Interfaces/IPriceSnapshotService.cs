using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using BitCoinPriceMonitor.Domain.Data.Entities;
using CSharpFunctionalExtensions;

namespace BitcoinPriceMonitor.Application.Interfaces
{
    public interface IPriceSnapshotService
    {
        Task<IPagedList<PriceSnapshot>> GetPriceSnapshots(DateTime? dateFilter = null, DateTime? endDateFilter = null, string? sourceFilter = null, int pageNo = 0, int pageSize = 10, bool orderByDate = false, bool orderByPrice = false, bool descending = false);

        Task<Maybe<PriceSnapshot>> FindById(string id); 
        Task Edit(PriceSnapshot priceSnapshot, string modifiersId);
        Task Delete(string id);

    }
}