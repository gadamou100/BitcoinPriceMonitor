using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Controllers;
using BitcoinPriceMonitor.ViewModels;
using BitCoinPriceMonitor.Domain.Data.Entities;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsCommon;
using Xunit;

namespace BitcoinPriceMonitor.Tests.UnitTests
{
    public class PriceSnapShotControllerUnitTest
    {
        [Fact]
        public async Task TestPriceSnapShotIndex()
        {
            var pageListMock = MockPagedListGetter.GetPageListMock();
            var serviceMock = new Mock<IPriceSnapshotService>();
            serviceMock.Setup(x => x.GetPriceSnapshots(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(Task.FromResult(pageListMock.Object));
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

        [Fact]
        public async Task TestPriceSnapShotGetModelForEdit()
        {
            var pageListMock = MockPagedListGetter.GetPageListMock();
            var serviceMock = new Mock<IPriceSnapshotService>();
            serviceMock.Setup(x => x.FindById(It.IsAny<string>())).Returns(Task.FromResult(Maybe.From(new PriceSnapshot { Id="test",Value=1})));
            var controller = new PriceSnapShotController(serviceMock.Object);
            //Act
            var actionResult = await controller.Edit("test");
            //Asser
            var viewResult = actionResult as ViewResult;
            var model = viewResult?.Model as PriceSnapshot;
            Assert.Equal("test", model?.Id);
            Assert.Equal(1, model?.Value);
        }

        [Fact]
        public async Task TestPriceSnapShotGetModelForEditNotFound()
        {
            var pageListMock = MockPagedListGetter.GetPageListMock();
            var serviceMock = new Mock<IPriceSnapshotService>();
            serviceMock.Setup(x => x.FindById(It.IsAny<string>())).Returns(Task.FromResult(Maybe<PriceSnapshot>.None));
            var controller = new PriceSnapShotController(serviceMock.Object);
            //Act
            var actionResult = await controller.Edit("test");
            //Asser
            var notFoundResult = actionResult as NotFoundResult;
            Assert.Equal(404, notFoundResult.StatusCode);
        }


        [Fact]
        public async Task TestPriceSnapShotEdit()
        {
            var pageListMock = MockPagedListGetter.GetPageListMock();
            var serviceMock = new Mock<IPriceSnapshotService>();
            serviceMock.Setup(x => x.Edit(It.IsAny<PriceSnapshot>(),It.IsAny<string>()));
            var formCollectionMock = new Mock<IFormCollection>();
            StringValues comments = "Comments";
            formCollectionMock.Setup(p => p.TryGetValue(nameof(PriceSnapshot.Comments), out comments)).Returns(true);
            var controller = new PriceSnapShotController(serviceMock.Object);
            //Act
            var actionResult = await controller.Edit("test",formCollectionMock.Object);
            //Asser
            var redirectResult = actionResult as RedirectToActionResult;
            Assert.Equal("Index", redirectResult.ActionName);
        }



        [Fact]
        public async Task TestPriceSnapShotGetModelForDelete()
        {
            var pageListMock = MockPagedListGetter.GetPageListMock();
            var serviceMock = new Mock<IPriceSnapshotService>();
            serviceMock.Setup(x => x.FindById(It.IsAny<string>())).Returns(Task.FromResult(Maybe.From(new PriceSnapshot { Id = "test", Value = 1 })));
            var controller = new PriceSnapShotController(serviceMock.Object);
            //Act
            var actionResult = await controller.Delete("test");
            //Asser
            var viewResult = actionResult as ViewResult;
            var model = viewResult?.Model as PriceSnapshot;
            Assert.Equal("test", model?.Id);
            Assert.Equal(1, model?.Value);
        }

        [Fact]
        public async Task TestPriceSnapShotGetModelForDeleterNotFound()
        {
            var pageListMock = MockPagedListGetter.GetPageListMock();
            var serviceMock = new Mock<IPriceSnapshotService>();
            serviceMock.Setup(x => x.FindById(It.IsAny<string>())).Returns(Task.FromResult(Maybe<PriceSnapshot>.None));
            var controller = new PriceSnapShotController(serviceMock.Object);
            //Act
            var actionResult = await controller.Delete("test");
            //Asser
            var notFoundResult = actionResult as NotFoundResult;
            Assert.Equal(404, notFoundResult.StatusCode);
        }



        [Fact]
        public async Task TestPriceSnapShotDelete()
        {
            var pageListMock = MockPagedListGetter.GetPageListMock();
            var serviceMock = new Mock<IPriceSnapshotService>();
            serviceMock.Setup(x => x.Delete(It.IsAny<string>()));
            var formCollectionMock = new Mock<IFormCollection>();
            StringValues comments = "Comments";
            var controller = new PriceSnapShotController(serviceMock.Object);
            //Act
            var actionResult = await controller.Delete("test", formCollectionMock.Object);
            //Asser
            var redirectResult = actionResult as RedirectToActionResult;
            Assert.Equal("Index", redirectResult.ActionName);
        }
    }
}
