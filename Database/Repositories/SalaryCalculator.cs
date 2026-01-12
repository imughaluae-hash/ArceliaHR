using ArceliaHR.Models;

namespace ArceliaHR.Services
{
    public static class SalaryCalculator
    {
        public static SalaryResult Calculate(
            decimal basicSalary,
            AttendanceSummary summary
        )
        {
            var result = new SalaryResult
            {
                EmployeeId = summary.EmployeeId,
                EmployeeName = summary.EmployeeName,
                Year = summary.Year,
                Month = summary.Month
            };

            // 1️⃣ Per-day rate
            result.PerDayRate =
                Math.Round(basicSalary / summary.CalendarDays, 2);

            // 2️⃣ Hourly rate (9 hours/day)
            result.HourlyRate =
                Math.Round(result.PerDayRate / 9m, 2);

            // 3️⃣ Present salary
            result.PresentPay =
                Math.Round(result.PerDayRate * summary.PresentDays, 2);

            // 4️⃣ Overtime salary
            result.OvertimePay =
                Math.Round(result.HourlyRate * summary.OvertimeHours, 2);

            // 5️⃣ Total salary
            result.TotalSalary =
                result.PresentPay + result.OvertimePay;

            return result;
        }
    }
}
