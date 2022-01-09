using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Domain.Constants;
using BitCoinPriceMonitor.Infrastructure.Utitlities;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinPriceMonitor.Infrastructure.Factories
{
    public class ExternalPriceRetriverFactory : IExternalPriceRetriverFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ExternalPriceRetriverFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Maybe<IExternalPriceRetriver> Create(string sourceId)
        {
            // Until now we have only REST price retrivers.
            // However in the future we need to support retriving from different sources.
            if (sourceId == SourceSeededIds.BitStamp || sourceId == SourceSeededIds.CoinBase)
            {
                var httpGetter = _serviceProvider.GetService(typeof(IHttpGetter)) as IHttpGetter;
                var jsonParserToPriceSnapshotFactory = _serviceProvider.GetService(typeof (IJsonParserToPriceSnapshotFactory)) as IJsonParserToPriceSnapshotFactory;
                var logger = _serviceProvider.GetService(typeof(ILogger<RestExternalPriceRetriever>)) as ILogger<RestExternalPriceRetriever>;
                return new RestExternalPriceRetriever(jsonParserToPriceSnapshotFactory,httpGetter,logger);
            }
            return Maybe<IExternalPriceRetriver>.None;
          
        }
    }
}
