namespace DispatchRecordAPI.Models
{
    public class DispatchRecord
    {
        public  DateTime Date { get; set; }
        public  string ServiceArea { get; set; }
        public  string Route { get; set; }
        public  string TruckNumber { get; set; }
        public  string Driver { get; set; }
        public  string HelperOne { get; set; }
        public string HelperTwo { get; set; }
        public  string RefuseType { get; set; }
    }
}
