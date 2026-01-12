namespace ArceliaHR.Models
{
    public class SalaryResult
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = "";

        public int Year { get; set; }
        public int Month { get; set; }

        public decimal PerDayRate { get; set; }
        public decimal HourlyRate { get; set; }

        public decimal PresentPay { get; set; }
        public decimal OvertimePay { get; set; }

        public decimal TotalSalary { get; set; }
    }
}
