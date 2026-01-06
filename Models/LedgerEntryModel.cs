namespace ArceliaHR.Models
    {
        public class LedgerEntryModel
        {
            public int? Id { get; set; }
            public int EmployeeId { get; set; }

            public DateTime TranDate { get; set; }
            public string TranType { get; set; } = "";

            public string? Description { get; set; }

            public decimal Debit { get; set; }
            public decimal Credit { get; set; }

            public string? ReferenceMonth { get; set; }
        }
    }
