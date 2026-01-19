namespace ArceliaHR.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime TransDate { get; set; }
        public string Description { get; set; } = "";
        public string TransType { get; set; } = "";  // Salary, Payment, Advance, Fine, Adjustment
        public decimal Amount { get; set; }  // + for Salary/Payment, - for deductions
        public int? Month { get; set; }
        public int? Year { get; set; }
        public DateTime CreatedAt { get; set; }

        // UI helper
        public decimal RunningBalance { get; set; }
    }
}