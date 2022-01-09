using BitCoinPriceMonitor.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Application.Interfaces
{
    public interface IPriceSanpshotPredicateBuilder
    {
        Expression<Func<PriceSnapshot, bool>> BuildPredicate(DateTime? dateFilter = null, DateTime? endDateFilter = null, string? sourceFilter = null);
    }
}
