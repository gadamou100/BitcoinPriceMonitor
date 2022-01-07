using BitcoinPriceMonitor.Domain.ExtensionMethods;
using BitCoinPriceMonitor.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BitcoinPriceMonitor.Domain.Tests
{
    
    public class PriceSourceExtensionsTests
    {
        [Fact]
        public async Task ConvertHeaderValuesToDictionaryTest()
        {
            //Arrange
            var priceSource = new PriceSource
            {
                HeaderParameters = "apiKey:test;accept-content:json;bearer:testToken"
            };
            //Act
            var result = priceSource.ConvertHeaderValuesToDictionary();
            //Assert
            Assert.Equal("test", result["apiKey"]);
            Assert.Equal("json", result["accept-content"]);
            Assert.Equal("testToken", result["bearer"]);



        }
    }
}
