using ArceliaHR.Database;
using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;
using Dapper;

namespace ArceliaHR
{
    public partial class Files : Form
    {
        private FileRepository fileRepo = new FileRepository();
        private List<EmployeeModel> employees = new List<EmployeeModel>();
        private List<string> tempFiles = new List<string>();


        public Files()
        {
            InitializeComponent();
            LoadEmployees();
            LoadFiles(); // load all files initially
        }

        #region Employee Combo Filter
        private void LoadEmployees()
        {
            try
            {
                using var conn = DbServices.DbContext.Open();
                employees = conn.Query<EmployeeModel>("SELECT Id, Name FROM Employees ORDER BY Name").ToList();

                // Insert an "All Employees" option at the top
                employees.Insert(0, new EmployeeModel { Id = 0, Name = "All Employees" });

                cmbEmployeeFilter.DataSource = employees;
                cmbEmployeeFilter.DisplayMember = "Name";
                cmbEmployeeFilter.ValueMember = "Id";
                cmbEmployeeFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employees: " + ex.Message);
            }
        }

        private void CmbEmployeeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFiles(); // reload files for the selected employee
        }
        #endregion

        #region Load Files
        private void LoadFiles()
        {
            listViewFiles.Items.Clear();
            imageListFiles.Images.Clear();

            int selectedEmployeeId = 0;
            if (cmbEmployeeFilter.SelectedItem is EmployeeModel emp)
                selectedEmployeeId = (int)emp.Id!;

            List<FileModel> files;

            if (selectedEmployeeId == 0)
            {
                // All employees
                files = fileRepo.GetAllFiles();
            }
            else
            {
                files = fileRepo.GetFilesByEmployee(selectedEmployeeId);
            }

            int imageIndex = 0;

            foreach (var file in files)
            {
                var icon = GetFileIcon(file);
                imageListFiles.Images.Add(icon);

                var item = new ListViewItem(file.FileName)
                {
                    Tag = file.Id,
                    ImageIndex = imageIndex++
                };
                listViewFiles.Items.Add(item);
            }
        }

        private Image GetFileIcon(FileModel file)
        {
            switch (file.Extension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".bmp":
                case ".gif":
                    {
                        using var ms = new MemoryStream(file.Data);
                        var img = Image.FromStream(ms);
                        return img.GetThumbnailImage(128, 128, null, IntPtr.Zero);
                    }
                case ".pdf":
                    return Properties.Resources.pdf_icon;
                case ".doc":
                case ".docx":
                    return Properties.Resources.word_icon;
                case ".xls":
                case ".xlsx":
                    return Properties.Resources.excel_icon;
                default:
                    return Properties.Resources.file_icon;
            }
        }
        #endregion

        #region DoubleClick Open File
        private void listViewFiles_DoubleClick(object sender, EventArgs e)
        {
            if (listViewFiles.SelectedItems.Count == 0) return;

            int fileId = (int)listViewFiles.SelectedItems[0].Tag!;
            var file = fileRepo.GetFileById(fileId);
            if (file == null) return;

            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + file.Extension);
            File.WriteAllBytes(tempPath, file.Data);

            // Remember this file for deletion later
            tempFiles.Add(tempPath);

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = tempPath,
                UseShellExecute = true
            });
        }
        #endregion
        private void Files_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var file in tempFiles)
            {
                try
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }
                catch
                {
                    // ignore if a file is locked or open
                }
            }
        }
        #region Add File Button
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            var addFileForm = new AddFileForm();
            addFileForm.ShowDialog();

            // After adding, refresh files for currently selected employee
            LoadFiles();
        }
        #endregion
    }
}
