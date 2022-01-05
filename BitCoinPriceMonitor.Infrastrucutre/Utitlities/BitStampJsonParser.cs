using BitcoinPriceMonitor.Application.DTOs;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Domain.Constants;
using BitCoinPriceMonitor.Domain.Data.Entities;
using BitCoinPriceMonitor.Infrastrucutre.ExtensionMethods;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinPriceMonitor.Infrastrucutre.Utitlities
{
    public class BitStampJsonParser : IJsonParserToPriceSnapshot
    {
        public Maybe<PriceSnapshot> ParseJsonToPriceSnapshot(string json)
        {
            var dto = json.Deserialize<BitstampDto>();
            if(dto.HasNoValue)
                return Maybe<PriceSnapshot>.None;
            BitstampDto value = dto.Value;
            var result = new PriceSnapshot
            {
                PriceSourceId = SourceSeededIds.BitStamp,
                Value = decimal.Parse(value.last),
                RetrievedTimeStamp = new DateTime(long.Parse(value.timestamp))
            };
            return result;
        }
    }
}
