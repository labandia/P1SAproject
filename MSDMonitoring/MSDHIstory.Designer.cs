namespace MSDMonitoring
{
    partial class MSDHIstory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MSDHIstory));
            this.ReelText = new System.Windows.Forms.Label();
            this.MonitorTable = new System.Windows.Forms.DataGridView();
            this.Exitbtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MonitorTable)).BeginInit();
            this.SuspendLayout();
            // 
            // ReelText
            // 
            this.ReelText.AutoSize = true;
            this.ReelText.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReelText.Location = new System.Drawing.Point(33, 54);
            this.ReelText.Name = "ReelText";
            this.ReelText.Size = new System.Drawing.Size(511, 28);
            this.ReelText.TabIndex = 81;
            this.ReelText.Text = " MOISTURE SENSITIVE DEVICES MONITORING";
            // 
            // MonitorTable
            // 
            this.MonitorTable.AllowUserToAddRows = false;
            this.MonitorTable.AllowUserToDeleteRows = false;
            this.MonitorTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MonitorTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.MonitorTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MonitorTable.EnableHeadersVisualStyles = false;
            this.MonitorTable.Location = new System.Drawing.Point(47, 116);
            this.MonitorTable.Name = "MonitorTable";
            this.MonitorTable.ReadOnly = true;
            this.MonitorTable.RowHeadersVisible = false;
            this.MonitorTable.Size = new System.Drawing.Size(1304, 567);
            this.MonitorTable.TabIndex = 82;
            // 
            // Exitbtn
            // 
            this.Exitbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Exitbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(23)))), ((int)(((byte)(30)))));
            this.Exitbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exitbtn.FlatAppearance.BorderSize = 0;
            this.Exitbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exitbtn.Image = ((System.Drawing.Image)(resources.GetObject("Exitbtn.Image")));
            this.Exitbtn.Location = new System.Drawing.Point(1296, 49);
            this.Exitbtn.Name = "Exitbtn";
            this.Exitbtn.Size = new System.Drawing.Size(55, 46);
            this.Exitbtn.TabIndex = 83;
            this.Exitbtn.UseVisualStyleBackColor = false;
            this.Exitbtn.Click += new System.EventHandler(this.Exitbtn_Click);
            // 
            // MSDHIstory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1418, 737);
            this.ControlBox = false;
            this.Controls.Add(this.Exitbtn);
            this.Controls.Add(this.MonitorTable);
            this.Controls.Add(this.ReelText);
            this.Name = "MSDHIstory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MSDHIstory";
            this.Load += new System.EventHandler(this.MSDHIstory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MonitorTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ReelText;
        private System.Windows.Forms.DataGridView MonitorTable;
        private System.Windows.Forms.Button Exitbtn;
    }
}