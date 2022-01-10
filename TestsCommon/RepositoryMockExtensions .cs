using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.Language.Flow;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace TestsCommon
{
    public static class RepositoryWorkMockExtensions
    {
        public static ISetup<IRepository<TEntity>, Task<IPagedList<TEntity>>> GetPageListAsyncSetUp<TEntity>(this Mock<IRepository<TEntity>> source) where TEntity : BaseEntity
        {
            return source.Setup(p => p
                           .GetPagedListAsync(
                           It.IsAny<Expression<Func<TEntity, TEntity>>>(),
                           It.IsAny<Expression<Func<TEntity, bool>>>(),
                           It.IsAny<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>(),
                           It.IsAny<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>(),
                           It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), default(CancellationToken), It.IsAny<bool>()));
        }
        
        public static ISetup<IRepository<TEntity>, Task<TEntity>> GetFirstOrDefaultAsyncSetUp<TEntity>(this Mock<IRepository<TEntity>> source) where TEntity : BaseEntity
        {
            return source.Setup(p => p.GetFirstOrDefaultAsync(
                           It.IsAny<Expression<Func<TEntity, TEntity>>>(),
                           It.IsAny<Expression<Func<TEntity, bool>>>(),
                           It.IsAny<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>(),
                           It.IsAny<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>(), It.IsAny<bool>(), It.IsAny<bool>()));
        }
    }
}
