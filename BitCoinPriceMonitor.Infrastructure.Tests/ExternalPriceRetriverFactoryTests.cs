using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BitcoinPriceMonitor.Domain.Constants;
using Moq;
using BitcoinPriceMonitor.Application.Interfaces;
using Microsoft.Extensions.Logging;
using BitCoinPriceMonitor.Infrastructure.Utitlities;
using BitCoinPriceMonitor.Infrastructure.Factories;

namespace BitCoinPriceMonitor.Infrastructure.Tests
{
    public class ExternalPriceRetriverFactoryTests
    {
        [Theory]
        [InlineData(SourceSeededIds.CoinBase,true)]
        [InlineData(SourceSeededIds.BitStamp,true)]
        [InlineData("InvalidId", false)]


        public async Task TestExternalPriceRetriverFactory(string sourceId, bool expectedHasValue)
        {
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IHttpGetter))).Returns(default(IHttpGetter));
            mockServiceProvider.Setup(p => p.GetService(typeof(IJsonParserToPriceSnapshotFactory))).Returns(default(IJsonParserToPriceSnapshotFactory));
            mockServiceProvider.Setup(p => p.GetService(typeof(ILogger<RestExternalPriceRetriever>))).Returns(default(ILogger<RestExternalPriceRetriever>));

            var factory = new ExternalPriceRetriverFactory(mockServiceProvider.Object);
            var result = factory.Create(sourceId);
            Assert.Equal(expectedHasValue, result.HasValue);

        }
    }
}
