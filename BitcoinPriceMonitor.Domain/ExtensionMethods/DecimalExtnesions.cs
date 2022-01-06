using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Domain.ExtensionMethods
{
    public static class DecimalExtnesions
    {
        public static decimal Round(this decimal source, int decimalDigits = 2, MidpointRounding midpointRounding= MidpointRounding.ToEven) 
            => Math.Round(source, decimalDigits, midpointRounding);
    }
}
