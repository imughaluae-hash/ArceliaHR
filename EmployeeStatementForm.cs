using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;
using System.ComponentModel;
using System.Linq;
using System.Drawing;

namespace ArceliaHR
{
    public partial class EmployeeStatementForm : Form
    {
        private int _employeeId;
        private string _employeeName;
        private TransactionRepository _repo = new TransactionRepository();
        private EmployeeRepository _empRepo = new EmployeeRepository();
        private AttendanceRepository _attRepo = new AttendanceRepository();
        private DataGridView dgList;

        public EmployeeStatementForm(int employeeId, string employeeName)
        {
            InitializeComponent();
            _employeeId = employeeId;
            _employeeName = employeeName;
            
            SetupUI();
            // LoadData(); // Moved to OnLoad to prevent NRE during initialization
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadData();
        }

        private void SetupUI()
        {
            this.Text = $"Statement of Account - {_employeeName}";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(10) };
            pnlTop.Controls.Add(new Label { Text = $"Employee: {_employeeName}", AutoSize = true, Font = new Font(this.Font.FontFamily, 12, FontStyle.Bold) });

            dgList = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false
            };

            this.Controls.Add(dgList);
            this.Controls.Add(pnlTop);
        }

        private void LoadData()
        {
            var rawTrans = _repo.GetByEmployee(_employeeId).OrderBy(t => t.TransDate).ThenBy(t => t.Id).ToList();

            // Transform to Statement View with Running Balance
            var statementList = new BindingList<StatementRow>();
            decimal runningBalance = 0;
            int sn = 1;

            foreach(var t in rawTrans)
            {
                // Logic: 
                // Payable (Credit to Emp) increase balance (Company owes Emp)
                // Paid (Debit to Emp) decrease balance
                
                decimal due = 0;
                decimal paid = 0;

                if (t.TransType == "SalaryDue" || t.TransType == "AdjustmentPlus")
                {
                    due = t.Amount;
                    runningBalance += due;
                }
                else // SalaryPaid, Advance, Fine, AdjustmentMinus
                {
                    paid = t.Amount;
                    runningBalance -= paid;
                }

                statementList.Add(new StatementRow
                {
                    SN = sn++,
                    Date = t.TransDate,
                    Description = FormatDescription(t),
                    DueAmount = due != 0 ? due.ToString("N2") : "",
                    Paid = paid != 0 ? paid.ToString("N2") : "",
                    Balance = runningBalance.ToString("N2")
                });
            }
            
            // --- Live Row Calculation (Current Salary Till Today) ---
            try 
            {
                var emp = _empRepo.GetById(_employeeId);
                if (emp != null && emp.BasicSalary > 0)
                {
                    int month = DateTime.Now.Month;
                    int year = DateTime.Now.Year;
                    
                    // Check if salary for this month is ALREADY generated in DB to avoid double counting
                    // If Transaction exists for this Month/Year with type 'SalaryDue', we should NOT show this live row?
                    // User prompt: "Current salary Till Today 645.16" 
                    // Usually this is for the *unpaid* / *unprocessed* part.
                    // If I already ran generic monthly salary, it is in DB transactions.
                    
                    // So we check if SalaryDue for current month exists.
                    bool salaryProcessed = _repo.IsSalaryGenerated(_employeeId, (year * 100) + month);
                    
                    if (!salaryProcessed)
                    {
                        var attList = _attRepo.GetByMonth(month, year).Where(a => a.EmployeeId == _employeeId).ToList();
                        int presentDays = attList.Count(a => a.Status == "P");
                        
                        // Basic Pro-rata
                        int daysInMonth = DateTime.DaysInMonth(year, month);
                        decimal perDay = (emp.BasicSalary ?? 0) / daysInMonth;
                        decimal currentEarned = perDay * presentDays;
                        
                        // Overtime? User prompt said "salary (pressent Days + Overtime)".
                        // Let's include OT too.
                        double otHours = attList.Sum(a => a.OvertimeHours);
                        decimal perHour = perDay / 9;
                        decimal otEarned = perHour * (decimal)otHours;
                        
                        decimal totalLive = currentEarned + otEarned;
                        
                        if (totalLive > 0)
                        {
                            runningBalance += totalLive;
                            statementList.Add(new StatementRow
                            {
                                SN = sn++,
                                Date = DateTime.Now,
                                Description = "Current salary Till Today (Provisional)", // Marked as provisional
                                DueAmount = totalLive.ToString("N2"),
                                Paid = "",
                                Balance = runningBalance.ToString("N2")
                            });
                        }
                    }
                }
            }
            catch { /* Ignore calculation errors for live row */ }
            // --------------------------------------------------------

            dgList.DataSource = statementList;
            
            // Format Cols
            if (dgList.Columns["SN"] != null) dgList.Columns["SN"].Width = 40;
            if (dgList.Columns["Date"] != null) dgList.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy";
            
            string[] alignRight = { "DueAmount", "Paid", "Balance" };
            foreach(var col in alignRight) 
                if (dgList.Columns[col] != null) dgList.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private string FormatDescription(TransactionModel t)
        {
            string desc = t.TransType;
            if (t.TransType == "SalaryDue") desc = "Salary Due";
            else if (t.TransType == "SalaryPaid") desc = "Salary Payment";
            else if (t.TransType == "AdjustmentPlus") desc = "Adjustment (+)";
            else if (t.TransType == "AdjustmentMinus") desc = "Adjustment (-)";

            if (!string.IsNullOrEmpty(t.Remarks)) desc += $" - {t.Remarks}";
            return desc;
        }

        public class StatementRow
        {
            public int SN { get; set; }
            public DateTime Date { get; set; }
            public string Description { get; set; } = "";
            public string DueAmount { get; set; } = "";
            public string Paid { get; set; } = "";
            public string Balance { get; set; } = "";
        }

        private System.ComponentModel.IContainer components = null;
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
