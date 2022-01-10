using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Domain.Exceptions;
using BitCoinPriceMonitor.Domain.Data.Entities;
using CSharpFunctionalExtensions;
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

        public async Task<Maybe<PriceSnapshot>> FindById(string id)
        {
            var model = await _unitOfWork.GetRepository<PriceSnapshot>().GetFirstOrDefaultAsync(p => p, predicate: p => p.Id == id, include: p => p.Include(x => x.PriceSource));
            if (model == null)
                return Maybe<PriceSnapshot>.None;
            return model;
        }

        public async Task Edit(PriceSnapshot priceSnapshot, string modifiersId)
        {
            priceSnapshot = priceSnapshot ?? throw new ArgumentNullException(nameof(priceSnapshot));
            IRepository<PriceSnapshot> repository = _unitOfWork.GetRepository<PriceSnapshot>();
            var storedEntity = await repository.GetFirstOrDefaultAsync(p => p, predicate: p => p.Id == priceSnapshot.Id);
            if (storedEntity == null)
                throw new PriceSanpshotNotFoundException($"Pirce snapshot with id: {priceSnapshot.Id} was not found");
            storedEntity.Comments = priceSnapshot.Comments;
            storedEntity.UpdaterId = modifiersId;
            storedEntity.UpdateTimeStamp = DateTime.UtcNow;
            repository.Update(storedEntity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            IRepository<PriceSnapshot> repository = _unitOfWork.GetRepository<PriceSnapshot>();
            var storedEntity = await repository.GetFirstOrDefaultAsync(p => p, predicate: p => p.Id == id);
            if (storedEntity == null)
                throw new PriceSanpshotNotFoundException($"Pirce snapshot with id: {id} was not found");
            repository.Delete(storedEntity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
