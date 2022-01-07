using BitcoinPriceMonitor.Domain.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BitcoinPriceMonitor.Domain.Tests
{
    public class DecimalExtensionTests
    {
        [Fact]
        public async Task TestRound()
        {
            //Arrange
            decimal test = (decimal) 23.43454545;
            decimal expectedResult = (decimal)23.43;
            //Act
            var result = test.Round();
            Assert.Equal(expectedResult, result);   

        }
    }
}
