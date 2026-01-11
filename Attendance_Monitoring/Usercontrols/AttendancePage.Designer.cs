namespace Attendance_Monitoring.Usercontrols
{
    partial class AttendancePage
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttendancePage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Timeclock = new System.Windows.Forms.Label();
            this.DisplayTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.selecttime = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Statustext = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TextName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.EmployID = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.attendancetable = new System.Windows.Forms.DataGridView();
            this.RecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date_today = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Employee_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Regular = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Overtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Shifts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dstart = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dend = new System.Windows.Forms.DateTimePicker();
            this.shiftselect = new System.Windows.Forms.ComboBox();
            this.exportbtn = new System.Windows.Forms.Button();
            this.Filterbtn = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attendancetable)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Transparent;
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(1043, 622);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 39);
            this.label5.TabIndex = 24;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Transparent;
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(261, 218);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 39);
            this.label6.TabIndex = 23;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Timeclock
            // 
            this.Timeclock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Timeclock.AutoSize = true;
            this.Timeclock.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Timeclock.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Timeclock.Location = new System.Drawing.Point(1088, 633);
            this.Timeclock.Name = "Timeclock";
            this.Timeclock.Size = new System.Drawing.Size(46, 20);
            this.Timeclock.TabIndex = 22;
            this.Timeclock.Text = "Clock";
            this.Timeclock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DisplayTotal
            // 
            this.DisplayTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DisplayTotal.AutoSize = true;
            this.DisplayTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayTotal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DisplayTotal.Location = new System.Drawing.Point(950, 48);
            this.DisplayTotal.Name = "DisplayTotal";
            this.DisplayTotal.Size = new System.Drawing.Size(132, 20);
            this.DisplayTotal.TabIndex = 21;
            this.DisplayTotal.Text = "Total Attendence: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(-4, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Time IN /  OUT: ";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.selecttime);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Location = new System.Drawing.Point(850, 3);
            this.panel5.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(261, 75);
            this.panel5.TabIndex = 3;
            // 
            // selecttime
            // 
            this.selecttime.AllowDrop = true;
            this.selecttime.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.selecttime.DropDownHeight = 200;
            this.selecttime.DropDownWidth = 174;
            this.selecttime.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selecttime.FormattingEnabled = true;
            this.selecttime.IntegralHeight = false;
            this.selecttime.ItemHeight = 25;
            this.selecttime.Items.AddRange(new object[] {
            "Time In\t",
            "Time out"});
            this.selecttime.Location = new System.Drawing.Point(0, 42);
            this.selecttime.Name = "selecttime";
            this.selecttime.Size = new System.Drawing.Size(261, 33);
            this.selecttime.TabIndex = 3;
            this.selecttime.SelectedIndexChanged += new System.EventHandler(this.Selecttime_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(-4, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Status: ";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.Statustext);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(570, 3);
            this.panel4.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(260, 75);
            this.panel4.TabIndex = 2;
            // 
            // Statustext
            // 
            this.Statustext.BackColor = System.Drawing.Color.White;
            this.Statustext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Statustext.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Statustext.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Statustext.ForeColor = System.Drawing.Color.White;
            this.Statustext.Location = new System.Drawing.Point(0, 42);
            this.Statustext.Name = "Statustext";
            this.Statustext.Size = new System.Drawing.Size(260, 33);
            this.Statustext.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(-4, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Employee Name : ";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.TextName);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(290, 3);
            this.panel3.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(260, 75);
            this.panel3.TabIndex = 1;
            // 
            // TextName
            // 
            this.TextName.BackColor = System.Drawing.Color.White;
            this.TextName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextName.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextName.ForeColor = System.Drawing.Color.White;
            this.TextName.Location = new System.Drawing.Point(0, 42);
            this.TextName.Margin = new System.Windows.Forms.Padding(10);
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(260, 33);
            this.TextName.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(-3, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Enter ID number  : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(40, 222);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(211, 27);
            this.textBox2.TabIndex = 19;
            this.textBox2.TextChanged += new System.EventHandler(this.Searchinput);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.EmployID);
            this.panel2.Location = new System.Drawing.Point(10, 3);
            this.panel2.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 75);
            this.panel2.TabIndex = 0;
            // 
            // EmployID
            // 
            this.EmployID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.EmployID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmployID.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.EmployID.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmployID.ForeColor = System.Drawing.Color.Black;
            this.EmployID.Location = new System.Drawing.Point(0, 29);
            this.EmployID.Name = "EmployID";
            this.EmployID.Size = new System.Drawing.Size(260, 46);
            this.EmployID.TabIndex = 2;
            this.EmployID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EnterTime);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(30, 84);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1121, 81);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // attendancetable
            // 
            this.attendancetable.AllowUserToAddRows = false;
            this.attendancetable.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.attendancetable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.attendancetable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attendancetable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.attendancetable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.attendancetable.BackgroundColor = System.Drawing.Color.White;
            this.attendancetable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.attendancetable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.attendancetable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.attendancetable.ColumnHeadersHeight = 35;
            this.attendancetable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.attendancetable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecordID,
            this.Date_today,
            this.Employee_ID,
            this.FullName,
            this.TimeOut,
            this.Regular,
            this.Gtotal,
            this.Overtime,
            this.Shifts,
            this.LateTime});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.attendancetable.DefaultCellStyle = dataGridViewCellStyle3;
            this.attendancetable.EnableHeadersVisualStyles = false;
            this.attendancetable.Location = new System.Drawing.Point(40, 264);
            this.attendancetable.Name = "attendancetable";
            this.attendancetable.ReadOnly = true;
            this.attendancetable.RowHeadersVisible = false;
            this.attendancetable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.attendancetable.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.attendancetable.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.attendancetable.RowTemplate.Height = 30;
            this.attendancetable.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.attendancetable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.attendancetable.Size = new System.Drawing.Size(1101, 351);
            this.attendancetable.TabIndex = 17;
            this.attendancetable.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Attendancetable_CellFormatting);
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
            this.Date_today.HeaderText = "Date/Time In";
            this.Date_today.Name = "Date_today";
            this.Date_today.ReadOnly = true;
            // 
            // Employee_ID
            // 
            this.Employee_ID.DataPropertyName = "Employee_ID";
            this.Employee_ID.HeaderText = "ID number";
            this.Employee_ID.Name = "Employee_ID";
            this.Employee_ID.ReadOnly = true;
            // 
            // FullName
            // 
            this.FullName.DataPropertyName = "FullName";
            this.FullName.HeaderText = "Employee Name";
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            // 
            // TimeOut
            // 
            this.TimeOut.DataPropertyName = "TimeOut";
            this.TimeOut.HeaderText = "TimeOut";
            this.TimeOut.Name = "TimeOut";
            this.TimeOut.ReadOnly = true;
            this.TimeOut.Visible = false;
            // 
            // Regular
            // 
            this.Regular.DataPropertyName = "Regular";
            this.Regular.HeaderText = "Regular";
            this.Regular.Name = "Regular";
            this.Regular.ReadOnly = true;
            this.Regular.Visible = false;
            // 
            // Gtotal
            // 
            this.Gtotal.DataPropertyName = "Gtotal";
            this.Gtotal.HeaderText = "Gtotal";
            this.Gtotal.Name = "Gtotal";
            this.Gtotal.ReadOnly = true;
            this.Gtotal.Visible = false;
            // 
            // Overtime
            // 
            this.Overtime.DataPropertyName = "Overtime";
            this.Overtime.HeaderText = "Overtime";
            this.Overtime.Name = "Overtime";
            this.Overtime.ReadOnly = true;
            this.Overtime.Visible = false;
            // 
            // Shifts
            // 
            this.Shifts.DataPropertyName = "Shifts";
            this.Shifts.HeaderText = "Shifts";
            this.Shifts.Name = "Shifts";
            this.Shifts.ReadOnly = true;
            // 
            // LateTime
            // 
            this.LateTime.DataPropertyName = "LateTime";
            this.LateTime.HeaderText = "Late Time";
            this.LateTime.Name = "LateTime";
            this.LateTime.ReadOnly = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(32, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(208, 32);
            this.label7.TabIndex = 25;
            this.label7.Text = "Daily Attendance ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(38, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(296, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Scan your employee ID barcode or type your ID manually";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(40, 198);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(160, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Search Employee or Fullname";
            // 
            // dstart
            // 
            this.dstart.CustomFormat = "yyyy-MM-dd";
            this.dstart.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dstart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dstart.Location = new System.Drawing.Point(306, 222);
            this.dstart.Name = "dstart";
            this.dstart.Size = new System.Drawing.Size(149, 27);
            this.dstart.TabIndex = 31;
            this.dstart.Value = new System.DateTime(2024, 8, 27, 0, 0, 0, 0);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(469, 195);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 15);
            this.label10.TabIndex = 30;
            this.label10.Text = "End Date";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(303, 195);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 15);
            this.label11.TabIndex = 29;
            this.label11.Text = "Start Date: ";
            // 
            // dend
            // 
            this.dend.CustomFormat = "yyyy-MM-dd";
            this.dend.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dend.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dend.Location = new System.Drawing.Point(472, 220);
            this.dend.Name = "dend";
            this.dend.Size = new System.Drawing.Size(149, 26);
            this.dend.TabIndex = 28;
            this.dend.Value = new System.DateTime(2024, 8, 27, 0, 0, 0, 0);
            // 
            // shiftselect
            // 
            this.shiftselect.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shiftselect.FormattingEnabled = true;
            this.shiftselect.Items.AddRange(new object[] {
            "ALL",
            "DAYSHIFT",
            "NIGHTSHIFT"});
            this.shiftselect.Location = new System.Drawing.Point(644, 218);
            this.shiftselect.Name = "shiftselect";
            this.shiftselect.Size = new System.Drawing.Size(120, 28);
            this.shiftselect.TabIndex = 34;
            // 
            // exportbtn
            // 
            this.exportbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportbtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportbtn.Image = ((System.Drawing.Image)(resources.GetObject("exportbtn.Image")));
            this.exportbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exportbtn.Location = new System.Drawing.Point(1043, 214);
            this.exportbtn.Name = "exportbtn";
            this.exportbtn.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.exportbtn.Size = new System.Drawing.Size(98, 34);
            this.exportbtn.TabIndex = 33;
            this.exportbtn.Text = "Export";
            this.exportbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.exportbtn.UseVisualStyleBackColor = true;
            // 
            // Filterbtn
            // 
            this.Filterbtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Filterbtn.ForeColor = System.Drawing.Color.Black;
            this.Filterbtn.Image = ((System.Drawing.Image)(resources.GetObject("Filterbtn.Image")));
            this.Filterbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Filterbtn.Location = new System.Drawing.Point(784, 218);
            this.Filterbtn.Name = "Filterbtn";
            this.Filterbtn.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Filterbtn.Size = new System.Drawing.Size(92, 28);
            this.Filterbtn.TabIndex = 32;
            this.Filterbtn.Text = "Load";
            this.Filterbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Filterbtn.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(641, 195);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 15);
            this.label12.TabIndex = 35;
            this.label12.Text = "filter by Shift";
            // 
            // AttendancePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label12);
            this.Controls.Add(this.shiftselect);
            this.Controls.Add(this.exportbtn);
            this.Controls.Add(this.Filterbtn);
            this.Controls.Add(this.dstart);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dend);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Timeclock);
            this.Controls.Add(this.DisplayTotal);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.attendancetable);
            this.Name = "AttendancePage";
            this.Size = new System.Drawing.Size(1194, 683);
            this.Load += new System.EventHandler(this.AttendancePage_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.attendancetable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Timeclock;
        private System.Windows.Forms.Label DisplayTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox selecttime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label Statustext;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label TextName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox EmployID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView attendancetable;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date_today;
        private System.Windows.Forms.DataGridViewTextBoxColumn Employee_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn Regular;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Overtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Shifts;
        private System.Windows.Forms.DataGridViewTextBoxColumn LateTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dstart;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dend;
        private System.Windows.Forms.ComboBox shiftselect;
        private System.Windows.Forms.Button exportbtn;
        private System.Windows.Forms.Button Filterbtn;
        private System.Windows.Forms.Label label12;
    }
}
