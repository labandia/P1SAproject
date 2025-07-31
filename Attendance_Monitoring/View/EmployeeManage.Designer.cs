namespace Attendance_Monitoring.View
{
    partial class EmployeeManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeManage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.addbtn = new System.Windows.Forms.Button();
            this.Removebtn = new System.Windows.Forms.Button();
            this.exportbtn = new System.Windows.Forms.Button();
            this.backbtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.searchbox = new System.Windows.Forms.TextBox();
            this.DisplayTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Employeetable = new System.Windows.Forms.DataGridView();
            this.EmployeeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fullname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Affiliation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.DepartmentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Employeetable)).BeginInit();
            this.SuspendLayout();
            // 
            // addbtn
            // 
            this.addbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(62)))), ((int)(((byte)(185)))));
            this.addbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(62)))), ((int)(((byte)(185)))));
            this.addbtn.FlatAppearance.BorderSize = 0;
            this.addbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.addbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(50)))), ((int)(((byte)(141)))));
            this.addbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addbtn.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addbtn.ForeColor = System.Drawing.Color.White;
            this.addbtn.Image = ((System.Drawing.Image)(resources.GetObject("addbtn.Image")));
            this.addbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addbtn.Location = new System.Drawing.Point(884, 68);
            this.addbtn.Name = "addbtn";
            this.addbtn.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.addbtn.Size = new System.Drawing.Size(165, 42);
            this.addbtn.TabIndex = 3;
            this.addbtn.Text = "Add Employee";
            this.addbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addbtn.UseVisualStyleBackColor = false;
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // Removebtn
            // 
            this.Removebtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Removebtn.BackColor = System.Drawing.Color.White;
            this.Removebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Removebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Removebtn.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.Removebtn.Location = new System.Drawing.Point(476, 64);
            this.Removebtn.Name = "Removebtn";
            this.Removebtn.Size = new System.Drawing.Size(173, 42);
            this.Removebtn.TabIndex = 4;
            this.Removebtn.Text = "Remove Employee";
            this.Removebtn.UseVisualStyleBackColor = false;
            this.Removebtn.Visible = false;
            this.Removebtn.Click += new System.EventHandler(this.Removebtn_Click);
            // 
            // exportbtn
            // 
            this.exportbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportbtn.BackColor = System.Drawing.Color.White;
            this.exportbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exportbtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.exportbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportbtn.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportbtn.Image = ((System.Drawing.Image)(resources.GetObject("exportbtn.Image")));
            this.exportbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exportbtn.Location = new System.Drawing.Point(1032, 181);
            this.exportbtn.Name = "exportbtn";
            this.exportbtn.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.exportbtn.Size = new System.Drawing.Size(103, 44);
            this.exportbtn.TabIndex = 5;
            this.exportbtn.Text = "Export";
            this.exportbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.exportbtn.UseVisualStyleBackColor = false;
            this.exportbtn.Click += new System.EventHandler(this.exportbtn_Click);
            // 
            // backbtn
            // 
            this.backbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.backbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backbtn.Image = ((System.Drawing.Image)(resources.GetObject("backbtn.Image")));
            this.backbtn.Location = new System.Drawing.Point(1071, 68);
            this.backbtn.Name = "backbtn";
            this.backbtn.Size = new System.Drawing.Size(52, 42);
            this.backbtn.TabIndex = 6;
            this.backbtn.UseVisualStyleBackColor = true;
            this.backbtn.Click += new System.EventHandler(this.backbtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(13)))), ((int)(((byte)(37)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.backbtn);
            this.panel1.Controls.Add(this.addbtn);
            this.panel1.Controls.Add(this.Removebtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1178, 158);
            this.panel1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(725, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 42);
            this.button1.TabIndex = 11;
            this.button1.Text = "Import";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(49, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 36);
            this.label2.TabIndex = 10;
            this.label2.Text = "Manage Employee";
            // 
            // searchbox
            // 
            this.searchbox.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchbox.Location = new System.Drawing.Point(42, 181);
            this.searchbox.Multiline = true;
            this.searchbox.Name = "searchbox";
            this.searchbox.Size = new System.Drawing.Size(247, 39);
            this.searchbox.TabIndex = 10;
            this.searchbox.TextChanged += new System.EventHandler(this.searchbox_TextChanged);
            // 
            // DisplayTotal
            // 
            this.DisplayTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DisplayTotal.AutoSize = true;
            this.DisplayTotal.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayTotal.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.DisplayTotal.Location = new System.Drawing.Point(38, 599);
            this.DisplayTotal.Name = "DisplayTotal";
            this.DisplayTotal.Size = new System.Drawing.Size(148, 19);
            this.DisplayTotal.TabIndex = 13;
            this.DisplayTotal.Text = "Total Attendence: ";
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(298, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 36);
            this.label1.TabIndex = 14;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Molding",
            "Press",
            "Rotor",
            "Winding",
            "Circuit",
            "Process Control"});
            this.comboBox1.Location = new System.Drawing.Point(350, 184);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(166, 29);
            this.comboBox1.TabIndex = 15;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // Employeetable
            // 
            this.Employeetable.AllowUserToAddRows = false;
            this.Employeetable.AllowUserToDeleteRows = false;
            this.Employeetable.AllowUserToResizeColumns = false;
            this.Employeetable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Employeetable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Employeetable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Employeetable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Employeetable.BackgroundColor = System.Drawing.Color.White;
            this.Employeetable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Employeetable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Employeetable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Employeetable.ColumnHeadersHeight = 35;
            this.Employeetable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Employeetable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmployeeID,
            this.Fullname,
            this.Process,
            this.Affiliation,
            this.Edit,
            this.Delete,
            this.DepartmentID});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Employeetable.DefaultCellStyle = dataGridViewCellStyle3;
            this.Employeetable.EnableHeadersVisualStyles = false;
            this.Employeetable.Location = new System.Drawing.Point(42, 240);
            this.Employeetable.Name = "Employeetable";
            this.Employeetable.ReadOnly = true;
            this.Employeetable.RowHeadersVisible = false;
            this.Employeetable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Employeetable.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.Employeetable.RowTemplate.Height = 30;
            this.Employeetable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Employeetable.Size = new System.Drawing.Size(1093, 340);
            this.Employeetable.TabIndex = 0;
            this.Employeetable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Employeetable_CellClick);
            // 
            // EmployeeID
            // 
            this.EmployeeID.DataPropertyName = "EmployeeID";
            this.EmployeeID.FillWeight = 78.53725F;
            this.EmployeeID.HeaderText = "EmployeeID";
            this.EmployeeID.Name = "EmployeeID";
            this.EmployeeID.ReadOnly = true;
            // 
            // Fullname
            // 
            this.Fullname.DataPropertyName = "FullName";
            this.Fullname.FillWeight = 78.53725F;
            this.Fullname.HeaderText = "Fullname";
            this.Fullname.Name = "Fullname";
            this.Fullname.ReadOnly = true;
            // 
            // Process
            // 
            this.Process.DataPropertyName = "Process";
            this.Process.FillWeight = 78.53725F;
            this.Process.HeaderText = "Process";
            this.Process.Name = "Process";
            this.Process.ReadOnly = true;
            // 
            // Affiliation
            // 
            this.Affiliation.DataPropertyName = "Affiliation";
            this.Affiliation.FillWeight = 78.53725F;
            this.Affiliation.HeaderText = "Affiliation";
            this.Affiliation.Name = "Affiliation";
            this.Affiliation.ReadOnly = true;
            // 
            // Edit
            // 
            this.Edit.FillWeight = 30F;
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = ((System.Drawing.Image)(resources.GetObject("Edit.Image")));
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Edit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Delete
            // 
            this.Delete.FillWeight = 30F;
            this.Delete.HeaderText = "Delete";
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DepartmentID
            // 
            this.DepartmentID.DataPropertyName = "Department_ID";
            this.DepartmentID.HeaderText = "DepartmentID";
            this.DepartmentID.Name = "DepartmentID";
            this.DepartmentID.ReadOnly = true;
            this.DepartmentID.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // EmployeeManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 633);
            this.ControlBox = false;
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DisplayTotal);
            this.Controls.Add(this.searchbox);
            this.Controls.Add(this.exportbtn);
            this.Controls.Add(this.Employeetable);
            this.Controls.Add(this.panel1);
            this.Name = "EmployeeManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmployeeManage";
            this.Load += new System.EventHandler(this.EmployeeManage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Employeetable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button addbtn;
        private System.Windows.Forms.Button Removebtn;
        private System.Windows.Forms.Button exportbtn;
        private System.Windows.Forms.Button backbtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox searchbox;
        private System.Windows.Forms.Label DisplayTotal;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView Employeetable;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process;
        private System.Windows.Forms.DataGridViewTextBoxColumn Affiliation;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}