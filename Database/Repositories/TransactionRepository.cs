using ArceliaHR.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using static ArceliaHR.Database.DbServices;

namespace ArceliaHR.Database.Repositories
{
    public class TransactionRepository
    {
        private const int HoursPerDay = 9;
        private const string SalaryTransType = "Salary";
        private const string PaymentTransType = "Payment";
        private const string AdvanceTransType = "Advance";
        private const string AdjustmentTransType = "Adjustment";
        private const string FineTransType = "Fine";
        private const string PresentStatus = "P";

        public void GenerateMonthlySalary(int employeeId, int month, int year)
        {
            using var conn = DbContext.Open();

            var exists = conn.QuerySingleOrDefault<int>(
                $"SELECT COUNT(*) FROM Transactions WHERE EmployeeId = @EmployeeId AND TransType = '{SalaryTransType}' AND Month = @Month AND Year = @Year",
                new { EmployeeId = employeeId, Month = month, Year = year });
            if (exists > 0) return;

            var emp = conn.QuerySingle<EmployeeModel>("SELECT * FROM Employees WHERE Id = @Id", new { Id = employeeId });

            var attendRepo = new AttendanceRepository();
            var attendances = attendRepo.GetByMonth(month, year).Where(a => a.EmployeeId == employeeId).ToList();

            int daysInMonth = DateTime.DaysInMonth(year, month);
            var (basicPay, otPay) = CalculateSalaryComponents(emp.BasicSalary ?? 0, attendances, daysInMonth);
            decimal payable = basicPay + otPay;

            conn.Execute(@"
                INSERT INTO Transactions (EmployeeId, TransDate, Description, TransType, Amount, Month, Year)
                VALUES (@EmployeeId, @TransDate, @Description, @TransType, @Amount, @Month, @Year)",
                new
                {
                    EmployeeId = employeeId,
                    TransDate = new DateTime(year, month, daysInMonth),
                    Description = $"Salary for {month:D2}/{year}",
                    TransType = SalaryTransType,
                    Amount = payable,
                    Month = month,
                    Year = year
                });
        }

