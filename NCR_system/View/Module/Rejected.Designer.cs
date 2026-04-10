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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.sectionfilter = new System.Windows.Forms.ComboBox();
            this.filteritems = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.Circuitval = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.windingval = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.Rotorval = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.Pressval = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MoldLabel = new System.Windows.Forms.Label();
            this.moldval = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RejectedGrid)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.OpenReject.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenReject.ForeColor = System.Drawing.Color.White;
            this.OpenReject.Location = new System.Drawing.Point(923, 27);
            this.OpenReject.Name = "OpenReject";
            this.OpenReject.Size = new System.Drawing.Size(149, 36);
            this.OpenReject.TabIndex = 11;
            this.OpenReject.Text = "Add Data";
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
            this.RejectedGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.RejectedGrid.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.RejectedGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.RejectedGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RejectedGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
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
            this.Process});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RejectedGrid.DefaultCellStyle = dataGridViewCellStyle9;
            this.RejectedGrid.Location = new System.Drawing.Point(27, 85);
            this.RejectedGrid.Name = "RejectedGrid";
            this.RejectedGrid.ReadOnly = true;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RejectedGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.RejectedGrid.RowHeadersVisible = false;
            this.RejectedGrid.RowTemplate.Height = 30;
            this.RejectedGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RejectedGrid.Size = new System.Drawing.Size(1045, 616);
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
            // 
            // RegNo
            // 
            this.RegNo.DataPropertyName = "RegNo";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.RegNo.HeaderText = "Registration No.";
            this.RegNo.Name = "RegNo";
            this.RegNo.ReadOnly = true;
            // 
            // DateIssued
            // 
            this.DateIssued.DataPropertyName = "DateIssued";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DateIssued.DefaultCellStyle = dataGridViewCellStyle3;
            this.DateIssued.HeaderText = "Date Issued";
            this.DateIssued.Name = "DateIssued";
            this.DateIssued.ReadOnly = true;
            // 
            // IssueGroup
            // 
            this.IssueGroup.DataPropertyName = "IssueGroup";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IssueGroup.DefaultCellStyle = dataGridViewCellStyle4;
            this.IssueGroup.HeaderText = "Issuing\nSection ";
            this.IssueGroup.Name = "IssueGroup";
            this.IssueGroup.ReadOnly = true;
            // 
            // SectionID
            // 
            this.SectionID.DataPropertyName = "SectionID";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SectionID.DefaultCellStyle = dataGridViewCellStyle5;
            this.SectionID.HeaderText = "Main In-charge";
            this.SectionID.Name = "SectionID";
            this.SectionID.ReadOnly = true;
            // 
            // ModelNo
            // 
            this.ModelNo.DataPropertyName = "ModelNo";
            this.ModelNo.HeaderText = "Model No. / Part No.";
            this.ModelNo.Name = "ModelNo";
            this.ModelNo.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle6;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // Contents
            // 
            this.Contents.DataPropertyName = "Contents";
            this.Contents.HeaderText = "Contents";
            this.Contents.Name = "Contents";
            this.Contents.ReadOnly = true;
            // 
            // DateCloseReg
            // 
            this.DateCloseReg.DataPropertyName = "DateCloseReg";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DateCloseReg.DefaultCellStyle = dataGridViewCellStyle7;
            this.DateCloseReg.HeaderText = "Date Registered";
            this.DateCloseReg.Name = "DateCloseReg";
            this.DateCloseReg.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Status.DefaultCellStyle = dataGridViewCellStyle8;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // Process
            // 
            this.Process.DataPropertyName = "Process";
            this.Process.HeaderText = "Process";
            this.Process.Name = "Process";
            this.Process.ReadOnly = true;
            this.Process.Visible = false;
            // 
            // sectionfilter
            // 
            this.sectionfilter.DropDownHeight = 200;
            this.sectionfilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sectionfilter.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.sectionfilter.FormattingEnabled = true;
            this.sectionfilter.IntegralHeight = false;
            this.sectionfilter.ItemHeight = 17;
            this.sectionfilter.Items.AddRange(new object[] {
            "Select: ALL",
            "Select: Molding",
            "Select: Press",
            "Select: Rotor",
            "Select: Winding",
            "Select: Circuit"});
            this.sectionfilter.Location = new System.Drawing.Point(27, 34);
            this.sectionfilter.MaxDropDownItems = 30;
            this.sectionfilter.Name = "sectionfilter";
            this.sectionfilter.Size = new System.Drawing.Size(153, 25);
            this.sectionfilter.TabIndex = 20;
            this.sectionfilter.SelectedIndexChanged += new System.EventHandler(this.sectionfilter_SelectedIndexChanged);
            // 
            // filteritems
            // 
            this.filteritems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filteritems.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.filteritems.FormattingEnabled = true;
            this.filteritems.Items.AddRange(new object[] {
            "Select: Close",
            "Select: Open"});
            this.filteritems.Location = new System.Drawing.Point(211, 34);
            this.filteritems.Name = "filteritems";
            this.filteritems.Size = new System.Drawing.Size(182, 25);
            this.filteritems.TabIndex = 21;
            this.filteritems.SelectedIndexChanged += new System.EventHandler(this.filteritems_SelectedIndexChanged);
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.flowLayoutPanel1);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.panel5);
            this.panel6.Controls.Add(this.panel4);
            this.panel6.Controls.Add(this.panel3);
            this.panel6.Controls.Add(this.panel2);
            this.panel6.Controls.Add(this.panel1);
            this.panel6.Location = new System.Drawing.Point(1106, 15);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(233, 604);
            this.panel6.TabIndex = 51;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(18, 56);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(190, 3);
            this.flowLayoutPanel1.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(34, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Active Cases by Section";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.Circuitval);
            this.panel5.Location = new System.Drawing.Point(25, 483);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(185, 79);
            this.panel5.TabIndex = 54;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(53, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 17);
            this.label11.TabIndex = 44;
            this.label11.Text = "CIRCUIT";
            // 
            // Circuitval
            // 
            this.Circuitval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Circuitval.AutoSize = true;
            this.Circuitval.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Circuitval.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Circuitval.Location = new System.Drawing.Point(63, 26);
            this.Circuitval.Name = "Circuitval";
            this.Circuitval.Size = new System.Drawing.Size(38, 45);
            this.Circuitval.TabIndex = 49;
            this.Circuitval.Text = "0";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.windingval);
            this.panel4.Location = new System.Drawing.Point(25, 383);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(185, 79);
            this.panel4.TabIndex = 53;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(50, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 17);
            this.label12.TabIndex = 42;
            this.label12.Text = "WNDING";
            // 
            // windingval
            // 
            this.windingval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.windingval.AutoSize = true;
            this.windingval.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windingval.ForeColor = System.Drawing.SystemColors.ControlText;
            this.windingval.Location = new System.Drawing.Point(63, 26);
            this.windingval.Name = "windingval";
            this.windingval.Size = new System.Drawing.Size(38, 45);
            this.windingval.TabIndex = 48;
            this.windingval.Text = "0";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.Rotorval);
            this.panel3.Location = new System.Drawing.Point(25, 282);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(185, 79);
            this.panel3.TabIndex = 52;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(57, 13);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 17);
            this.label14.TabIndex = 40;
            this.label14.Text = "ROTOR";
            // 
            // Rotorval
            // 
            this.Rotorval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Rotorval.AutoSize = true;
            this.Rotorval.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rotorval.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Rotorval.Location = new System.Drawing.Point(63, 24);
            this.Rotorval.Name = "Rotorval";
            this.Rotorval.Size = new System.Drawing.Size(38, 45);
            this.Rotorval.TabIndex = 47;
            this.Rotorval.Text = "0";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.Pressval);
            this.panel2.Location = new System.Drawing.Point(25, 180);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(185, 79);
            this.panel2.TabIndex = 51;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(59, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 17);
            this.label13.TabIndex = 38;
            this.label13.Text = "PRESS";
            // 
            // Pressval
            // 
            this.Pressval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Pressval.AutoSize = true;
            this.Pressval.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pressval.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Pressval.Location = new System.Drawing.Point(65, 26);
            this.Pressval.Name = "Pressval";
            this.Pressval.Size = new System.Drawing.Size(38, 45);
            this.Pressval.TabIndex = 46;
            this.Pressval.Text = "0";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.MoldLabel);
            this.panel1.Controls.Add(this.moldval);
            this.panel1.Location = new System.Drawing.Point(24, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 79);
            this.panel1.TabIndex = 50;
            // 
            // MoldLabel
            // 
            this.MoldLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoldLabel.AutoSize = true;
            this.MoldLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MoldLabel.ForeColor = System.Drawing.Color.Black;
            this.MoldLabel.Location = new System.Drawing.Point(50, 13);
            this.MoldLabel.Name = "MoldLabel";
            this.MoldLabel.Size = new System.Drawing.Size(68, 17);
            this.MoldLabel.TabIndex = 36;
            this.MoldLabel.Text = "MOLDING";
            // 
            // moldval
            // 
            this.moldval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moldval.AutoSize = true;
            this.moldval.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moldval.ForeColor = System.Drawing.SystemColors.ControlText;
            this.moldval.Location = new System.Drawing.Point(65, 25);
            this.moldval.Name = "moldval";
            this.moldval.Size = new System.Drawing.Size(38, 45);
            this.moldval.TabIndex = 45;
            this.moldval.Text = "0";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(983, 719);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 17);
            this.label4.TabIndex = 53;
            this.label4.Text = "Total Records";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 719);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 17);
            this.label2.TabIndex = 52;
            this.label2.Text = "Total Records";
            // 
            // Rejected
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.OpenReject);
            this.Controls.Add(this.filteritems);
            this.Controls.Add(this.RejectedGrid);
            this.Controls.Add(this.sectionfilter);
            this.Name = "Rejected";
            this.Size = new System.Drawing.Size(1366, 786);
            this.Load += new System.EventHandler(this.Rejected_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RejectedGrid)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label Circuitval;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label windingval;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label Rotorval;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label Pressval;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label MoldLabel;
        private System.Windows.Forms.Label moldval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}
