using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Services;
using BitcoinPriceMonitor.Domain.Constants;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsCommon;
using Xunit;

namespace BitcoinPriceMonitor.Application.Tests.UnitTests
{
    public class UnitOfWorkSaveInvocableTests
    {
        [Fact]
        public async Task TestInvoke()
        {
            //Act
            var id = Guid.NewGuid().ToString();
            var serviceProvider = ServiceProviderGetter.GetServiceProvider();
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            var unitOfWork2 = serviceProvider.GetRequiredService<IUnitOfWork>();
            var payload = new PriceSnapshot
            {
                Id = id,
                CreatedTimeStamp = DateTime.Now,
                PriceSourceId = SourceSeededIds.CoinBase,
                RetrievedTimeStamp = DateTime.Now,
                Value = 10,
                CreatorId=""
            };
            var loggerMock = new Mock<ILogger<UnitOfWorkSaveInvocable>>();
            UnitOfWorkSaveInvocable invocable = new UnitOfWorkSaveInvocable(unitOfWork,loggerMock.Object);
            invocable.Payload = payload;
            //Act
            await invocable.Invoke();
            //Assert
            var storedEnity = await unitOfWork2.GetRepository<PriceSnapshot>().GetFirstOrDefaultAsync( predicate: p=>p.Id== id);
            Assert.Equal(10, storedEnity.Value);

        }
    }
}
