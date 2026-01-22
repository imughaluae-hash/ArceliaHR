using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;

namespace ArceliaHR
{
    public partial class SalaryForm : Form
    {
        private int _employeeId;
        private EmployeeModel? _employee;
        private EmployeeRepository _empRepo = new();
        private AttendanceRepository _attRepo = new();
        private TransactionRepository _transRepo = new();
        
        // Controls
        private ComboBox? cmbMonth;
        private ComboBox? cmbYear;
        private Label? lblBasicData;
        private Label? lblAttData;
        private NumericUpDown? numDeductAdvance; // New control for Partial Deduction
        private NumericUpDown? numDeductFine;    // New control for Partial Deduction
        private Label? lblNetPay;
        private Button? btnSaveDue;
        private Button? btnPayNow;
        
        // Calculation Results
        private decimal _basicEarned;
        private decimal _otEarned;
        private decimal _totalDue;
        private decimal _advBalance;
        //private decimal _fineBalance;
        private decimal _netPayable;
        private int _selectedMonthKey; // YYYYMM

        public SalaryForm(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            SetupUI();
            LoadEmployee();
        }

        private void SetupUI()
        {
            this.Size = new Size(500, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            var mainLayout = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, Padding = new Padding(20), WrapContents = false };

            // Month Selection
            var pnlDate = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.LeftToRight };
            pnlDate.Controls.Add(new Label { Text = "Month:", AutoSize = true, Margin = new Padding(0, 5, 5, 0) });
            
            cmbMonth = new ComboBox { Width = 100, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbMonth.Items.AddRange(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Take(12).ToArray());
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;

            cmbYear = new ComboBox { Width = 80, DropDownStyle = ComboBoxStyle.DropDownList };
            int currentYear = DateTime.Now.Year;
            for(int i = currentYear - 1; i <= currentYear + 1; i++) cmbYear.Items.Add(i);
            cmbYear.SelectedItem = currentYear;
            
            var btnLoad = new Button { Text = "Load / Calculate" };
            btnLoad.Click += (s, e) => CalculateSalary();

            pnlDate.Controls.Add(cmbMonth);
            pnlDate.Controls.Add(cmbYear);
            pnlDate.Controls.Add(btnLoad);
            mainLayout.Controls.Add(pnlDate);

            // Employee Info
            lblBasicData = new Label { AutoSize = true, Margin = new Padding(0, 10, 0, 10), Font = new Font(this.Font, FontStyle.Bold) };
            mainLayout.Controls.Add(lblBasicData);

            // Attendance Breakdown
            var grpAtt = new GroupBox { Text = "Earnings (Based on Attendance)", Width = 440, Height = 120 };
            lblAttData = new Label { Dock = DockStyle.Fill, Padding = new Padding(10) };
            grpAtt.Controls.Add(lblAttData);
            mainLayout.Controls.Add(grpAtt);

            // Deductions
            var grpDed = new GroupBox { Text = "Deductions (Edit amounts to recover)", Width = 440, Height = 120 };
            var tableDed = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 2, Padding = new Padding(10) };
            
