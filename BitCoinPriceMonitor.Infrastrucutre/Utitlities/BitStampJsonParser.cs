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
    public class BitStampJsonParser : IJsonParserToPriceSnapshot
    {
        public Maybe<PriceSnapshot> ParseJsonToPriceSnapshot(string json)
        {
            try
            {
                var dto = json.Deserialize<BitstampDto>();
                if (dto.HasNoValue)
                    return Maybe<PriceSnapshot>.None;
                BitstampDto value = dto.Value;
                var result = new PriceSnapshot
                {
                    PriceSourceId = SourceSeededIds.BitStamp,
                    Value = decimal.Parse(value.last).Round(),
                    RetrievedTimeStamp = long.Parse(value.timestamp).UnixTimeStampToDateTime()
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
