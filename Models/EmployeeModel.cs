namespace ArceliaHR.Models
{
    public class EmployeeModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? FatherName { get; set; }
        public string? Religion { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string? Mobile { get; set; }
        public string? ICEContact { get; set; }
        public string? Nationality { get; set; }
        public string? Relation { get; set; }

        public string? PassportNumber { get; set; }
        public DateTime? PassportIssueDate { get; set; }
        public DateTime? PassportExpiryDate { get; set; }

        public string? IDNumber { get; set; }
        public DateTime? IDExpiryDate { get; set; }

        public string? Work { get; set; }
        public string? Department { get; set; }

        public byte[]? Picture { get; set; }
        public string? Status { get; set; }
        public decimal? BasicSalary { get; set; }
    }
    //public class SalaryTransactionModel
    //{
    //    public int Id { get; set; }
    //    public int EmployeeId { get; set; }

    //    public DateTime TranDate { get; set; }

    //    // ADVANCE, FINE, ADJUSTMENT, SALARY
    //    public string? TranType { get; set; }

    //    // +ve or -ve decided by type
    //    public decimal Amount { get; set; }

    //    public string? Description { get; set; }

    //    // Optional but powerful
    //    public string? RefMonth { get; set; } // "2026-01"
    //}


}
