namespace ArceliaHR
{
    partial class EmployeeStatementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeStatementForm));
            panel1 = new Panel();
            lblBalance = new Label();
            lblStatus = new Label();
            label4 = new Label();
            lblDepartment = new Label();
            label3 = new Label();
            lblName = new Label();
            label2 = new Label();
            label1 = new Label();
            panel2 = new Panel();
            dgStatement = new DataGridView();
            toolStrip1 = new ToolStrip();
            btnPrint = new ToolStripButton();
            brtnClose = new ToolStripButton();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgStatement).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(lblBalance);
            panel1.Controls.Add(lblStatus);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(lblDepartment);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(lblName);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(0, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(535, 82);
            panel1.TabIndex = 1;
            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Location = new Point(123, 59);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(94, 15);
            lblBalance.TabIndex = 0;
            lblBalance.Text = "Current Balance:";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(123, 42);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(42, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Status:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 59);
            label4.Name = "label4";
            label4.Size = new Size(94, 15);
            label4.TabIndex = 0;
            label4.Text = "Current Balance:";
            // 
            // lblDepartment
            // 
            lblDepartment.AutoSize = true;
            lblDepartment.Location = new Point(123, 25);
            lblDepartment.Name = "lblDepartment";
            lblDepartment.Size = new Size(73, 15);
            lblDepartment.TabIndex = 0;
            lblDepartment.Text = "Department:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 42);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 0;
            label3.Text = "Status:";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(123, 8);
            lblName.Name = "lblName";
            lblName.Size = new Size(97, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Employee Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 25);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 0;
            label2.Text = "Department:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 8);
            label1.Name = "label1";
            label1.Size = new Size(97, 15);
            label1.TabIndex = 0;
            label1.Text = "Employee Name:";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Controls.Add(dgStatement);
            panel2.Location = new Point(0, 89);
            panel2.Name = "panel2";
            panel2.Size = new Size(535, 591);
            panel2.TabIndex = 2;
            // 
            // dgStatement
            // 
            dgStatement.AllowUserToAddRows = false;
            dgStatement.AllowUserToDeleteRows = false;
            dgStatement.AllowUserToResizeRows = false;
            dgStatement.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgStatement.Dock = DockStyle.Fill;
            dgStatement.Location = new Point(0, 0);
            dgStatement.Name = "dgStatement";
            dgStatement.ReadOnly = true;
            dgStatement.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgStatement.Size = new Size(535, 591);
            dgStatement.TabIndex = 0;
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.Bottom;
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnPrint, brtnClose });
            toolStrip1.Location = new Point(0, 683);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(535, 25);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnPrint
            // 
            btnPrint.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnPrint.Image = (Image)resources.GetObject("btnPrint.Image");
            btnPrint.ImageTransparentColor = Color.Magenta;
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(36, 22);
            btnPrint.Text = "Print";
            // 
            // brtnClose
            // 
            brtnClose.DisplayStyle = ToolStripItemDisplayStyle.Text;
            brtnClose.Image = (Image)resources.GetObject("brtnClose.Image");
            brtnClose.ImageTransparentColor = Color.Magenta;
            brtnClose.Name = "brtnClose";
            brtnClose.Size = new Size(40, 22);
            brtnClose.Text = "Close";
            brtnClose.Click += brtnClose_Click;
            // 
            // EmployeeStatementForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(535, 708);
            Controls.Add(toolStrip1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            MinimumSize = new Size(551, 740);
            Name = "EmployeeStatementForm";
            Text = "Employee Statement";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgStatement).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel1;
        private Panel panel2;
        private DataGridView dgStatement;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label lblBalance;
        private Label lblStatus;
        private Label lblDepartment;
        private Label lblName;
        private ToolStrip toolStrip1;
        private ToolStripButton btnPrint;
        private ToolStripButton brtnClose;
    }
}