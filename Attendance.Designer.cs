namespace ArceliaHR
{
    partial class Attendance
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
            btnLoad = new Button();
            dtDate = new DateTimePicker();
            dgvAttendance = new DataGridView();
            btnSave = new Button();
            btnNextDay = new Button();
            btnPrevDay = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).BeginInit();
            SuspendLayout();
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(734, 12);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(75, 23);
            btnLoad.TabIndex = 0;
            btnLoad.Text = "LOAD";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // dtDate
            // 
            dtDate.Location = new Point(49, 12);
            dtDate.Name = "dtDate";
            dtDate.Size = new Size(200, 23);
            dtDate.TabIndex = 1;
            dtDate.ValueChanged += dtDate_ValueChanged;
            // 
            // dgvAttendance
            // 
            dgvAttendance.AllowUserToAddRows = false;
            dgvAttendance.AllowUserToDeleteRows = false;
            dgvAttendance.AllowUserToResizeRows = false;
            dgvAttendance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAttendance.Location = new Point(12, 41);
            dgvAttendance.Name = "dgvAttendance";
            dgvAttendance.Size = new Size(797, 368);
            dgvAttendance.TabIndex = 2;
            dgvAttendance.CellValueChanged += dgvAttendance_CellValueChanged;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(12, 415);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(281, 23);
            btnSave.TabIndex = 3;
            btnSave.Text = "SAVE";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnNextDay
            // 
            btnNextDay.Location = new Point(255, 11);
            btnNextDay.Name = "btnNextDay";
            btnNextDay.Size = new Size(23, 23);
            btnNextDay.TabIndex = 4;
            btnNextDay.Text = ">";
            btnNextDay.UseVisualStyleBackColor = true;
            btnNextDay.Click += btnNextDay_Click;
            // 
            // btnPrevDay
            // 
            btnPrevDay.Location = new Point(20, 11);
            btnPrevDay.Name = "btnPrevDay";
            btnPrevDay.Size = new Size(23, 23);
            btnPrevDay.TabIndex = 4;
            btnPrevDay.Text = "<";
            btnPrevDay.UseVisualStyleBackColor = true;
            btnPrevDay.Click += btnPrevDay_Click;
            // 
            // Attendance
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(821, 450);
            Controls.Add(btnPrevDay);
            Controls.Add(btnNextDay);
            Controls.Add(btnSave);
            Controls.Add(dgvAttendance);
            Controls.Add(dtDate);
            Controls.Add(btnLoad);
            MaximumSize = new Size(837, 489);
            MinimizeBox = false;
            MinimumSize = new Size(837, 489);
            Name = "Attendance";
            Text = "Attendance";
            Load += Attendance_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnLoad;
        private DateTimePicker dtDate;
        private DataGridView dgvAttendance;
        private Button btnSave;
        private Button btnNextDay;
        private Button btnPrevDay;
    }
}