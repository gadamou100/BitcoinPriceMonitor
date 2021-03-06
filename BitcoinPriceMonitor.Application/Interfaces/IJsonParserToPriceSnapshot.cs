using BitCoinPriceMonitor.Domain.Data.Entities;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Application.Interfaces
{
    public interface IJsonParserToPriceSnapshot
    {
        public Maybe<PriceSnapshot> ParseJsonToPriceSnapshot(string json);
    }
}
