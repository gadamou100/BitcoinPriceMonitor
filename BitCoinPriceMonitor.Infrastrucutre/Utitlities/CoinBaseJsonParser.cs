using BitcoinPriceMonitor.Application.DTOs;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Domain.Constants;
using BitcoinPriceMonitor.Domain.ExtensionMethods;
using BitCoinPriceMonitor.Domain.Data.Entities;
using BitCoinPriceMonitor.Infrastructure.ExtensionMethods;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinPriceMonitor.Infrastructure.Utitlities
{
    public class CoinBaseJsonParser : IJsonParserToPriceSnapshot
    {
        public Maybe<PriceSnapshot> ParseJsonToPriceSnapshot(string json)
        {
            try
            {
                var dto = json.Deserialize<CoinBaseDto>();
                if (dto.HasNoValue)
                    return Maybe<PriceSnapshot>.None;
                CoinBaseDto value = dto.Value;
                var result = new PriceSnapshot
                {
                    PriceSourceId = SourceSeededIds.CoinBase,
                    Value = decimal.Parse(value.price).Round(),
                    RetrievedTimeStamp = value.time
                };
                return result;
            }
            catch
            {
                return Maybe<PriceSnapshot>.None;
            }
        }
    }
}
