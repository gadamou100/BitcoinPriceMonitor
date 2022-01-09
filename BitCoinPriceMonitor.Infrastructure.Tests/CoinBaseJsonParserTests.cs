using BitcoinPriceMonitor.Domain.Constants;
using BitCoinPriceMonitor.Infrastructure.ExtensionMethods;
using BitCoinPriceMonitor.Infrastructure.Utitlities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BitCoinPriceMonitor.Infrastructure.Tests
{
    public class CoinBaseJsonParserTests
    {
        [Fact]
        public async Task ParseJsonToPriceSnapshotTest()
        {
            //Arrange
            var json = "{\"trade_id\":52799476,\"price\":\"1.2036\",\"size\":\"162.65\",\"time\":\"2022-01-07T07:43:19.842637Z\",\"bid\":\"1.2028\",\"ask\":\"1.2031\",\"volume\":\"109960472.31\"}";
            var time = DateTime.Parse("2022-01-07T07:43:19.842637Z").ToUniversalTime();
            var parser = new CoinBaseJsonParser();
            
            //Act
            var result = parser.ParseJsonToPriceSnapshot(json);
            Assert.True(result.HasValue);
            Assert.Equal(result.Value.Value, (decimal)1.20);
            Assert.Equal(result.Value.PriceSourceId, SourceSeededIds.CoinBase);
            Assert.Equal(result.Value.RetrievedTimeStamp, time);


        }
        [Fact]
        public async Task ParseInvalidJsonToPriceSnapshotTest()
        {
            //Arrange
            var json = "{\"trade_id\"=52799476,\"price\":\"1.2036\",\"size\":\"162.65\",\"time\":\"2022-01-07T07:43:19.842637Z\",\"bid\":\"1.2028\",\"ask\":\"1.2031\",\"volume\":\"109960472.31\"}";
            var time = DateTime.Parse("2022-01-07T07:43:19.842637Z").ToUniversalTime();
            var parser = new CoinBaseJsonParser();

            //Act
            var result = parser.ParseJsonToPriceSnapshot(json);
            Assert.True(result.HasNoValue);
        }
    }
}
