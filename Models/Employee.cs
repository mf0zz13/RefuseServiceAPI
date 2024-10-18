namespace DispatchRecordAPI.Models
{
    public class Employee
    {
        public required string EmployeeID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Role { get; set; }
        public DateTime? CdlExpiration { get; set; }
        public required DateTime EmploymentStartDate { get; set; }
        public DateTime? EmploymentEndDate { get; set; }
    }

}
