using BitCoinPriceMonitor.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Domain.ExtensionMethods
{
    public static class PriceSourceExtensions
    {
        public static IDictionary<string,string> ConvertHeaderValuesToDictionary(this PriceSource source)
        {
            var result = new Dictionary<string, string>();
            if (source?.HeaderParameters != null)
            {
                var headerArray = source.HeaderParameters.Split(";");
                foreach (var headerPair in headerArray)
                {
                    string[] headerPairArray = headerPair.Split(":");
                    string key = headerPairArray.Any() ? headerPairArray[0] : string.Empty;
                    string value = headerPairArray.Count() >= 1 ? headerPairArray[1] : string.Empty;
                    result.Add(key, value);
                }
            }
            return result;
        }
    }
}
