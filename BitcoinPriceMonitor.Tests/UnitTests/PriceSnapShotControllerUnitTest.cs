using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Controllers;
using BitcoinPriceMonitor.ViewModels;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BitcoinPriceMonitor.Tests.UnitTests
{
    public class PriceSnapShotControllerUnitTest
    {
        [Fact]
        public async Task TestPriceSnapShotIndex()
        {
            //Arrange
            List<PriceSnapshot> priceSnapshotList = GetData();

            var pageListMock = new Mock<IPagedList<PriceSnapshot>>();
            pageListMock.Setup(x => x.Items).Returns(priceSnapshotList);
            pageListMock.Setup(x => x.TotalPages).Returns(2);
            pageListMock.Setup(x => x.TotalCount).Returns(4);
            var serviceMock = new Mock<IPriceSnapshotService>();
            serviceMock.Setup(x => x.GetAllPriceSnapshots(null,null,null,0,10,false,false,false)).Returns(Task.FromResult(pageListMock.Object));
            var controller = new PriceSnapShotController(serviceMock.Object);
            //Act
            var actionResult = await controller.Index();
            //Asser
            var viewResult = actionResult as ViewResult;
            var model = viewResult?.Model as PricesIndexViewModel;
            Assert.Equal(4, model?.ListItems.TotalCount);
            Assert.Equal(2, model?.ListItems.TotalPages);
            Assert.Equal(4, model?.ListItems.Items.Count());
            Assert.Equal("00000000-0000-0000-0000-000000000001", model?.ListItems?.Items?.FirstOrDefault()?.Id);
        }

        private static List<PriceSnapshot> GetData()
        {
            return new List<PriceSnapshot>()
            {
                 new PriceSnapshot
                {

                Id = "00000000-0000-0000-0000-000000000001",
                CreatedTimeStamp = DateTime.MaxValue,
                PriceSourceId ="Mock"
                },
                  new PriceSnapshot
                {

                Id = "00000000-0000-0000-0000-000000000002",
                CreatedTimeStamp = DateTime.MaxValue,
                PriceSourceId ="Mock"
                },
                    new PriceSnapshot
                {

                Id = "00000000-0000-0000-0000-000000000003",
                CreatedTimeStamp = DateTime.MaxValue,
                PriceSourceId ="Mock"
                },
                  new PriceSnapshot
                {

                Id = "00000000-0000-0000-0000-000000000004",
                CreatedTimeStamp = DateTime.MaxValue,
                PriceSourceId ="Mock"
                },
            };
        }
    }
}
