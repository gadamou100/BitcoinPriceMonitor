using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Application.Services;
using BitcoinPriceMonitor.Domain.Exceptions;
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
    public class PriceSourceServiceUnitTests
    {

        [Fact]
        public async Task GetLatestPriceFromSourceTest()
        {
            //Arrange
            decimal expectedResult = (decimal)44532.23;
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.GetFirstOrDefaultAsyncSetUp<PriceSource>().Returns(Task.FromResult(new PriceSource()));
            var mockDataRetriever = new Mock<IExternalPriceRetriver>();
            mockDataRetriever.Setup(p => p.RetrieveLatestPrice(It.IsAny<PriceSource>())).Returns(Task.FromResult(Maybe.From(new PriceSnapshot { Value=expectedResult})));
            var retriverFactoryMock = new Mock<IExternalPriceRetriverFactory>();
            retriverFactoryMock.Setup(p => p.Create(It.IsAny<string>())).Returns(Maybe.From(mockDataRetriever.Object));
            var queueMock = new Mock<IQueue>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IUnitOfWork))).Returns(unitOfWorkMock.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(IExternalPriceRetriverFactory))).Returns(retriverFactoryMock.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(IQueue))).Returns(queueMock.Object);
            var service = new PriceSourceService(mockServiceProvider.Object);

            //Act
            var result = await service.GetLatestPriceFromSource("test", "test");

            //Assert
            Assert.Equal(expectedResult, result);

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
        public async Task GetLatestPriceFromSourceTestNotImplementedParser()
        {
            //Arrange
            decimal expectedResult = (decimal)44532.23;
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.GetFirstOrDefaultAsyncSetUp<PriceSource>().Returns(Task.FromResult(new PriceSource()));
            var retriverFactoryMock = new Mock<IExternalPriceRetriverFactory>();
            retriverFactoryMock.Setup(p => p.Create(It.IsAny<string>())).Returns(Maybe<IExternalPriceRetriver>.None);

            var queueMock = new Mock<IQueue>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IUnitOfWork))).Returns(unitOfWorkMock.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(IExternalPriceRetriverFactory))).Returns(retriverFactoryMock.Object);
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
            var mockDataRetriever = new Mock<IExternalPriceRetriver>();
            mockDataRetriever.Setup(p => p.RetrieveLatestPrice(It.IsAny<PriceSource>())).Returns(Task.FromResult(Maybe<PriceSnapshot>.None));
            var retriverFactoryMock = new Mock<IExternalPriceRetriverFactory>();
            retriverFactoryMock.Setup(p => p.Create(It.IsAny<string>())).Returns(Maybe.From(mockDataRetriever.Object));

            var queueMock = new Mock<IQueue>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(p => p.GetService(typeof(IUnitOfWork))).Returns(unitOfWorkMock.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(IExternalPriceRetriverFactory))).Returns(retriverFactoryMock.Object);
            mockServiceProvider.Setup(p => p.GetService(typeof(IQueue))).Returns(queueMock.Object);
            var service = new PriceSourceService(mockServiceProvider.Object);


            //Act
            var result = async () => await service.GetLatestPriceFromSource("test", "test");

            //Assert
            await Assert.ThrowsAsync<PriceSanpshotNotFoundException>(result);

        }
    }
}
