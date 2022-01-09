using Arch.EntityFrameworkCore.UnitOfWork;
using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Application.Services;
using BitCoinPriceMonitor.Infrastructure.Factories;
using BitCoinPriceMonitor.Infrastructure.Network;
using BitCoinPriceMonitor.Infrastructure.Persistance;
using BitCoinPriceMonitor.Infrastructure.Utitlities;
using Coravel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BitCoinPriceMonitor.Infrastructure.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static void InjectDependencies(this IServiceCollection services, string connectionString, bool injectIdentityServce=true)
        {

            if (injectIdentityServce)
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString))
                    .AddUnitOfWork<ApplicationDbContext>()
                    .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
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
            services.AddTransient<IPriceSnapshotOrderByBuilder, PriceSnapshotOrderByBuilder>();
            services.AddTransient<IPriceSanpshotPredicateBuilder, PriceSanpshotPredicateBuilder>();
            services.AddTransient<IExternalPriceRetriverFactory, ExternalPriceRetriverFactory>();

            services.AddQueue();



        }
    }
}
