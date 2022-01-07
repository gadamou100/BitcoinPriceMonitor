using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Controllers;
using BitcoinPriceMonitor.Domain.Constants;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsCommon;
using Xunit;

namespace BitcoinPriceMonitor.Tests.IntegrationTests
{
    public class PriceSourceControllerIntegrationTests
    {
        [Fact]
        public async Task PriceSourceControllerIndexTest()
        {
            //Arrange
            var serviceProvider = ServiceProviderGetter.GetServiceProvider();
            var service = serviceProvider.GetRequiredService<IPriceSourceService>();
            var controller = new PriceSourceController(service);
            //Act
            var actionResult = await controller.Index();
            //Assert
            var viewResult = actionResult as ViewResult;
            var model = viewResult?.Model as IEnumerable<PriceSource>;
            Assert.True(model?.Any());
            Assert.True(model?.Any(t=>t.Id== SourceSeededIds.CoinBase));
            Assert.True(model?.Any(t => t.Id == SourceSeededIds.BitStamp));
        }
        [Fact]
        public async Task GetLatestPriceFromSourceTest()
        {
            //Arrange
            var serviceProvider = ServiceProviderGetter.GetServiceProvider();
            var service = serviceProvider.GetRequiredService<IPriceSourceService>();
            var controller = new PriceSourceController(service);
            //Act
            var actionResult = await controller.GetLatestPriceFromSource(SourceSeededIds.BitStamp);
            //Assert
            var viewResult = actionResult as JsonResult;
            var value = (decimal) (viewResult?.Value ?? 0);
            Assert.NotEqual(0, value);
        }
    }
}
