using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Application.Services;
using BitcoinPriceMonitor.Domain.Constants;
using BitcoinPriceMonitor.Domain.ExtensionMethods;
using BitCoinPriceMonitor.Domain.Data.Entities;
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
    public class PriceSnapShotServiceIntegrationTests
    {
        
        [Fact]
        public async Task GetAllPriceSnapshotsTest()
        {
            //Arrange
            var serviceProvider = ServiceProviderGetter.GetServiceProvider();
            var uow = serviceProvider.GetRequiredService<IUnitOfWork>();
            var orderByBuilder = serviceProvider.GetRequiredService<IPriceSnapshotOrderByBuilder>();
            var predicateBuilder = serviceProvider.GetRequiredService<IPriceSanpshotPredicateBuilder>();
            var priceSnapShotService = new PriceSnapshotService(uow, predicateBuilder, orderByBuilder);
            //Act
            var result = await priceSnapShotService.GetPriceSnapshots();
            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
        }
    }
}
