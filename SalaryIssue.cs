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
    public partial class SalaryIssue : Form
    {
        private int EmployeeId;
        private decimal AdvanceDue;
        private decimal SalaryDue;
        private decimal BasicSalary;

        public SalaryIssue()
        {
            InitializeComponent();
            LoadMonths();
            WireEvents();
        }

        public void SetEmployee(int empId, string empName, decimal basic, decimal advanceDue, decimal salaryDue)
        {
            EmployeeId = empId;
            lblEmployee.Text = empName;
            BasicSalary = basic;
            AdvanceDue = advanceDue;
            SalaryDue = salaryDue;

            lblBasic.Text = basic.ToString("N2");
            lblAdvanceDue.Text = advanceDue.ToString("N2");
            lblSalaryDue.Text = salaryDue.ToString("N2");

            udBasic.Value = basic;
            udAdvance.Value = 0; // user can enter adjustment
            udFine.Value = 0;
            udOvertime.Value = 0;

            UpdateTotalPayable();
        }

        private void LoadMonths()
        {
            // Populate month dropdown: e.g., last 12 months
            cmbMonth.Items.Clear();
            for (int i = 0; i < 12; i++)
            {
                var month = DateTime.Now.AddMonths(-i).ToString("yyyy-MM");
                cmbMonth.Items.Add(month);
            }
            if (cmbMonth.Items.Count > 0) cmbMonth.SelectedIndex = 0;
        }

        private void WireEvents()
        {
            udBasic.ValueChanged += (s, e) => UpdateTotalPayable();
            udOvertime.ValueChanged += (s, e) => UpdateTotalPayable();
            udAdvance.ValueChanged += (s, e) => UpdateTotalPayable();
            udFine.ValueChanged += (s, e) => UpdateTotalPayable();
        }

        private void UpdateTotalPayable()
        {
            decimal total = udBasic.Value + udOvertime.Value - udAdvance.Value - udFine.Value;
            lblTotalPayable.Text = total.ToString("N2");
        }

        private void BtnIssue_Click(object sender, EventArgs e)
        {
            if (EmployeeId == 0) return;
            if (cmbMonth.SelectedItem == null)
            {
                MessageBox.Show("Please select a month.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string? month = cmbMonth.SelectedItem.ToString();
            var ledger = new EmployeeLedgerRepository();

            // Check if salary already issued
            if (ledger.IsSalaryIssued(EmployeeId, month!))
            {
                MessageBox.Show($"Salary already issued for {month}.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Add Salary
            ledger.Add(EmployeeId, DateTime.Now, "SALARY", udBasic.Value, "Salary for " + month, month);

            // Add Overtime
            if (udOvertime.Value > 0)
                ledger.Add(EmployeeId, DateTime.Now, "ADJUSTMENT", udOvertime.Value, "Overtime for " + month);

            // Deduct Advance
            if (udAdvance.Value > 0)
                ledger.Add(EmployeeId, DateTime.Now, "ADVANCE", udAdvance.Value, "Advance Deduction for " + month);

            // Deduct Fine
            if (udFine.Value > 0)
                ledger.Add(EmployeeId, DateTime.Now, "FINE", udFine.Value, "Fine Deduction for " + month);

            MessageBox.Show("Salary issued successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
