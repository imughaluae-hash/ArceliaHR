using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;

namespace ArceliaHR.Services
{
    public class AttendanceSummaryService
    {
        private readonly AttendanceRepository _attendanceRepo;

        public AttendanceSummaryService(AttendanceRepository attendanceRepo)
        {
            _attendanceRepo = attendanceRepo;
        }

        /// <summary>
        /// Salary till a specific date (used for "Current Salary Till Today")
        /// </summary>
        public SalaryResult CalculateSalaryTillDate(
            int employeeId,
            decimal? basicSalary,
            int year,
            int month,
            DateTime tillDate
        )
        {
            var summary = GetMonthlySummary(
                employeeId,
                year,
                month,
                tillDate
            );

            return SalaryCalculator.Calculate((decimal)basicSalary!, summary);
        }

        /// <summary>
        /// Attendance summary for one employee till a date
        /// </summary>
        public AttendanceSummary GetMonthlySummary(
            int employeeId,
            int year,
            int month,
            DateTime tillDate
        )
        {
            var records = _attendanceRepo
                .GetByMonth(month, year)
                .Where(x => x.EmployeeId == employeeId)
                .Where(x => x.AttDate <= tillDate)
                .ToList();

            int present = records.Count(x => x.Status == "P");
            int absent = records.Count(x => x.Status == "A");
            int leave = records.Count(x => x.Status == "L");

            decimal overtime =
                records.Sum(x => Convert.ToDecimal(x.OvertimeHours));

            return new AttendanceSummary
            {
                EmployeeId = employeeId,
                Year = year,
                Month = month,
                CalendarDays = DateTime.DaysInMonth(year, month),
                PresentDays = present,
                AbsentDays = absent,
                LeaveDays = leave,
                OvertimeHours = overtime
            };
        }

        /// <summary>
        /// Builds attendance summary for all employees (month close)
        /// </summary>
        public List<AttendanceSummary> BuildMonthSummary(int year, int month)
        {
            var summaries = new List<AttendanceSummary>();

            var employees = _attendanceRepo.GetAllEmployees().ToList();
            var attendanceRecords =
                _attendanceRepo.GetByMonth(month, year).ToList();

            int calendarDays = DateTime.DaysInMonth(year, month);

            foreach (var emp in employees)
            {
                var empRecords = attendanceRecords
                    .Where(a => a.EmployeeId == emp.Id)
                    .ToList();

                var summary = new AttendanceSummary
                {
                    EmployeeId = (int)emp.Id!,
                    EmployeeName = emp.Name!,
                    Year = year,
                    Month = month,
                    CalendarDays = calendarDays
                };

                foreach (var r in empRecords)
                {
                    if (r.Status == "P")
                        summary.PresentDays++;

                    if (r.Status == "A")
                        summary.AbsentDays++;

                    if (r.Status == "L")
                        summary.LeaveDays++;

                    summary.OvertimeHours +=
                        Convert.ToDecimal(r.OvertimeHours);
                }

                summaries.Add(summary);
            }

            return summaries;
        }
    }
}
