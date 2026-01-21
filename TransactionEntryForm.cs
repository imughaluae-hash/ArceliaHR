using ArceliaHR.Database.Repositories;
using ArceliaHR.Models;

namespace ArceliaHR
{
    public partial class TransactionEntryForm : Form
    {
        private int _employeeId;
        private string _employeeName;
        private string _transactionType; // "Advance", "Fine", "AdjustmentPlus", "AdjustmentMinus"
        
        public TransactionEntryForm(int employeeId, string employeeName, string transactionType)
        {
            InitializeComponent();
            _employeeId = employeeId;
            _employeeName = employeeName;
            _transactionType = transactionType;
            
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = $"{_transactionType} Entry - {_employeeName}";
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                RowCount = 5,
                ColumnCount = 2
            };
            
            // Date
            layout.Controls.Add(new Label { Text = "Date:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0);
            var dtpDate = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today, Width = 200 };
            layout.Controls.Add(dtpDate, 1, 0);

            // Amount
            layout.Controls.Add(new Label { Text = "Amount:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
            var numAmount = new NumericUpDown { DecimalPlaces = 2, Maximum = 100000, Width = 200 };
            layout.Controls.Add(numAmount, 1, 1);

            // Remarks
            layout.Controls.Add(new Label { Text = "Remarks:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 2);
            var txtRemarks = new TextBox { Multiline = true, Height = 60, Width = 200 };
            layout.Controls.Add(txtRemarks, 1, 2);

            // Buttons
            var btnPanel = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Bottom, Height = 40 };
            var btnSave = new Button { Text = "Save", DialogResult = DialogResult.OK };
            var btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel };
            
            btnSave.Click += (s, e) => 
            {
                if (numAmount.Value <= 0)
                {
                    MessageBox.Show("Amount must be greater than 0");
                    return;
                }
                SaveTransaction(dtpDate.Value, numAmount.Value, txtRemarks.Text);
            };

            btnPanel.Controls.Add(btnCancel);
            btnPanel.Controls.Add(btnSave);
            
            this.Controls.Add(btnPanel);
            this.Controls.Add(layout);
        }

        private void SaveTransaction(DateTime date, decimal amount, string remarks)
        {
            try 
            {
                var repo = new TransactionRepository();
                var model = new TransactionModel
                {
                    EmployeeId = _employeeId,
                    TransDate = date,
                    TransType = _transactionType,
                    Amount = amount,
                    Remarks = remarks
                };
                repo.Add(model);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
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
