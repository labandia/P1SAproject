namespace Attendance_Monitoring.View
{
    partial class UploadsData
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
            this.filepathText = new System.Windows.Forms.TextBox();
            this.SaveUpload = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.FileSelected = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // filepathText
            // 
            this.filepathText.Location = new System.Drawing.Point(29, 166);
            this.filepathText.Name = "filepathText";
            this.filepathText.Size = new System.Drawing.Size(387, 20);
            this.filepathText.TabIndex = 0;
            // 
            // SaveUpload
            // 
            this.SaveUpload.Location = new System.Drawing.Point(388, 310);
            this.SaveUpload.Name = "SaveUpload";
            this.SaveUpload.Size = new System.Drawing.Size(136, 23);
            this.SaveUpload.TabIndex = 1;
            this.SaveUpload.Text = "Upload Excel";
            this.SaveUpload.UseVisualStyleBackColor = true;
            this.SaveUpload.Click += new System.EventHandler(this.SaveUpload_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "-- Select Section --",
            "Molding",
            "Press",
            "Rotor",
            "Winding",
            "Circuit"});
            this.comboBox1.Location = new System.Drawing.Point(29, 312);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(312, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // FileSelected
            // 
            this.FileSelected.Location = new System.Drawing.Point(449, 163);
            this.FileSelected.Name = "FileSelected";
            this.FileSelected.Size = new System.Drawing.Size(75, 23);
            this.FileSelected.TabIndex = 3;
            this.FileSelected.Text = "Select File";
            this.FileSelected.UseVisualStyleBackColor = true;
            this.FileSelected.Click += new System.EventHandler(this.FileSelected_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "File Path :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 296);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Department section:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // UploadsData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 371);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FileSelected);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.SaveUpload);
            this.Controls.Add(this.filepathText);
            this.Name = "UploadsData";
            this.Text = "UploadsData";
            this.Load += new System.EventHandler(this.UploadsData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox filepathText;
        private System.Windows.Forms.Button SaveUpload;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button FileSelected;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}