using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;
using System.Data.SQLite;

namespace ArceliaHR
{
    public partial class SettingsForm : Form
    {
        private SettingsModel? _settings;
        private readonly SettingsRepository _repo = new();
        private string _dbPath = "";


        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
            LoadDatabasePath();
        }

        private void LoadSettings()
        {
            _settings = _repo.GetSettings();
            
            txtCompanyName.Text = _settings.CompanyName;
            udWorkHours.Value = _settings.WorkHoursPerDay;
            
            if (_settings.CompanyLogo != null && _settings.CompanyLogo.Length > 0)
            {
                using var ms = new MemoryStream(_settings.CompanyLogo);
                picCompanyLogo.Image = Image.FromStream(ms);
            }
        }

        private void LoadDatabasePath()
        {
#if DEBUG
            string appFolderName = "ArceliaHR_DEV";
#else
            string appFolderName = "ArceliaHR";
#endif
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folder = Path.Combine(appData, appFolderName);
            _dbPath = Path.Combine(folder, "ArceliaHR.db");
            
            txtDbPath.Text = _dbPath;
        }

        private void btnBrowseLogo_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFile = new();
            openFile.Title = "Select Company Logo";
            openFile.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp; *.gif)|*.jpg; *.jpeg; *.png; *.bmp; *.gif";
            
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                using var img = Image.FromFile(openFile.FileName);
                picCompanyLogo.Image = new Bitmap(img);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using SaveFileDialog saveFile = new();
                saveFile.Title = "Export Database";
                saveFile.Filter = "Database Files (*.db)|*.db|All Files (*.*)|*.*";
                saveFile.FileName = $"ArceliaHR_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.db";
                
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    // Close all database connections
                    SQLiteConnection.ClearAllPools();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    
                    // Copy the database file
                    File.Copy(_dbPath, saveFile.FileName, true);
                    
                    MessageBox.Show("Database exported successfully!", "Export Complete", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting database: {ex.Message}", "Export Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "WARNING: Importing a database will replace all current data.\n\n" +
                    "This action cannot be undone. Are you sure you want to continue?",
                    "Import Database",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                
                if (result != DialogResult.Yes)
                    return;
                
                using OpenFileDialog openFile = new();
                openFile.Title = "Import Database";
                openFile.Filter = "Database Files (*.db)|*.db|All Files (*.*)|*.*";
                
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    // Validate the file is a SQLite database
                    if (!IsValidSQLiteDatabase(openFile.FileName))
                    {
                        MessageBox.Show("The selected file is not a valid SQLite database.", 
                            "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    // Close all database connections
                    SQLiteConnection.ClearAllPools();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    
                    // Backup current database before replacing
                    string backupPath = _dbPath + ".backup";
                    File.Copy(_dbPath, backupPath, true);
                    
                    try
                    {
                        // Replace the database
                        File.Copy(openFile.FileName, _dbPath, true);
                        
                        MessageBox.Show(
                            "Database imported successfully!\n\n" +
                            "The application will now restart to use the new database.",
                            "Import Complete",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        
                        // Restart the application
                        Application.Restart();
                    }
                    catch
                    {
                        // Restore backup on failure
                        File.Copy(backupPath, _dbPath, true);
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing database: {ex.Message}", "Import Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidSQLiteDatabase(string filePath)
        {
            try
            {
                using var conn = new SQLiteConnection($"Data Source={filePath};Version=3;");
                conn.Open();
                
                // Try to query sqlite_master to verify it's a valid SQLite database
                using var cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' LIMIT 1", conn);
                cmd.ExecuteScalar();
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_settings == null) return;
                
                _settings.CompanyName = txtCompanyName.Text;
                _settings.WorkHoursPerDay = udWorkHours.Value;

                
                if (picCompanyLogo.Image != null)
                {
                    _settings.CompanyLogo = ImageToBytes(picCompanyLogo.Image);
                }
                
                _repo.UpdateSettings(_settings);
                
                MessageBox.Show("Settings saved successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Save Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private byte[]? ImageToBytes(Image img)
        {
            if (img == null) return null;

            using var ms = new MemoryStream();
            using var bmp = new Bitmap(img);
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
