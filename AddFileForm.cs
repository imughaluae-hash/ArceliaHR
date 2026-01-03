using ArceliaHR.Database;
using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;
using Dapper;

namespace ArceliaHR
{
    public partial class AddFileForm : Form
    {
        private int selectedEmployeeId;
        private string selectedFilePath = "";
        private List<EmployeeModel> employees = new List<EmployeeModel>();
        private FileRepository fileRepo = new FileRepository();

        public AddFileForm()
        {
            InitializeComponent();
            LoadEmployees();

            // Assign event handler for ComboBox selection
        }

        private void CmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmployee.SelectedIndex == -1)
            {
                selectedEmployeeId = 0;
                return;
            }

            // Get the selected EmployeeModel object
            if (cmbEmployee.SelectedItem is EmployeeModel emp)
            {
                selectedEmployeeId = (int)emp.Id!;
            }
            else
            {
                selectedEmployeeId = 0;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedFilePath = ofd.FileName;
                lblSelectedFile.Text = selectedFilePath;
            }
        }

        private void LoadEmployees()
        {
            try
            {
                using var conn = DbServices.DbContext.Open();
                employees = conn.Query<EmployeeModel>("SELECT Id, Name FROM Employees ORDER BY Name").ToList();

                cmbEmployee.DataSource = employees;
                cmbEmployee.DisplayMember = "Name";
                cmbEmployee.ValueMember = "Id";
                cmbEmployee.SelectedIndex = -1; // nothing selected by default
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employees: " + ex.Message);
            }
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeId == 0)
            {
                MessageBox.Show("Please select an employee.");
                return;
            }

            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Please choose a file to upload.");
                return;
            }

            try
            {
                fileRepo.AddFileForEmployee(selectedFilePath, selectedEmployeeId, txtFileName.Text);
                MessageBox.Show("File uploaded successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error uploading file: " + ex.Message);
            }
        }
    }
}
