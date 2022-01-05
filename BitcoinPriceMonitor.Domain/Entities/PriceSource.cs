using System.ComponentModel.DataAnnotations;

namespace BitCoinPriceMonitor.Domain.Data.Entities
{
    public class PriceSource : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string? HeaderParameters { get; set; }
        public IEnumerable<PriceSnapshot> PriceSnapshots { get; set; }

        public PriceSource()
        {
            PriceSnapshots = new HashSet<PriceSnapshot>();
        }
    }
}
