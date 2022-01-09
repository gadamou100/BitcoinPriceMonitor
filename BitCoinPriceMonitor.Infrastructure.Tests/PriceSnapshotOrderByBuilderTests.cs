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
    public class PriceSnapshotOrderByBuilderTests
    {
        [Fact]
        public async Task TestPriceSanpshotOrderByBuilderWithDefaultParams()
        {
            //Arrange
            var builder = new PriceSnapshotOrderByBuilder();
            Func<IQueryable<PriceSnapshot>, IOrderedQueryable<PriceSnapshot>> expectedResult = p => p.OrderBy(x => x.RetrievedTimeStamp);
            var expectedResultList = expectedResult(GetSampleQueryable()).ToList();
            //Act
            var result = builder.BuildOrderBy();
            var resultList = result(GetSampleQueryable()).ToList();

            //Assert
            Assert(expectedResultList, resultList);
        }
        [Fact]
        public async Task TestPriceSanpshotOrderByBuilderWithOrderByPrice()
        {
            //Arrange
            var builder = new PriceSnapshotOrderByBuilder();
            Func<IQueryable<PriceSnapshot>, IOrderedQueryable<PriceSnapshot>> expectedResult = p => p.OrderBy(x => x.Value);
            var expectedResultList = expectedResult(GetSampleQueryable()).ToList();
            //Act
            var result = builder.BuildOrderBy(orderByPrice: true);
            var resultList = result(GetSampleQueryable()).ToList();

            //Assert
            Assert(expectedResultList, resultList);
        }

        [Fact]
        public async Task TestPriceSanpshotOrderByBuilderWithOrderByPriceAndByRetrivedTimeStamp()
        {
            //Arrange
            var builder = new PriceSnapshotOrderByBuilder();
            Func<IQueryable<PriceSnapshot>, IOrderedQueryable<PriceSnapshot>> expectedResult = p => p.OrderBy(x => x.Value).ThenBy(x=>x.RetrievedTimeStamp);
            var expectedResultList = expectedResult(GetSampleQueryable()).ToList();
            //Act
            var result = builder.BuildOrderBy(orderByPrice: true, orderByDate: true);
            var resultList = result(GetSampleQueryable()).ToList();

            //Assert
            Assert(expectedResultList, resultList);
        }

        [Fact]
        public async Task TestPriceSanpshotOrderByBuilderWithDefaultParamsDescending()
        {
            //Arrange
            var builder = new PriceSnapshotOrderByBuilder();
            Func<IQueryable<PriceSnapshot>, IOrderedQueryable<PriceSnapshot>> expectedResult = p => p.OrderByDescending(x => x.RetrievedTimeStamp);
            var expectedResultList = expectedResult(GetSampleQueryable()).ToList();
            //Act
            var result = builder.BuildOrderBy(descending: true);
            var resultList = result(GetSampleQueryable()).ToList();

            //Assert
            Assert(expectedResultList, resultList);
        }
        [Fact]
        public async Task TestPriceSanpshotOrderByBuilderWithOrderByPriceDescending()
        {
            //Arrange
            var builder = new PriceSnapshotOrderByBuilder();
            Func<IQueryable<PriceSnapshot>, IOrderedQueryable<PriceSnapshot>> expectedResult = p => p.OrderByDescending(x => x.Value);
            var expectedResultList = expectedResult(GetSampleQueryable()).ToList();
            //Act
            var result = builder.BuildOrderBy(orderByPrice: true, descending: true);
            var resultList = result(GetSampleQueryable()).ToList();

            //Assert
            Assert(expectedResultList, resultList);
        }

        [Fact]
        public async Task TestPriceSanpshotOrderByBuilderWithOrderByPriceAndByRetrivedTimeStampDescending()
        {
            //Arrange
            var builder = new PriceSnapshotOrderByBuilder();
            Func<IQueryable<PriceSnapshot>, IOrderedQueryable<PriceSnapshot>> expectedResult = p => p.OrderByDescending(x => x.Value).ThenByDescending(x => x.RetrievedTimeStamp);
            var expectedResultList = expectedResult(GetSampleQueryable()).ToList();
            //Act
            var result = builder.BuildOrderBy(orderByPrice: true, orderByDate: true ,descending: true);
            var resultList = result(GetSampleQueryable()).ToList();

            //Assert
            Assert(expectedResultList, resultList);
        }



        private static void Assert(List<PriceSnapshot> expectedResultList, List<PriceSnapshot> resultList)
        {
            for (var i = 0; i < expectedResultList.Count; i++)
            {
                var expectedItem = expectedResultList[i];
                var item = resultList[i];
                Xunit.Assert.Equal(item.Value, expectedItem.Value);
                Xunit.Assert.Equal(item.RetrievedTimeStamp, expectedItem.RetrievedTimeStamp);
            }
        }

        private IQueryable<PriceSnapshot> GetSampleQueryable()
        {
            var now = DateTime.Today;
            var result = new PriceSnapshot[]
            {
                new PriceSnapshot{RetrievedTimeStamp = now, Value = (decimal)4454.21},
                new PriceSnapshot{RetrievedTimeStamp =now.AddDays(-1), Value = (decimal)4432.34},
                new PriceSnapshot{RetrievedTimeStamp = now, Value = (decimal)4454.11},
                new PriceSnapshot{RetrievedTimeStamp = now.AddDays(3), Value = (decimal)4435.34},
            };
            return result.AsQueryable();
        }
    }
}
