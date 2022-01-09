using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Application.Services;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Coravel.Queuing.Interfaces;
using CSharpFunctionalExtensions;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsCommon;
using Xunit;

namespace BitcoinPriceMonitor.Application.Tests.UnitTests
{
    public class PriceSourceSeriveUnitTests
    {

        [Fact]
        public async Task GetLatestPriceFromSourceTest()
        {
            //Arrange
            decimal expectedResult = (decimal)44532.23;
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.GetFirstOrDefaultAsyncSetUp<PriceSource>().Returns(Task.FromResult(new PriceSource()));
            var httpGetterMock  = new Mock<IHttpGetter>();
            httpGetterMock.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<IDictionary<string, string>?>()))
                .Returns(Task.FromResult("Mock JSON Response"));
            var mockJsonParser = new Mock<IJsonParserToPriceSnapshot>();
            mockJsonParser.Setup(p=>p.ParseJsonToPriceSnapshot(It.IsAny<string>())).Returns(new PriceSnapshot() { Value= expectedResult });
            var mockJsonParserFactory = new Mock<IJsonParserToPriceSnapshotFactory>();
            mockJsonParserFactory.Setup(p => p.CreateParser(It.IsAny<string>())).Returns(Maybe.From(mockJsonParser.Object));
            var queueMock = new Mock<IQueue>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p=>p.GetService(typeof(IUnitOfWork))).Returns(unitOfWorkMock.Object);
            mockServiceProvider.Setup(p=>p.GetService(typeof(IHttpGetter))).Returns(httpGetterMock.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(IJsonParserToPriceSnapshotFactory))).Returns(mockJsonParserFactory.Object);
            mockServiceProvider.Setup(p=>p.GetService(typeof(IQueue))).Returns(queueMock.Object);
            var service = new PriceSourceService(mockServiceProvider.Object);
            
            //Act
            var result = await service.GetLatestPriceFromSource("test", "test");

            //Assert
            Assert.Equal(expectedResult,result);

        }
        [Fact]
        public async Task GetLatestPriceFromSourceTestSourceNotFound()
        {
            //Arrange
            decimal expectedResult = (decimal)44532.23;
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.GetFirstOrDefaultAsyncSetUp<PriceSource>().Returns(Task.FromResult(default(PriceSource)));
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IUnitOfWork))).Returns(unitOfWorkMock.Object);

            var service = new PriceSourceService(mockServiceProvider.Object);

            //Act
            var result = async () => await service.GetLatestPriceFromSource("test", "test");

            //Assert
            await Assert.ThrowsAsync<InvalidDataException>(result);

        }
        [Fact]
        public async Task GetLatestPriceFromSourceTestWithEmptyHttpRespone()
        {
            //Arrange
            decimal expectedResult = (decimal)44532.23;
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.GetFirstOrDefaultAsyncSetUp<PriceSource>().Returns(Task.FromResult(new PriceSource()));
            var httpGetterMock = new Mock<IHttpGetter>();
            httpGetterMock.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<IDictionary<string, string>?>()))
                .Returns(Task.FromResult(""));
            var mockJsonParser = new Mock<IJsonParserToPriceSnapshot>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IUnitOfWork))).Returns(unitOfWorkMock.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(IHttpGetter))).Returns(httpGetterMock.Object);
            var service = new PriceSourceService(mockServiceProvider.Object);

            //Act
            var result = async () => await service.GetLatestPriceFromSource("test", "test");

            //Assert
            await Assert.ThrowsAsync<InvalidDataException>(result);

        }
        [Fact]
        public async Task GetLatestPriceFromSourceTestNotImplementedParser()
        {
            //Arrange
            decimal expectedResult = (decimal)44532.23;
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.GetFirstOrDefaultAsyncSetUp<PriceSource>().Returns(Task.FromResult(new PriceSource()));
            var httpGetterMock = new Mock<IHttpGetter>();
            httpGetterMock.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<IDictionary<string, string>?>()))
                .Returns(Task.FromResult("Mock JSON Response"));
            var mockJsonParser = new Mock<IJsonParserToPriceSnapshot>();
            mockJsonParser.Setup(p => p.ParseJsonToPriceSnapshot(It.IsAny<string>())).Returns(new PriceSnapshot() { Value = expectedResult });
            var mockJsonParserFactory = new Mock<IJsonParserToPriceSnapshotFactory>();
            mockJsonParserFactory.Setup(p => p.CreateParser(It.IsAny<string>())).Returns(Maybe<IJsonParserToPriceSnapshot>.None);
            var queueMock = new Mock<IQueue>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IUnitOfWork))).Returns(unitOfWorkMock.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(IHttpGetter))).Returns(httpGetterMock.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(IJsonParserToPriceSnapshotFactory))).Returns(mockJsonParserFactory.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(IQueue))).Returns(queueMock.Object);
            var service = new PriceSourceService(mockServiceProvider.Object);


            //Act
            var result = async () => await service.GetLatestPriceFromSource("test", "test");

            //Assert
            await Assert.ThrowsAsync<NotImplementedException>(result);

        }
        [Fact]
        public async Task GetLatestPriceFromSourceTestInvalidRespone()
        {
            //Arrange
            decimal expectedResult = (decimal)44532.23;
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.GetFirstOrDefaultAsyncSetUp<PriceSource>().Returns(Task.FromResult(new PriceSource()));
            var httpGetterMock = new Mock<IHttpGetter>();
            httpGetterMock.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<IDictionary<string, string>?>()))
                .Returns(Task.FromResult("Mock JSON Response"));
            var mockJsonParser = new Mock<IJsonParserToPriceSnapshot>();
            mockJsonParser.Setup(p => p.ParseJsonToPriceSnapshot(It.IsAny<string>())).Returns(Maybe<PriceSnapshot>.None);
            var mockJsonParserFactory = new Mock<IJsonParserToPriceSnapshotFactory>();
            mockJsonParserFactory.Setup(p => p.CreateParser(It.IsAny<string>())).Returns(Maybe.From(mockJsonParser.Object));
            var queueMock = new Mock<IQueue>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IUnitOfWork))).Returns(unitOfWorkMock.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(IHttpGetter))).Returns(httpGetterMock.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(IJsonParserToPriceSnapshotFactory))).Returns(mockJsonParserFactory.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(IQueue))).Returns(queueMock.Object);
            var service = new PriceSourceService(mockServiceProvider.Object);

            //Act
            var result = async () => await service.GetLatestPriceFromSource("test", "test");

            //Assert
            await Assert.ThrowsAsync<InvalidCastException>(result);

        }
    }
}
