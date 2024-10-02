namespace DispatchRecordAPI.Models
{
    public class DispatchRecord
    {
        public required DateTime Date { get; set; }
        public required string ServiceArea { get; set; }
        public required string Route { get; set; }
        public required string TruckNumber { get; set; }
        public required string Driver { get; set; }
        public required string HelperOne { get; set; }
        public string? HelperTwo { get; set; }
        public required string RefuseType { get; set; }
    }
}
