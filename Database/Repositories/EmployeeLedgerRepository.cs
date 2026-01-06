using ArceliaHR.Models;
using Dapper;
using static ArceliaHR.Database.DbServices;

namespace ArceliaHR.Database.Repositories
{
    public class EmployeeLedgerRepository
    {
        public void Add(
            int employeeId,
            DateTime tranDate,
            string tranType,
            decimal amount,
            string? description = null,
            string? referenceMonth = null
        )
        {
            decimal debit = 0;
            decimal credit = 0;

            switch (tranType)
            {
                case "ADVANCE":
                case "FINE":
                case "RECOVERY":
                    debit = amount;
                    break;

                case "SALARY":
                case "ADJUSTMENT":
                    credit = amount;
                    break;

                default:
                    throw new ArgumentException("Invalid transaction type");
            }

            using var conn = DbContext.Open();

            conn.Execute(@"
                INSERT INTO EmployeeLedger
                (EmployeeId, TranDate, TranType, Description, Debit, Credit, ReferenceMonth)
                VALUES
                (@EmployeeId, @TranDate, @TranType, @Description, @Debit, @Credit, @ReferenceMonth)
            ",
            new
            {
                EmployeeId = employeeId,
                TranDate = tranDate,
                TranType = tranType,
                Description = description,
                Debit = debit,
                Credit = credit,
                ReferenceMonth = referenceMonth
            });
        }

        public decimal GetBalance(int employeeId)
        {
            using var conn = DbContext.Open();

            return conn.ExecuteScalar<decimal>(@"
                SELECT IFNULL(SUM(Credit - Debit), 0)
                FROM EmployeeLedger
                WHERE EmployeeId = @EmployeeId
            ", new { EmployeeId = employeeId });
        }

        public bool IsSalaryIssued(int employeeId, string referenceMonth)
        {
            using var conn = DbContext.Open();

            return conn.ExecuteScalar<int>(@"
                SELECT COUNT(1)
                FROM EmployeeLedger
                WHERE EmployeeId = @EmployeeId
                  AND TranType = 'SALARY'
                  AND ReferenceMonth = @ReferenceMonth
            ",
            new { EmployeeId = employeeId, ReferenceMonth = referenceMonth }) > 0;
        }
    }
}
