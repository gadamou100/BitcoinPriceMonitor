using BitcoinPriceMonitor.Application.Interfaces;
using BitCoinPriceMonitor.Domain.Data.Entities;
using BitCoinPriceMonitor.Infrastructure.Utitlities;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BitCoinPriceMonitor.Infrastructure.Tests
{
    public class RestExternalPriceRetrieverTests
    {
        [Fact]
        public async Task TestRetrieveLatestPrice()
        {
            //Arrange
            var httGetterMock = new Mock<IHttpGetter>();
            httGetterMock.Setup(p=>p.Get(It.IsAny<string>(), It.IsAny<IDictionary<string, string>?>())).Returns(Task.FromResult("test response"));
            var jsonParserMock = new Mock<IJsonParserToPriceSnapshot>();
            jsonParserMock.Setup(p => p.ParseJsonToPriceSnapshot(It.IsAny<string>())).Returns(new PriceSnapshot());
            var jsonParserFactoryMock = new Mock<IJsonParserToPriceSnapshotFactory>();
            jsonParserFactoryMock.Setup(p => p.CreateParser(It.IsAny<string>())).Returns(Maybe.From(jsonParserMock.Object));
            var loggerMock = new Mock<ILogger<IExternalPriceRetriver>>();
            var retriver = new RestExternalPriceRetriever(jsonParserFactoryMock.Object, httGetterMock.Object,loggerMock.Object);
            //Act
            var result = await retriver.RetrieveLatestPrice(new PriceSource { Id = "Test", Name = "Test Source" });
            //Assert
            Assert.True(result.HasValue);

        }

        [Fact]
        public async Task TestRetrieveLatestPriceInvalidResponse()
        {
            //Arrange
            var httGetterMock = new Mock<IHttpGetter>();
            httGetterMock.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<IDictionary<string, string>?>())).Returns(Task.FromResult(""));
            var jsonParserMock = new Mock<IJsonParserToPriceSnapshot>();
            jsonParserMock.Setup(p => p.ParseJsonToPriceSnapshot(It.IsAny<string>())).Returns(new PriceSnapshot());
            var jsonParserFactoryMock = new Mock<IJsonParserToPriceSnapshotFactory>();
            jsonParserFactoryMock.Setup(p => p.CreateParser(It.IsAny<string>())).Returns(Maybe.From(jsonParserMock.Object));
            var loggerMock = new Mock<ILogger<IExternalPriceRetriver>>();
            var retriver = new RestExternalPriceRetriever(jsonParserFactoryMock.Object, httGetterMock.Object, loggerMock.Object);
            //Act
            var result = await retriver.RetrieveLatestPrice(new PriceSource { Id = "Test", Name = "Test Source" });
            //Assert
            Assert.True(result.HasNoValue);

        }

        [Fact]
        public async Task TestRetrieveLatestPriceUnsupportedParser()
        {
            //Arrange
            var httGetterMock = new Mock<IHttpGetter>();
            httGetterMock.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<IDictionary<string, string>?>())).Returns(Task.FromResult(""));
            var jsonParserFactoryMock = new Mock<IJsonParserToPriceSnapshotFactory>();
            jsonParserFactoryMock.Setup(p => p.CreateParser(It.IsAny<string>())).Returns(Maybe<IJsonParserToPriceSnapshot>.None);
            var loggerMock = new Mock<ILogger<IExternalPriceRetriver>>();
            var retriver = new RestExternalPriceRetriever(jsonParserFactoryMock.Object, httGetterMock.Object, loggerMock.Object);
            //Act
            var result = await retriver.RetrieveLatestPrice(new PriceSource { Id = "Test", Name = "Test Source" });
            //Assert
            Assert.True(result.HasNoValue);

        }
        [Fact]
        public async Task TestRetrieveLatestPriceParsingFails()
        {
            //Arrange
            var httGetterMock = new Mock<IHttpGetter>();
            httGetterMock.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<IDictionary<string, string>?>())).Returns(Task.FromResult("test response"));
            var jsonParserMock = new Mock<IJsonParserToPriceSnapshot>();
            jsonParserMock.Setup(p => p.ParseJsonToPriceSnapshot(It.IsAny<string>())).Returns(Maybe<PriceSnapshot>.None);
            var jsonParserFactoryMock = new Mock<IJsonParserToPriceSnapshotFactory>();
            jsonParserFactoryMock.Setup(p => p.CreateParser(It.IsAny<string>())).Returns(Maybe.From(jsonParserMock.Object));
            var loggerMock = new Mock<ILogger<IExternalPriceRetriver>>();
            var retriver = new RestExternalPriceRetriever(jsonParserFactoryMock.Object, httGetterMock.Object, loggerMock.Object);
            //Act
            var result = await retriver.RetrieveLatestPrice(new PriceSource { Id = "Test", Name = "Test Source" });
            //Assert
            Assert.True(result.HasNoValue);

        }
    }
}
