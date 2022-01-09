using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Domain.Constants;
using BitCoinPriceMonitor.Infrastructure.Utitlities;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinPriceMonitor.Infrastructure.Factories
{
    public class JsonParserFactory : IJsonParserToPriceSnapshotFactory
    {
        public Maybe<IJsonParserToPriceSnapshot> CreateParser(string sourceId)
        {
            if (sourceId == SourceSeededIds.BitStamp)
                return new BitStampJsonParser();
            if (sourceId == SourceSeededIds.CoinBase)
                return new CoinBaseJsonParser();
            return Maybe<IJsonParserToPriceSnapshot>.None;
        }
    }
}
