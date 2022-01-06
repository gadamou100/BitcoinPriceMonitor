using BitcoinPriceMonitor.Application.DTOs;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Domain.Constants;
using BitcoinPriceMonitor.Domain.ExtensionMethods;
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
                Value = decimal.Parse(value.last).Round(),
                RetrievedTimeStamp = UnixTimeStampToDateTime(long.Parse(value.timestamp))
            };
            return result;
        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dateTime;
        }

    }
}
