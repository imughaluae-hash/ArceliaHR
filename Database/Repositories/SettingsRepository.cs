using ArceliaHR.Models;
using Dapper;
using static ArceliaHR.Database.DbServices;

namespace ArceliaHR.Database.Repositories
{
    public class SettingsRepository
    {
        public SettingsModel GetSettings()
        {
            using var conn = DbContext.Open();
            
            var settings = conn.QuerySingleOrDefault<SettingsModel>(
                "SELECT * FROM Settings WHERE Id = 1");

            if (settings == null)
            {
                // Create default settings
                settings = new SettingsModel
                {
                    Id = 1,
                    CompanyName = "ArceliaHR",
                    WorkHoursPerDay = 9,
                    LastModified = DateTime.Now
                };

                conn.Execute(@"
                    INSERT INTO Settings (Id, CompanyName, WorkHoursPerDay, LastModified)
                    VALUES (@Id, @CompanyName, @WorkHoursPerDay, @LastModified)", settings);
            }

            return settings;
        }

        public void UpdateSettings(SettingsModel settings)
        {
            settings.LastModified = DateTime.Now;
            
            using var conn = DbContext.Open();
            
            conn.Execute(@"
                UPDATE Settings 
                SET CompanyName = @CompanyName,
                    CompanyLogo = @CompanyLogo,
                    WorkHoursPerDay = @WorkHoursPerDay,
                    LastModified = @LastModified
                WHERE Id = 1", settings);
        }

        public decimal GetWorkHoursPerDay()
        {
            using var conn = DbContext.Open();
            
            var hours = conn.QuerySingleOrDefault<decimal?>(
                "SELECT WorkHoursPerDay FROM Settings WHERE Id = 1");
            
            return hours ?? 9; // Default to 9 hours if not set
        }
    }
}
