namespace ArceliaHR.Models
{
    public class EmployeeStatementRow
    {
        public int SN { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = "";

        public decimal DueAmount { get; set; }
        public decimal Paid { get; set; }

        public decimal RunningBalance { get; set; }
    }
}
