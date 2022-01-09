using BitcoinPriceMonitor.Application.Interfaces;
using BitcoinPriceMonitor.Domain.ExtensionMethods;
using BitCoinPriceMonitor.Domain.Data.Entities;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace BitCoinPriceMonitor.Infrastructure.Utitlities
{
    public class RestExternalPriceRetriever : IExternalPriceRetriver
    {
        private readonly IHttpGetter _httpGetter;
        private readonly IJsonParserToPriceSnapshotFactory _jsonParserToPriceSnapshotFactory;
        private readonly ILogger<IExternalPriceRetriver> _logger;
        public RestExternalPriceRetriever(IJsonParserToPriceSnapshotFactory jsonParserToPriceSnapshotFactory, IHttpGetter httpGetter, ILogger<IExternalPriceRetriver> logger)
        {
            _jsonParserToPriceSnapshotFactory = jsonParserToPriceSnapshotFactory;
            _httpGetter = httpGetter;
            _logger = logger;
        }

        public async Task<Maybe<PriceSnapshot>> RetrieveLatestPrice(PriceSource source)
        {

            try
            {
                var httpResponse = await _httpGetter.Get(source.Url, source.ConvertHeaderValuesToDictionary());
                if (string.IsNullOrEmpty(httpResponse))
                    throw new InvalidDataException($"Server responded with an empty response");

                var jsonParser = _jsonParserToPriceSnapshotFactory.CreateParser(source.Id);
                if (jsonParser.HasNoValue)
                    throw new NotImplementedException($"Parser for source: {source.Name} is not implemented");
                var priceSnapShot = jsonParser.Value.ParseJsonToPriceSnapshot(httpResponse);
                if (priceSnapShot.HasNoValue)
                    throw new InvalidCastException($"Failed to parse the response json to {typeof(PriceSnapshot).FullName}");
                return priceSnapShot;
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                return Maybe<PriceSnapshot>.None;
            }
        }
    }
}
