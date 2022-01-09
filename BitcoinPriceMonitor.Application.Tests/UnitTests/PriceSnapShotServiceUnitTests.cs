using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Application.Services;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestsCommon;
using Xunit;

namespace BitcoinPriceMonitor.Application.Tests.UnitTests
{
    public class PriceSnapShotServiceUnitTests
    {
        [Theory]
        [InlineData(null,null,null,0,10,false,false,false)]
        [InlineData(null, null, null, -1, 2, false, false, false)]
        public async Task GetPriceSnapshotsTestss(DateTime? dateFilter, DateTime? endDateFilter, string? sourceFilter, int pageNo, int pageSize, bool orderByDate, bool orderByPrice, bool descending)
        {
            //Arrange
            var pageListMock = MockPagedListGetter.GetPageListMock();
            Func<IQueryable<PriceSnapshot>, IOrderedQueryable<PriceSnapshot>> orderBy = default;
            Expression<Func<PriceSnapshot, bool>> predicate = default;
            var predicateBuilderMock = new Mock<IPriceSanpshotPredicateBuilder>();
            predicateBuilderMock.Setup(p => p.BuildPredicate(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string?>())).Returns(predicate);
            var orderByBuilderMock = new Mock<IPriceSnapshotOrderByBuilder>();
            orderByBuilderMock.Setup(p => p.BuildOrderBy(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(orderBy);
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.GetPageListAsyncSetUp<PriceSnapshot>()
                          .Returns(Task.FromResult(pageListMock.Object));

            //Act
            var service = new PriceSnapshotService(unitOfWorkMock.Object, predicateBuilderMock.Object, orderByBuilderMock.Object);

            //Act
            var result = await service.GetPriceSnapshots(dateFilter, endDateFilter, sourceFilter, pageNo, pageSize, orderByDate, orderByPrice, descending);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Items.Count() == pageListMock.Object.TotalCount);



        }
    }
}
