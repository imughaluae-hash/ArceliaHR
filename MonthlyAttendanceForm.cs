using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;

namespace ArceliaHR
{
    public partial class MonthlyAttendanceForm : Form
    {
        private List<MonthlyAttendanceModel> _monthlyData = new();
        private int _month, _year;

        public MonthlyAttendanceForm()
        {
            InitializeComponent();
            _month = DateTime.Today.Month;
            _year = DateTime.Today.Year;
        }

        private void MonthlyAttendanceForm_Load(object sender, EventArgs e)
        {
            dgvMonthlyAttendance.EnableHeadersVisualStyles = false;
            dtMonth.Value = new DateTime(_year, _month, 1);
            SetupMonthlyGrid(_month, _year);
            LoadMonthlyData();
        }

        private void dtMonth_ValueChanged(object sender, EventArgs e)
        {
            _month = dtMonth.Value.Month;
            _year = dtMonth.Value.Year;

            SetupMonthlyGrid(_month, _year);
            LoadMonthlyData();
        }

        #region Grid Setup

        private void SetupMonthlyGrid(int month, int year)
        {
            dgvMonthlyAttendance.AutoGenerateColumns = false;
            dgvMonthlyAttendance.Columns.Clear();

            dgvMonthlyAttendance.ReadOnly = true;
            dgvMonthlyAttendance.AllowUserToAddRows = false;
            dgvMonthlyAttendance.AllowUserToDeleteRows = false;
            dgvMonthlyAttendance.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvMonthlyAttendance.MultiSelect = false;

            foreach (DataGridViewColumn col in dgvMonthlyAttendance.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            // Employee Name first
            dgvMonthlyAttendance.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EmployeeName",
                HeaderText = "Employee",
                ReadOnly = true,
                Width = 180
            });

            int daysInMonth = DateTime.DaysInMonth(year, month);

