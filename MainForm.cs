using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;

namespace ArceliaHR
{
    public partial class MainForm : Form
    {
        private int _lastSelectedEmployeeId = 0;
        private int _lastFirstDisplayedRow = 0;

        private readonly IEmployeeRepository _employees = new EmployeeRepository();
        private BindingSource _bsEmployees = new BindingSource();
        private List<EmployeeModel> _allEmployees = new List<EmployeeModel>();
        private string _currentSearchColumn = "Name";

        public MainForm()
        {
            InitializeComponent();
            dgList.DataBindingComplete += DgList_DataBindingComplete;
            btnPay.Click += btnPay_Click;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeSearchControls();
            LoadEmployees();
            UpdateEditButtonState();
        }

        private void btnAddEmp_Click(object sender, EventArgs e)
        {
            var form = new EmpForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees(true);
            }
        }

        private void btnEditEmp_Click(object sender, EventArgs e)
        {
            int empId = GetSelectedEmployeeId();
            if (empId == 0)
            {
                MessageBox.Show("Please select an employee");
                return;
            }

            SaveGridState();
            var form = new EmpForm(empId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees(true);
            }
        }

        private void dgList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgList.Rows.Count == 0 || e.RowIndex < 0) return;

            int empId = GetSelectedEmployeeId();
            if (empId == 0) return;

            SaveGridState();
            var form = new EmpForm(empId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees(true);
            }
        }

        private void LoadEmployees(bool restoreSelection = false)
        {
            SaveGridState();

            dgList.SuspendLayout();

            dgList.RowTemplate.Height = 60;
            dgList.AutoGenerateColumns = true;
            dgList.ReadOnly = true;
            dgList.AllowUserToAddRows = false;
            dgList.AllowUserToDeleteRows = false;

            // Load all employees and store them
            _allEmployees = _employees.GetAll().ToList();
            _bsEmployees.DataSource = _allEmployees;
            dgList.DataSource = _bsEmployees;

            if (dgList.Columns["Id"] != null)
                dgList.Columns["Id"].Visible = false;

            FormatDataGrid();

            if (dgList.Columns["Picture"] != null)
                dgList.Columns["Picture"].DisplayIndex = 0;

            dgList.ResumeLayout();

            if (restoreSelection)
                RestoreGridState();

            UpdateEditButtonState();
        }


        private int GetSelectedEmployeeId()
        {
            if (dgList.CurrentRow?.DataBoundItem is EmployeeModel emp)
                return emp.Id ?? 0;

            return 0;
        }

        private void SaveGridState()
        {
            if (dgList.CurrentRow?.DataBoundItem is EmployeeModel emp)
                _lastSelectedEmployeeId = emp.Id ?? 0;

            if (dgList.FirstDisplayedScrollingRowIndex >= 0)
                _lastFirstDisplayedRow = dgList.FirstDisplayedScrollingRowIndex;
        }


        private void RestoreGridState()
        {
            if (_lastSelectedEmployeeId == 0 || dgList.Rows.Count == 0)
                return;

            foreach (DataGridViewRow row in dgList.Rows)
            {
                if (row.DataBoundItem is EmployeeModel emp &&
                    emp.Id == _lastSelectedEmployeeId)
                {
                    row.Selected = true;

                    var cell = row.Cells.Cast<DataGridViewCell>()
                        .FirstOrDefault(c => c.Visible);

                    if (cell != null)
                        dgList.CurrentCell = cell;

                    if (_lastFirstDisplayedRow >= 0 &&
                        _lastFirstDisplayedRow < dgList.RowCount)
                    {
                        dgList.FirstDisplayedScrollingRowIndex =
                            _lastFirstDisplayedRow;
                    }
                    break;
                }
            }
        }


        private void FormatDataGrid()
        {
            // Picture column
            if (dgList.Columns["Picture"] != null)
            {
                var picCol = (DataGridViewImageColumn)dgList.Columns["Picture"];
                picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
                picCol.Width = 60;
                picCol.SortMode = DataGridViewColumnSortMode.NotSortable; // images cannot sort
            }

            // Format date columns to show only date (no time)
            string[] dateColumns = { "DateOfBirth", "PassportIssueDate", "PassportExpiryDate", "IDExpiryDate" };
            foreach (string dateCol in dateColumns)
            {
                if (dgList.Columns[dateCol] != null)
                {
                    dgList.Columns[dateCol].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            }

            // Other columns
            foreach (DataGridViewColumn col in dgList.Columns)
            {
                if (col.Name != "Picture")
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                col.DefaultCellStyle.Padding = new Padding(5, 5, 5, 5);
                if (col.Name != "Picture")
                    col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        // This ensures row colors persist after sorting
        private void DgList_DataBindingComplete(
            object? sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgList.Rows)
            {
                if (row.DataBoundItem is EmployeeModel emp)
                {
                    row.DefaultCellStyle.BackColor =
                        emp.Status == "Active" ? Color.LightGreen :
                        emp.Status == "DeActive" ? Color.LightPink :
                        Color.White;
                }
            }
        }

        private void UpdateEditButtonState()
        {
            btnEditEmp.Enabled = dgList.Rows.Count > 0;
        }

        private void btnAttend_Click(object sender, EventArgs e)
        {
            var aForm = new MonthlyAttendanceForm();
            aForm.ShowDialog();
        }

        private void btnFiles_Click(object sender, EventArgs e)
        {
            var FilesForm = new Files();
            FilesForm.ShowDialog();
        }

        private void btnPay_Click(object? sender, EventArgs e)
        {
            var form = new PaymentControlPanel();
            form.ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void InitializeSearchControls()
        {
            // Populate search column combobox
            cmbSearch.Items.Clear();
            cmbSearch.Items.AddRange(new string[] {
                "Name",
                "FatherName",
                "Religion",
                "MaritalStatus",
                "Gender",
                "Mobile",
                "ICEContact",
                "Nationality",
                "PassportNumber",
                "IDNumber",
                "Work",
                "Department",
                "Status"
            });
            cmbSearch.SelectedIndex = 0; // Default to Name
            _currentSearchColumn = "Name";

            // Wire up event handlers
            txtSearch.TextChanged += TxtSearch_TextChanged;
            cmbSearch.SelectedIndexChanged += CmbSearch_SelectedIndexChanged;
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            ApplySearchFilter();
        }

        private void CmbSearch_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbSearch.SelectedItem != null)
            {
                _currentSearchColumn = cmbSearch.SelectedItem.ToString() ?? "Name";
                ApplySearchFilter();
            }
        }

        private void ApplySearchFilter()
        {
            if (_allEmployees == null || _allEmployees.Count == 0)
                return;

            string searchText = txtSearch.Text?.Trim() ?? "";

            List<EmployeeModel> filteredList;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Show all records
                filteredList = _allEmployees;
            }
            else
            {
                // Apply filter based on selected column using LINQ
                filteredList = _allEmployees.Where(emp =>
                {
                    var propValue = GetPropertyValue(emp, _currentSearchColumn);
                    if (propValue == null) return false;
                    return propValue.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;
                }).ToList();
            }

            // Update the binding source with filtered data
            _bsEmployees.DataSource = filteredList;
            _bsEmployees.ResetBindings(false);

            UpdateEditButtonState();
        }

        private string GetPropertyValue(EmployeeModel emp, string propertyName)
        {
            var prop = typeof(EmployeeModel).GetProperty(propertyName);
            if (prop == null) return string.Empty;

            var value = prop.GetValue(emp);
            return value?.ToString() ?? string.Empty;
        }
    }
}

