using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Application.DTOs
{
    public record CoinBaseDto
    {

        public int trade_id { get; set; }
        public string price { get; set; }
        public string size { get; set; }
        public DateTime time { get; set; }
        public string bid { get; set; }
        public string ask { get; set; }
        public string volume { get; set; }

    }
}
