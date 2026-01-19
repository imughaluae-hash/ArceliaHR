namespace ArceliaHR.Models
{
    public class EmployeeSummaryModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal TotalPayable { get; set; }
        public decimal BalanceAdvance { get; set; }
        public decimal BalanceFine { get; set; }
        public decimal TotalBalance { get; set; }
    }
}