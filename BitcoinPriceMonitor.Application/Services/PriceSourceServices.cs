using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Interfaces;
using BitCoinPriceMonitor.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Application.Services
{
    public class PriceSourceServices : IPriceSourceServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public PriceSourceServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<PriceSource>> GetPriceSources()
        {
            var model = await _unitOfWork.GetRepository<PriceSource>().GetPagedListAsync(pageSize: int.MaxValue);
            return model.Items;
        }
        public async Task<decimal> GetLatestPriceFromSource(string sourceId)
        {
            throw new NotImplementedException();
        }
    }
}
