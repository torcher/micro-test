namespace hhe_service.Models.Entities
{
    public class HHE
    {
        public Guid Id { get; set; }
        public string DeviceSerialNumber { get; set; }
        public long DispensedMicroLitre { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
