using BitCoinPriceMonitor.Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace TestsCommon
{
    public static class ServiceProviderGetter
    {
        public static IServiceProvider GetServiceProvider()
        {
            var servicesCollection = new ServiceCollection();
            servicesCollection.InjectDependencies("Server=(localdb)\\mssqllocaldb;Database=aspnet-BitcoinPriceMonitor-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true", false);
            var serviceProvider = servicesCollection.BuildServiceProvider();
            return serviceProvider;
        }

    }
}