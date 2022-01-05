namespace BitCoinPriceMonitor.Domain.Data.Entities
{
    public class PriceSnapshot : BaseEntity
    {
        public string PriceSourceId { get; set; }
        public decimal Value { get; set;}
        public DateTime RetrievedTimeStamp { get; set; }
        public PriceSource PriceSource { get; set; }
        public string Comments { get; set; }
    }
}
