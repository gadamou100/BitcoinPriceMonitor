using BitcoinPriceMonitor.Domain.Attributes;
using BitcoinPriceMonitor.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Domain.Enums
{
    public enum PriceSourceEnum
    {
        [EnumId(SourceSeededIds.BitStamp)]
        BitStamp = 1,
        [EnumId(SourceSeededIds.CoinBase)]
        CoinBase = 2
    }
}
