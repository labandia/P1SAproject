namespace Parts_locator
{
    partial class Form2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.attendancetable)).BeginInit();
            this.SuspendLayout();
            // 
            // attendancetable
            // 
            this.attendancetable.AllowUserToAddRows = false;
            this.attendancetable.AllowUserToDeleteRows = false;
            this.attendancetable.AllowUserToResizeColumns = false;
            this.attendancetable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.attendancetable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.attendancetable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attendancetable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.attendancetable.BackgroundColor = System.Drawing.Color.White;
            this.attendancetable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.attendancetable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.attendancetable.ColumnHeadersHeight = 35;
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
            this.LateTime,
            this.Edit,
            this.Delete});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.attendancetable.DefaultCellStyle = dataGridViewCellStyle3;
            this.attendancetable.Location = new System.Drawing.Point(39, 60);
            this.attendancetable.Name = "attendancetable";
            this.attendancetable.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.attendancetable.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.attendancetable.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attendancetable.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.attendancetable.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.attendancetable.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.attendancetable.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.attendancetable.RowTemplate.Height = 40;
            this.attendancetable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.attendancetable.Size = new System.Drawing.Size(1101, 351);
            this.attendancetable.TabIndex = 18;
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
            this.Employee_ID.Visible = false;
            // 
            // FullName
            // 
            this.FullName.DataPropertyName = "FullName";
            this.FullName.HeaderText = "Employee Name";
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            this.FullName.Visible = false;
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
            // Edit
            // 
            this.Edit.DataPropertyName = "Edit";
            this.Edit.HeaderText = "Edit";
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            // 
            // Delete
            // 
            this.Delete.DataPropertyName = "Delete";
            this.Delete.HeaderText = "Delete";
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 749);
            this.Controls.Add(this.attendancetable);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.attendancetable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView attendancetable;
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
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
    }
}