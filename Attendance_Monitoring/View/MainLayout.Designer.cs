namespace Attendance_Monitoring.View
{
    partial class MainLayout
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.CRmonitorMenu = new System.Windows.Forms.Button();
            this.AttendanceMenu = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Employbtn = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.Employbtn);
            this.panel2.Controls.Add(this.CRmonitorMenu);
            this.panel2.Controls.Add(this.AttendanceMenu);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(64, 644);
            this.panel2.TabIndex = 0;
            // 
            // CRmonitorMenu
            // 
            this.CRmonitorMenu.Location = new System.Drawing.Point(3, 147);
            this.CRmonitorMenu.Name = "CRmonitorMenu";
            this.CRmonitorMenu.Size = new System.Drawing.Size(58, 23);
            this.CRmonitorMenu.TabIndex = 1;
            this.CRmonitorMenu.Text = "CR";
            this.CRmonitorMenu.UseVisualStyleBackColor = true;
            this.CRmonitorMenu.Click += new System.EventHandler(this.CRmonitorMenu_Click);
            // 
            // AttendanceMenu
            // 
            this.AttendanceMenu.Location = new System.Drawing.Point(0, 100);
            this.AttendanceMenu.Name = "AttendanceMenu";
            this.AttendanceMenu.Size = new System.Drawing.Size(64, 23);
            this.AttendanceMenu.TabIndex = 0;
            this.AttendanceMenu.Text = "Attend";
            this.AttendanceMenu.UseVisualStyleBackColor = true;
            this.AttendanceMenu.Click += new System.EventHandler(this.AttendanceMenu_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(64, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1114, 644);
            this.panel1.TabIndex = 1;
            // 
            // Employbtn
            // 
            this.Employbtn.Location = new System.Drawing.Point(3, 194);
            this.Employbtn.Name = "Employbtn";
            this.Employbtn.Size = new System.Drawing.Size(58, 23);
            this.Employbtn.TabIndex = 2;
            this.Employbtn.Text = "Employee";
            this.Employbtn.UseVisualStyleBackColor = true;
            // 
            // MainLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 644);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "MainLayout";
            this.Text = "MainLayout";
            this.Load += new System.EventHandler(this.MainLayout_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button CRmonitorMenu;
        private System.Windows.Forms.Button AttendanceMenu;
        private System.Windows.Forms.Panel panel1;
        private Usercontrols.AttendancePage attendancePage1;
        private System.Windows.Forms.Button Employbtn;
    }
}