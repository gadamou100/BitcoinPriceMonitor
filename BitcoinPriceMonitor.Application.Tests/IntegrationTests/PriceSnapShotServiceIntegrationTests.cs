using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Application.Services;
using BitcoinPriceMonitor.Domain.Constants;
using BitcoinPriceMonitor.Domain.ExtensionMethods;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsCommon;
using Xunit;

namespace BitcoinPriceMonitor.Application.Tests.IntegrationTests
{
    public class PriceSnapShotServiceIntegrationTests
    {
        
        [Fact]
        public async Task GetAllPriceSnapshotsTest()
        {
            //Arrange
            var serviceProvider = ServiceProviderGetter.GetServiceProvider();
            var uow = serviceProvider.GetRequiredService<IUnitOfWork>();
            var orderByBuilder = serviceProvider.GetRequiredService<IPriceSnapshotOrderByBuilder>();
            var predicateBuilder = serviceProvider.GetRequiredService<IPriceSanpshotPredicateBuilder>();
            var priceSnapShotService = new PriceSnapshotService(uow, predicateBuilder, orderByBuilder);
            //Act
            var result = await priceSnapShotService.GetPriceSnapshots();
            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
        }

        [Fact]
        public async Task TestEdit()
        {
            decimal initialValue = (decimal)44.35;

            //Arrange
            var entity = new PriceSnapshot
            {
                Id = "test",
                PriceSourceId = SourceSeededIds.BitStamp,
                CreatorId = "",
                CreatedTimeStamp = DateTime.Now,
                Value = initialValue,
                RetrievedTimeStamp = DateTime.Now,

            };

            var serviceProvider = ServiceProviderGetter.GetServiceProvider();
            var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            var storedEnity = await unitOfWork.GetRepository<PriceSnapshot>().GetFirstOrDefaultAsync(predicate: p=>p.Id=="test");
            if (storedEnity == null)
            {
                await unitOfWork.GetRepository<PriceSnapshot>().InsertAsync(entity);
                await unitOfWork.SaveChangesAsync();
            }
            var priceSourceService = new PriceSnapshotService(unitOfWork,default,default);
            entity.Comments = "Test";
            entity.Value = 0;
            //Act
            await priceSourceService.Edit(entity, "test");
            //Assert
            storedEnity = await unitOfWork.GetRepository<PriceSnapshot>().GetFirstOrDefaultAsync(predicate: p => p.Id == "test");
            Assert.Equal("Test",storedEnity.Comments);
            Assert.Equal(initialValue.Round(),storedEnity.Value.Round());//value should not change
        }

        [Fact]
        public async Task TestDelete()
        {
            //Arrange
            var entity = new PriceSnapshot
            {
                Id = "test",
                PriceSourceId = SourceSeededIds.BitStamp,
                CreatorId = "",
                CreatedTimeStamp = DateTime.Now,
                Value = (decimal) 23,
                RetrievedTimeStamp = DateTime.Now,

            };

            var serviceProvider = ServiceProviderGetter.GetServiceProvider();
            var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            var storedEnity = await unitOfWork.GetRepository<PriceSnapshot>().GetFirstOrDefaultAsync(predicate: p => p.Id == "test");
            if (storedEnity == null)
            {
                await unitOfWork.GetRepository<PriceSnapshot>().InsertAsync(entity);
                await unitOfWork.SaveChangesAsync();
            }
            var priceSourceService = new PriceSnapshotService(unitOfWork, default, default);
            //Act
            await priceSourceService.Delete("test");
            //Assert
            storedEnity = await unitOfWork.GetRepository<PriceSnapshot>().GetFirstOrDefaultAsync(predicate: p => p.Id == "test");
            Assert.Null(storedEnity);

        }
    }
}
