using ArceliaHR.Models;
using ArceliaHR.Services;
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
        public List<LedgerEntryModel> GetAllEntries(int employeeId)
        {
            using var conn = DbServices.DbContext.Open();

            return conn.Query<LedgerEntryModel>(@"
        SELECT * FROM EmployeeLedger
        WHERE EmployeeId = @EmployeeId
        ORDER BY TranDate, Id
    ", new { EmployeeId = employeeId }).ToList();
        }
        public decimal GetDueAmount(int employeeId, string tranType)
        {
            using var conn = DbContext.Open();

            return conn.ExecuteScalar<decimal>(@"
        SELECT IFNULL(SUM(Debit - Credit), 0)
        FROM EmployeeLedger
        WHERE EmployeeId = @EmployeeId
          AND TranType = @TranType
    ",
            new { EmployeeId = employeeId, TranType = tranType });
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
        public List<LedgerEntryModel> GetByEmployee(int employeeId)
        {
            using var conn = DbContext.Open();

            return conn.Query<LedgerEntryModel>(@"
        SELECT *
        FROM EmployeeLedger
        WHERE EmployeeId = @EmployeeId
        ORDER BY TranDate, Id
    ",
            new { EmployeeId = employeeId }).ToList();
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
        public List<LedgerEntryModel> GetByEmployeeAndMonth(int employeeId, string referenceMonth)
        {
            using var conn = DbContext.Open();
            return conn.Query<LedgerEntryModel>(@"
        SELECT *
        FROM EmployeeLedger
        WHERE EmployeeId = @EmployeeId
          AND ReferenceMonth = @ReferenceMonth
        ORDER BY TranDate, Id
    ",
            new { EmployeeId = employeeId, ReferenceMonth = referenceMonth }).ToList();
        }
        public void UpdateSalaryEntry(int id, decimal amount)
        {
            using var conn = DbContext.Open();
            conn.Execute(@"
        UPDATE EmployeeLedger
        SET Credit = @Amount
        WHERE Id = @Id
    ",
            new { Amount = amount, Id = id });
        }
        public decimal GetDueAdvance(int employeeId)
        {
            using var conn = DbContext.Open();

            return conn.ExecuteScalar<decimal>(@"
        SELECT IFNULL(
            SUM(CASE WHEN TranType = 'ADVANCE' THEN Debit ELSE 0 END)
          - SUM(CASE WHEN TranType = 'RECOVERY'
                     AND Description LIKE '%ADVANCE%' THEN Debit ELSE 0 END)
        ,0)
        FROM EmployeeLedger
        WHERE EmployeeId = @EmployeeId
    ", new { EmployeeId = employeeId });
        }
        public decimal GetDueFine(int employeeId)
        {
            using var conn = DbContext.Open();

            return conn.ExecuteScalar<decimal>(@"
        SELECT IFNULL(
            SUM(CASE WHEN TranType = 'FINE' THEN Debit ELSE 0 END)
          - SUM(CASE WHEN TranType = 'RECOVERY'
                     AND Description LIKE '%FINE%' THEN Debit ELSE 0 END)
        ,0)
        FROM EmployeeLedger
        WHERE EmployeeId = @EmployeeId
    ", new { EmployeeId = employeeId });
        }
        public decimal GetSalaryPaid(int employeeId)
        {
            using var conn = DbContext.Open();

            return conn.ExecuteScalar<decimal>(@"
        SELECT IFNULL(
            SUM(CASE WHEN TranType = 'RECOVERY'
                     AND Description LIKE '%SALARY%' THEN Debit ELSE 0 END)
        ,0)
        FROM EmployeeLedger
        WHERE EmployeeId = @EmployeeId
    ", new { EmployeeId = employeeId });
        }
        public decimal GetSalaryEarned(int employeeId)
        {
            using var conn = DbContext.Open();

            return conn.ExecuteScalar<decimal>(@"
        SELECT IFNULL(SUM(Credit),0)
        FROM EmployeeLedger
        WHERE EmployeeId = @EmployeeId
          AND TranType IN ('SALARY','ADJUSTMENT')
    ", new { EmployeeId = employeeId });
        }
        public decimal GetOutstandingSalary(int employeeId)
        {
            return GetSalaryEarned(employeeId) - GetSalaryPaid(employeeId);
        }

    public List<EmployeeStatementRow> BuildStatement(
    int employeeId,
    decimal currentMonthSalaryTillToday = 0,
    DateTime? tillDate = null
)
        {
            using var conn = DbContext.Open();

            var ledger = conn.Query<LedgerEntryModel>(@"
        SELECT *
        FROM EmployeeLedger
        WHERE EmployeeId = @EmployeeId
        ORDER BY TranDate, Id
    ", new { EmployeeId = employeeId }).ToList();

            var result = new List<EmployeeStatementRow>();
            decimal runningBalance = 0;
            int sn = 1;

            foreach (var l in ledger)
            {
                decimal due = 0;
                decimal paid = 0;

                if (l.Credit > 0)
                    due = l.Credit;

                if (l.Debit > 0)
                    paid = l.Debit;

                runningBalance += due - paid;

                result.Add(new EmployeeStatementRow
                {
                    SN = sn++,
                    Date = l.TranDate,
                    Description = l.Description ?? l.TranType,
                    DueAmount = due,
                    Paid = paid,
                    RunningBalance = runningBalance
                });
            }

            // 🔥 Inject virtual row: Current Salary Till Today
            if (currentMonthSalaryTillToday > 0)
            {
                runningBalance += currentMonthSalaryTillToday;

                result.Add(new EmployeeStatementRow
                {
                    SN = sn++,
                    Date = tillDate ?? DateTime.Today,
                    Description = "Current Salary Till Today",
                    DueAmount = currentMonthSalaryTillToday,
                    Paid = 0,
                    RunningBalance = runningBalance
                });
            }

            return result;
        }


    } }
