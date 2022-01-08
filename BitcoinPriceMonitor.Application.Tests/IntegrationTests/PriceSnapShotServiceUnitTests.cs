using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Services;
using BitcoinPriceMonitor.Domain.Constants;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsCommon;
using Xunit;

namespace BitcoinPriceMonitor.Application.Tests.IntegrationTests
{
    public class PriceSnapShotServiceUnitTests
    {
        [Fact]
        public async Task TestGetPriceSource()
        {
            //Arrange
            var serviceProvider = ServiceProviderGetter.GetServiceProvider();
            var priceSourceService = new PriceSourceService(serviceProvider);
            //Act
            var result = await priceSourceService.GetPriceSources();
            //Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task TestGetLatestPriceFromSource()
        {
            //Arrange
            var serviceProvider = ServiceProviderGetter.GetServiceProvider();
            var priceSourceService = new PriceSourceService(serviceProvider);
            //Act
            var result = await priceSourceService.GetLatestPriceFromSource(SourceSeededIds.BitStamp, "");
            //Assert
            Assert.True(result > 0);
        }
    }
}
