using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;
using System.Data;

namespace ArceliaHR
{
    public partial class PaymentControlPanel : Form
    {
        private TransactionRepository _repo = new TransactionRepository();
        private DataGridView? dgActive;
        private DataGridView? dgInactive;

        public PaymentControlPanel()
        {
            InitializeComponent();
            SetupUI();
            LoadData();
        }

        private void SetupUI()
        {
            this.Text = "Payment Control Panel";
            this.Size = new Size(1000, 600);
            this.WindowState = FormWindowState.Maximized;

            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = this.Height / 2
            };

            // Active Panel
            var pnlActive = new GroupBox { Text = "Active Employees", Dock = DockStyle.Fill };
            dgActive = CreateGrid();
            pnlActive.Controls.Add(dgActive);
            
            // Inactive Panel
            var pnlInactive = new GroupBox { Text = "Inactive Employees", Dock = DockStyle.Fill };
            dgInactive = CreateGrid();
            pnlInactive.Controls.Add(dgInactive);

            split.Panel1.Controls.Add(pnlActive);
            split.Panel2.Controls.Add(pnlInactive);

            // Toolbar
            var toolbar = new ToolStrip();
            var btnRefresh = new ToolStripButton("Refresh", null, (s, e) => LoadData());
            
            var btnAdvance = new ToolStripButton("Add Advance", null, (s, e) => OpenTransaction("Advance"));
            var btnFine = new ToolStripButton("Add Fine", null, (s, e) => OpenTransaction("Fine"));
            var btnAdjPlus = new ToolStripButton("Adj (+)", null, (s, e) => OpenTransaction("AdjustmentPlus"));
            var btnAdjMinus = new ToolStripButton("Adj (-)", null, (s, e) => OpenTransaction("AdjustmentMinus"));
            
            var sep = new ToolStripSeparator();
            var btnPaySalary = new ToolStripButton("Pay Salary", null, BtnPaySalary_Click); // Will implement later

            toolbar.Items.AddRange(new ToolStripItem[] { btnRefresh, new ToolStripSeparator(), btnAdvance, btnFine, btnAdjPlus, btnAdjMinus, sep, btnPaySalary });

            this.Controls.Add(split);
            this.Controls.Add(toolbar);
        }

        private DataGridView CreateGrid()
        {
            var dg = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false
            };
            dg.DoubleClick += Dg_DoubleClick;
            return dg;
        }

        private void LoadData()
        {
            try
            {
                var summaries = _repo.GetBalanceSummaries(null).ToList();

                var active = summaries.Where(s => s.Status == "Active").ToList();
                var inactive = summaries.Where(s => s.Status != "Active").ToList();

                dgActive!.DataSource = active;
                dgInactive!.DataSource = inactive;

                FormatGrid(dgActive);
                FormatGrid(dgInactive);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private void FormatGrid(DataGridView dg)
        {
            if (dg.Columns["EmployeeId"] != null) dg.Columns["EmployeeId"].Visible = false;
            
            // Format currency columns
            string[] moneyCols = { "TotalPayable", "TotalPaid", "TotalAdvance", "TotalFine", "CurrentBalance" };
            foreach(var col in moneyCols)
            {
                if (dg.Columns[col] != null) 
                {
                    dg.Columns[col].DefaultCellStyle.Format = "N2";
                    dg.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        private void OpenTransaction(string type)
        {
            var emp = GetSelectedEmployee();
            if (emp == null) return;

            using (var form = new TransactionEntryForm(emp.EmployeeId, emp.EmployeeName, type))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Refresh to show new balance
                }
            }
        }

        private void BtnPaySalary_Click(object? sender, EventArgs e)
        {
            var emp = GetSelectedEmployee();
            if (emp == null) return;

            using (var form = new SalaryForm(emp.EmployeeId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void Dg_DoubleClick(object? sender, EventArgs e)
        {
            var emp = GetSelectedEmployee();
            if (emp == null) return;

            using (var form = new EmployeeStatementForm(emp.EmployeeId, emp.EmployeeName))
            {
                form.ShowDialog();
            }
        }

        private EmployeeBalanceModel? GetSelectedEmployee()
        {
            DataGridView? activeGrid = null;
            if (dgActive!.Focused || dgActive.ContainsFocus) activeGrid = dgActive;
            else if (dgInactive!.Focused || dgInactive.ContainsFocus) activeGrid = dgInactive;

            // Fallback: use whichever has selection if focus is elsewhere (like toolbar)
            if (activeGrid == null)
            {
                if (dgActive.SelectedRows.Count > 0) activeGrid = dgActive;
                else if (dgInactive!.SelectedRows.Count > 0) activeGrid = dgInactive;
            }

            if (activeGrid != null && activeGrid.CurrentRow != null)
            {
                return activeGrid.CurrentRow.DataBoundItem as EmployeeBalanceModel;
            }
            
            MessageBox.Show("Please select an employee first.");
            return null;
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
