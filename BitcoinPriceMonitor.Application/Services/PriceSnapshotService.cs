using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using BitcoinPriceMonitor.Application.Interfaces;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Application.Services
{
    public class PriceSnapshotService : IPriceSnapshotService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PriceSnapshotService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IPagedList<PriceSnapshot>> GetAllPriceSnapshots(DateTime? dateFilter = null, string? sourceFilter = null, int pageNo = 0, int pageSize = 20, bool orderByDate = false, bool orderByDateDesc = false, bool orderByPrice = false, bool orderByPriceDesc = false)
        {
            var orderBy = BuildOrderBy(orderByDate, orderByDateDesc, orderByPrice, orderByPriceDesc);
            var predicate = BuildPredicate(dateFilter, sourceFilter);
            var model = await _unitOfWork.GetRepository<PriceSnapshot>()
                .GetPagedListAsync(pageSize: pageSize, pageIndex: pageNo, include: p => p.Include(x => x.PriceSource), orderBy: orderBy, predicate: predicate);
        
            return model;
        }

        private static Func<IQueryable<PriceSnapshot>, IOrderedQueryable<PriceSnapshot>> BuildOrderBy(bool orderByDate = false, bool oderByDateDesc = false, bool orderByPrice = false, bool orderByPriceDesc = false)
        {
            Func<IQueryable<PriceSnapshot>, IOrderedQueryable<PriceSnapshot>> result = p => p.OrderBy(x => x.RetrievedTimeStamp);
            //By Default we order by retrived date.
            if (orderByDate == false && orderByPrice == false)
                return result;
            if (orderByPrice)
                result = orderByPriceDesc ? (p => p.OrderByDescending(x => x.Value)) : (p => p.OrderBy(x => x.Value));

            if (orderByDate)
                result = oderByDateDesc ? (p => p.OrderByDescending(x => x.RetrievedTimeStamp)) : (p => p.OrderBy(x => x.RetrievedTimeStamp));

            return result;
        }

        private static Expression<Func<PriceSnapshot, bool>> BuildPredicate(DateTime? dateFilter = null, string? sourceFilter = null)
        {
            var dateStart = dateFilter == null ? (DateTime) SqlDateTime.MinValue : dateFilter.Value.Date;
            var dateEnd = dateFilter == null ? (DateTime)SqlDateTime.MaxValue : dateFilter.Value.Date.AddDays(1).AddMilliseconds(-1);

            Expression<Func<PriceSnapshot, bool>> result = p => (p.RetrievedTimeStamp >= dateStart && p.RetrievedTimeStamp <= dateEnd)
            && (p.PriceSourceId == sourceFilter || sourceFilter == null);
            return result;
        }
    }
}
