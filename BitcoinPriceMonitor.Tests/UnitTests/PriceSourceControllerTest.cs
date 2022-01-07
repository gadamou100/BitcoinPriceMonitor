using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Controllers;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Microsoft.AspNetCore.Mvc;
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
            var serviceMock = new Mock<IPriceSourceServices>();
            serviceMock.Setup(x => x.GetPriceSources()).Returns(Task.FromResult(expectedResult));
            var controller = new PriceSourceController(serviceMock.Object);
            //Act
            var actionResult = await controller.Index();
            //Assert
            var viewResult = actionResult as ViewResult;
            var model = viewResult?.Model as IEnumerable<PriceSource>;
            Assert.Equal(2, model?.Count());
            Assert.Equal("00000000-0000-0000-0000-000000000001", model?.FirstOrDefault()?.Id);
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
