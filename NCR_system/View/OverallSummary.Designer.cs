namespace NCR_system.View
{
    partial class OverallSummary
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
            this.label2 = new System.Windows.Forms.Label();
            this.chartContent = new System.Windows.Forms.Panel();
            this.overbtn = new System.Windows.Forms.Button();
            this.NCRMenu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(389, 30);
            this.label2.TabIndex = 36;
            this.label2.Text = "P1SA NON CONFORMITY/ OVERVIEW";
            // 
            // chartContent
            // 
            this.chartContent.Location = new System.Drawing.Point(1, 133);
            this.chartContent.Name = "chartContent";
            this.chartContent.Size = new System.Drawing.Size(1347, 617);
            this.chartContent.TabIndex = 47;
            // 
            // overbtn
            // 
            this.overbtn.Location = new System.Drawing.Point(30, 82);
            this.overbtn.Name = "overbtn";
            this.overbtn.Size = new System.Drawing.Size(75, 23);
            this.overbtn.TabIndex = 48;
            this.overbtn.Text = "OVERVIEW";
            this.overbtn.UseVisualStyleBackColor = true;
            this.overbtn.Click += new System.EventHandler(this.overbtn_Click);
            // 
            // NCRMenu
            // 
            this.NCRMenu.Location = new System.Drawing.Point(124, 82);
            this.NCRMenu.Name = "NCRMenu";
            this.NCRMenu.Size = new System.Drawing.Size(182, 23);
            this.NCRMenu.TabIndex = 49;
            this.NCRMenu.Text = "NCR MAIN REGISTRATION";
            this.NCRMenu.UseVisualStyleBackColor = true;
            this.NCRMenu.Click += new System.EventHandler(this.NCRMenu_Click);
            // 
            // OverallSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 747);
            this.Controls.Add(this.NCRMenu);
            this.Controls.Add(this.overbtn);
            this.Controls.Add(this.chartContent);
            this.Controls.Add(this.label2);
            this.Name = "OverallSummary";
            this.Text = "OverallSummary";
            this.Load += new System.EventHandler(this.OverallSummary_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel chartContent;
        private System.Windows.Forms.Button overbtn;
        private System.Windows.Forms.Button NCRMenu;
    }
}