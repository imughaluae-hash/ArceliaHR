namespace ArceliaHR.Models
{
    public class AttendanceModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime AttDate { get; set; }

        public string Status { get; set; } = "P";   // P, A, L, H
        public double OvertimeHours { get; set; } = 0;

        public string? Remarks { get; set; }

        // UI helpers (not stored)
        public string EmployeeName { get; set; } = "";
    }
    public class MonthlyAttendanceModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = "";

        public Dictionary<int, string> Days { get; set; } = new();
        public Dictionary<int, double> Overtime { get; set; } = new();

        // 🔑 REQUIRED for DataGridView binding
        public string this[int day]
        {
            get => Days.ContainsKey(day) ? Days[day] : "";
            set => Days[day] = value;
        }
    }
}
