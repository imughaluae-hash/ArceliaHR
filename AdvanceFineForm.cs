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
    public partial class AdvanceFineForm : Form
    {
        public int EmployeeId { get; set; }
        public AdvanceFineForm()
        {
            InitializeComponent();
        }
        public void SetEmployee(int id, string name)
        {
            EmployeeId = id;
            lblName.Text = name;

            var repo = new EmployeeLedgerRepository();
            lblCurrBalance.Text = repo.GetBalance(EmployeeId).ToString("N2");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (EmployeeId == 0) return; // check if employee is set

            var repo = new EmployeeLedgerRepository();
            repo.Add(
                EmployeeId,
                dtTran.Value,
                cmbType.SelectedItem?.ToString() ?? "ADVANCE",
                udAmount.Value,
                txtDescr.Text
            );

            DialogResult = DialogResult.OK;
            Close();
        }

    }
}
