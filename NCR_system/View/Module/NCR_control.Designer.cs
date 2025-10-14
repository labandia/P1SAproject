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
            this.projectitle = new System.Windows.Forms.Label();
            this.NCRSummary = new System.Windows.Forms.DataGridView();
            this.NCRGrid = new System.Windows.Forms.DataGridView();
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
            this.NCRSummary.Location = new System.Drawing.Point(672, 151);
            this.NCRSummary.Name = "NCRSummary";
            this.NCRSummary.ReadOnly = true;
            this.NCRSummary.Size = new System.Drawing.Size(585, 308);
            this.NCRSummary.TabIndex = 9;
            // 
            // NCRGrid
            // 
            this.NCRGrid.AllowUserToAddRows = false;
            this.NCRGrid.AllowUserToDeleteRows = false;
            this.NCRGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NCRGrid.Location = new System.Drawing.Point(47, 151);
            this.NCRGrid.Name = "NCRGrid";
            this.NCRGrid.ReadOnly = true;
            this.NCRGrid.Size = new System.Drawing.Size(585, 373);
            this.NCRGrid.TabIndex = 8;
            this.NCRGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.NCRGrid_CellFormatting);
            // 
            // NCR_control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NCRSummary);
            this.Controls.Add(this.NCRGrid);
            this.Controls.Add(this.projectitle);
            this.Name = "NCR_control";
            this.Size = new System.Drawing.Size(1304, 674);
            ((System.ComponentModel.ISupportInitialize)(this.NCRSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NCRGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label projectitle;
        private System.Windows.Forms.DataGridView NCRSummary;
        public System.Windows.Forms.DataGridView NCRGrid;
    }
}
