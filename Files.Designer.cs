namespace ArceliaHR
{
    partial class Files
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
            components = new System.ComponentModel.Container();
            btnRefresh = new Button();
            btnAddFile = new Button();
            listViewFiles = new ListView();
            imageListFiles = new ImageList(components);
            cmbEmployeeFilter = new ComboBox();
            SuspendLayout();
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(91, 12);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 23);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnAddFile
            // 
            btnAddFile.Location = new Point(10, 12);
            btnAddFile.Name = "btnAddFile";
            btnAddFile.Size = new Size(75, 23);
            btnAddFile.TabIndex = 1;
            btnAddFile.Text = "Add File";
            btnAddFile.UseVisualStyleBackColor = true;
            btnAddFile.Click += btnAddFile_Click;
            // 
            // listViewFiles
            // 
            listViewFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewFiles.LargeImageList = imageListFiles;
            listViewFiles.Location = new Point(10, 41);
            listViewFiles.MultiSelect = false;
            listViewFiles.Name = "listViewFiles";
            listViewFiles.Size = new Size(778, 397);
            listViewFiles.TabIndex = 2;
            listViewFiles.UseCompatibleStateImageBehavior = false;
            listViewFiles.DoubleClick += listViewFiles_DoubleClick;
            // 
            // imageListFiles
            // 
            imageListFiles.ColorDepth = ColorDepth.Depth32Bit;
            imageListFiles.ImageSize = new Size(128, 128);
            imageListFiles.TransparentColor = Color.Transparent;
            // 
            // cmbEmployeeFilter
            // 
            cmbEmployeeFilter.FormattingEnabled = true;
            cmbEmployeeFilter.Location = new Point(172, 12);
            cmbEmployeeFilter.Name = "cmbEmployeeFilter";
            cmbEmployeeFilter.Size = new Size(121, 23);
            cmbEmployeeFilter.TabIndex = 3;
            cmbEmployeeFilter.SelectedIndexChanged += CmbEmployeeFilter_SelectedIndexChanged;
            // 
            // Files
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cmbEmployeeFilter);
            Controls.Add(listViewFiles);
            Controls.Add(btnAddFile);
            Controls.Add(btnRefresh);
            Name = "Files";
            Text = "Files";
            FormClosing += Files_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private Button btnRefresh;
        private Button btnAddFile;
        private ListView listViewFiles;
        private ImageList imageListFiles;
        private ComboBox cmbEmployeeFilter;
    }
}