using ArceliaHR.Database.Repositories;
using ArceliaHR.Services;


namespace ArceliaHR
{
    public partial class EmployeeStatementForm : Form
    {
        public EmployeeStatementForm()
        {
            InitializeComponent();
        }

        public int EmployeeId { get; private set; }
        public decimal? BacissSalary { get; private set; }

        public void SetEmployee(int empId, string empName, string department, string status)
        {
            EmployeeId = empId;

            lblName.Text = empName;
            lblDepartment.Text = department;
            lblStatus.Text = status;

            LoadStatement();
        }

        private void LoadStatement()
        {
            var repoEmployee = new EmployeeRepository();
            var employee = repoEmployee.GetById(EmployeeId);

            var ledgerRepo = new EmployeeLedgerRepository();

            var today = DateTime.Today;

            // 🔹 TODO: Load from Employees table
            decimal? basicSalary = employee!.BasicSalary;
            BacissSalary = employee!.BasicSalary;

            var attendanceRepo = new AttendanceRepository();
            var attendanceService = new AttendanceSummaryService(attendanceRepo);

            var salaryTillToday = attendanceService.CalculateSalaryTillDate(
                EmployeeId,
                basicSalary,
                today.Year,
                today.Month,
                today
            );

            decimal currentSalaryTillToday = salaryTillToday.TotalSalary;

            var statementRows = ledgerRepo.BuildStatement(
                EmployeeId,
                currentSalaryTillToday,
                today
            );

            dgStatement.AutoGenerateColumns = true;
            dgStatement.DataSource = statementRows;

            if (statementRows.Any())
                lblBalance.Text =
                    $"Current Balance: {statementRows.Last().RunningBalance:N2}";
            else
                lblBalance.Text = "Current Balance: 0.00";

            FormatGrid();
        }

        private void FormatGrid()
        {
            dgStatement.Columns["SN"].Width = 50;
            dgStatement.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgStatement.Columns["DueAmount"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            dgStatement.Columns["Paid"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            dgStatement.Columns["RunningBalance"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgStatement.Columns["DueAmount"].DefaultCellStyle.Format = "N2";
            dgStatement.Columns["Paid"].DefaultCellStyle.Format = "N2";
            dgStatement.Columns["RunningBalance"].DefaultCellStyle.Format = "N2";

            dgStatement.ReadOnly = true;
            dgStatement.AllowUserToAddRows = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgStatement_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgStatement.Rows[e.RowIndex];
            var desc = row.Cells["Description"].Value?.ToString();

            if (desc != "Current Salary Till Today")
                return;

            ShowSalaryBreakdownPopup();
        }
        private void ShowSalaryBreakdownPopup()
        {
            var today = DateTime.Today;

            var attendanceRepo = new AttendanceRepository();
            var attendanceService = new AttendanceSummaryService(attendanceRepo);

            var summary = attendanceService.GetMonthlySummary(
                EmployeeId,
                today.Year,
                today.Month,
                today
            );

            decimal basicSalary = (decimal)BacissSalary!/* load from Employee table */;
            var salary = SalaryCalculator.Calculate(basicSalary, summary);

            var text = $@"
Name:           {lblName.Text}
Month:          {today:MMM yyyy}
Period:         01 {today:MMM yyyy} → {today:dd MMM yyyy}

Basic Salary:   {basicSalary:N2}

Days Present:   {summary.PresentDays} x {salary.PerDayRate:N2} = {salary.PresentPay:N2}
Overtime Hours: {summary.OvertimeHours} x {salary.HourlyRate:N2} = {salary.OvertimePay:N2}

--------------------------------
Total Salary Till Today: {salary.TotalSalary:N2}
";

            MessageBox.Show(
                text.Trim(),
                "Salary Breakdown",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

    }
}