        public void AddTransaction(TransactionModel trans)
        {
            using var conn = DbContext.Open();
            conn.Execute(@"
                INSERT INTO Transactions (EmployeeId, TransDate, Description, TransType, Amount, Month, Year)
                VALUES (@EmployeeId, @TransDate, @Description, @TransType, @Amount, @Month, @Year)", trans);
        }

        public List<TransactionModel> GetByEmployee(int employeeId)
        {
            using var conn = DbContext.Open();
            return conn.Query<TransactionModel>(
                "SELECT * FROM Transactions WHERE EmployeeId = @Id ORDER BY TransDate ASC, CreatedAt ASC",
                new { Id = employeeId }).ToList();
        }

        public List<EmployeeSummaryModel> GetSummaries(string status)
        {
            using var conn = DbContext.Open();
            var employees = conn.Query<EmployeeModel>(
                "SELECT Id, Name FROM Employees WHERE Status = @Status ORDER BY Name",
                new { Status = status });

            var summaries = new List<EmployeeSummaryModel>();
            foreach (var emp in employees)
            {
                var trans = GetByEmployee(emp.Id!.Value);

                decimal totalPayable = trans.Where(t => t.TransType == SalaryTransType).Sum(t => t.Amount);
                decimal balanceAdvance = Math.Abs(trans.Where(t => t.TransType == AdvanceTransType || t.TransType == AdjustmentTransType).Sum(t => t.Amount));
                decimal balanceFine = Math.Abs(trans.Where(t => t.TransType == FineTransType).Sum(t => t.Amount));
                decimal paid = trans.Where(t => t.TransType == PaymentTransType).Sum(t => t.Amount);

                decimal totalBalance = totalPayable - balanceAdvance - balanceFine - paid;

                summaries.Add(new EmployeeSummaryModel
                {
                    Id = emp.Id.Value,
                    Name = emp.Name!,
                    TotalPayable = totalPayable,
                    BalanceAdvance = balanceAdvance,
                    BalanceFine = balanceFine,
                    TotalBalance = totalBalance
                });
            }
            return summaries;
        }

        public bool IsMonthPaid(int employeeId, int month, int year)
        {
            using var conn = DbContext.Open();
            return conn.QuerySingleOrDefault<int>(
                $"SELECT COUNT(*) FROM Transactions WHERE EmployeeId = @EmployeeId AND TransType = '{PaymentTransType}' AND Month = @Month AND Year = @Year",
                new { EmployeeId = employeeId, Month = month, Year = year }) > 0;
        }

        public (decimal PendingSalary, decimal Basic, decimal Ot, decimal Advances, decimal Fines) GetPendingBreakdown(int employeeId, int month, int year)
        {
            using var conn = DbContext.Open();

            var salaryTrans = conn.QuerySingleOrDefault<TransactionModel>(
                $"SELECT * FROM Transactions WHERE EmployeeId = @EmployeeId AND TransType = '{SalaryTransType}' AND Month = @Month AND Year = @Year",
                new { EmployeeId = employeeId, Month = month, Year = year });

            if (salaryTrans == null) return (0m, 0m, 0m, 0m, 0m);

            decimal pendingSalary = salaryTrans.Amount;

            var emp = conn.QuerySingle<EmployeeModel>("SELECT * FROM Employees WHERE Id = @Id", new { Id = employeeId });
            var attendRepo = new AttendanceRepository();
            var attendances = attendRepo.GetByMonth(month, year).Where(a => a.EmployeeId == employeeId).ToList();

            int daysInMonth = DateTime.DaysInMonth(year, month);
            var (basic, ot) = CalculateSalaryComponents(emp.BasicSalary ?? 0, attendances, daysInMonth);

            var allTrans = GetByEmployee(employeeId);
            decimal advances = Math.Abs(allTrans.Where(t => t.TransType == AdvanceTransType || t.TransType == AdjustmentTransType).Sum(t => t.Amount));
            decimal fines = Math.Abs(allTrans.Where(t => t.TransType == FineTransType).Sum(t => t.Amount));

            decimal priorPaid = allTrans.Where(t => t.TransType == PaymentTransType && t.Month == month && t.Year == year).Sum(t => t.Amount);
            pendingSalary -= priorPaid;

            return (pendingSalary, basic, ot, advances, fines);
        }

        public decimal CalculatePartialSalary(int employeeId, DateTime tillDate)
        {
            using var conn = DbContext.Open();
            var emp = conn.QuerySingle<EmployeeModel>("SELECT * FROM Employees WHERE Id = @Id", new { Id = employeeId });

            int month = tillDate.Month;
            int year = tillDate.Year;
            int daysInMonth = DateTime.DaysInMonth(year, month);

            if (emp.BasicSalary == null) return 0;

            var attendRepo = new AttendanceRepository();
            var attendances = attendRepo.GetByMonth(month, year)
                .Where(a => a.EmployeeId == employeeId && a.AttDate <= tillDate)
                .ToList();

            var (basic, ot) = CalculateSalaryComponents(emp.BasicSalary.Value, attendances, daysInMonth);
            return basic + ot;
        }

        private (decimal Basic, decimal Ot) CalculateSalaryComponents(decimal basicSalary, List<AttendanceModel> attendances, int daysInMonth)
        {
            decimal perDay = basicSalary / daysInMonth;
            decimal perHour = perDay / HoursPerDay;

            int presentDays = attendances.Count(a => a.Status == PresentStatus);
            double otHours = attendances.Sum(a => a.OvertimeHours);

            decimal basic = perDay * presentDays;
            decimal ot = perHour * (decimal)otHours;
            return (basic, ot);
        }
    }
}