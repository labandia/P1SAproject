namespace FootWristStrapsAnalysis
{
    partial class FolderPathInputDialog
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
            this.button4 = new System.Windows.Forms.Button();
            this.btnOpenfolder = new System.Windows.Forms.Button();
            this.folderText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.button4.FlatAppearance.BorderSize = 2;
            this.button4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(46, 175);
            this.button4.Name = "button4";
            this.button4.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.button4.Size = new System.Drawing.Size(460, 51);
            this.button4.TabIndex = 108;
            this.button4.Text = "Save the path";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnOpenfolder
            // 
            this.btnOpenfolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenfolder.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnOpenfolder.FlatAppearance.BorderSize = 2;
            this.btnOpenfolder.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenfolder.Location = new System.Drawing.Point(404, 54);
            this.btnOpenfolder.Name = "btnOpenfolder";
            this.btnOpenfolder.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnOpenfolder.Size = new System.Drawing.Size(102, 29);
            this.btnOpenfolder.TabIndex = 107;
            this.btnOpenfolder.Text = "Select folder";
            this.btnOpenfolder.UseVisualStyleBackColor = true;
            this.btnOpenfolder.Click += new System.EventHandler(this.btnOpenfolder_Click);
            // 
            // folderText
            // 
            this.folderText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.folderText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.folderText.Location = new System.Drawing.Point(46, 115);
            this.folderText.Name = "folderText";
            this.folderText.Size = new System.Drawing.Size(460, 25);
            this.folderText.TabIndex = 106;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(43, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 17);
            this.label2.TabIndex = 105;
            this.label2.Text = "Folder Path Doesnt Exists : ";
            // 
            // FolderPathInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 277);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnOpenfolder);
            this.Controls.Add(this.folderText);
            this.Controls.Add(this.label2);
            this.Name = "FolderPathInputDialog";
            this.Text = "-";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnOpenfolder;
        private System.Windows.Forms.TextBox folderText;
        private System.Windows.Forms.Label label2;
    }
}