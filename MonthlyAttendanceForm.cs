using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
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
            // new formatting 
            dgvMonthlyAttendance.Columns[0].Frozen = true;
            for (int i = 1; i < dgvMonthlyAttendance.Columns.Count; i++)
            {
                dgvMonthlyAttendance.Columns[i].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }
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
                {
                    col.HeaderCell.Style.BackColor = Color.Yellow;
                }
            }

            dgvMonthlyAttendance.CellFormatting += DgvMonthlyAttendance_CellFormatting;
        }

        private void DgvMonthlyAttendance_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex == 0) return;

            var row = dgvMonthlyAttendance.Rows[e.RowIndex].DataBoundItem as MonthlyAttendanceModel;
            if (row == null) return;

            int day = e.ColumnIndex; // assuming first column is employee name
            e.Value = row.Days.ContainsKey(day) ? row.Days[day] : "";

            if (e.ColumnIndex == 0) return;

            var status = e.Value?.ToString();
            if (status == "P") e.CellStyle!.BackColor = Color.LightGreen;
            else if (status == "A") e.CellStyle!.BackColor = Color.LightPink;
            else if (status == "L") e.CellStyle!.BackColor = Color.LightYellow;
            else if (status == "H") e.CellStyle!.BackColor = Color.LightBlue;

        }

        #endregion

        #region Load Data

        private void LoadMonthlyData()
        {
            var repo = new AttendanceRepository();
            _monthlyData = GetMonthlyData(_month, _year);
            dgvMonthlyAttendance.DataSource = _monthlyData;
        }

        private List<MonthlyAttendanceModel> GetMonthlyData(int month, int year)
        {
            var repo = new AttendanceRepository();
            var employees = repo.GetAllEmployees(); // Id + Name
            var monthRecords = repo.GetByMonth(month, year);

            var list = new List<MonthlyAttendanceModel>();

            foreach (var emp in employees)
            {
                var model = new MonthlyAttendanceModel
                {
                    EmployeeId = (int)emp.Id!,
                    EmployeeName = emp.Name
                };

                for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
                {
                    var record = monthRecords.FirstOrDefault(r => r.EmployeeId == emp.Id && r.AttDate.Day == day);
                    model.Days[day] = record?.Status ?? "P";
                    model.Overtime[day] = record?.OvertimeHours ?? 0;
                }

                list.Add(model);
            }


            return list;
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
