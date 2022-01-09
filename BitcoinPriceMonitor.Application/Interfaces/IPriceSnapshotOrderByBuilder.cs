using BitCoinPriceMonitor.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Application.Interfaces
{
    public interface IPriceSnapshotOrderByBuilder
    {
        public Func<IQueryable<PriceSnapshot>, IOrderedQueryable<PriceSnapshot>> BuildOrderBy(bool orderByDate = false, bool orderByPrice = false, bool descending = false);
    }
}
