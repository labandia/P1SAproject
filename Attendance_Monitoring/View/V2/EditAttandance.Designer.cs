namespace Attendance_Monitoring.View.V2
{
    partial class EditAttandance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditAttandance));
            this.IDText = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Fullname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Savebtn = new System.Windows.Forms.Button();
            this.TimeOutText = new System.Windows.Forms.DateTimePicker();
            this.regText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TimeInText = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.overText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.gTotalText = new System.Windows.Forms.TextBox();
            this.Cancebtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IDText
            // 
            this.IDText.AutoSize = true;
            this.IDText.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDText.Location = new System.Drawing.Point(30, 44);
            this.IDText.Name = "IDText";
            this.IDText.Size = new System.Drawing.Size(180, 28);
            this.IDText.TabIndex = 27;
            this.IDText.Text = "Employee ID : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(32, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 21;
            this.label3.Text = "Time IN :";
            // 
            // Fullname
            // 
            this.Fullname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Fullname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Fullname.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fullname.Location = new System.Drawing.Point(36, 135);
            this.Fullname.Multiline = true;
            this.Fullname.Name = "Fullname";
            this.Fullname.Size = new System.Drawing.Size(370, 37);
            this.Fullname.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(32, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "Full Name :";
            // 
            // Savebtn
            // 
            this.Savebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(62)))), ((int)(((byte)(185)))));
            this.Savebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Savebtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(62)))), ((int)(((byte)(185)))));
            this.Savebtn.FlatAppearance.BorderSize = 0;
            this.Savebtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Savebtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(50)))), ((int)(((byte)(141)))));
            this.Savebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Savebtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Savebtn.ForeColor = System.Drawing.Color.White;
            this.Savebtn.Image = ((System.Drawing.Image)(resources.GetObject("Savebtn.Image")));
            this.Savebtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Savebtn.Location = new System.Drawing.Point(218, 390);
            this.Savebtn.Name = "Savebtn";
            this.Savebtn.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.Savebtn.Size = new System.Drawing.Size(192, 46);
            this.Savebtn.TabIndex = 16;
            this.Savebtn.Text = "Save";
            this.Savebtn.UseVisualStyleBackColor = false;
            this.Savebtn.Click += new System.EventHandler(this.Savebtn_Click);
            // 
            // TimeOutText
            // 
            this.TimeOutText.CustomFormat = "hh:mm";
            this.TimeOutText.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TimeOutText.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TimeOutText.Location = new System.Drawing.Point(183, 222);
            this.TimeOutText.Name = "TimeOutText";
            this.TimeOutText.ShowUpDown = true;
            this.TimeOutText.Size = new System.Drawing.Size(99, 25);
            this.TimeOutText.TabIndex = 32;
            this.TimeOutText.Value = new System.DateTime(2026, 3, 12, 15, 30, 0, 0);
            this.TimeOutText.ValueChanged += new System.EventHandler(this.TimeOutText_ValueChanged);
            // 
            // regText
            // 
            this.regText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.regText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.regText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regText.Location = new System.Drawing.Point(36, 308);
            this.regText.Multiline = true;
            this.regText.Name = "regText";
            this.regText.ReadOnly = true;
            this.regText.Size = new System.Drawing.Size(102, 37);
            this.regText.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(180, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 17);
            this.label4.TabIndex = 23;
            this.label4.Text = "Time Out";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // TimeInText
            // 
            this.TimeInText.CustomFormat = "hh:mm";
            this.TimeInText.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.TimeInText.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TimeInText.Location = new System.Drawing.Point(35, 222);
            this.TimeInText.Name = "TimeInText";
            this.TimeInText.ShowUpDown = true;
            this.TimeInText.Size = new System.Drawing.Size(99, 25);
            this.TimeInText.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(33, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 34;
            this.label1.Text = "Regular";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(165, 278);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 36;
            this.label5.Text = "Over Time :";
            // 
            // overText
            // 
            this.overText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.overText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.overText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.overText.Location = new System.Drawing.Point(168, 308);
            this.overText.Multiline = true;
            this.overText.Name = "overText";
            this.overText.ReadOnly = true;
            this.overText.Size = new System.Drawing.Size(102, 37);
            this.overText.TabIndex = 35;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(305, 278);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 17);
            this.label7.TabIndex = 38;
            this.label7.Text = "Total :";
            // 
            // gTotalText
            // 
            this.gTotalText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gTotalText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gTotalText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gTotalText.Location = new System.Drawing.Point(308, 308);
            this.gTotalText.Multiline = true;
            this.gTotalText.Name = "gTotalText";
            this.gTotalText.ReadOnly = true;
            this.gTotalText.Size = new System.Drawing.Size(102, 37);
            this.gTotalText.TabIndex = 37;
            // 
            // Cancebtn
            // 
            this.Cancebtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancebtn.Location = new System.Drawing.Point(35, 390);
            this.Cancebtn.Name = "Cancebtn";
            this.Cancebtn.Size = new System.Drawing.Size(169, 46);
            this.Cancebtn.TabIndex = 39;
            this.Cancebtn.Text = "Cancel";
            this.Cancebtn.UseVisualStyleBackColor = true;
            // 
            // EditAttandance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 489);
            this.Controls.Add(this.Cancebtn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.gTotalText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.overText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TimeInText);
            this.Controls.Add(this.TimeOutText);
            this.Controls.Add(this.IDText);
            this.Controls.Add(this.regText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Fullname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Savebtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditAttandance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "-";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label IDText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Fullname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Savebtn;
        private System.Windows.Forms.DateTimePicker TimeOutText;
        private System.Windows.Forms.TextBox regText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker TimeInText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox overText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox gTotalText;
        private System.Windows.Forms.Button Cancebtn;
    }
}