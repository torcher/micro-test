namespace data_collector.Models
{
    public class Activation
    {
        public Guid DeviceId { get; set; }
        public string DeviceSerialNumber { get; set; }

        public long DispensedMicroLitre { get; set; }

        public long DispensedTimestamp { get; set; }

        public DateTime DispensedDateTime {  get => DateTime.UnixEpoch.AddMilliseconds(DispensedTimestamp); }
    }
}
