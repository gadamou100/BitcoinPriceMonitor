using BitCoinPriceMonitor.Infrastrucutre.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BitCoinPriceMonitor.Infrastructure.Tests
{
    public class HttpClientWrapperTests
    {
        [Fact]
        public async Task HttpClientWrapperTestGet()
        {
            //Arrange
            HttpClientWrapper httpClient = new HttpClientWrapper();
            //Act
            var result = await httpClient.Get("https://www.bitstamp.net/api/ticker/");
            //Assert
            Assert.NotNull(result);
        }
    }
}
