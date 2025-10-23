namespace NCR_system.View.Module
{
    partial class ShipRejected
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
            this.projectitle = new System.Windows.Forms.Label();
            this.SummaryRejected = new System.Windows.Forms.DataGridView();
            this.RejectedGrid = new System.Windows.Forms.DataGridView();
            this.OpenShip = new System.Windows.Forms.Button();
            this.RecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateIssued = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssueGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Contents = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCloseReg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.SummaryRejected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RejectedGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // projectitle
            // 
            this.projectitle.AutoSize = true;
            this.projectitle.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectitle.ForeColor = System.Drawing.Color.Black;
            this.projectitle.Location = new System.Drawing.Point(70, 69);
            this.projectitle.Name = "projectitle";
            this.projectitle.Size = new System.Drawing.Size(119, 28);
            this.projectitle.TabIndex = 6;
            this.projectitle.Text = "Shipment";
            // 
            // SummaryRejected
            // 
            this.SummaryRejected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SummaryRejected.Location = new System.Drawing.Point(534, 40);
            this.SummaryRejected.Name = "SummaryRejected";
            this.SummaryRejected.Size = new System.Drawing.Size(320, 252);
            this.SummaryRejected.TabIndex = 9;
            // 
            // RejectedGrid
            // 
            this.RejectedGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RejectedGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.RejectedGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RejectedGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecordID,
            this.DateIssued,
            this.IssueGroup,
            this.SectionID,
            this.ModelNo,
            this.Quantity,
            this.Contents,
            this.DateCloseReg,
            this.Status,
            this.Process,
            this.Edit,
            this.Delete});
            this.RejectedGrid.EnableHeadersVisualStyles = false;
            this.RejectedGrid.Location = new System.Drawing.Point(57, 346);
            this.RejectedGrid.Name = "RejectedGrid";
            this.RejectedGrid.RowHeadersVisible = false;
            this.RejectedGrid.Size = new System.Drawing.Size(1147, 318);
            this.RejectedGrid.TabIndex = 8;
            this.RejectedGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RejectedGrid_CellClick);
            this.RejectedGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.RejectedGrid_CellFormatting);
            // 
            // OpenShip
            // 
            this.OpenShip.Location = new System.Drawing.Point(967, 76);
            this.OpenShip.Name = "OpenShip";
            this.OpenShip.Size = new System.Drawing.Size(75, 23);
            this.OpenShip.TabIndex = 10;
            this.OpenShip.Text = "OpenShip";
            this.OpenShip.UseVisualStyleBackColor = true;
            this.OpenShip.Click += new System.EventHandler(this.OpenShip_Click);
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
            this.DateIssued.HeaderText = "Date Issued";
            this.DateIssued.Name = "DateIssued";
            this.DateIssued.ReadOnly = true;
            // 
            // IssueGroup
            // 
            this.IssueGroup.DataPropertyName = "IssueGroup";
            this.IssueGroup.HeaderText = "Issuing\nSection / Group";
            this.IssueGroup.Name = "IssueGroup";
            this.IssueGroup.ReadOnly = true;
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
            this.ModelNo.HeaderText = "Model No. / Part No.";
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
            // DateCloseReg
            // 
            this.DateCloseReg.DataPropertyName = "DateCloseReg";
            this.DateCloseReg.HeaderText = "Date Registered";
            this.DateCloseReg.Name = "DateCloseReg";
            this.DateCloseReg.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
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
            this.Edit.Name = "Edit";
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete";
            this.Delete.Name = "Delete";
            // 
            // ShipRejected
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.OpenShip);
            this.Controls.Add(this.SummaryRejected);
            this.Controls.Add(this.RejectedGrid);
            this.Controls.Add(this.projectitle);
            this.Name = "ShipRejected";
            this.Size = new System.Drawing.Size(1301, 757);
            ((System.ComponentModel.ISupportInitialize)(this.SummaryRejected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RejectedGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label projectitle;
        private System.Windows.Forms.DataGridView SummaryRejected;
        private System.Windows.Forms.DataGridView RejectedGrid;
        private System.Windows.Forms.Button OpenShip;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateIssued;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssueGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Contents;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCloseReg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
    }
}
