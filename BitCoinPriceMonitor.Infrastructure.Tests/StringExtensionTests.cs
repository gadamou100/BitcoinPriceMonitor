using BitcoinPriceMonitor.Application.DTOs;
using BitCoinPriceMonitor.Infrastructure.ExtensionMethods;
using System;
using Xunit;

namespace BitCoinPriceMonitor.Infrastructure.Tests
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData("test",false)]
        [InlineData("", true)]
        [InlineData(null, true)]

        public void TestIsNullOrEmpty(string source, bool expectedResult)
        {
            //Act
            var result = source.IsNullOrEmpty();
            //Assert
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void TestDesiarilizerUsingBitStampDto()
        {
            //Arrange
            var json = "{\"volume\": \"2675.62899350\", \"last\": \"41306.56\", \"timestamp\": \"1641539992\", \"bid\": \"41288.88\", \"vwap\": \"42611.35\", \"high\": \"43584.75\", \"low\": \"40938.98\", \"ask\": \"41305.04\", \"open\": 43093.4}";
            //Act
            var dto = json.Deserialize<BitstampDto>();
            //Assert
            Assert.True(dto.HasValue);
            var result = dto.Value;
            Assert.Equal("2675.62899350", result.volume);
            Assert.Equal("41306.56", result.last);
            Assert.Equal("1641539992", result.timestamp);
            Assert.Equal("41288.88", result.bid);
            Assert.Equal("42611.35", result.vwap);
            Assert.Equal("43584.75", result.high);
            Assert.Equal("40938.98", result.low);
            Assert.Equal("41305.04", result.ask);
            Assert.Equal(Math.Round(43093.4,2), Math.Round(result.open,2));
        }

        [Fact]
        public void TestDesiarilizerUsingCoinBaseDto()
        {
            //Arrange
            var json = "{\"trade_id\":52799476,\"price\":\"1.2036\",\"size\":\"162.65\",\"time\":\"2022-01-07T07:43:19.842637Z\",\"bid\":\"1.2028\",\"ask\":\"1.2031\",\"volume\":\"109960472.31\"}";
            var time = DateTime.Parse("2022-01-07T07:43:19.842637Z").ToUniversalTime();
            //Act
            var dto = json.Deserialize<CoinBaseDto>();
            //Assert
            Assert.True(dto.HasValue);
            var result = dto.Value;
            Assert.Equal(52799476, result.trade_id);
            Assert.Equal("1.2036", result.price);
            Assert.Equal("162.65", result.size);
            Assert.Equal(time, result.time);
            Assert.Equal("1.2028", result.bid);
            Assert.Equal("1.2031", result.ask);
            Assert.Equal("109960472.31", result.volume);

        }
    }
}