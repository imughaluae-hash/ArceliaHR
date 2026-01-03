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

        public MainForm()
        {
            InitializeComponent();
            dgList.DataBindingComplete += DgList_DataBindingComplete;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
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

            var employees = _employees.GetAll().ToList();
            _bsEmployees.DataSource = new SortableBindingList<EmployeeModel>(employees);
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

        private void btnDatabseTest_Click(object sender, EventArgs e)
        {
            var FilesForm = new Files();
            FilesForm.ShowDialog();

        }
    }
}
