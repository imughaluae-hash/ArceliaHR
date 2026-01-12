public class AttendanceSummary
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = "";

    public int Year { get; set; }
    public int Month { get; set; }

    public int CalendarDays { get; set; }

    public int PresentDays { get; set; }
    public int AbsentDays { get; set; }
    public int LeaveDays { get; set; }

    public decimal OvertimeHours { get; set; }
}
