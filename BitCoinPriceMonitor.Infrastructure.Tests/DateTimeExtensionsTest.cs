using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BitCoinPriceMonitor.Infrastructure.ExtensionMethods;
namespace BitCoinPriceMonitor.Infrastructure.Tests
{
    public class DateTimeExtensionsTest
    {
        [Fact]
        public async  Task UnixTimeStampToDateTime()
        {
            //Assert
            var dateFormat = "yyyy-MM-dd hh:mm:ss zz";
            long unixEpoch = 1641570408;
            var expectedResult = DateTime.ParseExact("2022-01-07 03:46:48 +00", dateFormat ,null).ToUniversalTime();
            //Act
            var result = unixEpoch.UnixTimeStampToDateTime();
            //Assert
            Assert.Equal(expectedResult.ToString(dateFormat), result.ToString(dateFormat));
        }
    }
}
