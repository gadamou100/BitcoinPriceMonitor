using Arch.EntityFrameworkCore.UnitOfWork;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Coravel.Invocable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Application.Services
{
    public class UnitOfWorkSaveInvocable : IInvocable, IInvocableWithPayload<PriceSnapshot>
    {
        public PriceSnapshot Payload { get; set; }
        public readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkSaveInvocable(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Invoke()
        {
            try
            {
                if (Payload == null)
                    return;
                await _unitOfWork.GetRepository<PriceSnapshot>().InsertAsync(Payload);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //toodo replace with a loger after the addition of the logger.
                Debug.WriteLine($"{e}");
            }
        }
    }
}
