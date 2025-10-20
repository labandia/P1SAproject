namespace NCR_system.View.Module
{
    partial class Customer_Complaint_user
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Customer_Complaint_user));
            this.projectitle = new System.Windows.Forms.Label();
            this.CustomDatagrid = new System.Windows.Forms.DataGridView();
            this.CustSummaryGrid = new System.Windows.Forms.DataGridView();
            this.OpenCC = new System.Windows.Forms.Button();
            this.SelectedProcess = new System.Windows.Forms.ComboBox();
            this.Externalbtn = new System.Windows.Forms.Button();
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
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.CustomDatagrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustSummaryGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // projectitle
            // 
            this.projectitle.AutoSize = true;
            this.projectitle.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectitle.ForeColor = System.Drawing.Color.Black;
            this.projectitle.Location = new System.Drawing.Point(43, 47);
            this.projectitle.Name = "projectitle";
            this.projectitle.Size = new System.Drawing.Size(253, 28);
            this.projectitle.TabIndex = 3;
            this.projectitle.Text = "Customer Complaint";
            // 
            // CustomDatagrid
            // 
            this.CustomDatagrid.AllowUserToAddRows = false;
            this.CustomDatagrid.AllowUserToDeleteRows = false;
            this.CustomDatagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.Edit,
            this.Delete});
            this.CustomDatagrid.Location = new System.Drawing.Point(48, 191);
            this.CustomDatagrid.Name = "CustomDatagrid";
            this.CustomDatagrid.ReadOnly = true;
            this.CustomDatagrid.RowHeadersVisible = false;
            this.CustomDatagrid.Size = new System.Drawing.Size(1233, 489);
            this.CustomDatagrid.TabIndex = 4;
            this.CustomDatagrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CustomDatagrid_CellClick);
            this.CustomDatagrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.CustomDatagrid_CellFormatting);
            // 
            // CustSummaryGrid
            // 
            this.CustSummaryGrid.AllowUserToAddRows = false;
            this.CustSummaryGrid.AllowUserToDeleteRows = false;
            this.CustSummaryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustSummaryGrid.Location = new System.Drawing.Point(905, 24);
            this.CustSummaryGrid.Name = "CustSummaryGrid";
            this.CustSummaryGrid.ReadOnly = true;
            this.CustSummaryGrid.Size = new System.Drawing.Size(376, 148);
            this.CustSummaryGrid.TabIndex = 5;
            // 
            // OpenCC
            // 
            this.OpenCC.Location = new System.Drawing.Point(634, 80);
            this.OpenCC.Name = "OpenCC";
            this.OpenCC.Size = new System.Drawing.Size(75, 23);
            this.OpenCC.TabIndex = 6;
            this.OpenCC.Text = "OpenCC";
            this.OpenCC.UseVisualStyleBackColor = true;
            this.OpenCC.Click += new System.EventHandler(this.OpenCC_Click);
            // 
            // SelectedProcess
            // 
            this.SelectedProcess.DisplayMember = "External";
            this.SelectedProcess.FormattingEnabled = true;
            this.SelectedProcess.Items.AddRange(new object[] {
            "External",
            "SDC"});
            this.SelectedProcess.Location = new System.Drawing.Point(732, 80);
            this.SelectedProcess.Name = "SelectedProcess";
            this.SelectedProcess.Size = new System.Drawing.Size(121, 21);
            this.SelectedProcess.TabIndex = 7;
            this.SelectedProcess.ValueMember = "External";
            this.SelectedProcess.SelectedIndexChanged += new System.EventHandler(this.SelectedProcess_SelectedIndexChanged);
            // 
            // Externalbtn
            // 
            this.Externalbtn.Location = new System.Drawing.Point(653, 136);
            this.Externalbtn.Name = "Externalbtn";
            this.Externalbtn.Size = new System.Drawing.Size(75, 23);
            this.Externalbtn.TabIndex = 8;
            this.Externalbtn.Text = "Externalbtn";
            this.Externalbtn.UseVisualStyleBackColor = true;
            this.Externalbtn.Click += new System.EventHandler(this.Externalbtn_Click);
            // 
            // RecordID
            // 
            this.RecordID.DataPropertyName = "RecordID";
            this.RecordID.HeaderText = "RecordID";
            this.RecordID.Name = "RecordID";
            this.RecordID.ReadOnly = true;
            this.RecordID.Visible = false;
            // 
            // DateCreated
            // 
            this.DateCreated.DataPropertyName = "DateCreated";
            this.DateCreated.HeaderText = "DateCreated";
            this.DateCreated.Name = "DateCreated";
            this.DateCreated.ReadOnly = true;
            // 
            // SectionID
            // 
            this.SectionID.DataPropertyName = "SectionID";
            this.SectionID.HeaderText = "Section in charge";
            this.SectionID.Name = "SectionID";
            this.SectionID.ReadOnly = true;
            // 
            // ModelNo
            // 
            this.ModelNo.DataPropertyName = "ModelNo";
            this.ModelNo.HeaderText = "Model No/ Part no.";
            this.ModelNo.Name = "ModelNo";
            this.ModelNo.ReadOnly = true;
            // 
            // LotNo
            // 
            this.LotNo.DataPropertyName = "LotNo";
            this.LotNo.HeaderText = "Lot No.";
            this.LotNo.Name = "LotNo";
            this.LotNo.ReadOnly = true;
            // 
            // NGQty
            // 
            this.NGQty.DataPropertyName = "NGQty";
            this.NGQty.HeaderText = "NG Qty";
            this.NGQty.Name = "NGQty";
            this.NGQty.ReadOnly = true;
            // 
            // Details
            // 
            this.Details.DataPropertyName = "Details";
            this.Details.HeaderText = "Details";
            this.Details.Name = "Details";
            this.Details.ReadOnly = true;
            // 
            // RegNo
            // 
            this.RegNo.DataPropertyName = "RegNo";
            this.RegNo.HeaderText = "RegNo";
            this.RegNo.Name = "RegNo";
            this.RegNo.ReadOnly = true;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "CustomerName";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // CCtype
            // 
            this.CCtype.DataPropertyName = "CCtype";
            this.CCtype.HeaderText = "CCtype";
            this.CCtype.Name = "CCtype";
            this.CCtype.ReadOnly = true;
            this.CCtype.Visible = false;
            // 
            // Edit
            // 
            this.Edit.DataPropertyName = "Edit";
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = ((System.Drawing.Image)(resources.GetObject("Edit.Image")));
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            // 
            // Delete
            // 
            this.Delete.DataPropertyName = "Delete";
            this.Delete.HeaderText = "Delete";
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            // 
            // Customer_Complaint_user
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Externalbtn);
            this.Controls.Add(this.SelectedProcess);
            this.Controls.Add(this.OpenCC);
            this.Controls.Add(this.CustSummaryGrid);
            this.Controls.Add(this.CustomDatagrid);
            this.Controls.Add(this.projectitle);
            this.Name = "Customer_Complaint_user";
            this.Size = new System.Drawing.Size(1349, 766);
            this.Load += new System.EventHandler(this.Customer_Complaint_user_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CustomDatagrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustSummaryGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label projectitle;
        public System.Windows.Forms.DataGridView CustomDatagrid;
        public System.Windows.Forms.DataGridView CustSummaryGrid;
        private System.Windows.Forms.Button OpenCC;
        private System.Windows.Forms.ComboBox SelectedProcess;
        private System.Windows.Forms.Button Externalbtn;
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
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
    }
}
