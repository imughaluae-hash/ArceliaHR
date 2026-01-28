using ArceliaHR.Models;
using Dapper;
using static ArceliaHR.Database.DbServices;

namespace ArceliaHR.Database.Repositories
{
    public class TransactionRepository
    {
        public void Add(TransactionModel trans)
        {
            using var conn = DbContext.Open();
            string query = @"
                INSERT INTO Transactions (EmployeeId, TransDate, TransType, Amount, Month, Remarks)
                VALUES (@EmployeeId, @TransDate, @TransType, @Amount, @Month, @Remarks);
            ";
            conn.Execute(query, trans);
        }

        public IEnumerable<TransactionModel> GetByEmployee(int employeeId)
        {
            using var conn = DbContext.Open();
            string query = @"
                SELECT t.*, e.Name as EmployeeName 
                FROM Transactions t
                JOIN Employees e ON t.EmployeeId = e.Id
                WHERE t.EmployeeId = @EmployeeId
                ORDER BY t.TransDate DESC, t.Id DESC;
            ";
            return conn.Query<TransactionModel>(query, new { EmployeeId = employeeId });
        }

        public IEnumerable<EmployeeBalanceModel> GetBalanceSummaries(string? statusFilter = null)
        {
            using var conn = DbContext.Open();

            string sql = @"
                SELECT 
                    e.Id as EmployeeId,
                    e.Name as EmployeeName,
                    e.Status,
                    CAST(COALESCE(SUM(CASE WHEN t.TransType IN ('SalaryDue', 'AdjustmentPlus', 'AdvanceRecovery', 'FineRecovery') THEN t.Amount ELSE 0 END), 0) AS REAL) as TotalPayable,
                    CAST(COALESCE(SUM(CASE WHEN t.TransType IN ('SalaryPaid', 'AdjustmentMinus') THEN t.Amount ELSE 0 END), 0) AS REAL) as TotalPaid,
                    CAST(COALESCE(SUM(CASE WHEN t.TransType = 'Advance' THEN t.Amount ELSE 0 END), 0) AS REAL) as TotalAdvance,
                    CAST(COALESCE(SUM(CASE WHEN t.TransType = 'Fine' THEN t.Amount ELSE 0 END), 0) AS REAL) as TotalFine

                FROM Employees e
                LEFT JOIN Transactions t ON e.Id = t.EmployeeId
                WHERE 1=1
            ";

            if (!string.IsNullOrEmpty(statusFilter))
            {
                sql += " AND e.Status = @Status ";
            }

            sql += " GROUP BY e.Id, e.Name, e.Status ORDER BY e.Name";

            return conn.Query<EmployeeBalanceModel>(sql, new { Status = statusFilter });
        }

        public bool IsSalaryGenerated(int employeeId, int month)
        {
            using var conn = DbContext.Open();
            int count = conn.ExecuteScalar<int>(@"
                SELECT COUNT(1) FROM Transactions 
                WHERE EmployeeId = @EmployeeId AND Month = @Month AND TransType = 'SalaryDue'
            ", new { EmployeeId = employeeId, Month = month });
            return count > 0;
        }

        public decimal GetCurrentBalance(int employeeId)
        {
            using var conn = DbContext.Open();
            string sql = @"
                 SELECT 
                    CAST(
                        COALESCE(SUM(CASE WHEN TransType IN ('SalaryDue', 'AdjustmentPlus', 'AdvanceRecovery', 'FineRecovery') THEN Amount ELSE 0 END), 0) 
                        - COALESCE(SUM(CASE WHEN TransType IN ('SalaryPaid', 'AdjustmentMinus', 'Advance', 'Fine') THEN Amount ELSE 0 END), 0)
                    AS REAL)
                 FROM Transactions
                 WHERE EmployeeId = @EmployeeId
            ";
            return conn.ExecuteScalar<decimal>(sql, new { EmployeeId = employeeId });
        }
    public (decimal Advance, decimal Fine) GetOutstandingBalances(int employeeId)
        {
            using var conn = DbContext.Open();
            string sql = @"
                SELECT 
                    CAST(COALESCE(SUM(CASE WHEN TransType = 'Advance' THEN Amount ELSE 0 END), 0) AS REAL) as TotalAdvance,
                    CAST(COALESCE(SUM(CASE WHEN TransType = 'AdvanceRecovery' THEN Amount ELSE 0 END), 0) AS REAL) as RecoveredAdvance,
                    
                    CAST(COALESCE(SUM(CASE WHEN TransType = 'Fine' THEN Amount ELSE 0 END), 0) AS REAL) as TotalFine,
                    CAST(COALESCE(SUM(CASE WHEN TransType = 'FineRecovery' THEN Amount ELSE 0 END), 0) AS REAL) as RecoveredFine
                FROM Transactions
                WHERE EmployeeId = @EmployeeId
            ";
            
            var result = conn.QueryFirstOrDefault(sql, new { EmployeeId = employeeId });
            
            if (result == null) return (0, 0);

            decimal outstandingAdv = (decimal)result.TotalAdvance - (decimal)result.RecoveredAdvance;
            decimal outstandingFine = (decimal)result.TotalFine - (decimal)result.RecoveredFine;

            // Ensure we don't return negative balances (if recovery somehow exceeded original due)
            if (outstandingAdv < 0) outstandingAdv = 0;
            if (outstandingFine < 0) outstandingFine = 0;

            return (outstandingAdv, outstandingFine);
        }
    }
}
