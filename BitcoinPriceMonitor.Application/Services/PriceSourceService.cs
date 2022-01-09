using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Domain.ExtensionMethods;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Coravel;
using Coravel.Queuing.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Application.Services
{
    public class PriceSourceService : IPriceSourceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceProvider _serviceProvider;
        public PriceSourceService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _unitOfWork = _serviceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork;

        }
        public async Task<IEnumerable<PriceSource>> GetPriceSources()
        {
            var model = await _unitOfWork.GetRepository<PriceSource>().GetPagedListAsync(pageSize: int.MaxValue);
            return model.Items;
        }
        public async Task<decimal> GetLatestPriceFromSource(string sourceId, string userId)
        {

            var sourceTask = _unitOfWork.GetRepository<PriceSource>()
                   .GetFirstOrDefaultAsync(selector: s => new PriceSource { Name = s.Name, Url = s.Url, HeaderParameters = s.HeaderParameters }, predicate: p => p.Id == sourceId);
            var httpGetter = _serviceProvider.GetService(typeof(IHttpGetter)) as IHttpGetter;
            var source = await sourceTask;
            if (source == default)
                throw new InvalidDataException($"Cannot found source with Id: {sourceId}");

            var httpResponse = await httpGetter.Get(source.Url, source.ConvertHeaderValuesToDictionary());
            if (string.IsNullOrEmpty(httpResponse))
                throw new InvalidDataException($"Server responded with an empty response");
            var jsonParserFactory = _serviceProvider.GetService(typeof(IJsonParserToPriceSnapshotFactory)) as IJsonParserToPriceSnapshotFactory;
            var jsonParser = jsonParserFactory.CreateParser(sourceId);
            if (jsonParser.HasNoValue)
                throw new NotImplementedException($"Parser for source: {source.Name} is not implemented");
            var priceSnapShot = jsonParser.Value.ParseJsonToPriceSnapshot(httpResponse);
            if (priceSnapShot.HasNoValue)
                throw new InvalidCastException($"Failed to parse the response json to {typeof(PriceSnapshot).FullName}");
            var priceSnapShotValue = priceSnapShot.Value;
            priceSnapShotValue.Id = Guid.NewGuid().ToString();
            priceSnapShotValue.CreatedTimeStamp = DateTime.UtcNow;
            priceSnapShotValue.CreatorId = userId ?? String.Empty;

            // Because saving requires a database trip that is costly, add it to the queue for
            // asynchronus consumption and return the result to the user.
            var queue = _serviceProvider.GetService(typeof(IQueue)) as IQueue ;
            _ = queue.QueueInvocableWithPayload<UnitOfWorkSaveInvocable, PriceSnapshot>(priceSnapShotValue);


            return priceSnapShotValue.Value;


        }
    }
}
