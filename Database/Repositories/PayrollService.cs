using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;

namespace ArceliaHR.Services
{
    public class PayrollService
    {
        private readonly AttendanceRepository _attendanceRepo;
        private readonly EmployeeRepository _employeeRepo;
        private readonly EmployeeLedgerRepository _ledgerRepo;

        public PayrollService(
            AttendanceRepository attendanceRepo,
            EmployeeRepository employeeRepo,
            EmployeeLedgerRepository ledgerRepo)
        {
            _attendanceRepo = attendanceRepo;
            _employeeRepo = employeeRepo;
            _ledgerRepo = ledgerRepo;
        }

        /// <summary>
        /// Checks if the month is editable today
        /// Rule: Month editable in itself + next month, locked from second next month
        /// </summary>
        public bool IsMonthEditable(string yearMonth)
        {
            var firstDay = DateTime.Parse(yearMonth + "-01");
            var lockDate = firstDay.AddMonths(2);
            return DateTime.Today < lockDate;
        }

        /// <summary>
        /// Processes salaries for a given month
        /// </summary>
        public void ProcessMonth(int year, int month)
        {
            string yearMonth = $"{year}-{month:D2}";

            if (!IsMonthEditable(yearMonth))
            {
                throw new InvalidOperationException($"Salary for {yearMonth} is locked and cannot be processed.");
            }

            // 1️⃣ Build attendance summary
            var attendanceSummaryService = new AttendanceSummaryService(_attendanceRepo);
            var summaries = attendanceSummaryService.BuildMonthSummary(year, month);

            foreach (var summary in summaries)
            {
                // 2️⃣ Get employee data
                var employee = _employeeRepo.GetById(summary.EmployeeId);
                if (employee == null) continue; // safety check

                // 3️⃣ Calculate salary
                var salary = SalaryCalculator.Calculate((int)employee.BasicSalary!, summary);

                // 4️⃣ Check if SALARY entry already exists for this month
                var existing = _ledgerRepo.GetByEmployeeAndMonth(summary.EmployeeId, yearMonth)
                    .FirstOrDefault(e => e.TranType == "SALARY");

                if (existing != null)
                {
                    // Update existing entry (optional) or skip
                    _ledgerRepo.UpdateSalaryEntry((int)existing.Id!, (decimal)salary.TotalSalary);
                }
                else
                {
                    // 5️⃣ Insert new ledger entry
                    _ledgerRepo.Add(
                        employeeId: summary.EmployeeId,
                        tranDate: DateTime.Today,
                        tranType: "SALARY",
                        amount: (decimal)salary.TotalSalary, // convert double to decimal
                        description: $"Salary for {yearMonth}",
                        referenceMonth: yearMonth
                    );
                }
            }
        }
    }
}
