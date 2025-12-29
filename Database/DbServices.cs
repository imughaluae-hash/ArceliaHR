using Dapper;
using System.Data;
using System.Data.SQLite;

namespace ArceliaHR.Database
{
    public class DbServices
    {
        public static class DbContext
        {
            private static string connString;
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
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string folder = Path.Combine(appData, "ArceliaHR");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
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
                IDCardNumber TEXT,
                IDExpiryDate TEXT,

                Work TEXT,
                Department TEXT,

                Picture BLOB,
                Status TEXT
                 );");
            }
        }
    }
}
