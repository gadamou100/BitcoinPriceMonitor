using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Application.Interfaces
{
    public interface IHttpGetter
    {
        public Task<string> Get(string url, IDictionary<string,string>? headerParams=null); 
    }
}
