using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using BitcoinPriceMonitor.Application.Interfaces;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IPriceSanpshotPredicateBuilder _priceSanpshotPredicateBuilder;
        private readonly IPriceSnapshotOrderByBuilder _priceSnapshotOrderByBuilder;

        public PriceSnapshotService(IUnitOfWork unitOfWork, IPriceSanpshotPredicateBuilder priceSanpshotPredicateBuilder, IPriceSnapshotOrderByBuilder priceSnapshotOrderByBuilder)
        {
            _unitOfWork = unitOfWork;
            _priceSanpshotPredicateBuilder = priceSanpshotPredicateBuilder;
            _priceSnapshotOrderByBuilder = priceSnapshotOrderByBuilder;
        }

        public async Task<IPagedList<PriceSnapshot>> GetPriceSnapshots(DateTime? dateFilter = null, DateTime? endDateFilter = null, string? sourceFilter = null, int pageNo = 0, int pageSize = 10, bool orderByDate = false,  bool orderByPrice = false, bool descending = false)
        {
            if(pageNo<0)
                pageNo= 0;
            if(pageSize<0)
                pageSize= 10;
            var orderBy = _priceSnapshotOrderByBuilder.BuildOrderBy(orderByDate, orderByPrice, descending);
            var predicate = _priceSanpshotPredicateBuilder.BuildPredicate(dateFilter,endDateFilter, sourceFilter);
            var model = await _unitOfWork.GetRepository<PriceSnapshot>()
                .GetPagedListAsync(p=>p,pageSize: pageSize, pageIndex: pageNo, include: p => p.Include(x => x.PriceSource), orderBy: orderBy, predicate: predicate);
        
            return model;
        }
    }
}
