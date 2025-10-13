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
            this.projectitle = new System.Windows.Forms.Label();
            this.CustomDatagrid = new System.Windows.Forms.DataGridView();
            this.CustSummaryGrid = new System.Windows.Forms.DataGridView();
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
            this.CustomDatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustomDatagrid.Location = new System.Drawing.Point(48, 191);
            this.CustomDatagrid.Name = "CustomDatagrid";
            this.CustomDatagrid.ReadOnly = true;
            this.CustomDatagrid.Size = new System.Drawing.Size(1233, 489);
            this.CustomDatagrid.TabIndex = 4;
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
            // Customer_Complaint_user
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CustSummaryGrid);
            this.Controls.Add(this.CustomDatagrid);
            this.Controls.Add(this.projectitle);
            this.Name = "Customer_Complaint_user";
            this.Size = new System.Drawing.Size(1349, 766);
            ((System.ComponentModel.ISupportInitialize)(this.CustomDatagrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustSummaryGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label projectitle;
        public System.Windows.Forms.DataGridView CustomDatagrid;
        public System.Windows.Forms.DataGridView CustSummaryGrid;
    }
}
