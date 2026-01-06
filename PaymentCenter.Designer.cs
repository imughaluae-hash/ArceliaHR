namespace ArceliaHR
{
    partial class PaymentCenter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentCenter));
            toolStrip1 = new ToolStrip();
            btnAdvFine = new ToolStripButton();
            btnSalary = new ToolStripButton();
            splitContainer1 = new SplitContainer();
            dgActive = new DataGridView();
            dgInactive = new DataGridView();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgActive).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgInactive).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnAdvFine, btnSalary });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(800, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnAdvFine
            // 
            btnAdvFine.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAdvFine.Enabled = false;
            btnAdvFine.Image = (Image)resources.GetObject("btnAdvFine.Image");
            btnAdvFine.ImageTransparentColor = Color.Magenta;
            btnAdvFine.Name = "btnAdvFine";
            btnAdvFine.Size = new Size(23, 22);
            btnAdvFine.Text = "Adv / FIne";
            btnAdvFine.Click += btnAdvFine_Click;
            // 
            // btnSalary
            // 
            btnSalary.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSalary.Enabled = false;
            btnSalary.Image = (Image)resources.GetObject("btnSalary.Image");
            btnSalary.ImageTransparentColor = Color.Magenta;
            btnSalary.Name = "btnSalary";
            btnSalary.Size = new Size(23, 22);
            btnSalary.Text = "Salary";
            btnSalary.Click += btnSalary_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 25);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dgActive);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dgInactive);
            splitContainer1.Size = new Size(800, 425);
            splitContainer1.SplitterDistance = 225;
            splitContainer1.TabIndex = 1;
            // 
            // dgActive
            // 
            dgActive.AllowUserToAddRows = false;
            dgActive.AllowUserToDeleteRows = false;
            dgActive.AllowUserToResizeRows = false;
            dgActive.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgActive.Dock = DockStyle.Fill;
            dgActive.Location = new Point(0, 0);
            dgActive.MultiSelect = false;
            dgActive.Name = "dgActive";
            dgActive.ReadOnly = true;
            dgActive.RowHeadersVisible = false;
            dgActive.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgActive.Size = new Size(800, 225);
            dgActive.TabIndex = 0;
            dgActive.CellDoubleClick += dgActive_CellDoubleClick;
            dgActive.SelectionChanged += dgActive_SelectionChanged;
            // 
            // dgInactive
            // 
            dgInactive.AllowUserToAddRows = false;
            dgInactive.AllowUserToDeleteRows = false;
            dgInactive.AllowUserToResizeRows = false;
            dgInactive.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgInactive.Dock = DockStyle.Fill;
            dgInactive.Location = new Point(0, 0);
            dgInactive.MultiSelect = false;
            dgInactive.Name = "dgInactive";
            dgInactive.ReadOnly = true;
            dgInactive.RowHeadersVisible = false;
            dgInactive.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgInactive.Size = new Size(800, 196);
            dgInactive.TabIndex = 0;
            dgInactive.CellDoubleClick += dgInactive_CellDoubleClick;
            dgInactive.SelectionChanged += dgInactive_SelectionChanged;
            // 
            // PaymentCenter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Controls.Add(toolStrip1);
            MinimizeBox = false;
            MinimumSize = new Size(816, 489);
            Name = "PaymentCenter";
            Text = "Payment Center";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgActive).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgInactive).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton btnAdvFine;
        private ToolStripButton btnSalary;
        private SplitContainer splitContainer1;
        private DataGridView dgActive;
        private DataGridView dgInactive;
    }
}