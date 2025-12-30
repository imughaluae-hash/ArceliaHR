namespace ArceliaHR
{
    partial class MonthlyAttendanceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dtMonth = new DateTimePicker();
            dgvMonthlyAttendance = new DataGridView();
            btnSave = new Button();
            btnAddAttendance = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMonthlyAttendance).BeginInit();
            SuspendLayout();
            // 
            // dtMonth
            // 
            dtMonth.CustomFormat = "\"MMMM yyyy\"";
            dtMonth.Format = DateTimePickerFormat.Custom;
            dtMonth.Location = new Point(12, 12);
            dtMonth.Name = "dtMonth";
            dtMonth.Size = new Size(200, 23);
            dtMonth.TabIndex = 0;
            dtMonth.ValueChanged += dtMonth_ValueChanged;
            // 
            // dgvMonthlyAttendance
            // 
            dgvMonthlyAttendance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMonthlyAttendance.Location = new Point(12, 41);
            dgvMonthlyAttendance.Name = "dgvMonthlyAttendance";
            dgvMonthlyAttendance.Size = new Size(776, 397);
            dgvMonthlyAttendance.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(218, 12);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnAddAttendance
            // 
            btnAddAttendance.Location = new Point(299, 12);
            btnAddAttendance.Name = "btnAddAttendance";
            btnAddAttendance.Size = new Size(114, 23);
            btnAddAttendance.TabIndex = 3;
            btnAddAttendance.Text = "Add Attendance";
            btnAddAttendance.UseVisualStyleBackColor = true;
            btnAddAttendance.Click += btnAddAttendance_Click;
            // 
            // MonthlyAttendanceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnAddAttendance);
            Controls.Add(btnSave);
            Controls.Add(dgvMonthlyAttendance);
            Controls.Add(dtMonth);
            Name = "MonthlyAttendanceForm";
            Text = "MonthlyAttendanceForm";
            Load += MonthlyAttendanceForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMonthlyAttendance).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DateTimePicker dtMonth;
        private DataGridView dgvMonthlyAttendance;
        private Button btnSave;
        private Button btnAddAttendance;
    }
}