            tableDed.Controls.Add(new Label { Text = "Advance Balance:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0);
            numDeductAdvance = new NumericUpDown { DecimalPlaces = 2, Maximum = 1000000, Width = 150 };
            numDeductAdvance.ValueChanged += (s, e) => CalculateNetPay();
            tableDed.Controls.Add(numDeductAdvance, 1, 0);

            tableDed.Controls.Add(new Label { Text = "Fine Balance:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
            numDeductFine = new NumericUpDown { DecimalPlaces = 2, Maximum = 1000000, Width = 150 };
            numDeductFine.ValueChanged += (s, e) => CalculateNetPay();
            tableDed.Controls.Add(numDeductFine, 1, 1);

            grpDed.Controls.Add(tableDed);
            mainLayout.Controls.Add(grpDed);

            // Net Pay
            lblNetPay = new Label { AutoSize = true, Margin = new Padding(0, 10, 0, 10), Font = new Font(this.Font, FontStyle.Bold | FontStyle.Underline), ForeColor = Color.Blue };
            mainLayout.Controls.Add(lblNetPay);

            // Buttons
            var pnlBtns = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.LeftToRight };
            btnSaveDue = new Button { Text = "Record as Due", Width = 120, Height = 40, Enabled = false };
            btnPayNow = new Button { Text = "Pay Salary Now", Width = 120, Height = 40, Enabled = false };
            
            btnSaveDue.Click += (s, e) => SaveTransaction(false);
            btnPayNow.Click += (s, e) => SaveTransaction(true);

            pnlBtns.Controls.Add(btnSaveDue);
            pnlBtns.Controls.Add(btnPayNow);
            mainLayout.Controls.Add(pnlBtns);

            this.Controls.Add(mainLayout);
        }

        private void LoadEmployee()
        {
            _employee = _empRepo.GetById(_employeeId)!;
            if (_employee == null)
            {
                MessageBox.Show("Employee not found");
                Close();
                return;
            }
            this.Text = $"Salary Calculation - {_employee.Name}";
            lblBasicData!.Text = $"Basic Salary: {_employee.BasicSalary:N2}";
        }

        private void CalculateSalary()
        {
            if (_employee!.BasicSalary == null || _employee.BasicSalary == 0)
            {
                MessageBox.Show("Basic Salary is 0. Please update employee profile.");
                return;
            }

            int month = cmbMonth!.SelectedIndex + 1;
            int year = (int)cmbYear!.SelectedItem!;
            _selectedMonthKey = (year * 100) + month;

            if (_transRepo.IsSalaryGenerated(_employeeId, _selectedMonthKey))
            {
                MessageBox.Show("Salary for this month has already been generated/recorded.");
                btnSaveDue!.Enabled = false;
                btnPayNow!.Enabled = false;
                return;
            }

            // 1. Get Attendance
            var allAtt = _attRepo.GetByMonth(month, year);
            var empAtt = allAtt.Where(a => a.EmployeeId == _employeeId).ToList();

            int presentDays = empAtt.Count(a => a.Status == "P"); // Only Present counts
            double overtimeHrs = empAtt.Sum(a => a.OvertimeHours);

            // 2. Calculations
            int daysInMonth = DateTime.DaysInMonth(year, month);
            decimal basicSalary = _employee.BasicSalary ?? 0;
            decimal perDay = basicSalary / daysInMonth; 
            decimal perHour = perDay / 9; 

            _basicEarned = perDay * presentDays;
            _otEarned = perHour * (decimal)overtimeHrs;
            _totalDue = _basicEarned + _otEarned;

            lblAttData!.Text = $"Present Days: {presentDays} / {daysInMonth}\n" +
                              $"Overtime Hours: {overtimeHrs}\n\n" +
                              $"Basic Earned: {_basicEarned:N2}\n" +
                              $"Overtime Earned: {_otEarned:N2}\n" +
                              $"Total Earnings: {_totalDue:N2}";

            // 3. Get Balances (Advance/Fine)
            // We need SEPARATE balances for Advance and Fine to control deduction limits
            var summaries = _transRepo.GetBalanceSummaries().FirstOrDefault(s => s.EmployeeId == _employeeId);
            
            // NOTE: GetBalanceSummaries returns TOTAL Advance/Fine history, not current balance. 
            // We need a way to know how much Advance is OUTSTANDING. 
            // Simplified Logic: 
            // We'll calculate TotalAdvanceTaken - TotalAdvanceRecovered (via Pay deductions)
            // But we don't track recoveries separately yet (they are just 'SalaryPaid'). 
            // 
            // Alternative: Just use the total Ledger Balance. 
            // If Ledger is negative (-500), it's likely Advance/Fine.
            // We can distribute it.
            // 
            // Let's use GetCurrentBalance() -> If -500.
            // We assume it's all Advance for defaults (or split if we had data).
            // For now, let's just let user input what they want to deduct.
            // We'll default the "Advance Deduction" to Min(Abs(CurrentBalance), TotalDue).
            
            decimal currentBalance = _transRepo.GetCurrentBalance(_employeeId);
            _advBalance = 0;
            //_fineBalance = 0;

            if (currentBalance < 0)
            {
                // We owe company. Default to recovering all of it (up to salary amount)
                _advBalance = Math.Abs(currentBalance); 
            }

            // Set numeric updowns limits and defaults
            // Logic: Default deduction is whatever is outstanding, but capped at salary amount.
            
            decimal defaultDeduction = _advBalance; 
            if (defaultDeduction > _totalDue) defaultDeduction = _totalDue;

            // We put it all in Advance input for simplicity since we don't distinguish yet
            numDeductAdvance!.Value = defaultDeduction;
            numDeductFine!.Value = 0; // Default 0 for fine
            
            CalculateNetPay();

            btnSaveDue!.Enabled = true;
            btnPayNow!.Enabled = true;
        }

        private void CalculateNetPay()
        {
            decimal deductAdv = numDeductAdvance!.Value;
            decimal deductFine = numDeductFine!.Value;
            
            _netPayable = _totalDue - (deductAdv + deductFine);
            
            lblNetPay!.Text = $"Total Earnings: {_totalDue:N2}\n" +
                             $"Less Advance: -{deductAdv:N2}\n" +
                             $"Less Fine: -{deductFine:N2}\n" +
                             $"Net Payable: {_netPayable:N2}";
        }

        private void SaveTransaction(bool payNow)
        {
            try
            {
                // 1. Save Salary Due (Credit to Emp)
                var dueTrans = new TransactionModel
                {
                    EmployeeId = _employeeId,
                    TransDate = DateTime.Now,
                    TransType = "SalaryDue",
                    Amount = _totalDue,
                    Month = _selectedMonthKey,
                    Remarks = $"Salary for {cmbMonth!.Text} {cmbYear!.SelectedItem}"
                };
                _transRepo.Add(dueTrans);

                // 2. If Pay Now, Save Salary Paid (Debit to Emp)
                if (payNow)
                {
                    // Use the calculated Net Payable (which accounts for user-input partial deductions)
                    decimal amountToPay = _netPayable;
                    
                    if (amountToPay < 0) amountToPay = 0; // Should not happen if validation is correct

                    decimal deductAdv = numDeductAdvance!.Value;
                    decimal deductFine = numDeductFine!.Value;
                    
                    string payRemarks = $"Payment for {cmbMonth.Text} {cmbYear.SelectedItem}";
                    if (deductAdv > 0) payRemarks += $", Less Adv: {deductAdv:N2}";
                    if (deductFine > 0) payRemarks += $", Less Fine: {deductFine:N2}";

                    if (amountToPay > 0 || deductAdv > 0 || deductFine > 0)
                    {
                        var paidTrans = new TransactionModel
                        {
                            EmployeeId = _employeeId,
                            TransDate = DateTime.Now,
                            TransType = "SalaryPaid",
                            Amount = amountToPay,
                            Remarks = payRemarks
                        };
                        _transRepo.Add(paidTrans);
                        MessageBox.Show($"Salary Recorded & Paid! Amount: {amountToPay:N2}");
                    }
                    else
                    {
                        MessageBox.Show("Salary Recorded. No Payout (Full Deduction cover).");
                    }
                }
                else
                {
                    MessageBox.Show("Salary Recorded as Due.");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private System.ComponentModel.IContainer? components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }
    }
}
