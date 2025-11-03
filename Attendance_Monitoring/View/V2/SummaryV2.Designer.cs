namespace Attendance_Monitoring.View.V2
{
    partial class SummaryV2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SummaryV2));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dstart = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dend = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.exportbtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.shifts = new System.Windows.Forms.ComboBox();
            this.searchbox = new System.Windows.Forms.TextBox();
            this.Filterbtn = new System.Windows.Forms.Button();
            this.summarytable = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date_today = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Employee_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Regular = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Overtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShiftsTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.summarytable)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dstart
            // 
            this.dstart.CustomFormat = "yyyy-MM-dd";
            this.dstart.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dstart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dstart.Location = new System.Drawing.Point(41, 193);
            this.dstart.Name = "dstart";
            this.dstart.Size = new System.Drawing.Size(149, 26);
            this.dstart.TabIndex = 30;
            this.dstart.Value = new System.DateTime(2024, 8, 27, 0, 0, 0, 0);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Transparent;
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(904, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 36);
            this.label6.TabIndex = 29;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dend
            // 
            this.dend.CustomFormat = "yyyy-MM-dd";
            this.dend.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dend.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dend.Location = new System.Drawing.Point(224, 193);
            this.dend.Name = "dend";
            this.dend.Size = new System.Drawing.Size(149, 26);
            this.dend.TabIndex = 21;
            this.dend.Value = new System.DateTime(2024, 8, 27, 0, 0, 0, 0);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(1073, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 46);
            this.button1.TabIndex = 12;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(22, 616);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 18);
            this.label4.TabIndex = 28;
            this.label4.Text = "Total Records: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(221, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "End date (yyyy-mm-dd):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(38, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 16);
            this.label2.TabIndex = 26;
            this.label2.Text = "Start date (yyyy/mm/dd):";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(649, 189);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.button2.Size = new System.Drawing.Size(94, 34);
            this.button2.TabIndex = 25;
            this.button2.Text = "Reset";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // exportbtn
            // 
            this.exportbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportbtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportbtn.Image = ((System.Drawing.Image)(resources.GetObject("exportbtn.Image")));
            this.exportbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exportbtn.Location = new System.Drawing.Point(800, 189);
            this.exportbtn.Name = "exportbtn";
            this.exportbtn.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.exportbtn.Size = new System.Drawing.Size(98, 34);
            this.exportbtn.TabIndex = 24;
            this.exportbtn.Text = "Export";
            this.exportbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.exportbtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(13)))), ((int)(((byte)(37)))));
            this.label1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(37, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(489, 28);
            this.label1.TabIndex = 5;
            this.label1.Text = "Attendance summary of time IN and OUT";
            // 
            // shifts
            // 
            this.shifts.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shifts.FormattingEnabled = true;
            this.shifts.Items.AddRange(new object[] {
            "DAYSHIFT",
            "NIGHTSHIFT"});
            this.shifts.Location = new System.Drawing.Point(394, 193);
            this.shifts.Name = "shifts";
            this.shifts.Size = new System.Drawing.Size(120, 28);
            this.shifts.TabIndex = 31;
            // 
            // searchbox
            // 
            this.searchbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchbox.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchbox.Location = new System.Drawing.Point(951, 190);
            this.searchbox.Multiline = true;
            this.searchbox.Name = "searchbox";
            this.searchbox.Size = new System.Drawing.Size(177, 34);
            this.searchbox.TabIndex = 23;
            // 
            // Filterbtn
            // 
            this.Filterbtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Filterbtn.ForeColor = System.Drawing.Color.Black;
            this.Filterbtn.Image = ((System.Drawing.Image)(resources.GetObject("Filterbtn.Image")));
            this.Filterbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Filterbtn.Location = new System.Drawing.Point(533, 189);
            this.Filterbtn.Name = "Filterbtn";
            this.Filterbtn.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Filterbtn.Size = new System.Drawing.Size(92, 34);
            this.Filterbtn.TabIndex = 22;
            this.Filterbtn.Text = "Load";
            this.Filterbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Filterbtn.UseVisualStyleBackColor = true;
            // 
            // summarytable
            // 
            this.summarytable.AllowUserToAddRows = false;
            this.summarytable.AllowUserToDeleteRows = false;
            this.summarytable.AllowUserToResizeColumns = false;
            this.summarytable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.summarytable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.summarytable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.summarytable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.summarytable.BackgroundColor = System.Drawing.Color.White;
            this.summarytable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.summarytable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.summarytable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.summarytable.ColumnHeadersHeight = 35;
            this.summarytable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.summarytable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecordID,
            this.Date_today,
            this.Employee_ID,
            this.FullName,
            this.TimeOut,
            this.Regular,
            this.Overtime,
            this.Gtotal,
            this.ShiftsTime,
            this.LateTime,
            this.Action});
            this.summarytable.EnableHeadersVisualStyles = false;
            this.summarytable.Location = new System.Drawing.Point(42, 251);
            this.summarytable.Name = "summarytable";
            this.summarytable.ReadOnly = true;
            this.summarytable.RowHeadersVisible = false;
            this.summarytable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.summarytable.RowTemplate.Height = 30;
            this.summarytable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.summarytable.Size = new System.Drawing.Size(1086, 294);
            this.summarytable.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(13)))), ((int)(((byte)(37)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1178, 128);
            this.panel1.TabIndex = 32;
            // 
            // RecordID
            // 
            this.RecordID.DataPropertyName = "RecordID";
            this.RecordID.HeaderText = "RecordID";
            this.RecordID.Name = "RecordID";
            this.RecordID.ReadOnly = true;
            this.RecordID.Visible = false;
            // 
            // Date_today
            // 
            this.Date_today.DataPropertyName = "TimeIn";
            this.Date_today.HeaderText = "Date/ Time";
            this.Date_today.Name = "Date_today";
            this.Date_today.ReadOnly = true;
            // 
            // Employee_ID
            // 
            this.Employee_ID.DataPropertyName = "Employee_ID";
            this.Employee_ID.HeaderText = "Employee ID";
            this.Employee_ID.Name = "Employee_ID";
            this.Employee_ID.ReadOnly = true;
            // 
            // FullName
            // 
            this.FullName.DataPropertyName = "FullName";
            this.FullName.HeaderText = "Full Name";
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            // 
            // TimeOut
            // 
            this.TimeOut.DataPropertyName = "TimeOut";
            this.TimeOut.HeaderText = "Time OUT";
            this.TimeOut.Name = "TimeOut";
            this.TimeOut.ReadOnly = true;
            // 
            // Regular
            // 
            this.Regular.DataPropertyName = "Regular";
            this.Regular.HeaderText = "Regular Time";
            this.Regular.Name = "Regular";
            this.Regular.ReadOnly = true;
            // 
            // Overtime
            // 
            this.Overtime.DataPropertyName = "Overtime";
            this.Overtime.HeaderText = "Overtime";
            this.Overtime.Name = "Overtime";
            this.Overtime.ReadOnly = true;
            // 
            // Gtotal
            // 
            this.Gtotal.DataPropertyName = "Gtotal";
            this.Gtotal.HeaderText = "Total";
            this.Gtotal.Name = "Gtotal";
            this.Gtotal.ReadOnly = true;
            // 
            // ShiftsTime
            // 
            this.ShiftsTime.DataPropertyName = "Shifts";
            this.ShiftsTime.HeaderText = "Shifts";
            this.ShiftsTime.Name = "ShiftsTime";
            this.ShiftsTime.ReadOnly = true;
            // 
            // LateTime
            // 
            this.LateTime.DataPropertyName = "LateTime";
            this.LateTime.HeaderText = "Time Late";
            this.LateTime.Name = "LateTime";
            this.LateTime.ReadOnly = true;
            // 
            // Action
            // 
            this.Action.DataPropertyName = "Action";
            this.Action.HeaderText = "Action";
            this.Action.Name = "Action";
            this.Action.ReadOnly = true;
            this.Action.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // SummaryV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 644);
            this.Controls.Add(this.dstart);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dend);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.exportbtn);
            this.Controls.Add(this.shifts);
            this.Controls.Add(this.searchbox);
            this.Controls.Add(this.Filterbtn);
            this.Controls.Add(this.summarytable);
            this.Controls.Add(this.panel1);
            this.Name = "SummaryV2";
            this.Text = "SummaryV2";
            this.Load += new System.EventHandler(this.SummaryV2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.summarytable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dstart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dend;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button exportbtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox shifts;
        private System.Windows.Forms.TextBox searchbox;
        private System.Windows.Forms.Button Filterbtn;
        private System.Windows.Forms.DataGridView summarytable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date_today;
        private System.Windows.Forms.DataGridViewTextBoxColumn Employee_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn Regular;
        private System.Windows.Forms.DataGridViewTextBoxColumn Overtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShiftsTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn LateTime;
        private System.Windows.Forms.DataGridViewImageColumn Action;
    }
}