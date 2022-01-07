using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsCommon
{
    public static class MockPagedListGetter
    {
        public static Mock<IPagedList<PriceSnapshot>>? GetPageListMock()
        {
            //Arrange
            List<PriceSnapshot> priceSnapshotList = GetData();

            Mock<IPagedList<PriceSnapshot>>? pageListMock = new Mock<IPagedList<PriceSnapshot>>();
            pageListMock.Setup(x => x.Items).Returns(priceSnapshotList);
            pageListMock.Setup(x => x.TotalPages).Returns(2);
            pageListMock.Setup(x => x.TotalCount).Returns(4);
            return pageListMock;
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
