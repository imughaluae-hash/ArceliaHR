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
            btnAddAttendance = new Button();
            panel1 = new Panel();
            btnPrevMonth = new Button();
            btnNextMonth = new Button();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvMonthlyAttendance).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // dtMonth
            // 
            dtMonth.CustomFormat = "MMMM yyyy";
            dtMonth.Format = DateTimePickerFormat.Custom;
            dtMonth.Location = new Point(226, 8);
            dtMonth.Name = "dtMonth";
            dtMonth.Size = new Size(109, 23);
            dtMonth.TabIndex = 0;
            dtMonth.ValueChanged += dtMonth_ValueChanged;
            // 
            // dgvMonthlyAttendance
            // 
            dgvMonthlyAttendance.AllowUserToAddRows = false;
            dgvMonthlyAttendance.AllowUserToDeleteRows = false;
            dgvMonthlyAttendance.AllowUserToResizeColumns = false;
            dgvMonthlyAttendance.AllowUserToResizeRows = false;
            dgvMonthlyAttendance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMonthlyAttendance.Dock = DockStyle.Fill;
            dgvMonthlyAttendance.Location = new Point(0, 0);
            dgvMonthlyAttendance.Name = "dgvMonthlyAttendance";
            dgvMonthlyAttendance.Size = new Size(800, 407);
            dgvMonthlyAttendance.TabIndex = 1;
            // 
            // btnAddAttendance
            // 
            btnAddAttendance.Location = new Point(12, 10);
            btnAddAttendance.Name = "btnAddAttendance";
            btnAddAttendance.Size = new Size(114, 23);
            btnAddAttendance.TabIndex = 3;
            btnAddAttendance.Text = "Add Attendance";
            btnAddAttendance.UseVisualStyleBackColor = true;
            btnAddAttendance.Click += btnAddAttendance_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnPrevMonth);
            panel1.Controls.Add(btnNextMonth);
            panel1.Controls.Add(dtMonth);
            panel1.Controls.Add(btnAddAttendance);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 42);
            panel1.TabIndex = 4;
            // 
            // btnPrevMonth
            // 
            btnPrevMonth.Location = new Point(170, 8);
            btnPrevMonth.Name = "btnPrevMonth";
            btnPrevMonth.Size = new Size(50, 23);
            btnPrevMonth.TabIndex = 4;
            btnPrevMonth.Text = "< Prev";
            btnPrevMonth.UseVisualStyleBackColor = true;
            btnPrevMonth.Click += btnPrevMonth_Click;
            // 
            // btnNextMonth
            // 
            btnNextMonth.Location = new Point(341, 8);
            btnNextMonth.Name = "btnNextMonth";
            btnNextMonth.Size = new Size(56, 23);
            btnNextMonth.TabIndex = 4;
            btnNextMonth.Text = "Next >";
            btnNextMonth.UseVisualStyleBackColor = true;
            btnNextMonth.Click += btnNextMonth_Click;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Controls.Add(dgvMonthlyAttendance);
            panel2.Location = new Point(0, 48);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 407);
            panel2.TabIndex = 5;
            // 
            // MonthlyAttendanceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "MonthlyAttendanceForm";
            Text = "MonthlyAttendanceForm";
            Load += MonthlyAttendanceForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMonthlyAttendance).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DateTimePicker dtMonth;
        private DataGridView dgvMonthlyAttendance;
        private Button btnAddAttendance;
        private Panel panel1;
        private Panel panel2;
        private Button btnPrevMonth;
        private Button btnNextMonth;
    }
}