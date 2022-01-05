using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Domain.Attributes
{
    public class EnumIdAttribute : Attribute
    {
        public EnumIdAttribute(string id) 
        {
            Id = id;
        }
        public string Id { get; }

    }
}
