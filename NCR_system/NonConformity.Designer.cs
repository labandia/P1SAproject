namespace NCR_system
{
    partial class NonConformity
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NonConformity));
            this.NonComTable = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ExCC = new System.Windows.Forms.TabPage();
            this.SDCCC = new System.Windows.Forms.TabPage();
            this.ProcessMenu = new System.Windows.Forms.TabPage();
            this.RejectedMenu = new System.Windows.Forms.TabPage();
            this.ShipmentMenu = new System.Windows.Forms.TabPage();
            this.RegistrationMenu = new System.Windows.Forms.TabPage();
            this.RecurrenceMenu = new System.Windows.Forms.TabPage();
            this.CustomDatagrid = new System.Windows.Forms.DataGridView();
            this.RecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NGQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Details = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.NonComTable)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.ExCC.SuspendLayout();
            this.SDCCC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomDatagrid)).BeginInit();
            this.SuspendLayout();
            // 
            // NonComTable
            // 
            this.NonComTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NonComTable.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.NonComTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NonComTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NonComTable.Location = new System.Drawing.Point(104, 65);
            this.NonComTable.Name = "NonComTable";
            this.NonComTable.Size = new System.Drawing.Size(871, 439);
            this.NonComTable.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.ExCC);
            this.tabControl1.Controls.Add(this.SDCCC);
            this.tabControl1.Controls.Add(this.ProcessMenu);
            this.tabControl1.Controls.Add(this.RejectedMenu);
            this.tabControl1.Controls.Add(this.ShipmentMenu);
            this.tabControl1.Controls.Add(this.RegistrationMenu);
            this.tabControl1.Controls.Add(this.RecurrenceMenu);
            this.tabControl1.Location = new System.Drawing.Point(12, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1430, 643);
            this.tabControl1.TabIndex = 83;
            // 
            // ExCC
            // 
            this.ExCC.Controls.Add(this.CustomDatagrid);
            this.ExCC.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExCC.Location = new System.Drawing.Point(4, 22);
            this.ExCC.Name = "ExCC";
            this.ExCC.Padding = new System.Windows.Forms.Padding(3);
            this.ExCC.Size = new System.Drawing.Size(1422, 617);
            this.ExCC.TabIndex = 0;
            this.ExCC.Text = "External Customer Complaint";
            this.ExCC.UseVisualStyleBackColor = true;
            // 
            // SDCCC
            // 
            this.SDCCC.Controls.Add(this.NonComTable);
            this.SDCCC.Location = new System.Drawing.Point(4, 22);
            this.SDCCC.Name = "SDCCC";
            this.SDCCC.Padding = new System.Windows.Forms.Padding(3);
            this.SDCCC.Size = new System.Drawing.Size(1111, 617);
            this.SDCCC.TabIndex = 1;
            this.SDCCC.Text = "SDC Customer Complaint";
            this.SDCCC.UseVisualStyleBackColor = true;
            // 
            // ProcessMenu
            // 
            this.ProcessMenu.Location = new System.Drawing.Point(4, 22);
            this.ProcessMenu.Name = "ProcessMenu";
            this.ProcessMenu.Padding = new System.Windows.Forms.Padding(3);
            this.ProcessMenu.Size = new System.Drawing.Size(1111, 617);
            this.ProcessMenu.TabIndex = 2;
            this.ProcessMenu.Text = "INPROCESS DEFECT";
            this.ProcessMenu.UseVisualStyleBackColor = true;
            // 
            // RejectedMenu
            // 
            this.RejectedMenu.Location = new System.Drawing.Point(4, 22);
            this.RejectedMenu.Name = "RejectedMenu";
            this.RejectedMenu.Padding = new System.Windows.Forms.Padding(3);
            this.RejectedMenu.Size = new System.Drawing.Size(1111, 617);
            this.RejectedMenu.TabIndex = 3;
            this.RejectedMenu.Text = "REJECTED LOT";
            this.RejectedMenu.UseVisualStyleBackColor = true;
            // 
            // ShipmentMenu
            // 
            this.ShipmentMenu.Location = new System.Drawing.Point(4, 22);
            this.ShipmentMenu.Name = "ShipmentMenu";
            this.ShipmentMenu.Padding = new System.Windows.Forms.Padding(3);
            this.ShipmentMenu.Size = new System.Drawing.Size(1111, 617);
            this.ShipmentMenu.TabIndex = 4;
            this.ShipmentMenu.Text = "SHIPMENT DELAY";
            this.ShipmentMenu.UseVisualStyleBackColor = true;
            // 
            // RegistrationMenu
            // 
            this.RegistrationMenu.Location = new System.Drawing.Point(4, 22);
            this.RegistrationMenu.Name = "RegistrationMenu";
            this.RegistrationMenu.Padding = new System.Windows.Forms.Padding(3);
            this.RegistrationMenu.Size = new System.Drawing.Size(1111, 617);
            this.RegistrationMenu.TabIndex = 5;
            this.RegistrationMenu.Text = "MAIN REGISTRATION";
            this.RegistrationMenu.UseVisualStyleBackColor = true;
            // 
            // RecurrenceMenu
            // 
            this.RecurrenceMenu.Location = new System.Drawing.Point(4, 22);
            this.RecurrenceMenu.Name = "RecurrenceMenu";
            this.RecurrenceMenu.Size = new System.Drawing.Size(1111, 617);
            this.RecurrenceMenu.TabIndex = 6;
            this.RecurrenceMenu.Text = "RECURRENCE";
            this.RecurrenceMenu.UseVisualStyleBackColor = true;
            // 
            // CustomDatagrid
            // 
            this.CustomDatagrid.AllowUserToAddRows = false;
            this.CustomDatagrid.AllowUserToDeleteRows = false;
            this.CustomDatagrid.AllowUserToResizeColumns = false;
            this.CustomDatagrid.AllowUserToResizeRows = false;
            this.CustomDatagrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomDatagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CustomDatagrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.CustomDatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustomDatagrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecordID,
            this.DateCreated,
            this.SectionID,
            this.ModelNo,
            this.LotNo,
            this.NGQty,
            this.Details,
            this.RegNo,
            this.CustomerName,
            this.Status,
            this.CCtype,
            this.Delete});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CustomDatagrid.DefaultCellStyle = dataGridViewCellStyle8;
            this.CustomDatagrid.Location = new System.Drawing.Point(6, 71);
            this.CustomDatagrid.Name = "CustomDatagrid";
            this.CustomDatagrid.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CustomDatagrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.CustomDatagrid.RowHeadersVisible = false;
            this.CustomDatagrid.RowTemplate.Height = 30;
            this.CustomDatagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CustomDatagrid.Size = new System.Drawing.Size(1393, 512);
            this.CustomDatagrid.TabIndex = 5;
            // 
            // RecordID
            // 
            this.RecordID.DataPropertyName = "RecordID";
            this.RecordID.HeaderText = "RecordID";
            this.RecordID.Name = "RecordID";
            this.RecordID.ReadOnly = true;
            this.RecordID.Visible = false;
            this.RecordID.Width = 72;
            // 
            // DateCreated
            // 
            this.DateCreated.DataPropertyName = "DateCreated";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DateCreated.DefaultCellStyle = dataGridViewCellStyle2;
            this.DateCreated.HeaderText = "Date";
            this.DateCreated.Name = "DateCreated";
            this.DateCreated.ReadOnly = true;
            this.DateCreated.Width = 71;
            // 
            // SectionID
            // 
            this.SectionID.DataPropertyName = "SectionID";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SectionID.DefaultCellStyle = dataGridViewCellStyle3;
            this.SectionID.HeaderText = "Section in charge";
            this.SectionID.Name = "SectionID";
            this.SectionID.ReadOnly = true;
            this.SectionID.Width = 135;
            // 
            // ModelNo
            // 
            this.ModelNo.DataPropertyName = "ModelNo";
            this.ModelNo.HeaderText = "Model No/ Part no.";
            this.ModelNo.Name = "ModelNo";
            this.ModelNo.ReadOnly = true;
            this.ModelNo.Width = 126;
            // 
            // LotNo
            // 
            this.LotNo.DataPropertyName = "LotNo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.LotNo.DefaultCellStyle = dataGridViewCellStyle4;
            this.LotNo.HeaderText = "Lot No.";
            this.LotNo.Name = "LotNo";
            this.LotNo.ReadOnly = true;
            this.LotNo.Width = 78;
            // 
            // NGQty
            // 
            this.NGQty.DataPropertyName = "NGQty";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NGQty.DefaultCellStyle = dataGridViewCellStyle5;
            this.NGQty.HeaderText = "NG Qty";
            this.NGQty.Name = "NGQty";
            this.NGQty.ReadOnly = true;
            this.NGQty.Width = 81;
            // 
            // Details
            // 
            this.Details.DataPropertyName = "Details";
            this.Details.HeaderText = "Details of Problem";
            this.Details.Name = "Details";
            this.Details.ReadOnly = true;
            this.Details.Width = 136;
            // 
            // RegNo
            // 
            this.RegNo.DataPropertyName = "RegNo";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegNo.DefaultCellStyle = dataGridViewCellStyle6;
            this.RegNo.HeaderText = "Registration No.";
            this.RegNo.Name = "RegNo";
            this.RegNo.ReadOnly = true;
            this.RegNo.Width = 124;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Width = 127;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Status.DefaultCellStyle = dataGridViewCellStyle7;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 76;
            // 
            // CCtype
            // 
            this.CCtype.DataPropertyName = "CCtype";
            this.CCtype.HeaderText = "CCtype";
            this.CCtype.Name = "CCtype";
            this.CCtype.ReadOnly = true;
            this.CCtype.Visible = false;
            this.CCtype.Width = 81;
            // 
            // Delete
            // 
            this.Delete.DataPropertyName = "Delete";
            this.Delete.HeaderText = "Delete";
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Width = 63;
            // 
            // NonConformity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1454, 749);
            this.Controls.Add(this.tabControl1);
            this.Name = "NonConformity";
            this.Text = "NonConformity";
            this.Load += new System.EventHandler(this.NonConformity_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NonComTable)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ExCC.ResumeLayout(false);
            this.SDCCC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CustomDatagrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView NonComTable;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ExCC;
        private System.Windows.Forms.TabPage SDCCC;
        private System.Windows.Forms.TabPage ProcessMenu;
        private System.Windows.Forms.TabPage RejectedMenu;
        private System.Windows.Forms.TabPage ShipmentMenu;
        private System.Windows.Forms.TabPage RegistrationMenu;
        private System.Windows.Forms.TabPage RecurrenceMenu;
        public System.Windows.Forms.DataGridView CustomDatagrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCreated;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NGQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Details;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCtype;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
    }
}