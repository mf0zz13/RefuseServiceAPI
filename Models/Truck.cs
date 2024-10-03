namespace DispatchRecordAPI.Models
{
    public class Truck
    {
        public required string TruckNumber { get; set; }
        public required bool NeedMaintenance { get; set; }
        public DateTime RegistrationExpirationDate { get; set; }
    }
}
