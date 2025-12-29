namespace ArceliaHR
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tsMain = new ToolStrip();
            btnAddEmp = new ToolStripButton();
            btnViewEmp = new ToolStripButton();
            btnEditEmp = new ToolStripButton();
            btnDelEmp = new ToolStripButton();
            btnFiles = new ToolStripButton();
            btnAttend = new ToolStripButton();
            btnPay = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            txtSearch = new ToolStripTextBox();
            cmbSearch = new ToolStripComboBox();
            toolStripSeparator2 = new ToolStripSeparator();
            btnDatabseTest = new ToolStripButton();
            panelGrid = new Panel();
            dgList = new DataGridView();
            tsMain.SuspendLayout();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgList).BeginInit();
            SuspendLayout();
            // 
            // tsMain
            // 
            tsMain.Items.AddRange(new ToolStripItem[] { btnAddEmp, btnViewEmp, btnEditEmp, btnDelEmp, btnFiles, btnAttend, btnPay, toolStripSeparator1, txtSearch, cmbSearch, toolStripSeparator2, btnDatabseTest });
            tsMain.Location = new Point(0, 0);
            tsMain.Name = "tsMain";
            tsMain.Size = new Size(800, 38);
            tsMain.TabIndex = 0;
            tsMain.Text = "toolStrip1";
            // 
            // btnAddEmp
            // 
            btnAddEmp.Image = (Image)resources.GetObject("btnAddEmp.Image");
            btnAddEmp.ImageTransparentColor = Color.Magenta;
            btnAddEmp.Name = "btnAddEmp";
            btnAddEmp.Size = new Size(33, 35);
            btnAddEmp.Text = "Add";
            btnAddEmp.TextImageRelation = TextImageRelation.ImageAboveText;
            btnAddEmp.Click += btnAddEmp_Click;
            // 
            // btnViewEmp
            // 
            btnViewEmp.Image = (Image)resources.GetObject("btnViewEmp.Image");
            btnViewEmp.ImageTransparentColor = Color.Magenta;
            btnViewEmp.Name = "btnViewEmp";
            btnViewEmp.Size = new Size(36, 35);
            btnViewEmp.Text = "View";
            btnViewEmp.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // btnEditEmp
            // 
            btnEditEmp.Image = (Image)resources.GetObject("btnEditEmp.Image");
            btnEditEmp.ImageTransparentColor = Color.Magenta;
            btnEditEmp.Name = "btnEditEmp";
            btnEditEmp.Size = new Size(31, 35);
            btnEditEmp.Text = "Edit";
            btnEditEmp.TextImageRelation = TextImageRelation.ImageAboveText;
            btnEditEmp.Click += btnEditEmp_Click;
            // 
            // btnDelEmp
            // 
            btnDelEmp.Image = (Image)resources.GetObject("btnDelEmp.Image");
            btnDelEmp.ImageTransparentColor = Color.Magenta;
            btnDelEmp.Name = "btnDelEmp";
            btnDelEmp.Size = new Size(44, 35);
            btnDelEmp.Text = "Delete";
            btnDelEmp.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // btnFiles
            // 
            btnFiles.Image = (Image)resources.GetObject("btnFiles.Image");
            btnFiles.ImageTransparentColor = Color.Magenta;
            btnFiles.Name = "btnFiles";
            btnFiles.Size = new Size(34, 35);
            btnFiles.Text = "Files";
            btnFiles.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // btnAttend
            // 
            btnAttend.Image = (Image)resources.GetObject("btnAttend.Image");
            btnAttend.ImageTransparentColor = Color.Magenta;
            btnAttend.Name = "btnAttend";
            btnAttend.Size = new Size(72, 35);
            btnAttend.Text = "Attendance";
            btnAttend.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // btnPay
            // 
            btnPay.Image = (Image)resources.GetObject("btnPay.Image");
            btnPay.ImageTransparentColor = Color.Magenta;
            btnPay.Name = "btnPay";
            btnPay.Size = new Size(63, 35);
            btnPay.Text = "Payments";
            btnPay.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 38);
            // 
            // txtSearch
            // 
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(100, 38);
            // 
            // cmbSearch
            // 
            cmbSearch.Name = "cmbSearch";
            cmbSearch.Size = new Size(121, 38);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 38);
            // 
            // btnDatabseTest
            // 
            btnDatabseTest.Image = (Image)resources.GetObject("btnDatabseTest.Image");
            btnDatabseTest.ImageTransparentColor = Color.Magenta;
            btnDatabseTest.Name = "btnDatabseTest";
            btnDatabseTest.Size = new Size(98, 35);
            btnDatabseTest.Text = "DataBase Test";
            //btnDatabseTest.Click += btnDatabseTest_Click;
            // 
            // panelGrid
            // 
            panelGrid.Controls.Add(dgList);
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Location = new Point(0, 38);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(800, 412);
            panelGrid.TabIndex = 1;
            // 
            // dgList
            // 
            dgList.AllowUserToAddRows = false;
            dgList.AllowUserToDeleteRows = false;
            dgList.AllowUserToOrderColumns = true;
            dgList.AllowUserToResizeRows = false;
            dgList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgList.Dock = DockStyle.Fill;
            dgList.Location = new Point(0, 0);
            dgList.MultiSelect = false;
            dgList.Name = "dgList";
            dgList.ReadOnly = true;
            dgList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgList.Size = new Size(800, 412);
            dgList.TabIndex = 0;
            dgList.CellDoubleClick += dgList_CellDoubleClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelGrid);
            Controls.Add(tsMain);
            Name = "MainForm";
            Text = "Arcelia HR";
            WindowState = FormWindowState.Maximized;
            Load += MainForm_Load;
            tsMain.ResumeLayout(false);
            tsMain.PerformLayout();
            panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip tsMain;
        private Panel panelGrid;
        private DataGridView dgList;
        private ToolStripButton btnAddEmp;
        private ToolStripButton btnViewEmp;
        private ToolStripButton btnEditEmp;
        private ToolStripButton btnDelEmp;
        private ToolStripButton btnFiles;
        private ToolStripButton btnAttend;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripTextBox txtSearch;
        private ToolStripComboBox cmbSearch;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnPay;
        private ToolStripButton btnDatabseTest;
    }
}
