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
            this.SuspendLayout();
            // 
            // projectitle
            // 
            this.projectitle.AutoSize = true;
            this.projectitle.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectitle.ForeColor = System.Drawing.Color.Black;
            this.projectitle.Location = new System.Drawing.Point(85, 75);
            this.projectitle.Name = "projectitle";
            this.projectitle.Size = new System.Drawing.Size(77, 28);
            this.projectitle.TabIndex = 4;
            this.projectitle.Text = "NCR  ";
            // 
            // NCR_control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.projectitle);
            this.Name = "NCR_control";
            this.Size = new System.Drawing.Size(1304, 674);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label projectitle;
    }
}
