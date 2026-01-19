using Dapper;
using System.Data;
using System.Data.SQLite;

namespace ArceliaHR.Database
{
    public class DbServices
    {
        public static class DbContext
        {
            private static string? connString;
            private static bool initialized;
            public static IDbConnection Open()
            {
                if (connString == null) connString = BuildConnectionString();

                var conn = new SQLiteConnection(connString);
                conn.Open();
                if (!initialized)
                {
                    CreatTables(conn);
                    initialized = true;
                }
                return conn;

            }
private static string BuildConnectionString()
{
#if DEBUG
    string appFolderName = "ArceliaHR_DEV";
#else
    string appFolderName = "ArceliaHR";
#endif

    string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    string folder = Path.Combine(appData, appFolderName);

    if (!Directory.Exists(folder))
        Directory.CreateDirectory(folder);

    string dbPath = Path.Combine(folder, "ArceliaHR.db");
    return $"Data Source={dbPath};Version=3;";
}
            private static void CreatTables(IDbConnection conn)
            {
                conn.Execute(@"
                CREATE TABLE IF NOT EXISTS Employees (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT,
                    FatherName TEXT,
                    BasicSalary REAL NOT NULL DEFAULT 0,
                    Religion TEXT,
                    MaritalStatus TEXT,
                    Gender TEXT,
                    DateOfBirth TEXT,

                    Mobile TEXT,
                    ICEContact TEXT,
                    Nationality TEXT,
                    Relation TEXT,

                    PassportNumber TEXT,
                    PassportIssueDate TEXT,
                    PassportExpiryDate TEXT,

                    IDNumber TEXT,
                    IDExpiryDate TEXT,

                    Work TEXT,
                    Department TEXT,

                    Picture BLOB,
                    Status TEXT

                 );");
                conn.Execute(@"
                CREATE TABLE IF NOT EXISTS Attendance (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    EmployeeId INTEGER NOT NULL,
                    AttDate DATE NOT NULL,
                    Status TEXT NOT NULL DEFAULT 'P',
                    OvertimeHours REAL NOT NULL DEFAULT 0,
                    Remarks TEXT,
                    UNIQUE(EmployeeId, AttDate)
                );");
                conn.Execute(@"CREATE UNIQUE INDEX IF NOT EXISTS
                    IX_Attendance_Unique
                    ON Attendance(EmployeeId, AttDate
                );");
                conn.Execute(@"
                CREATE TABLE IF NOT EXISTS Files (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    EmployeeId INTEGER NOT NULL,
                    FileName TEXT NOT NULL,
                    Extension TEXT NOT NULL,
                    MimeType TEXT,
                    FileSize INTEGER,
                    Data BLOB NOT NULL,
                    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY(EmployeeId) REFERENCES Employees(Id)
                );");
            }
        }
    }
}
