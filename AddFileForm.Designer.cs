namespace ArceliaHR
{
    partial class AddFileForm
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
            cmbEmployee = new ComboBox();
            txtFileName = new TextBox();
            lblSelectedFile = new Label();
            btnBrowse = new Button();
            btnAddFile = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // cmbEmployee
            // 
            cmbEmployee.FormattingEnabled = true;
            cmbEmployee.Location = new Point(92, 12);
            cmbEmployee.Name = "cmbEmployee";
            cmbEmployee.Size = new Size(180, 23);
            cmbEmployee.TabIndex = 0;
            cmbEmployee.SelectedIndexChanged += CmbEmployee_SelectedIndexChanged;
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(92, 41);
            txtFileName.Name = "txtFileName";
            txtFileName.Size = new Size(180, 23);
            txtFileName.TabIndex = 1;
            // 
            // lblSelectedFile
            // 
            lblSelectedFile.AutoSize = true;
            lblSelectedFile.Location = new Point(92, 96);
            lblSelectedFile.Name = "lblSelectedFile";
            lblSelectedFile.Size = new Size(36, 15);
            lblSelectedFile.TabIndex = 2;
            lblSelectedFile.Text = "None";
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(92, 70);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(180, 23);
            btnBrowse.TabIndex = 3;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnAddFile
            // 
            btnAddFile.Location = new Point(92, 114);
            btnAddFile.Name = "btnAddFile";
            btnAddFile.Size = new Size(180, 23);
            btnAddFile.TabIndex = 3;
            btnAddFile.Text = "SAVE";
            btnAddFile.UseVisualStyleBackColor = true;
            btnAddFile.Click += btnAddFile_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 15);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 2;
            label2.Text = "Employee";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 44);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 2;
            label3.Text = "File Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 96);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 2;
            label4.Text = "Path";
            // 
            // AddFileForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(292, 149);
            Controls.Add(btnAddFile);
            Controls.Add(btnBrowse);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lblSelectedFile);
            Controls.Add(txtFileName);
            Controls.Add(cmbEmployee);
            MaximizeBox = false;
            MaximumSize = new Size(308, 188);
            MinimizeBox = false;
            MinimumSize = new Size(308, 188);
            Name = "AddFileForm";
            Text = "AddFileForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbEmployee;
        private TextBox txtFileName;
        private Label lblSelectedFile;
        private Button btnBrowse;
        private Button btnAddFile;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}