using Arch.EntityFrameworkCore.UnitOfWork;
using BitCoinPriceMonitor.Domain.Data.Entities;
using Coravel.Invocable;
using Microsoft.Extensions.Logging;
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
        public readonly ILogger<UnitOfWorkSaveInvocable> _logger;


        public UnitOfWorkSaveInvocable(IUnitOfWork unitOfWork, ILogger<UnitOfWorkSaveInvocable> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
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
                _logger.LogError($"{e}");
            }
        }
    }
}
