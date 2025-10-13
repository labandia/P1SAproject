namespace NCR_system.View.Module
{
    partial class Rejected
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
            ((System.ComponentModel.ISupportInitialize)(this.SummaryRejected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RejectedGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // projectitle
            // 
            this.projectitle.AutoSize = true;
            this.projectitle.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectitle.ForeColor = System.Drawing.Color.Black;
            this.projectitle.Location = new System.Drawing.Point(50, 51);
            this.projectitle.Name = "projectitle";
            this.projectitle.Size = new System.Drawing.Size(115, 28);
            this.projectitle.TabIndex = 5;
            this.projectitle.Text = "Rejected";
            // 
            // SummaryRejected
            // 
            this.SummaryRejected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SummaryRejected.Location = new System.Drawing.Point(835, 133);
            this.SummaryRejected.Name = "SummaryRejected";
            this.SummaryRejected.Size = new System.Drawing.Size(320, 318);
            this.SummaryRejected.TabIndex = 7;
            // 
            // RejectedGrid
            // 
            this.RejectedGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RejectedGrid.Location = new System.Drawing.Point(46, 133);
            this.RejectedGrid.Name = "RejectedGrid";
            this.RejectedGrid.Size = new System.Drawing.Size(726, 318);
            this.RejectedGrid.TabIndex = 6;
            this.RejectedGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.RejectedGrid_CellFormatting);
            // 
            // Rejected
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SummaryRejected);
            this.Controls.Add(this.RejectedGrid);
            this.Controls.Add(this.projectitle);
            this.Name = "Rejected";
            this.Size = new System.Drawing.Size(1197, 700);
            ((System.ComponentModel.ISupportInitialize)(this.SummaryRejected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RejectedGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label projectitle;
        private System.Windows.Forms.DataGridView SummaryRejected;
        private System.Windows.Forms.DataGridView RejectedGrid;
    }
}
