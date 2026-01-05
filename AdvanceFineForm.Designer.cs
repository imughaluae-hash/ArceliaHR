namespace ArceliaHR
{
    partial class AdvanceFineForm
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
            label1 = new Label();
            label2 = new Label();
            lblName = new Label();
            lblCurrBalance = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            cmbType = new ComboBox();
            dtTran = new DateTimePicker();
            txtDescr = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            udAmount = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)udAmount).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 20);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 0;
            label1.Text = "Employee:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 48);
            label2.Name = "label2";
            label2.Size = new Size(94, 15);
            label2.TabIndex = 0;
            label2.Text = "Current Balance:";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(126, 20);
            lblName.Name = "lblName";
            lblName.Size = new Size(62, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Employee:";
            // 
            // lblCurrBalance
            // 
            lblCurrBalance.AutoSize = true;
            lblCurrBalance.Location = new Point(126, 48);
            lblCurrBalance.Name = "lblCurrBalance";
            lblCurrBalance.Size = new Size(94, 15);
            lblCurrBalance.TabIndex = 0;
            lblCurrBalance.Text = "Current Balance:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 77);
            label3.Name = "label3";
            label3.Size = new Size(94, 15);
            label3.TabIndex = 0;
            label3.Text = "Transaction Type";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 109);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 0;
            label4.Text = "Date";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(16, 135);
            label5.Name = "label5";
            label5.Size = new Size(51, 15);
            label5.TabIndex = 0;
            label5.Text = "Amount";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(16, 164);
            label6.Name = "label6";
            label6.Size = new Size(67, 15);
            label6.TabIndex = 0;
            label6.Text = "Description";
            // 
            // cmbType
            // 
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.FormattingEnabled = true;
            cmbType.Items.AddRange(new object[] { "ADVANCE", "FINE", "ADJUSTMENT" });
            cmbType.Location = new Point(126, 74);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(121, 23);
            cmbType.TabIndex = 1;
            // 
            // dtTran
            // 
            dtTran.Format = DateTimePickerFormat.Short;
            dtTran.Location = new Point(126, 103);
            dtTran.Name = "dtTran";
            dtTran.Size = new Size(121, 23);
            dtTran.TabIndex = 2;
            // 
            // txtDescr
            // 
            txtDescr.Location = new Point(126, 161);
            txtDescr.Multiline = true;
            txtDescr.Name = "txtDescr";
            txtDescr.Size = new Size(121, 50);
            txtDescr.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(45, 217);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(136, 217);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // udAmount
            // 
            udAmount.DecimalPlaces = 2;
            udAmount.Location = new Point(126, 132);
            udAmount.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            udAmount.Name = "udAmount";
            udAmount.Size = new Size(120, 23);
            udAmount.TabIndex = 5;
            udAmount.ThousandsSeparator = true;
            // 
            // AdvanceFineForm
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(268, 251);
            ControlBox = false;
            Controls.Add(udAmount);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtDescr);
            Controls.Add(dtTran);
            Controls.Add(cmbType);
            Controls.Add(lblCurrBalance);
            Controls.Add(lblName);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            MaximizeBox = false;
            MaximumSize = new Size(284, 290);
            MinimizeBox = false;
            MinimumSize = new Size(284, 290);
            Name = "AdvanceFineForm";
            Text = "Advance Fine Form";
            ((System.ComponentModel.ISupportInitialize)udAmount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label lblName;
        private Label lblCurrBalance;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private ComboBox cmbType;
        private DateTimePicker dtTran;
        private TextBox txtDescr;
        private Button btnSave;
        private Button btnCancel;
        private NumericUpDown udAmount;
    }
}