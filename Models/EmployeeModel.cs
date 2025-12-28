using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string? IDCardNumber { get; set; }
        public DateTime? IDExpiryDate { get; set; }

        public string? Work { get; set; }
        public string? Department { get; set; }

        public byte[]? Picture { get; set; }
        public string? Status { get; set; }
    }

}
