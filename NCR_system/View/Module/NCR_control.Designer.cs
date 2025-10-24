namespace NCR_system.View.Module
{
    partial class NCR_control
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NCR_control));
            this.projectitle = new System.Windows.Forms.Label();
            this.NCRSummary = new System.Windows.Forms.DataGridView();
            this.NCRGrid = new System.Windows.Forms.DataGridView();
            this.OpenReject = new System.Windows.Forms.Button();
            this.SelectedProcess = new System.Windows.Forms.ComboBox();
            this.CountDisplay = new System.Windows.Forms.Label();
            this.RecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateIssued = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssuedGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Contents = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateRegist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TargetDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePath = new System.Windows.Forms.DataGridViewLinkColumn();
            this.DateCloseReg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CircularStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.NCRSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NCRGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // projectitle
            // 
            this.projectitle.AutoSize = true;
            this.projectitle.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectitle.ForeColor = System.Drawing.Color.Black;
            this.projectitle.Location = new System.Drawing.Point(67, 61);
            this.projectitle.Name = "projectitle";
            this.projectitle.Size = new System.Drawing.Size(77, 28);
            this.projectitle.TabIndex = 4;
            this.projectitle.Text = "NCR  ";
            // 
            // NCRSummary
            // 
            this.NCRSummary.AllowUserToAddRows = false;
            this.NCRSummary.AllowUserToDeleteRows = false;
            this.NCRSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NCRSummary.Location = new System.Drawing.Point(890, 13);
            this.NCRSummary.Name = "NCRSummary";
            this.NCRSummary.ReadOnly = true;
            this.NCRSummary.Size = new System.Drawing.Size(369, 163);
            this.NCRSummary.TabIndex = 9;
            // 
            // NCRGrid
            // 
            this.NCRGrid.AllowUserToAddRows = false;
            this.NCRGrid.AllowUserToDeleteRows = false;
            this.NCRGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NCRGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecordID,
            this.DateIssued,
            this.RegNo,
            this.IssuedGroup,
            this.SectionID,
            this.ModelNo,
            this.Quantity,
            this.Contents,
            this.DateRegist,
            this.Status,
            this.TargetDate,
            this.FilePath,
            this.DateCloseReg,
            this.CircularStatus,
            this.Process,
            this.Edit,
            this.Delete});
            this.NCRGrid.Location = new System.Drawing.Point(49, 210);
            this.NCRGrid.Name = "NCRGrid";
            this.NCRGrid.ReadOnly = true;
            this.NCRGrid.Size = new System.Drawing.Size(1210, 373);
            this.NCRGrid.TabIndex = 8;
            this.NCRGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.NCRGrid_CellContentClick);
            this.NCRGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.NCRGrid_CellFormatting);
            // 
            // OpenReject
            // 
            this.OpenReject.Location = new System.Drawing.Point(510, 68);
            this.OpenReject.Name = "OpenReject";
            this.OpenReject.Size = new System.Drawing.Size(125, 37);
            this.OpenReject.TabIndex = 12;
            this.OpenReject.Text = "Open NCR";
            this.OpenReject.UseVisualStyleBackColor = true;
            // 
            // SelectedProcess
            // 
            this.SelectedProcess.DisplayMember = "External";
            this.SelectedProcess.FormattingEnabled = true;
            this.SelectedProcess.Items.AddRange(new object[] {
            "Main Registration",
            "Recurrence"});
            this.SelectedProcess.Location = new System.Drawing.Point(722, 68);
            this.SelectedProcess.Name = "SelectedProcess";
            this.SelectedProcess.Size = new System.Drawing.Size(121, 21);
            this.SelectedProcess.TabIndex = 13;
            this.SelectedProcess.ValueMember = "External";
            this.SelectedProcess.SelectedIndexChanged += new System.EventHandler(this.SelectedProcess_SelectedIndexChanged);
            // 
            // CountDisplay
            // 
            this.CountDisplay.AutoSize = true;
            this.CountDisplay.Location = new System.Drawing.Point(55, 603);
            this.CountDisplay.Name = "CountDisplay";
            this.CountDisplay.Size = new System.Drawing.Size(35, 13);
            this.CountDisplay.TabIndex = 14;
            this.CountDisplay.Text = "label1";
            // 
            // RecordID
            // 
            this.RecordID.DataPropertyName = "RecordID";
            this.RecordID.HeaderText = "RecordID";
            this.RecordID.Name = "RecordID";
            this.RecordID.ReadOnly = true;
            this.RecordID.Visible = false;
            // 
            // DateIssued
            // 
            this.DateIssued.DataPropertyName = "DateIssued";
            this.DateIssued.HeaderText = "DateIssued";
            this.DateIssued.Name = "DateIssued";
            this.DateIssued.ReadOnly = true;
            // 
            // RegNo
            // 
            this.RegNo.DataPropertyName = "RegNo";
            this.RegNo.HeaderText = "Reg No.";
            this.RegNo.Name = "RegNo";
            this.RegNo.ReadOnly = true;
            // 
            // IssuedGroup
            // 
            this.IssuedGroup.DataPropertyName = "IssuedGroup";
            this.IssuedGroup.HeaderText = "Issuing Section / Group";
            this.IssuedGroup.Name = "IssuedGroup";
            this.IssuedGroup.ReadOnly = true;
            // 
            // SectionID
            // 
            this.SectionID.DataPropertyName = "SectionID";
            this.SectionID.HeaderText = "Main In-charge";
            this.SectionID.Name = "SectionID";
            this.SectionID.ReadOnly = true;
            // 
            // ModelNo
            // 
            this.ModelNo.DataPropertyName = "ModelNo";
            this.ModelNo.HeaderText = "Model No. /Part No.";
            this.ModelNo.Name = "ModelNo";
            this.ModelNo.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // Contents
            // 
            this.Contents.DataPropertyName = "Contents";
            this.Contents.HeaderText = "Contents";
            this.Contents.Name = "Contents";
            this.Contents.ReadOnly = true;
            // 
            // DateRegist
            // 
            this.DateRegist.DataPropertyName = "DateRegist";
            this.DateRegist.HeaderText = "Date Registered";
            this.DateRegist.Name = "DateRegist";
            this.DateRegist.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Report Ok";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // TargetDate
            // 
            this.TargetDate.DataPropertyName = "TargetDate";
            this.TargetDate.HeaderText = "Target Date to be closed";
            this.TargetDate.Name = "TargetDate";
            this.TargetDate.ReadOnly = true;
            // 
            // FilePath
            // 
            this.FilePath.DataPropertyName = "FilePath";
            this.FilePath.HeaderText = "FilePath";
            this.FilePath.Name = "FilePath";
            this.FilePath.ReadOnly = true;
            // 
            // DateCloseReg
            // 
            this.DateCloseReg.DataPropertyName = "DateCloseReg";
            this.DateCloseReg.HeaderText = "Date Closed";
            this.DateCloseReg.Name = "DateCloseReg";
            this.DateCloseReg.ReadOnly = true;
            // 
            // CircularStatus
            // 
            this.CircularStatus.DataPropertyName = "CircularStatus";
            this.CircularStatus.HeaderText = "For Circulation Status";
            this.CircularStatus.Name = "CircularStatus";
            this.CircularStatus.ReadOnly = true;
            // 
            // Process
            // 
            this.Process.DataPropertyName = "Process";
            this.Process.HeaderText = "Process";
            this.Process.Name = "Process";
            this.Process.ReadOnly = true;
            this.Process.Visible = false;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = ((System.Drawing.Image)(resources.GetObject("Edit.Image")));
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete";
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            // 
            // NCR_control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CountDisplay);
            this.Controls.Add(this.SelectedProcess);
            this.Controls.Add(this.OpenReject);
            this.Controls.Add(this.NCRSummary);
            this.Controls.Add(this.NCRGrid);
            this.Controls.Add(this.projectitle);
            this.Name = "NCR_control";
            this.Size = new System.Drawing.Size(1304, 674);
            this.Load += new System.EventHandler(this.NCR_control_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NCRSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NCRGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label projectitle;
        private System.Windows.Forms.DataGridView NCRSummary;
        public System.Windows.Forms.DataGridView NCRGrid;
        private System.Windows.Forms.Button OpenReject;
        private System.Windows.Forms.ComboBox SelectedProcess;
        private System.Windows.Forms.Label CountDisplay;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateIssued;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssuedGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Contents;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateRegist;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn TargetDate;
        private System.Windows.Forms.DataGridViewLinkColumn FilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCloseReg;
        private System.Windows.Forms.DataGridViewTextBoxColumn CircularStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
    }
}
