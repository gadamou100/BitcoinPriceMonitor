using BitcoinPriceMonitor.Domain.Constants;
using BitCoinPriceMonitor.Infrastrucutre.ExtensionMethods;
using BitCoinPriceMonitor.Infrastrucutre.Utitlities;
using System.Threading.Tasks;
using Xunit;

namespace BitCoinPriceMonitor.Infrastructure.Tests
{
    public class BitStampJsonParserTests
    {
        [Fact]
        public async Task ParseJsonToPriceSnapshotTest()
        {
            //Arrange
            var json = "{\"volume\": \"2675.62899350\", \"last\": \"41306.56\", \"timestamp\": \"1641539992\", \"bid\": \"41288.88\", \"vwap\": \"42611.35\", \"high\": \"43584.75\", \"low\": \"40938.98\", \"ask\": \"41305.04\", \"open\": 43093.4}";
            var bitStampJsonParser = new BitStampJsonParser();
            
            //Act
            var result = bitStampJsonParser.ParseJsonToPriceSnapshot(json);
            Assert.True(result.HasValue);
            Assert.Equal(result.Value.Value, (decimal)41306.56);
            Assert.Equal(result.Value.PriceSourceId, SourceSeededIds.BitStamp);
            Assert.Equal(result.Value.RetrievedTimeStamp, ((long)1641539992).UnixTimeStampToDateTime());
        }

        [Fact]
        public async Task ParseInvalidJsonToPriceSnapshotTest()
        {
            //Arrange
            var json = "{\"volume\"= \"2675.62899350\", \"last\": \"41306.56\", \"timestamp\": \"1641539992\", \"bid\": \"41288.88\", \"vwap\": \"42611.35\", \"high\": \"43584.75\", \"low\": \"40938.98\", \"ask\": \"41305.04\", \"open\": 43093.4}";
            var bitStampJsonParser = new BitStampJsonParser();

            //Act
            var result = bitStampJsonParser.ParseJsonToPriceSnapshot(json);
            Assert.True(result.HasNoValue);
        }

    }
}
