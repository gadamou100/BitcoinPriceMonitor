using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Services;
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
    public class PriceSourceServiceUnitTests
    {
        [Fact]
        public async Task GetAllPriceSnapshotsTest()
        {
            //Arrange
            var serviceProvider = ServiceProviderGetter.GetServiceProvider();
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            var priceSnapShotService = new PriceSnapshotService(unitOfWork);
            //Act
            var result = await priceSnapShotService.GetAllPriceSnapshots();
            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
        }
    }
}
