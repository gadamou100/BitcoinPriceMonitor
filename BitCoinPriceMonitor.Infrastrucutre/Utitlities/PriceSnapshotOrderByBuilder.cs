using BitcoinPriceMonitor.Application.Interfaces;
using BitCoinPriceMonitor.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinPriceMonitor.Infrastructure.Utitlities
{
    public class PriceSnapshotOrderByBuilder : IPriceSnapshotOrderByBuilder
    {
        public Func<IQueryable<PriceSnapshot>, IOrderedQueryable<PriceSnapshot>> BuildOrderBy(bool orderByDate = false, bool orderByPrice = false, bool descending = false)
        {
            Func<IQueryable<PriceSnapshot>, IOrderedQueryable<PriceSnapshot>> result = p => p.OrderBy(x => x.RetrievedTimeStamp);
            //By Default we order by retrived date.
            if (orderByDate == false && orderByPrice == false)
                return descending ? p => p.OrderByDescending(x => x.RetrievedTimeStamp) : result;
            if (orderByPrice)
            {

                if (orderByDate)
                    result = descending
                        ? (p => p.OrderByDescending(x => x.Value).ThenByDescending(x => x.RetrievedTimeStamp))
                        : (p => p.OrderBy(x => x.Value).ThenBy(x => x.RetrievedTimeStamp));
                else
                    result = descending ? (p => p.OrderByDescending(x => x.Value)) : (p => p.OrderBy(x => x.Value));

            }
            else if (orderByDate)
                result = descending ? (p => p.OrderByDescending(x => x.RetrievedTimeStamp)) : (p => p.OrderBy(x => x.RetrievedTimeStamp));

            return result;
        }
    }
}
