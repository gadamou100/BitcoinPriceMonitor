using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Application.Services;
using BitcoinPriceMonitor.Domain.Constants;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestsCommon;
using Xunit;

namespace BitcoinPriceMonitor.Application.Tests.IntegrationTests
{
    public class PriceSourceServiceIntegrationTests
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
