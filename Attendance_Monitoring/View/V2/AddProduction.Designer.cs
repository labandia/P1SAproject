namespace Attendance_Monitoring.View.V2
{
    partial class AddProduction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProduction));
            this.label9 = new System.Windows.Forms.Label();
            this.Name_error = new System.Windows.Forms.Label();
            this.Emp_error = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.selectsection = new System.Windows.Forms.ComboBox();
            this.Affili = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.process = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Fullname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.EmpID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Cancebtn = new System.Windows.Forms.Button();
            this.Savebtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(238, 482);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(178, 16);
            this.label9.TabIndex = 31;
            this.label9.Text = "Select a department section *";
            this.label9.Visible = false;
            // 
            // Name_error
            // 
            this.Name_error.AutoSize = true;
            this.Name_error.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.Name_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Name_error.Location = new System.Drawing.Point(238, 211);
            this.Name_error.Name = "Name_error";
            this.Name_error.Size = new System.Drawing.Size(177, 16);
            this.Name_error.TabIndex = 30;
            this.Name_error.Text = "Employee Name is required *";
            this.Name_error.Visible = false;
            // 
            // Emp_error
            // 
            this.Emp_error.AutoSize = true;
            this.Emp_error.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.Emp_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Emp_error.Location = new System.Drawing.Point(276, 123);
            this.Emp_error.Name = "Emp_error";
            this.Emp_error.Size = new System.Drawing.Size(139, 16);
            this.Emp_error.TabIndex = 29;
            this.Emp_error.Text = "ID number is required *";
            this.Emp_error.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(41, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(203, 28);
            this.label6.TabIndex = 28;
            this.label6.Text = "Add Employees ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(42, 482);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 16);
            this.label5.TabIndex = 27;
            this.label5.Text = "Select Section :";
            // 
            // selectsection
            // 
            this.selectsection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.selectsection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectsection.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectsection.FormattingEnabled = true;
            this.selectsection.IntegralHeight = false;
            this.selectsection.Items.AddRange(new object[] {
            "Molding",
            "Press",
            "Rotor",
            "Winding",
            "Circuit",
            "Process Control"});
            this.selectsection.Location = new System.Drawing.Point(45, 507);
            this.selectsection.Name = "selectsection";
            this.selectsection.Size = new System.Drawing.Size(370, 36);
            this.selectsection.TabIndex = 26;
            // 
            // Affili
            // 
            this.Affili.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Affili.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Affili.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Affili.Location = new System.Drawing.Point(45, 413);
            this.Affili.Multiline = true;
            this.Affili.Name = "Affili";
            this.Affili.Size = new System.Drawing.Size(370, 37);
            this.Affili.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(42, 388);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 24;
            this.label4.Text = "Affiliation";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // process
            // 
            this.process.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.process.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.process.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.process.Location = new System.Drawing.Point(45, 320);
            this.process.Multiline = true;
            this.process.Name = "process";
            this.process.Size = new System.Drawing.Size(370, 37);
            this.process.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(42, 296);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 22;
            this.label3.Text = "Process : ";
            // 
            // Fullname
            // 
            this.Fullname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Fullname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Fullname.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fullname.Location = new System.Drawing.Point(45, 233);
            this.Fullname.Multiline = true;
            this.Fullname.Name = "Fullname";
            this.Fullname.Size = new System.Drawing.Size(370, 37);
            this.Fullname.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(42, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "Full Name :";
            // 
            // EmpID
            // 
            this.EmpID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.EmpID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EmpID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmpID.Location = new System.Drawing.Point(45, 147);
            this.EmpID.Multiline = true;
            this.EmpID.Name = "EmpID";
            this.EmpID.Size = new System.Drawing.Size(370, 37);
            this.EmpID.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(42, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Employee ID :";
            // 
            // Cancebtn
            // 
            this.Cancebtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancebtn.Location = new System.Drawing.Point(47, 584);
            this.Cancebtn.Name = "Cancebtn";
            this.Cancebtn.Size = new System.Drawing.Size(169, 46);
            this.Cancebtn.TabIndex = 17;
            this.Cancebtn.Text = "Cancel";
            this.Cancebtn.UseVisualStyleBackColor = true;
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
            this.Savebtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Savebtn.ForeColor = System.Drawing.Color.White;
            this.Savebtn.Image = ((System.Drawing.Image)(resources.GetObject("Savebtn.Image")));
            this.Savebtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Savebtn.Location = new System.Drawing.Point(241, 584);
            this.Savebtn.Name = "Savebtn";
            this.Savebtn.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.Savebtn.Size = new System.Drawing.Size(174, 46);
            this.Savebtn.TabIndex = 16;
            this.Savebtn.Text = "Save";
            this.Savebtn.UseVisualStyleBackColor = false;
            this.Savebtn.Click += new System.EventHandler(this.Savebtn_Click);
            // 
            // AddProduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 682);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Name_error);
            this.Controls.Add(this.Emp_error);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.selectsection);
            this.Controls.Add(this.Affili);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.process);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Fullname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EmpID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancebtn);
            this.Controls.Add(this.Savebtn);
            this.Name = "AddProduction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddProduction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label Name_error;
        private System.Windows.Forms.Label Emp_error;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox selectsection;
        private System.Windows.Forms.TextBox Affili;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox process;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Fullname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox EmpID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancebtn;
        private System.Windows.Forms.Button Savebtn;
    }
}