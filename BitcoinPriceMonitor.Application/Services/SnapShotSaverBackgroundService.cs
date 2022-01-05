using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Application.Services
{
    public class SnapShotSaverBackgroundService : BackgroundService, IUnitOfWorkChannelSaver
    {
        private readonly Channel<IUnitOfWork> _unitOfWorkChannel;
        private readonly ChannelWriter<IUnitOfWork> _writer;
        private readonly ChannelReader<IUnitOfWork> _reader;
        public SnapShotSaverBackgroundService()
        {
            _unitOfWorkChannel = Channel.CreateBounded<IUnitOfWork>(100);
            _writer = _unitOfWorkChannel.Writer;
            _reader = _unitOfWorkChannel.Reader;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (true)
            {
                try
                {
                    if (stoppingToken.IsCancellationRequested)
                        break;
                    while (await _reader.WaitToReadAsync().AsTask())
                        while (_reader.TryRead(out var unitOfWork))
                            await unitOfWork.SaveChangesAsync();
                }
                catch (Exception e)
                {

                    throw;
                }
              
            }
        }
        public async ValueTask AddUnitOfWorkForSaving(IUnitOfWork unitOfWork)
        {
            try
            {
                 
                await _writer.WriteAsync(unitOfWork);
            }
            catch (Exception e)
            {
                //todo add logging
            }
        }

    }
}
