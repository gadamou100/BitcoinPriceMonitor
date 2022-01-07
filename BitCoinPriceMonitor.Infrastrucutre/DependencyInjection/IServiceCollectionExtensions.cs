using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Application.Services;
using BitCoinPriceMonitor.Infrastrucutre.Factories;
using BitCoinPriceMonitor.Infrastrucutre.Network;
using BitCoinPriceMonitor.Infrastrucutre.Persistance;
using Coravel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinPriceMonitor.Infrastrucutre.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static void InjectDependencies(this IServiceCollection services, string connectionString, bool injectIdentityServce=true)
        {

            if (injectIdentityServce)
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString))
                    .AddUnitOfWork<ApplicationDbContext>()
                    .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationDbContext>();
            else
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString))
                    .AddUnitOfWork<ApplicationDbContext>();
            services.AddTransient<IPriceSourceService, PriceSourceService>();
            services.AddTransient<IPriceSnapshotService, PriceSnapshotService>();
            services.AddTransient<IHttpGetter, HttpClientWrapper>();
            services.AddTransient<IJsonParserToPriceSnapshotFactory, JsonParserFactory>();
            services.AddTransient<UnitOfWorkSaveInvocable>();
            services.AddQueue();



        }
    }
}
