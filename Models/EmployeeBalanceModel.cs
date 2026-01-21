namespace ArceliaHR.Models
{
    public class EmployeeBalanceModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = "";
        public string Status { get; set; } = ""; // Active/DeActive

        // Aggregates
        public decimal TotalPayable { get; set; }      // SalaryDue + AdjustmentPlus
        public decimal TotalPaid { get; set; }         // SalaryPaid + AdjustmentMinus
        public decimal TotalAdvance { get; set; }      // Advance
        public decimal TotalFine { get; set; }         // Fine
        
        // Calculated: Payable - (Paid + Advance + Fine)
        // Note: Paid includes SalaryPaid. 
        // Logic: Balance is what Company owes Employee.
        // Balance = TotalPayable - TotalPaid - TotalAdvance - TotalFine (assuming Paid/Advance/Fine are tracked as positive numbers in column, but conceptually deductions)
        public decimal CurrentBalance => TotalPayable - (TotalPaid + TotalAdvance + TotalFine);
    }
}
