using ArceliaHR.Models;
using Dapper;
using static ArceliaHR.Database.DbServices;

namespace ArceliaHR.Database.Repositories
{
    public class AttendanceRepository
    {
        public List<AttendanceModel> GetByDate(DateTime date)
        {
            using var conn = DbContext.Open();

            var sql = @"
SELECT 
    e.Id AS EmployeeId,
    e.Name AS EmployeeName,
    IFNULL(a.Id, 0) AS Id,
    @Date AS AttDate,
    IFNULL(a.Status, 'P') AS Status,
    IFNULL(a.OvertimeHours, 0) AS OvertimeHours,
    a.Remarks
FROM Employees e
LEFT JOIN Attendance a 
    ON a.EmployeeId = e.Id 
    AND a.AttDate = @Date
WHERE e.Status = 'Active'
ORDER BY e.Name;
";

            return conn.Query<AttendanceModel>(sql, new { Date = date.Date }).ToList();
        }

        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            using var conn = DbContext.Open();
            return conn.Query<EmployeeModel>("SELECT Id, Name FROM Employees WHERE Status = 'Active';");
        }
        public IEnumerable<AttendanceModel> GetByMonth(int month, int year)
        {
            using var conn = DbContext.Open();
            return conn.Query<AttendanceModel>(
                @"SELECT 
            e.Id AS EmployeeId,
            e.Name AS EmployeeName,
            IFNULL(a.Id, 0) AS Id,
            a.AttDate,
            IFNULL(a.Status, 'P') AS Status,
            CAST(IFNULL(a.OvertimeHours, 0) AS REAL) AS OvertimeHours,
            a.Remarks
        FROM Employees e
        LEFT JOIN Attendance a
            ON a.EmployeeId = e.Id
            AND strftime('%m', a.AttDate) = @Month
            AND strftime('%Y', a.AttDate) = @Year
        WHERE e.Status = 'Active'
        ORDER BY e.Name;",
                new { Month = month.ToString("D2"), Year = year.ToString() }
            );
        }
        public void Save(IEnumerable<AttendanceModel> list)
        {
            using var conn = DbContext.Open();
            using var tran = conn.BeginTransaction();

            foreach (var a in list)
            {
                conn.Execute(@"
        INSERT INTO Attendance (
            EmployeeId, AttDate, Status, OvertimeHours, Remarks
        )
        VALUES (
            @EmployeeId, @AttDate, @Status, @OvertimeHours, @Remarks
        )
        ON CONFLICT(EmployeeId, AttDate)
        DO UPDATE SET
            Status = excluded.Status,
            OvertimeHours = excluded.OvertimeHours,
            Remarks = excluded.Remarks;
        ", a, tran);
            }

            tran.Commit();
        }

    }

}
