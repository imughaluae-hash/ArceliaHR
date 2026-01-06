using ArceliaHR.Database.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArceliaHR
{
    public partial class EmployeeStatementForm : Form
    {
        public EmployeeStatementForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public int EmployeeId { get; private set; }


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
            var ledgerRepo = new EmployeeLedgerRepository();
            var ledgerEntries = ledgerRepo.GetAllEntries(EmployeeId);

            decimal runningBalance = 0;
            int sn = 1;
            var rows = new List<object>();

            foreach (var entry in ledgerEntries)
            {
                decimal amount = 0;
                decimal paid = 0;

                switch (entry.TranType)
                {
                    case "ADVANCE":
                    case "FINE":
                        paid = entry.Debit;
                        amount = 0;
                        runningBalance -= paid;
                        break;

                    case "SALARY":
                        amount = entry.Credit;
                        paid = 0;
                        runningBalance += amount;
                        break;

                    case "RECOVERY": // advance deduction
                        paid = entry.Credit;
                        amount = 0;
                        runningBalance -= paid;
                        break;
                }

                rows.Add(new
                {
                    SN = sn++,
                    Date = entry.TranDate.ToString("dd/MM/yyyy"),
                    Description = entry.Description,
                    Amount = amount,
                    Paid = paid,
                    RunningBalance = runningBalance
                });
            }

            // Optional: add final summary row if salary exists
            var totalSalaryPaid = ledgerEntries
                .Where(x => x.TranType == "SALARY")
                .Sum(x => x.Credit)
                - ledgerEntries.Where(x => x.TranType == "RECOVERY").Sum(x => x.Credit);

            if (totalSalaryPaid > 0)
            {
                rows.Add(new
                {
                    SN = sn,
                    Date = "",
                    Description = $"Total Salary Paid for {ledgerEntries.FirstOrDefault(x => x.TranType == "SALARY")?.ReferenceMonth}",
                    Amount = 0,
                    Paid = totalSalaryPaid,
                    RunningBalance = 0
                });
            }

            dgStatement.DataSource = rows;

            // Update current balance label
            lblBalance.Text = $"Current Balance: {ledgerRepo.GetBalance(EmployeeId):N2}";
        }


        private void brtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
