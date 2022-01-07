using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Controllers;
using BitcoinPriceMonitor.ViewModels;
using BitCoinPriceMonitor.Domain.Data.Entities;
using BitCoinPriceMonitor.Infrastrucutre.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsCommon;
using Xunit;

namespace BitcoinPriceMonitor.Tests.IntegrationTests
{
    public class PriceSnapShotControllerIntegrationTests
    {
        [Fact]
        public async Task PriceSnapShotControllerIndex()
        {

            //Arrange
            var serviceProvider = ServiceProviderGetter.GetServiceProvider();
            var service = serviceProvider.GetRequiredService<IPriceSnapshotService>();
            var controller = new PriceSnapShotController(service);
            //Act
            var actionResult = await controller.Index();
            //Assert
            var viewResult = actionResult as ViewResult;
            var model = viewResult?.Model as PricesIndexViewModel;
            Assert.NotNull(model?.ListItems);

        }
        
    }
}
