namespace BitCoinPriceMonitor.Domain.Data.Entities
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
        public string CreatorId { get; set; }
        public DateTime? UpdateTimeStamp { get; set; }
        public string? UpdaterId { get; set; }
    }
}