            for (int day = 1; day <= daysInMonth; day++)
            {
                dgvMonthlyAttendance.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = $"Days[{day}]",
                    HeaderText = day.ToString(),
                    Width = 30
                });
            }

            // formatting
            dgvMonthlyAttendance.Columns[0].Frozen = true;
            for (int i = 1; i < dgvMonthlyAttendance.Columns.Count; i++)
                dgvMonthlyAttendance.Columns[i].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

            dgvMonthlyAttendance.RowHeadersVisible = false;
            dgvMonthlyAttendance.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvMonthlyAttendance.RowTemplate.Height = 24;
            dgvMonthlyAttendance.ColumnHeadersDefaultCellStyle.Font =
                new Font("Calibri", 9, FontStyle.Bold);

            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(year, month, day);
                var col = dgvMonthlyAttendance.Columns[day];
                if (date.DayOfWeek == DayOfWeek.Friday)
                    col.HeaderCell.Style.BackColor = Color.Yellow;
                if (date > DateTime.Today)
                    col.DefaultCellStyle.BackColor = Color.LightGray; // future days
            }

            // ---- SUMMARY COLUMNS ----
            string[] summaryCols = { "TotalP", "TotalA", "TotalL", "TotalH", "TotalOT" };
            string[] summaryHeaders = { "P", "A", "L", "H", "OT" };
            int[] widths = { 40, 40, 40, 40, 60 };
            Color[] colors = { Color.LightGreen, Color.LightPink, Color.LightYellow, Color.LightBlue, Color.Gainsboro };

            for (int i = 0; i < summaryCols.Length; i++)
            {
                dgvMonthlyAttendance.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = summaryCols[i],
                    HeaderText = summaryHeaders[i],
                    Width = widths[i],
                    ReadOnly = true,
                    DefaultCellStyle = { BackColor = colors[i] }
                });
            }

            dgvMonthlyAttendance.Columns["TotalOT"].DefaultCellStyle.Format = "0.##";

            dgvMonthlyAttendance.CellFormatting += DgvMonthlyAttendance_CellFormatting;
        }

        private void DgvMonthlyAttendance_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex == 0) return;

            var model = dgvMonthlyAttendance.Rows[e.RowIndex].DataBoundItem as MonthlyAttendanceModel;
            if (model == null) return;

            int day = e.ColumnIndex;

            if (!model.Days.ContainsKey(day)) return;

            e.Value = model.Days[day];

            var status = e.Value?.ToString();
            if (string.IsNullOrEmpty(status))
            {
                e.CellStyle!.BackColor = Color.White;
                return;
            }

            // Take only first character for coloring
            switch (status[0])
            {
                case 'P': e.CellStyle!.BackColor = Color.LightGreen; break;
                case 'A': e.CellStyle!.BackColor = Color.LightPink; break;
                case 'L': e.CellStyle!.BackColor = Color.LightYellow; break;
                case 'H': e.CellStyle!.BackColor = Color.LightBlue; break;
                default: e.CellStyle!.BackColor = Color.White; break;
            }
        }

        #endregion

        #region Load Data

        private void LoadMonthlyData()
        {
            _monthlyData = GetMonthlyData(_month, _year);
            dgvMonthlyAttendance.DataSource = _monthlyData;
            UpdateSummaryColumns();
        }

        private List<MonthlyAttendanceModel> GetMonthlyData(int month, int year)
        {
            var repo = new AttendanceRepository();
            var employees = repo.GetAllEmployees();
            var monthRecords = repo.GetByMonth(month, year);

            var list = new List<MonthlyAttendanceModel>();

            foreach (var emp in employees)
            {
                var model = new MonthlyAttendanceModel
                {
                    EmployeeId = (int)emp.Id!,
                    EmployeeName = emp.Name!
                };

                int daysInMonth = DateTime.DaysInMonth(year, month);

                for (int day = 1; day <= daysInMonth; day++)
                {
                    var record = monthRecords.FirstOrDefault(r => r.EmployeeId == emp.Id && r.AttDate.Day == day);
                    model.Days[day] = record?.Status ?? ""; // <-- no default "P"
                    model.Overtime[day] = record?.OvertimeHours ?? 0;

                    // Combine OT with status like P(1)
                    if (!string.IsNullOrEmpty(model.Days[day]) && model.Overtime[day] > 0)
                        model.Days[day] += $"({model.Overtime[day]})";
                }

                list.Add(model);
            }

            return list;
        }

        private void UpdateSummaryColumns()
        {
            int daysInMonth = DateTime.DaysInMonth(_year, _month);

            foreach (DataGridViewRow row in dgvMonthlyAttendance.Rows)
            {
                if (row.DataBoundItem is not MonthlyAttendanceModel model)
                    continue;

                int p = 0, a = 0, l = 0, h = 0;
                double ot = 0;

                for (int day = 1; day <= daysInMonth; day++)
                {
                    var cell = model.Days[day];
                    if (string.IsNullOrEmpty(cell)) continue; // skip future/unsaved

                    if (cell.StartsWith("P")) p++;
                    else if (cell.StartsWith("A")) a++;
                    else if (cell.StartsWith("L")) l++;
                    else if (cell.StartsWith("H")) h++;

                    ot += model.Overtime[day];
                }

                row.Cells["TotalP"].Value = p;
                row.Cells["TotalA"].Value = a;
                row.Cells["TotalL"].Value = l;
                row.Cells["TotalH"].Value = h;
                row.Cells["TotalOT"].Value = ot;
            }
        }

        #endregion

        private void btnAddAttendance_Click(object sender, EventArgs e)
        {
            var attend = new Attendance();
            attend.ShowDialog();
            LoadMonthlyData();
        }

        private void btnPrevMonth_Click(object sender, EventArgs e)
        {
            dtMonth.Value = dtMonth.Value.AddMonths(-1);
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            dtMonth.Value = dtMonth.Value.AddMonths(1);
        }
    }
}
