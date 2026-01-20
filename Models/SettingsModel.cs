namespace ArceliaHR.Models
{
    public class SettingsModel
    {
        public int Id { get; set; } = 1; // Singleton record
        public string? CompanyName { get; set; }
        public byte[]? CompanyLogo { get; set; }
        public decimal WorkHoursPerDay { get; set; } = 9;
        public DateTime LastModified { get; set; }
    }
}
