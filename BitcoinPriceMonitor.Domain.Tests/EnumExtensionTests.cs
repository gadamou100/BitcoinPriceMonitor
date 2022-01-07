using BitcoinPriceMonitor.Domain.Constants;
using BitcoinPriceMonitor.Domain.Enums;
using BitcoinPriceMonitor.Domain.ExtensionMethods;
using System.Threading.Tasks;
using Xunit;

namespace BitcoinPriceMonitor.Domain.Tests
{
    public class EnumExtensionTests
    {
        //Arrange
        [Theory]
        [InlineData(SourceSeededIds.BitStamp, PriceSourceEnum.BitStamp)]
        [InlineData(SourceSeededIds.CoinBase, PriceSourceEnum.CoinBase)]
        public async Task GetUidTest(string uid, PriceSourceEnum @enum)
        {
            //Act
            var result = @enum.GetUid();
            //Assert
            Assert.Equal(uid, result);
        }
    }
}