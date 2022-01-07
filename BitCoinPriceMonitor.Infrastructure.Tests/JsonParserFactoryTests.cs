using BitcoinPriceMonitor.Domain.Constants;
using BitCoinPriceMonitor.Infrastrucutre.Utitlities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BitCoinPriceMonitor.Infrastrucutre.Factories;
namespace BitCoinPriceMonitor.Infrastructure.Tests
{
    public class JsonParserFactoryTests
    {
        [Theory]
        [InlineData(SourceSeededIds.CoinBase,typeof(CoinBaseJsonParser))]
        [InlineData(SourceSeededIds.BitStamp, typeof(BitStampJsonParser))]

        public async Task CreateParserTest(string sourceId, Type returnObject)
        {
            //Arranage
            var jsonParserFactory = new JsonParserFactory();
            //Act
            var result = jsonParserFactory.CreateParser(sourceId);
            //Assert
            Assert.True(result.HasValue);
            Assert.Equal(result.Value.GetType(), returnObject);   
        }
    }
}
