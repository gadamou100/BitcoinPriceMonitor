using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinPriceMonitor.Infrastrucutre.DependencyInjection
{
    public static class WebApplicationBuilderExtensions
    {
        public static void InjectLogger(this WebApplicationBuilder webApplicationBuilder)
        {
            _ = webApplicationBuilder.Logging.ClearProviders();
            _ = webApplicationBuilder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
            _ = webApplicationBuilder.Host.UseNLog();

        }
    }
}
