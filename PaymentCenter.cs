using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;
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
    public partial class PaymentCenter : Form
    {
        public PaymentCenter()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void btnAdvFine_Click(object sender, EventArgs e)
        {
            DataGridViewRow? row = dgActive.SelectedRows.Count > 0 ? dgActive.SelectedRows[0] :
                                    dgInactive.SelectedRows.Count > 0 ? dgInactive.SelectedRows[0] :
                                    null;

            if (row == null) return;

            int empId = Convert.ToInt32(row.Cells["Id"].Value);
            string empName = row.Cells["Name"].Value.ToString() ?? "Unknown";

            var form = new AdvanceFineForm();
            form.SetEmployee(empId, empName);

            if (form.ShowDialog() == DialogResult.OK)
            {
                // Refresh the grid after transaction
                LoadEmployees();
            }
        }
        private void LoadEmployees()
        {
            var repo = new EmployeeRepository();
            var ledgerRepo = new EmployeeLedgerRepository();
            var all = repo.GetAll().ToList();

            var displayList = all.Select(emp => new
            {
                emp.Id,
                emp.Name,
                BasicSalary = emp.BasicSalary ?? 0,
                DueAdvance = emp.Id.HasValue ? ledgerRepo.GetDueAmount(emp.Id.Value, "ADVANCE") : 0,
                DueFine = emp.Id.HasValue ? ledgerRepo.GetDueAmount(emp.Id.Value, "FINE") : 0,
                Balance = emp.Id.HasValue ? ledgerRepo.GetBalance(emp.Id.Value) : 0

            }).ToList();

            dgActive.DataSource = displayList.Where(x => all.First(e => e.Id == x.Id).Status == "Active").ToList();
            dgInactive.DataSource = displayList.Where(x => all.First(e => e.Id == x.Id).Status == "DeActive").ToList();
        }




        private void dgActive_SelectionChanged(object sender, EventArgs e)
        {
            bool rowSelected = dgActive.SelectedRows.Count > 0;
            if (rowSelected)
                dgInactive.ClearSelection();

            btnAdvFine.Enabled = rowSelected || dgInactive.SelectedRows.Count > 0;
            btnSalary.Enabled = rowSelected || dgInactive.SelectedRows.Count > 0;
        }

        private void dgInactive_SelectionChanged(object sender, EventArgs e)
        {
            bool rowSelected = dgInactive.SelectedRows.Count > 0;
            if (rowSelected)
                dgActive.ClearSelection();

            btnAdvFine.Enabled = rowSelected || dgActive.SelectedRows.Count > 0;
            btnSalary.Enabled = rowSelected || dgActive.SelectedRows.Count > 0;
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            // Get selected row
            DataGridViewRow? row = dgActive.SelectedRows.Count > 0 ? dgActive.SelectedRows[0] :
                                   dgInactive.SelectedRows.Count > 0 ? dgInactive.SelectedRows[0] :
                                   null;

            if (row == null) return;

            int empId = Convert.ToInt32(row.Cells["Id"].Value);
            string empName = row.Cells["Name"].Value.ToString() ?? "Unknown";
            decimal basicSalary = Convert.ToDecimal(row.Cells["BasicSalary"].Value);
            decimal dueAdvance = Convert.ToDecimal(row.Cells["DueAdvance"].Value);
            decimal dueFine = Convert.ToDecimal(row.Cells["DueFine"].Value);

            // Open SalaryIssue form
            var form = new SalaryIssue();
            form.SetEmployee(empId, empName, basicSalary, dueAdvance, dueFine);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees(); // Refresh grid after issuing salary
            }
        }
        private void OpenStatementForSelectedEmployee()
        {
            DataGridViewRow? row =
                dgActive.SelectedRows.Count > 0 ? dgActive.SelectedRows[0] :
                dgInactive.SelectedRows.Count > 0 ? dgInactive.SelectedRows[0] :
                null;

            if (row == null) return;

            int empId = Convert.ToInt32(row.Cells["Id"].Value);
            string empName = row.Cells["Name"].Value?.ToString() ?? "Unknown";

            var form = new EmployeeStatementForm();
            form.SetEmployee(empId, empName,"Imran","Active");
            form.ShowDialog();
        }


        private void dgActive_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenStatementForSelectedEmployee();
        }

        private void dgInactive_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenStatementForSelectedEmployee();
        }
    }
}
