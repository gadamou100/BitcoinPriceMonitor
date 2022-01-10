using BitCoinPriceMonitor.Domain.Data.Entities;
using BitCoinPriceMonitor.Infrastructure.Utitlities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BitCoinPriceMonitor.Infrastructure.Tests
{
    public class PriceSanpshotPredicateBuilderTests
    {
        [Theory]
        [InlineData(0, 0, null)]
        [InlineData(1,2,null)]
        [InlineData(1, 5, null)]
        [InlineData(0, 0, "bit")]
        [InlineData(1, 2, "stamp")]
        [InlineData(1, 5, "BIT STAMP")]
        [InlineData(0, 0, "coin")]
        [InlineData(1, 2, "base")]
        [InlineData(1, 5, "COIN BASe")]
        public async Task BuildPredicateTests(int startDateOffSet, int endDateOffset, string? sourceName)
        {
            var referenceDate = DateTime.Today;
            var startDate = referenceDate.AddDays(-startDateOffSet);
            var endDate = referenceDate.AddDays(endDateOffset);
            var data = GetSampleData();
            var builder = new PriceSanpshotPredicateBuilder();

            //Act
            var predicate = builder.BuildPredicate(startDate, endDate,sourceName);
            var resultList = data.AsQueryable().Where(predicate);
            foreach (var item in resultList)
            {
                Assert.True(item.RetrievedTimeStamp >= startDate);
                Assert.True(item.RetrievedTimeStamp <= endDate.AddDays(1).AddMilliseconds(-1));
                if(sourceName !=null)
                    Assert.True(item.PriceSource.Name.Contains(sourceName, StringComparison.OrdinalIgnoreCase));    
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("bit")]
        [InlineData("stamp")]
        [InlineData("BIT STAMP")]
        [InlineData("coin")]
        [InlineData("base")]
        [InlineData("COIN BASe")]
        public async Task BuildPredicateTestsWithoutDateParams( string? sourceName)
        {
            var data = GetSampleData();
            var builder = new PriceSanpshotPredicateBuilder();

            //Act
            var predicate = builder.BuildPredicate( sourceFilter: sourceName);
            var resultList = data.AsQueryable().Where(predicate);
            foreach (var item in resultList)
            {

                if (sourceName != null)
                    Assert.True(item.PriceSource.Name.Contains(sourceName, StringComparison.OrdinalIgnoreCase));
                else
                    Assert.Equal(resultList.Count() , data.Count());
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("bit")]
        [InlineData("stamp")]
        [InlineData("BIT STAMP")]
        [InlineData("coin")]
        [InlineData("base")]
        [InlineData("COIN BASe")]
        public async Task BuildPredicateTestsWithExitLimitsDateParams(string? sourceName)
        {
            var data = GetSampleData();
            var builder = new PriceSanpshotPredicateBuilder();

            //Act
            var predicate = builder.BuildPredicate(dateFilter: DateTime.MinValue, endDateFilter: DateTime.MaxValue,sourceFilter: sourceName);
            var resultList = data.AsQueryable().Where(predicate);
            foreach (var item in resultList)
            {

                if (sourceName != null)
                    Assert.True(item.PriceSource.Name.Contains(sourceName, StringComparison.OrdinalIgnoreCase));
                else
                    Assert.Equal(resultList.Count(), data.Count());
            }
        }


        private IEnumerable<PriceSnapshot> GetSampleData()
        {
            var now = DateTime.Today;
            var bitstampSource = new PriceSource { Name = "Bit Stamp" };
            var coinBaseSource = new PriceSource { Name = "Coin Base" };
            var result = new PriceSnapshot[]
            {
                new PriceSnapshot{RetrievedTimeStamp = now, Value = (decimal)4454.21, PriceSource=bitstampSource},
                new PriceSnapshot{RetrievedTimeStamp =now.AddDays(-1), Value = (decimal)4432.34, PriceSource=coinBaseSource},
                new PriceSnapshot{RetrievedTimeStamp = now, Value = (decimal)4454.11, PriceSource= coinBaseSource},
                new PriceSnapshot{RetrievedTimeStamp = now.AddDays(3), Value = (decimal)4435.34, PriceSource = bitstampSource},
                   new PriceSnapshot{RetrievedTimeStamp = now.AddHours(3), Value = (decimal)4454.21, PriceSource=bitstampSource},
                new PriceSnapshot{RetrievedTimeStamp =now.AddDays(-2), Value = (decimal)4432.34, PriceSource=coinBaseSource},
                new PriceSnapshot{RetrievedTimeStamp = now, Value = (decimal)4454.11, PriceSource= coinBaseSource},
                new PriceSnapshot{RetrievedTimeStamp = now.AddDays(8), Value = (decimal)4435.34, PriceSource = bitstampSource},
            };
            return result;
        }
    }


}
