using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Controllers;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BitcoinPriceMonitor.Tests.UnitTests
{
    public class PriceSourceControllerTest
    {
        [Fact]
        public async Task PriceSourceControllerIndexTest()
        {
            //Arrange
            IEnumerable<PriceSource> expectedResult = GetData();
            var serviceMock = new Mock<IPriceSourceService>();
            serviceMock.Setup(x => x.GetPriceSources()).Returns(Task.FromResult(expectedResult));
            var mockLooger = new Mock<ILogger<PriceSourceController>>();
            var controller = new PriceSourceController(serviceMock.Object,mockLooger.Object);
            //Act
            var actionResult = await controller.Index();
            //Assert
            var viewResult = actionResult as ViewResult;
            var model = viewResult?.Model as IEnumerable<PriceSource>;
            Assert.Equal(2, model?.Count());
            Assert.Equal("00000000-0000-0000-0000-000000000001", model?.FirstOrDefault()?.Id);
        }
        [Fact]
        public async Task GetLatestPriceFromSourceTest()
        {
            //Arrange
            decimal expectedDummyValue = (decimal)2323.45;
            var serviceMock = new Mock<IPriceSourceService>();
            serviceMock.Setup(p => p.GetLatestPriceFromSource("test", "")).Returns(Task.FromResult(expectedDummyValue));
            var mockLooger = new Mock<ILogger<PriceSourceController>>();
            var controller = new PriceSourceController(serviceMock.Object,mockLooger.Object);

            //Act
            var actionResult = await controller.GetLatestPriceFromSource("test");
            //Assert
            var viewResult = actionResult as JsonResult;
            var value = (decimal)(viewResult?.Value ?? 0);
            Assert.Equal(expectedDummyValue, value);
        }

        private static IEnumerable<PriceSource> GetData()
        {
            return new List<PriceSource>
            {
                new PriceSource
                {

                Id = "00000000-0000-0000-0000-000000000001",
                CreatedTimeStamp = DateTime.MaxValue,
                Name ="Mock"
                },
                  new PriceSource
                {

                Id = "00000000-0000-0000-0000-000000000002",
                CreatedTimeStamp = DateTime.MaxValue,
                Name ="Mock 2"
                },
            };
        }
    }
}
