namespace NCR_system.View.Module
{
    partial class Rejected
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle51 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle59 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle60 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle52 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle53 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle54 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle55 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle56 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle57 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle58 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rejected));
            this.OpenReject = new System.Windows.Forms.Button();
            this.RejectedGrid = new System.Windows.Forms.DataGridView();
            this.RecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateIssued = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssueGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Contents = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCloseReg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.sectionfilter = new System.Windows.Forms.ComboBox();
            this.filteritems = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.Circuitval = new System.Windows.Forms.Label();
            this.windingval = new System.Windows.Forms.Label();
            this.Rotorval = new System.Windows.Forms.Label();
            this.Pressval = new System.Windows.Forms.Label();
            this.moldval = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.MoldLabel = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RejectedGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenReject
            // 
            this.OpenReject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenReject.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.OpenReject.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight;
            this.OpenReject.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.OpenReject.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.OpenReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenReject.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.OpenReject.ForeColor = System.Drawing.Color.White;
            this.OpenReject.Location = new System.Drawing.Point(1148, 296);
            this.OpenReject.Name = "OpenReject";
            this.OpenReject.Size = new System.Drawing.Size(136, 32);
            this.OpenReject.TabIndex = 11;
            this.OpenReject.Text = "OpenReject";
            this.OpenReject.UseVisualStyleBackColor = false;
            this.OpenReject.Click += new System.EventHandler(this.OpenReject_Click);
            // 
            // RejectedGrid
            // 
            this.RejectedGrid.AllowUserToAddRows = false;
            this.RejectedGrid.AllowUserToDeleteRows = false;
            this.RejectedGrid.AllowUserToResizeColumns = false;
            this.RejectedGrid.AllowUserToResizeRows = false;
            this.RejectedGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RejectedGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridViewCellStyle51.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle51.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle51.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle51.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle51.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle51.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle51.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RejectedGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle51;
            this.RejectedGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RejectedGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecordID,
            this.RegNo,
            this.DateIssued,
            this.IssueGroup,
            this.SectionID,
            this.ModelNo,
            this.Quantity,
            this.Contents,
            this.DateCloseReg,
            this.Status,
            this.Process,
            this.Edit,
            this.Delete});
            dataGridViewCellStyle59.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle59.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle59.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            dataGridViewCellStyle59.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle59.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle59.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle59.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle59.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RejectedGrid.DefaultCellStyle = dataGridViewCellStyle59;
            this.RejectedGrid.Location = new System.Drawing.Point(36, 361);
            this.RejectedGrid.Name = "RejectedGrid";
            this.RejectedGrid.ReadOnly = true;
            dataGridViewCellStyle60.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle60.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle60.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle60.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle60.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle60.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle60.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RejectedGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle60;
            this.RejectedGrid.RowHeadersVisible = false;
            this.RejectedGrid.RowTemplate.Height = 30;
            this.RejectedGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RejectedGrid.Size = new System.Drawing.Size(1275, 395);
            this.RejectedGrid.TabIndex = 12;
            this.RejectedGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RejectedGrid_CellClick);
            this.RejectedGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.RejectedGrid_CellFormatting_1);
            // 
            // RecordID
            // 
            this.RecordID.DataPropertyName = "RecordID";
            this.RecordID.HeaderText = "RecordID";
            this.RecordID.Name = "RecordID";
            this.RecordID.ReadOnly = true;
            this.RecordID.Visible = false;
            this.RecordID.Width = 72;
            // 
            // RegNo
            // 
            this.RegNo.DataPropertyName = "RegNo";
            dataGridViewCellStyle52.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegNo.DefaultCellStyle = dataGridViewCellStyle52;
            this.RegNo.HeaderText = "Registration No.";
            this.RegNo.Name = "RegNo";
            this.RegNo.ReadOnly = true;
            this.RegNo.Width = 119;
            // 
            // DateIssued
            // 
            this.DateIssued.DataPropertyName = "DateIssued";
            dataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DateIssued.DefaultCellStyle = dataGridViewCellStyle53;
            this.DateIssued.HeaderText = "Date Issued";
            this.DateIssued.Name = "DateIssued";
            this.DateIssued.ReadOnly = true;
            this.DateIssued.Width = 97;
            // 
            // IssueGroup
            // 
            this.IssueGroup.DataPropertyName = "IssueGroup";
            dataGridViewCellStyle54.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IssueGroup.DefaultCellStyle = dataGridViewCellStyle54;
            this.IssueGroup.HeaderText = "Issuing\nSection ";
            this.IssueGroup.Name = "IssueGroup";
            this.IssueGroup.ReadOnly = true;
            this.IssueGroup.Width = 85;
            // 
            // SectionID
            // 
            this.SectionID.DataPropertyName = "SectionID";
            dataGridViewCellStyle55.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SectionID.DefaultCellStyle = dataGridViewCellStyle55;
            this.SectionID.HeaderText = "Main In-charge";
            this.SectionID.Name = "SectionID";
            this.SectionID.ReadOnly = true;
            this.SectionID.Width = 114;
            // 
            // ModelNo
            // 
            this.ModelNo.DataPropertyName = "ModelNo";
            this.ModelNo.HeaderText = "Model No. / Part No.";
            this.ModelNo.Name = "ModelNo";
            this.ModelNo.ReadOnly = true;
            this.ModelNo.Width = 103;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle56.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle56;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 89;
            // 
            // Contents
            // 
            this.Contents.DataPropertyName = "Contents";
            this.Contents.HeaderText = "Contents";
            this.Contents.Name = "Contents";
            this.Contents.ReadOnly = true;
            this.Contents.Width = 91;
            // 
            // DateCloseReg
            // 
            this.DateCloseReg.DataPropertyName = "DateCloseReg";
            dataGridViewCellStyle57.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DateCloseReg.DefaultCellStyle = dataGridViewCellStyle57;
            this.DateCloseReg.HeaderText = "Date Registered";
            this.DateCloseReg.Name = "DateCloseReg";
            this.DateCloseReg.ReadOnly = true;
            this.DateCloseReg.Width = 117;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            dataGridViewCellStyle58.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Status.DefaultCellStyle = dataGridViewCellStyle58;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 76;
            // 
            // Process
            // 
            this.Process.DataPropertyName = "Process";
            this.Process.HeaderText = "Process";
            this.Process.Name = "Process";
            this.Process.ReadOnly = true;
            this.Process.Visible = false;
            this.Process.Width = 83;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = ((System.Drawing.Image)(resources.GetObject("Edit.Image")));
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Width = 45;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete";
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Width = 58;
            // 
            // sectionfilter
            // 
            this.sectionfilter.DropDownHeight = 200;
            this.sectionfilter.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sectionfilter.FormattingEnabled = true;
            this.sectionfilter.IntegralHeight = false;
            this.sectionfilter.ItemHeight = 15;
            this.sectionfilter.Items.AddRange(new object[] {
            "-- Filter Section --",
            "Molding",
            "Press",
            "Rotor",
            "Winding",
            "Circuit"});
            this.sectionfilter.Location = new System.Drawing.Point(40, 308);
            this.sectionfilter.MaxDropDownItems = 30;
            this.sectionfilter.Name = "sectionfilter";
            this.sectionfilter.Size = new System.Drawing.Size(153, 23);
            this.sectionfilter.TabIndex = 20;
            this.sectionfilter.SelectedIndexChanged += new System.EventHandler(this.sectionfilter_SelectedIndexChanged);
            // 
            // filteritems
            // 
            this.filteritems.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.filteritems.FormattingEnabled = true;
            this.filteritems.Items.AddRange(new object[] {
            "-- Filter Status --",
            "Open",
            "Close"});
            this.filteritems.Location = new System.Drawing.Point(211, 308);
            this.filteritems.Name = "filteritems";
            this.filteritems.Size = new System.Drawing.Size(182, 23);
            this.filteritems.TabIndex = 21;
            this.filteritems.SelectedIndexChanged += new System.EventHandler(this.filteritems_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(71)))));
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.filteritems);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.OpenReject);
            this.panel1.Controls.Add(this.sectionfilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1352, 355);
            this.panel1.TabIndex = 33;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(36)))), ((int)(((byte)(62)))));
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Location = new System.Drawing.Point(40, 25);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1066, 246);
            this.panel5.TabIndex = 37;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.cartesianChart1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 56);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.panel6.Size = new System.Drawing.Size(1066, 190);
            this.panel6.TabIndex = 1;
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartesianChart1.Location = new System.Drawing.Point(30, 20);
            this.cartesianChart1.Margin = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(1006, 150);
            this.cartesianChart1.TabIndex = 2;
            this.cartesianChart1.Text = "QuantityChart";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(76)))));
            this.panel7.Controls.Add(this.label7);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1066, 56);
            this.panel7.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(20, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(200, 21);
            this.label7.TabIndex = 14;
            this.label7.Text = "Numbers Cases By Status";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label9.Location = new System.Drawing.Point(208, 285);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 15);
            this.label9.TabIndex = 31;
            this.label9.Text = "Filter by Open/Close";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(37, 285);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 29;
            this.label6.Text = "Filter by Section";
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.Controls.Add(this.Circuitval);
            this.panel9.Controls.Add(this.windingval);
            this.panel9.Controls.Add(this.Rotorval);
            this.panel9.Controls.Add(this.Pressval);
            this.panel9.Controls.Add(this.moldval);
            this.panel9.Controls.Add(this.label11);
            this.panel9.Controls.Add(this.MoldLabel);
            this.panel9.Controls.Add(this.button5);
            this.panel9.Controls.Add(this.button1);
            this.panel9.Controls.Add(this.label12);
            this.panel9.Controls.Add(this.button2);
            this.panel9.Controls.Add(this.button4);
            this.panel9.Controls.Add(this.label13);
            this.panel9.Controls.Add(this.label14);
            this.panel9.Controls.Add(this.button3);
            this.panel9.Location = new System.Drawing.Point(1112, 25);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(172, 246);
            this.panel9.TabIndex = 38;
            // 
            // Circuitval
            // 
            this.Circuitval.AutoSize = true;
            this.Circuitval.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Circuitval.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Circuitval.Location = new System.Drawing.Point(47, 196);
            this.Circuitval.Name = "Circuitval";
            this.Circuitval.Size = new System.Drawing.Size(19, 21);
            this.Circuitval.TabIndex = 49;
            this.Circuitval.Text = "0";
            // 
            // windingval
            // 
            this.windingval.AutoSize = true;
            this.windingval.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windingval.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.windingval.Location = new System.Drawing.Point(122, 131);
            this.windingval.Name = "windingval";
            this.windingval.Size = new System.Drawing.Size(19, 21);
            this.windingval.TabIndex = 48;
            this.windingval.Text = "0";
            // 
            // Rotorval
            // 
            this.Rotorval.AutoSize = true;
            this.Rotorval.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rotorval.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Rotorval.Location = new System.Drawing.Point(47, 132);
            this.Rotorval.Name = "Rotorval";
            this.Rotorval.Size = new System.Drawing.Size(19, 21);
            this.Rotorval.TabIndex = 47;
            this.Rotorval.Text = "0";
            // 
            // Pressval
            // 
            this.Pressval.AutoSize = true;
            this.Pressval.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pressval.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Pressval.Location = new System.Drawing.Point(122, 65);
            this.Pressval.Name = "Pressval";
            this.Pressval.Size = new System.Drawing.Size(19, 21);
            this.Pressval.TabIndex = 46;
            this.Pressval.Text = "0";
            // 
            // moldval
            // 
            this.moldval.AutoSize = true;
            this.moldval.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moldval.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.moldval.Location = new System.Drawing.Point(47, 67);
            this.moldval.Name = "moldval";
            this.moldval.Size = new System.Drawing.Size(19, 21);
            this.moldval.TabIndex = 45;
            this.moldval.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label11.Location = new System.Drawing.Point(20, 176);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 15);
            this.label11.TabIndex = 44;
            this.label11.Text = "Circuit";
            // 
            // MoldLabel
            // 
            this.MoldLabel.AutoSize = true;
            this.MoldLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MoldLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.MoldLabel.Location = new System.Drawing.Point(20, 41);
            this.MoldLabel.Name = "MoldLabel";
            this.MoldLabel.Size = new System.Drawing.Size(52, 15);
            this.MoldLabel.TabIndex = 36;
            this.MoldLabel.Text = "Molding";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Aqua;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Location = new System.Drawing.Point(23, 201);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(15, 13);
            this.button5.TabIndex = 43;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DodgerBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(23, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(15, 13);
            this.button1.TabIndex = 35;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label12.Location = new System.Drawing.Point(95, 110);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 15);
            this.label12.TabIndex = 42;
            this.label12.Text = "Winding";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(98, 73);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(15, 13);
            this.button2.TabIndex = 37;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Yellow;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(98, 137);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(15, 13);
            this.button4.TabIndex = 41;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label13.Location = new System.Drawing.Point(95, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 15);
            this.label13.TabIndex = 38;
            this.label13.Text = "Press";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label14.Location = new System.Drawing.Point(20, 110);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(39, 15);
            this.label14.TabIndex = 40;
            this.label14.Text = "Rotor";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(23, 137);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(15, 13);
            this.button3.TabIndex = 39;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // Rejected
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RejectedGrid);
            this.Name = "Rejected";
            this.Size = new System.Drawing.Size(1352, 786);
            this.Load += new System.EventHandler(this.Rejected_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RejectedGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button OpenReject;
        private System.Windows.Forms.DataGridView RejectedGrid;
        private System.Windows.Forms.ComboBox sectionfilter;
        private System.Windows.Forms.ComboBox filteritems;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateIssued;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssueGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Contents;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCloseReg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label Circuitval;
        private System.Windows.Forms.Label windingval;
        private System.Windows.Forms.Label Rotorval;
        private System.Windows.Forms.Label Pressval;
        private System.Windows.Forms.Label moldval;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label MoldLabel;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button3;
    }
}
