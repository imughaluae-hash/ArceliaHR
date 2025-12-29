using ArceliaHR.Database;
using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ArceliaHR
{
    public partial class MainForm : Form
    {
        private int _lastSelectedEmployeeId = 0;
        private int _lastFirstDisplayedRow = 0;
        public MainForm()
        {
            InitializeComponent();

        }
        private readonly IEmployeeRepository _employees =
    new EmployeeRepository();
        private void btnAddEmp_Click(object sender, EventArgs e)
        {
            var EmpForms = new EmpForm();
            EmpForms.ShowDialog();
        }

        private void btnDatabseTest_Click(object sender, EventArgs e)
        {
            //_employees.Add(new EmployeeModel
            //{
            //    Name = "Imran"
            //});
            //LoadEmployees();
        }
        private void LoadEmployees(bool restoreSelection = false)
        {
            dgList.RowTemplate.Height = 60; // fixed height for picture row
            dgList.ReadOnly = true;
            dgList.AllowUserToAddRows = false;
            dgList.AllowUserToDeleteRows = false;

            dgList.DataSource = _employees.GetAll().ToList();

            if (dgList.Columns["Id"] != null)
                dgList.Columns["Id"].Visible = false;

            FormatDataGrid();
            if (dgList.Columns["Picture"] != null)
                dgList.Columns["Picture"].DisplayIndex = 0;

            if (restoreSelection && dgList.Rows.Count > 0)
                RestoreGridState();
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
            form.ShowDialog();

            LoadEmployees(restoreSelection: true);
        }
        private int GetSelectedEmployeeId()
        {
            if (dgList.CurrentRow?.DataBoundItem is EmployeeModel emp)
                return (int)emp.Id;

            return 0;
        }

        private void dgList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (dgList.Rows.Count == 0 || e.RowIndex < 0)
                return;

            int empId = GetSelectedEmployeeId();
            if (empId == 0) return;
            SaveGridState();
            var form = new EmpForm(empId);
            form.ShowDialog();

            LoadEmployees(restoreSelection: true);
        }
        private void SaveGridState()
        {
            if (dgList.CurrentRow?.DataBoundItem is EmployeeModel emp)
                _lastSelectedEmployeeId = (int)emp.Id;

            if (dgList.FirstDisplayedScrollingRowIndex >= 0)
                _lastFirstDisplayedRow = dgList.FirstDisplayedScrollingRowIndex;
        }
        private void RestoreGridState()
        {
            if (_lastSelectedEmployeeId == 0) return;

            foreach (DataGridViewRow row in dgList.Rows)
            {
                if (row.DataBoundItem is EmployeeModel emp &&
                    emp.Id == _lastSelectedEmployeeId)
                {
                    row.Selected = true;

                    var firstVisibleCell = row.Cells
                        .Cast<DataGridViewCell>()
                        .FirstOrDefault(c => c.Visible);

                    if (firstVisibleCell != null)
                        dgList.CurrentCell = firstVisibleCell;

                    if (_lastFirstDisplayedRow >= 0 &&
                        _lastFirstDisplayedRow < dgList.RowCount)
                    {
                        dgList.FirstDisplayedScrollingRowIndex = _lastFirstDisplayedRow;
                    }

                    break;
                }
            }
        }
        private void FormatDataGrid()
        {
            //dgList.RowTemplate.Height = 60; // increase to fit pictures
            //dgList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //// Set text alignment for all columns to top-left
            //foreach (DataGridViewColumn col in dgList.Columns)
            //{
            //    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            //    col.DefaultCellStyle.Padding = new Padding(5, 5, 5, 5);
            //}

            //// Highlight Active/DeActive employees
            //foreach (DataGridViewRow row in dgList.Rows)
            //{
            //    if (row.DataBoundItem is EmployeeModel emp)
            //    {
            //        if (emp.Status == "Active")
            //            row.DefaultCellStyle.BackColor = Color.LightGreen;
            //        else if (emp.Status == "DeActive")
            //            row.DefaultCellStyle.BackColor = Color.LightPink;
            //    }
            //}

            //// Optional: picture column display
            //if (dgList.Columns["Picture"] != null)
            //{
            //    dgList.Columns["Picture"].Width = 60;
            //    dgList.Columns["Picture"].DefaultCellStyle.NullValue = null; // in case no image
            //    dgList.Columns["Picture"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}
            //foreach (DataGridViewColumn col in dgList.Columns)
            //{
            //    col.DefaultCellStyle.Padding = new Padding(5, 5, 5, 5);
            //}
            //if (dgList.Columns["Picture"] != null)
            //    ((DataGridViewImageColumn)dgList.Columns["Picture"]).ImageLayout = DataGridViewImageCellLayout.Stretch;
            // Picture column fixed size
            if (dgList.Columns["Picture"] != null)
            {
                var picCol = (DataGridViewImageColumn)dgList.Columns["Picture"];
                picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
                picCol.Width = 60;
            }

            // Auto-size all other columns based on content
            foreach (DataGridViewColumn col in dgList.Columns)
            {
                if (col.Name != "Picture")
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                col.DefaultCellStyle.Padding = new Padding(5, 5, 5, 5);
            }

            // Highlight Active/DeActive employees
            foreach (DataGridViewRow row in dgList.Rows)
            {
                if (row.DataBoundItem is EmployeeModel emp)
                {
                    if (emp.Status == "Active")
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    else if (emp.Status == "DeActive")
                        row.DefaultCellStyle.BackColor = Color.LightPink;
                }
            }

            // Enable column sorting
            foreach (DataGridViewColumn col in dgList.Columns)
                col.SortMode = DataGridViewColumnSortMode.Automatic;

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadEmployees();
            if (dgList.Rows.Count == 0)
            {
                // Optional UX hint
                MessageBox.Show("No employees found");
                btnEditEmp.Enabled = false;
            }
            else
            {
                //lblStatus.Text = "";
                btnEditEmp.Enabled = true;
            }
        }
    }
}
