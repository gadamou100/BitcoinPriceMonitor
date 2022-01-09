using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Domain.Exceptions
{
    public class PriceSanpshotNotFoundException : Exception
    {
        public PriceSanpshotNotFoundException()
        {
        }

        public PriceSanpshotNotFoundException(string? message) : base(message)
        {
        }

        public PriceSanpshotNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PriceSanpshotNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
