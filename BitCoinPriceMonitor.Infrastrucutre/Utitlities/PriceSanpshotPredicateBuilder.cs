using BitcoinPriceMonitor.Application.Interfaces;
using BitCoinPriceMonitor.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinPriceMonitor.Infrastructure.Utitlities
{
    public class PriceSanpshotPredicateBuilder : IPriceSanpshotPredicateBuilder
    {
        public Expression<Func<PriceSnapshot, bool>> BuildPredicate(DateTime? dateFilter = null, DateTime? endDateFilter = null, string? sourceFilter = null)
        {
            var dateStart = dateFilter == null || dateFilter < (DateTime)SqlDateTime.MinValue
               ? (DateTime)SqlDateTime.MinValue
               : dateFilter.Value.Date;
            var dateEnd = endDateFilter == null || endDateFilter >= (DateTime)SqlDateTime.MaxValue
                ? (DateTime)SqlDateTime.MaxValue
                : endDateFilter.Value.Date.AddDays(1).AddMilliseconds(-1);

            Expression<Func<PriceSnapshot, bool>> result = p => (p.RetrievedTimeStamp >= dateStart && p.RetrievedTimeStamp <= dateEnd)
            && (sourceFilter == null || p.PriceSource.Name.ToLower().Contains(sourceFilter.ToLower()));
            return result;
        }
    }
}
