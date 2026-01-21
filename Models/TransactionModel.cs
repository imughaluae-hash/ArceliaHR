namespace ArceliaHR.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime TransDate { get; set; }
        
        // "SalaryDue", "SalaryPaid", "Advance", "Fine", "AdjustmentPlus", "AdjustmentMinus"
        public string TransType { get; set; } = ""; 
        public decimal Amount { get; set; }
        public int? Month { get; set; } // YYYYMM
        public string? Remarks { get; set; }

        // Wrapper for display
        public string EmployeeName { get; set; } = "";
    }
}
