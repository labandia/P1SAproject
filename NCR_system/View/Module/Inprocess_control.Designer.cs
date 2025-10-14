namespace NCR_system.View.Module
{
    partial class Inprocess_control
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
            this.InprocessGrid = new System.Windows.Forms.DataGridView();
            this.SummaryInprocess = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.InprocessGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SummaryInprocess)).BeginInit();
            this.SuspendLayout();
            // 
            // projectitle
            // 
            this.projectitle.AutoSize = true;
            this.projectitle.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectitle.ForeColor = System.Drawing.Color.Black;
            this.projectitle.Location = new System.Drawing.Point(50, 58);
            this.projectitle.Name = "projectitle";
            this.projectitle.Size = new System.Drawing.Size(124, 28);
            this.projectitle.TabIndex = 5;
            this.projectitle.Text = "Inprocess";
            // 
            // InprocessGrid
            // 
            this.InprocessGrid.AllowUserToAddRows = false;
            this.InprocessGrid.AllowUserToDeleteRows = false;
            this.InprocessGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InprocessGrid.Location = new System.Drawing.Point(55, 135);
            this.InprocessGrid.Name = "InprocessGrid";
            this.InprocessGrid.ReadOnly = true;
            this.InprocessGrid.Size = new System.Drawing.Size(585, 373);
            this.InprocessGrid.TabIndex = 6;
            // 
            // SummaryInprocess
            // 
            this.SummaryInprocess.AllowUserToAddRows = false;
            this.SummaryInprocess.AllowUserToDeleteRows = false;
            this.SummaryInprocess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SummaryInprocess.Location = new System.Drawing.Point(680, 135);
            this.SummaryInprocess.Name = "SummaryInprocess";
            this.SummaryInprocess.ReadOnly = true;
            this.SummaryInprocess.Size = new System.Drawing.Size(585, 308);
            this.SummaryInprocess.TabIndex = 7;
            // 
            // Inprocess_control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SummaryInprocess);
            this.Controls.Add(this.InprocessGrid);
            this.Controls.Add(this.projectitle);
            this.Name = "Inprocess_control";
            this.Size = new System.Drawing.Size(1319, 725);
            ((System.ComponentModel.ISupportInitialize)(this.InprocessGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SummaryInprocess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label projectitle;
        private System.Windows.Forms.DataGridView SummaryInprocess;
        public System.Windows.Forms.DataGridView InprocessGrid;
    }
}
