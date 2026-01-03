using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;
using System.ComponentModel;

namespace ArceliaHR
{
    public partial class Attendance : Form
    {
        private List<AttendanceModel> _list = new();
        public Attendance()
        {
            InitializeComponent();
        }

        private void Attendance_Load(object sender, EventArgs e)
        {
            dgvAttendance.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvAttendance.IsCurrentCellDirty)
                    dgvAttendance.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            dgvAttendance.EditingControlShowing += dgvAttendance_EditingControlShowing;
            GridFormating();
        }
        private void dgvAttendance_EditingControlShowing(
    object? sender,
    DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvAttendance.CurrentCell.OwningColumn.Name == "OvertimeHours")
            {
                if (e.Control is TextBox tb)
                {
                    tb.KeyPress -= Overtime_KeyPress;
                    tb.KeyPress += Overtime_KeyPress;
                }
            }
        }

        private void Overtime_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // allow digits, backspace, and dot
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // allow only ONE dot
            if (e.KeyChar == '.' &&
                sender is TextBox tb &&
                tb.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        public void GridFormating()
        {
            dgvAttendance.AutoGenerateColumns = false;
            dgvAttendance.Columns.Clear();

            dgvAttendance.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EmployeeId",
                DataPropertyName = "EmployeeId",
                HeaderText = "ID",
                ReadOnly = true
            });

            dgvAttendance.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EmployeeName",
                DataPropertyName = "EmployeeName",
                HeaderText = "Name",
                ReadOnly = true,
                Width = 180
            });

            dgvAttendance.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "Status",
                DataPropertyName = "Status",
                HeaderText = "Att",
                DataSource = new[] { "P", "A", "L", "H" },
                Width = 60
            });

            dgvAttendance.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OvertimeHours",
                DataPropertyName = "OvertimeHours",
                HeaderText = "OT (hrs)",
                Width = 80
            });

            dgvAttendance.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Remarks",
                DataPropertyName = "Remarks",
                HeaderText = "Remarks",
                Width = 150
            });
        }


        private void dgvAttendance_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            dgvAttendance.CellValueChanged -= dgvAttendance_CellValueChanged!; // unsubscribe

            var row = dgvAttendance.Rows[e.RowIndex];
            var status = row.Cells["Status"].Value?.ToString();

            // Only change row color
            if (status == "P")
            {
                row.DefaultCellStyle.BackColor = Color.LightGreen;
            }
            else if (status == "A")
            {
                row.DefaultCellStyle.BackColor = Color.LightPink;
            }
            else if (status == "L")
            {
                row.DefaultCellStyle.BackColor = Color.LightYellow;
            }
            else if (status == "H")
            {
                row.DefaultCellStyle.BackColor = Color.LightBlue;
            }

            // OT is always editable
            row.Cells["OvertimeHours"].ReadOnly = false;

            dgvAttendance.CellValueChanged += dgvAttendance_CellValueChanged!; // re-subscribe
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            var repo = new AttendanceRepository();
            _list = repo.GetByDate(dtDate.Value);

            dgvAttendance.DataSource = new BindingList<AttendanceModel>(_list);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dgvAttendance.EndEdit();

            var repo = new AttendanceRepository();
            repo.Save(_list);

            MessageBox.Show("Attendance saved successfully");
        }

        private void dtDate_ValueChanged(object sender, EventArgs e)
        {
            var repo = new AttendanceRepository();
            _list = repo.GetByDate(dtDate.Value);

            dgvAttendance.DataSource = new BindingList<AttendanceModel>(_list);
        }

        private void btnPrevDay_Click(object sender, EventArgs e)
        {
            dtDate.Value = dtDate.Value.AddDays(-1);
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
            dtDate.Value = dtDate.Value.AddDays(1);
        }
    }
}
