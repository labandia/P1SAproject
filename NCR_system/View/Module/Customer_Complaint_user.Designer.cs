namespace NCR_system.View.Module
{
    partial class Customer_Complaint_user
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Customer_Complaint_user));
            this.CustomDatagrid = new System.Windows.Forms.DataGridView();
            this.OpenCC = new System.Windows.Forms.Button();
            this.SelectedProcess = new System.Windows.Forms.ComboBox();
            this.Externalbtn = new System.Windows.Forms.Button();
            this.filteritems = new System.Windows.Forms.ComboBox();
            this.sectionfilter = new System.Windows.Forms.ComboBox();
            this.searchText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
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
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.RecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NGQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Details = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.UploadImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.CustomDatagrid)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // CustomDatagrid
            // 
            this.CustomDatagrid.AllowUserToAddRows = false;
            this.CustomDatagrid.AllowUserToDeleteRows = false;
            this.CustomDatagrid.AllowUserToResizeColumns = false;
            this.CustomDatagrid.AllowUserToResizeRows = false;
            this.CustomDatagrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomDatagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CustomDatagrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.CustomDatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustomDatagrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecordID,
            this.DateCreated,
            this.SectionID,
            this.ModelNo,
            this.LotNo,
            this.NGQty,
            this.Details,
            this.RegNo,
            this.CustomerName,
            this.Status,
            this.CCtype,
            this.Delete,
            this.UploadImage});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CustomDatagrid.DefaultCellStyle = dataGridViewCellStyle8;
            this.CustomDatagrid.Location = new System.Drawing.Point(6, 393);
            this.CustomDatagrid.Name = "CustomDatagrid";
            this.CustomDatagrid.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CustomDatagrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.CustomDatagrid.RowHeadersVisible = false;
            this.CustomDatagrid.RowTemplate.Height = 30;
            this.CustomDatagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CustomDatagrid.Size = new System.Drawing.Size(1524, 410);
            this.CustomDatagrid.TabIndex = 4;
            this.CustomDatagrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CustomDatagrid_CellClick);
            this.CustomDatagrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CustomDatagrid_CellDoubleClick);
            this.CustomDatagrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.CustomDatagrid_CellFormatting);
            // 
            // OpenCC
            // 
            this.OpenCC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenCC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.OpenCC.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.OpenCC.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.OpenCC.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(15)))), ((int)(((byte)(168)))));
            this.OpenCC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenCC.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenCC.ForeColor = System.Drawing.Color.White;
            this.OpenCC.Location = new System.Drawing.Point(1294, 323);
            this.OpenCC.Name = "OpenCC";
            this.OpenCC.Size = new System.Drawing.Size(136, 41);
            this.OpenCC.TabIndex = 6;
            this.OpenCC.Text = "Add SDC";
            this.OpenCC.UseVisualStyleBackColor = false;
            this.OpenCC.Click += new System.EventHandler(this.OpenCC_Click);
            // 
            // SelectedProcess
            // 
            this.SelectedProcess.DisplayMember = "External";
            this.SelectedProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectedProcess.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.SelectedProcess.FormattingEnabled = true;
            this.SelectedProcess.ItemHeight = 15;
            this.SelectedProcess.Items.AddRange(new object[] {
            "External",
            "SDC"});
            this.SelectedProcess.Location = new System.Drawing.Point(667, 333);
            this.SelectedProcess.Name = "SelectedProcess";
            this.SelectedProcess.Size = new System.Drawing.Size(159, 23);
            this.SelectedProcess.TabIndex = 7;
            this.SelectedProcess.ValueMember = "External";
            this.SelectedProcess.SelectedIndexChanged += new System.EventHandler(this.SelectedProcess_SelectedIndexChanged);
            // 
            // Externalbtn
            // 
            this.Externalbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Externalbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.Externalbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.Externalbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.Externalbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(15)))), ((int)(((byte)(168)))));
            this.Externalbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Externalbtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.Externalbtn.ForeColor = System.Drawing.Color.White;
            this.Externalbtn.Location = new System.Drawing.Point(1083, 318);
            this.Externalbtn.Name = "Externalbtn";
            this.Externalbtn.Size = new System.Drawing.Size(147, 46);
            this.Externalbtn.TabIndex = 8;
            this.Externalbtn.Text = "Add External";
            this.Externalbtn.UseVisualStyleBackColor = false;
            this.Externalbtn.Click += new System.EventHandler(this.Externalbtn_Click);
            // 
            // filteritems
            // 
            this.filteritems.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.filteritems.FormattingEnabled = true;
            this.filteritems.Items.AddRange(new object[] {
            "-- Filter Status --",
            "Open",
            "Close"});
            this.filteritems.Location = new System.Drawing.Point(852, 333);
            this.filteritems.Name = "filteritems";
            this.filteritems.Size = new System.Drawing.Size(121, 23);
            this.filteritems.TabIndex = 11;
            this.filteritems.SelectedIndexChanged += new System.EventHandler(this.filteritems_SelectedIndexChanged);
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
            this.sectionfilter.Location = new System.Drawing.Point(527, 333);
            this.sectionfilter.MaxDropDownItems = 30;
            this.sectionfilter.Name = "sectionfilter";
            this.sectionfilter.Size = new System.Drawing.Size(121, 23);
            this.sectionfilter.TabIndex = 12;
            this.sectionfilter.SelectedIndexChanged += new System.EventHandler(this.sectionfilter_SelectedIndexChanged);
            // 
            // searchText
            // 
            this.searchText.Location = new System.Drawing.Point(346, 335);
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(157, 20);
            this.searchText.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(343, 310);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "Search ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(529, 310);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 15);
            this.label5.TabIndex = 23;
            this.label5.Text = "Filter by Section";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(664, 310);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 15);
            this.label7.TabIndex = 24;
            this.label7.Text = "Filter by ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.Control;
            this.label9.Location = new System.Drawing.Point(849, 310);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 15);
            this.label9.TabIndex = 25;
            this.label9.Text = "Filter by Open/Close";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panel7.Controls.Add(this.panel9);
            this.panel7.Controls.Add(this.chart1);
            this.panel7.Controls.Add(this.label9);
            this.panel7.Controls.Add(this.OpenCC);
            this.panel7.Controls.Add(this.Externalbtn);
            this.panel7.Controls.Add(this.label7);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.SelectedProcess);
            this.panel7.Controls.Add(this.searchText);
            this.panel7.Controls.Add(this.filteritems);
            this.panel7.Controls.Add(this.sectionfilter);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1530, 387);
            this.panel7.TabIndex = 34;
            // 
            // panel9
            // 
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
            this.panel9.Location = new System.Drawing.Point(59, 20);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(172, 330);
            this.panel9.TabIndex = 34;
            // 
            // Circuitval
            // 
            this.Circuitval.AutoSize = true;
            this.Circuitval.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Circuitval.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Circuitval.Location = new System.Drawing.Point(47, 290);
            this.Circuitval.Name = "Circuitval";
            this.Circuitval.Size = new System.Drawing.Size(25, 30);
            this.Circuitval.TabIndex = 49;
            this.Circuitval.Text = "0";
            // 
            // windingval
            // 
            this.windingval.AutoSize = true;
            this.windingval.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windingval.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.windingval.Location = new System.Drawing.Point(47, 233);
            this.windingval.Name = "windingval";
            this.windingval.Size = new System.Drawing.Size(25, 30);
            this.windingval.TabIndex = 48;
            this.windingval.Text = "0";
            // 
            // Rotorval
            // 
            this.Rotorval.AutoSize = true;
            this.Rotorval.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rotorval.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Rotorval.Location = new System.Drawing.Point(47, 173);
            this.Rotorval.Name = "Rotorval";
            this.Rotorval.Size = new System.Drawing.Size(25, 30);
            this.Rotorval.TabIndex = 47;
            this.Rotorval.Text = "0";
            // 
            // Pressval
            // 
            this.Pressval.AutoSize = true;
            this.Pressval.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pressval.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Pressval.Location = new System.Drawing.Point(47, 110);
            this.Pressval.Name = "Pressval";
            this.Pressval.Size = new System.Drawing.Size(25, 30);
            this.Pressval.TabIndex = 46;
            this.Pressval.Text = "0";
            // 
            // moldval
            // 
            this.moldval.AutoSize = true;
            this.moldval.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moldval.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.moldval.Location = new System.Drawing.Point(47, 45);
            this.moldval.Name = "moldval";
            this.moldval.Size = new System.Drawing.Size(25, 30);
            this.moldval.TabIndex = 45;
            this.moldval.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label11.Location = new System.Drawing.Point(20, 268);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 21);
            this.label11.TabIndex = 44;
            this.label11.Text = "Circuit";
            // 
            // MoldLabel
            // 
            this.MoldLabel.AutoSize = true;
            this.MoldLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MoldLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.MoldLabel.Location = new System.Drawing.Point(20, 22);
            this.MoldLabel.Name = "MoldLabel";
            this.MoldLabel.Size = new System.Drawing.Size(75, 21);
            this.MoldLabel.TabIndex = 36;
            this.MoldLabel.Text = "Molding";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Aqua;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Location = new System.Drawing.Point(23, 298);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(15, 13);
            this.button5.TabIndex = 43;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DodgerBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(23, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(15, 13);
            this.button1.TabIndex = 35;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label12.Location = new System.Drawing.Point(19, 211);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 21);
            this.label12.TabIndex = 42;
            this.label12.Text = "Winding";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(23, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(15, 13);
            this.button2.TabIndex = 37;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Yellow;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(23, 242);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(15, 13);
            this.button4.TabIndex = 41;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label13.Location = new System.Drawing.Point(20, 85);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 21);
            this.label13.TabIndex = 38;
            this.label13.Text = "Press";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label14.Location = new System.Drawing.Point(20, 149);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 21);
            this.label14.TabIndex = 40;
            this.label14.Text = "Rotor";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(23, 184);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(15, 13);
            this.button3.TabIndex = 39;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(256, 20);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1241, 255);
            this.chart1.TabIndex = 33;
            this.chart1.Text = "chart1";
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
            // DateCreated
            // 
            this.DateCreated.DataPropertyName = "DateCreated";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DateCreated.DefaultCellStyle = dataGridViewCellStyle2;
            this.DateCreated.HeaderText = "Date";
            this.DateCreated.Name = "DateCreated";
            this.DateCreated.ReadOnly = true;
            this.DateCreated.Width = 68;
            // 
            // SectionID
            // 
            this.SectionID.DataPropertyName = "SectionID";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SectionID.DefaultCellStyle = dataGridViewCellStyle3;
            this.SectionID.HeaderText = "Section in charge";
            this.SectionID.Name = "SectionID";
            this.SectionID.ReadOnly = true;
            this.SectionID.Width = 123;
            // 
            // ModelNo
            // 
            this.ModelNo.DataPropertyName = "ModelNo";
            this.ModelNo.HeaderText = "Model No/ Part no.";
            this.ModelNo.Name = "ModelNo";
            this.ModelNo.ReadOnly = true;
            this.ModelNo.Width = 119;
            // 
            // LotNo
            // 
            this.LotNo.DataPropertyName = "LotNo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.LotNo.DefaultCellStyle = dataGridViewCellStyle4;
            this.LotNo.HeaderText = "Lot No.";
            this.LotNo.Name = "LotNo";
            this.LotNo.ReadOnly = true;
            this.LotNo.Width = 78;
            // 
            // NGQty
            // 
            this.NGQty.DataPropertyName = "NGQty";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NGQty.DefaultCellStyle = dataGridViewCellStyle5;
            this.NGQty.HeaderText = "NG Qty";
            this.NGQty.Name = "NGQty";
            this.NGQty.ReadOnly = true;
            this.NGQty.Width = 77;
            // 
            // Details
            // 
            this.Details.DataPropertyName = "Details";
            this.Details.HeaderText = "Details of Problem";
            this.Details.Name = "Details";
            this.Details.ReadOnly = true;
            this.Details.Width = 129;
            // 
            // RegNo
            // 
            this.RegNo.DataPropertyName = "RegNo";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegNo.DefaultCellStyle = dataGridViewCellStyle6;
            this.RegNo.HeaderText = "Registration No.";
            this.RegNo.Name = "RegNo";
            this.RegNo.ReadOnly = true;
            this.RegNo.Width = 119;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Width = 118;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Status.DefaultCellStyle = dataGridViewCellStyle7;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 76;
            // 
            // CCtype
            // 
            this.CCtype.DataPropertyName = "CCtype";
            this.CCtype.HeaderText = "CCtype";
            this.CCtype.Name = "CCtype";
            this.CCtype.ReadOnly = true;
            this.CCtype.Visible = false;
            this.CCtype.Width = 81;
            // 
            // Delete
            // 
            this.Delete.DataPropertyName = "Delete";
            this.Delete.HeaderText = "Delete";
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Width = 58;
            // 
            // UploadImage
            // 
            this.UploadImage.DataPropertyName = "UploadImage";
            this.UploadImage.HeaderText = "UploadImage";
            this.UploadImage.Name = "UploadImage";
            this.UploadImage.ReadOnly = true;
            this.UploadImage.Visible = false;
            this.UploadImage.Width = 114;
            // 
            // Customer_Complaint_user
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.CustomDatagrid);
            this.Name = "Customer_Complaint_user";
            this.Size = new System.Drawing.Size(1530, 806);
            this.Load += new System.EventHandler(this.Customer_Complaint_user_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CustomDatagrid)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.DataGridView CustomDatagrid;
        private System.Windows.Forms.Button OpenCC;
        private System.Windows.Forms.ComboBox SelectedProcess;
        private System.Windows.Forms.Button Externalbtn;
        private System.Windows.Forms.ComboBox filteritems;
        private System.Windows.Forms.ComboBox sectionfilter;
        private System.Windows.Forms.TextBox searchText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel7;
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
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCreated;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NGQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Details;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCtype;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn UploadImage;
    }
}
