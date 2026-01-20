namespace ArceliaHR
{
    partial class SettingsForm
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
            tabControl = new TabControl();
            tabGeneral = new TabPage();
            btnBrowseLogo = new Button();
            picCompanyLogo = new PictureBox();
            lblLogo = new Label();
            udWorkHours = new NumericUpDown();
            lblWorkHours = new Label();
            txtCompanyName = new TextBox();
            lblCompanyName = new Label();
            tabDatabase = new TabPage();
            lblCurrentDbPath = new Label();
            txtDbPath = new TextBox();
            btnImport = new Button();
            btnExport = new Button();
            lblDatabaseInfo = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            tabControl.SuspendLayout();
            tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCompanyLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)udWorkHours).BeginInit();
            tabDatabase.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabGeneral);
            tabControl.Controls.Add(tabDatabase);
            tabControl.Location = new Point(12, 12);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(560, 360);
            tabControl.TabIndex = 0;
            // 
            // tabGeneral
            // 
            tabGeneral.Controls.Add(btnBrowseLogo);
            tabGeneral.Controls.Add(picCompanyLogo);
            tabGeneral.Controls.Add(lblLogo);
            tabGeneral.Controls.Add(udWorkHours);
            tabGeneral.Controls.Add(lblWorkHours);
            tabGeneral.Controls.Add(txtCompanyName);
            tabGeneral.Controls.Add(lblCompanyName);
            tabGeneral.Location = new Point(4, 24);
            tabGeneral.Name = "tabGeneral";
            tabGeneral.Padding = new Padding(3);
            tabGeneral.Size = new Size(552, 332);
            tabGeneral.TabIndex = 0;
            tabGeneral.Text = "General";
            tabGeneral.UseVisualStyleBackColor = true;
            // 
            // btnBrowseLogo
            // 
            btnBrowseLogo.Location = new Point(420, 145);
            btnBrowseLogo.Name = "btnBrowseLogo";
            btnBrowseLogo.Size = new Size(100, 30);
            btnBrowseLogo.TabIndex = 6;
            btnBrowseLogo.Text = "Browse...";
            btnBrowseLogo.UseVisualStyleBackColor = true;
            btnBrowseLogo.Click += btnBrowseLogo_Click;
            // 
            // picCompanyLogo
            // 
            picCompanyLogo.BorderStyle = BorderStyle.FixedSingle;
            picCompanyLogo.Location = new Point(150, 115);
            picCompanyLogo.Name = "picCompanyLogo";
            picCompanyLogo.Size = new Size(250, 150);
            picCompanyLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picCompanyLogo.TabIndex = 5;
            picCompanyLogo.TabStop = false;
            // 
            // lblLogo
            // 
            lblLogo.AutoSize = true;
            lblLogo.Location = new Point(30, 115);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(92, 15);
            lblLogo.TabIndex = 4;
            lblLogo.Text = "Company Logo:";
            // 
            // udWorkHours
            // 
            udWorkHours.DecimalPlaces = 1;
            udWorkHours.Location = new Point(150, 70);
            udWorkHours.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            udWorkHours.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            udWorkHours.Name = "udWorkHours";
            udWorkHours.Size = new Size(120, 23);
            udWorkHours.TabIndex = 3;
            udWorkHours.Value = new decimal(new int[] { 9, 0, 0, 0 });
            // 
            // lblWorkHours
            // 
            lblWorkHours.AutoSize = true;
            lblWorkHours.Location = new Point(30, 72);
            lblWorkHours.Name = "lblWorkHours";
            lblWorkHours.Size = new Size(107, 15);
            lblWorkHours.TabIndex = 2;
            lblWorkHours.Text = "Work Hours / Day:";
            // 
            // txtCompanyName
            // 
            txtCompanyName.Location = new Point(150, 30);
            txtCompanyName.Name = "txtCompanyName";
            txtCompanyName.Size = new Size(370, 23);
            txtCompanyName.TabIndex = 1;
            // 
            // lblCompanyName
            // 
            lblCompanyName.AutoSize = true;
            lblCompanyName.Location = new Point(30, 33);
            lblCompanyName.Name = "lblCompanyName";
            lblCompanyName.Size = new Size(97, 15);
            lblCompanyName.TabIndex = 0;
            lblCompanyName.Text = "Company Name:";
            // 
            // tabDatabase
            // 
            tabDatabase.Controls.Add(lblCurrentDbPath);
            tabDatabase.Controls.Add(txtDbPath);
            tabDatabase.Controls.Add(btnImport);
            tabDatabase.Controls.Add(btnExport);
            tabDatabase.Controls.Add(lblDatabaseInfo);
            tabDatabase.Location = new Point(4, 24);
            tabDatabase.Name = "tabDatabase";
            tabDatabase.Padding = new Padding(3);
            tabDatabase.Size = new Size(552, 332);
            tabDatabase.TabIndex = 1;
            tabDatabase.Text = "Database";
            tabDatabase.UseVisualStyleBackColor = true;
            // 
            // lblCurrentDbPath
            // 
            lblCurrentDbPath.AutoSize = true;
            lblCurrentDbPath.Location = new Point(30, 30);
            lblCurrentDbPath.Name = "lblCurrentDbPath";
            lblCurrentDbPath.Size = new Size(127, 15);
            lblCurrentDbPath.TabIndex = 4;
            lblCurrentDbPath.Text = "Current Database Path:";
            // 
            // txtDbPath
            // 
            txtDbPath.Location = new Point(30, 55);
            txtDbPath.Multiline = true;
            txtDbPath.Name = "txtDbPath";
            txtDbPath.ReadOnly = true;
            txtDbPath.Size = new Size(490, 50);
            txtDbPath.TabIndex = 3;
            // 
            // btnImport
            // 
            btnImport.Location = new Point(30, 200);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(200, 35);
            btnImport.TabIndex = 2;
            btnImport.Text = "Import Database";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(30, 150);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(200, 35);
            btnExport.TabIndex = 1;
            btnExport.Text = "Export Database (Backup)";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // lblDatabaseInfo
            // 
            lblDatabaseInfo.AutoSize = true;
            lblDatabaseInfo.ForeColor = SystemColors.GrayText;
            lblDatabaseInfo.Location = new Point(30, 120);
            lblDatabaseInfo.Name = "lblDatabaseInfo";
            lblDatabaseInfo.Size = new Size(420, 15);
            lblDatabaseInfo.TabIndex = 0;
            lblDatabaseInfo.Text = "Export creates a backup copy. Import replaces the current database (use with caution).";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(380, 385);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 30);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(480, 385);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 30);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 431);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(tabControl);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            Load += SettingsForm_Load;
            tabControl.ResumeLayout(false);
            tabGeneral.ResumeLayout(false);
            tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCompanyLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)udWorkHours).EndInit();
            tabDatabase.ResumeLayout(false);
            tabDatabase.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage tabGeneral;
        private TabPage tabDatabase;
        private Label lblCompanyName;
        private TextBox txtCompanyName;
        private Label lblWorkHours;
        private NumericUpDown udWorkHours;
        private Label lblLogo;
        private PictureBox picCompanyLogo;
        private Button btnBrowseLogo;
        private Label lblDatabaseInfo;
        private Button btnExport;
        private Button btnImport;
        private TextBox txtDbPath;
        private Label lblCurrentDbPath;
        private Button btnSave;
        private Button btnCancel;
    }
}
