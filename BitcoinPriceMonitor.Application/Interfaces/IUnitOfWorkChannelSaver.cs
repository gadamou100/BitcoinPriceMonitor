using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Application.Interfaces
{
    public interface IUnitOfWorkChannelSaver : IHostedService
    {
        ValueTask AddUnitOfWorkForSaving(IUnitOfWork unitOfWork);
    }
}
