using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Application.DTOs
{
    public record BitstampDto
    {
            public string volume { get; set; }
            public string last { get; set; }
            public string timestamp { get; set; }
            public string bid { get; set; }
            public string vwap { get; set; }
            public string high { get; set; }
            public string low { get; set; }
            public string ask { get; set; }
            public float open { get; set; }
    }
}
