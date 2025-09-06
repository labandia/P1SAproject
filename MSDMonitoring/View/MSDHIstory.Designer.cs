namespace MSDMonitoring
{
    partial class MSDHIstory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MSDHIstory));
            this.ReelText = new System.Windows.Forms.Label();
            this.MonitorTable = new System.Windows.Forms.DataGridView();
            this.Exitbtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.BtnLast = new System.Windows.Forms.Button();
            this.BtnFirst = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalPages = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCurrentPage = new System.Windows.Forms.Label();
            this.BtnPrev = new System.Windows.Forms.Button();
            this.BtnNext = new System.Windows.Forms.Button();
            this.Exportbtn = new System.Windows.Forms.Button();
            this.ReelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Partnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FloorLife = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reel_Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity_IN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlanQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Input_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Exphours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalHours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemainLife = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Print = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.MonitorTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ReelText
            // 
            this.ReelText.AutoSize = true;
            this.ReelText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(23)))), ((int)(((byte)(30)))));
            this.ReelText.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReelText.ForeColor = System.Drawing.Color.White;
            this.ReelText.Location = new System.Drawing.Point(27, 30);
            this.ReelText.Name = "ReelText";
            this.ReelText.Size = new System.Drawing.Size(443, 24);
            this.ReelText.TabIndex = 81;
            this.ReelText.Text = " MOISTURE SENSITIVE DEVICES MONITORING";
            // 
            // MonitorTable
            // 
            this.MonitorTable.AllowUserToAddRows = false;
            this.MonitorTable.AllowUserToDeleteRows = false;
            this.MonitorTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MonitorTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.MonitorTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MonitorTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.MonitorTable.BackgroundColor = System.Drawing.Color.White;
            this.MonitorTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MonitorTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.MonitorTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MonitorTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.MonitorTable.ColumnHeadersHeight = 45;
            this.MonitorTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.MonitorTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReelID,
            this.Partnumber,
            this.Location,
            this.Position,
            this.FloorLife,
            this.Level,
            this.LotNo,
            this.Line,
            this.DateIn,
            this.TimeIn,
            this.Reel_Quantity,
            this.InputName,
            this.DateOut,
            this.TimeOut,
            this.Quantity_IN,
            this.PlanQty,
            this.Input_Name,
            this.SupplierName,
            this.Exphours,
            this.TotalHours,
            this.RemainLife,
            this.RecordID,
            this.Print});
            this.MonitorTable.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MonitorTable.DefaultCellStyle = dataGridViewCellStyle3;
            this.MonitorTable.EnableHeadersVisualStyles = false;
            this.MonitorTable.GridColor = System.Drawing.Color.White;
            this.MonitorTable.Location = new System.Drawing.Point(31, 155);
            this.MonitorTable.Name = "MonitorTable";
            this.MonitorTable.ReadOnly = true;
            this.MonitorTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.MonitorTable.RowHeadersVisible = false;
            this.MonitorTable.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.MonitorTable.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.MonitorTable.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MonitorTable.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(201)))), ((int)(((byte)(189)))));
            this.MonitorTable.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MonitorTable.RowTemplate.Height = 50;
            this.MonitorTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MonitorTable.Size = new System.Drawing.Size(1294, 506);
            this.MonitorTable.TabIndex = 82;
            this.MonitorTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MonitorTable_CellClick);
            this.MonitorTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MonitorTable_CellContentClick);
            this.MonitorTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MonitorTable_CellDoubleClick);
            this.MonitorTable.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.MonitorTable_CellFormatting);
            // 
            // Exitbtn
            // 
            this.Exitbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Exitbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(23)))), ((int)(((byte)(30)))));
            this.Exitbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exitbtn.FlatAppearance.BorderSize = 0;
            this.Exitbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exitbtn.Image = ((System.Drawing.Image)(resources.GetObject("Exitbtn.Image")));
            this.Exitbtn.Location = new System.Drawing.Point(1267, 22);
            this.Exitbtn.Name = "Exitbtn";
            this.Exitbtn.Size = new System.Drawing.Size(55, 46);
            this.Exitbtn.TabIndex = 83;
            this.Exitbtn.UseVisualStyleBackColor = false;
            this.Exitbtn.Click += new System.EventHandler(this.Exitbtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(23)))), ((int)(((byte)(30)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1370, 83);
            this.pictureBox1.TabIndex = 84;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Transparent;
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(303, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 39);
            this.label6.TabIndex = 86;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // searchBox
            // 
            this.searchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBox.Location = new System.Drawing.Point(31, 103);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(266, 38);
            this.searchBox.TabIndex = 85;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // BtnLast
            // 
            this.BtnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnLast.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLast.Location = new System.Drawing.Point(1267, 677);
            this.BtnLast.Name = "BtnLast";
            this.BtnLast.Size = new System.Drawing.Size(57, 30);
            this.BtnLast.TabIndex = 94;
            this.BtnLast.Text = "Last";
            this.BtnLast.UseVisualStyleBackColor = true;
            this.BtnLast.Click += new System.EventHandler(this.BtnLast_Click);
            // 
            // BtnFirst
            // 
            this.BtnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFirst.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnFirst.Location = new System.Drawing.Point(1086, 677);
            this.BtnFirst.Name = "BtnFirst";
            this.BtnFirst.Size = new System.Drawing.Size(64, 30);
            this.BtnFirst.TabIndex = 93;
            this.BtnFirst.Text = "First";
            this.BtnFirst.UseVisualStyleBackColor = true;
            this.BtnFirst.Click += new System.EventHandler(this.BtnFirst_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(112, 677);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 16);
            this.label5.TabIndex = 92;
            this.label5.Text = "Of";
            // 
            // lblTotalPages
            // 
            this.lblTotalPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPages.AutoSize = true;
            this.lblTotalPages.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalPages.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTotalPages.Location = new System.Drawing.Point(148, 677);
            this.lblTotalPages.Name = "lblTotalPages";
            this.lblTotalPages.Size = new System.Drawing.Size(14, 16);
            this.lblTotalPages.TabIndex = 91;
            this.lblTotalPages.Text = "0";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(36, 677);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 90;
            this.label1.Text = "Page";
            // 
            // lblCurrentPage
            // 
            this.lblCurrentPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCurrentPage.AutoSize = true;
            this.lblCurrentPage.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentPage.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCurrentPage.Location = new System.Drawing.Point(86, 677);
            this.lblCurrentPage.Name = "lblCurrentPage";
            this.lblCurrentPage.Size = new System.Drawing.Size(14, 16);
            this.lblCurrentPage.TabIndex = 89;
            this.lblCurrentPage.Text = "0";
            // 
            // BtnPrev
            // 
            this.BtnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrev.Image = ((System.Drawing.Image)(resources.GetObject("BtnPrev.Image")));
            this.BtnPrev.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnPrev.Location = new System.Drawing.Point(1169, 677);
            this.BtnPrev.Name = "BtnPrev";
            this.BtnPrev.Size = new System.Drawing.Size(35, 30);
            this.BtnPrev.TabIndex = 88;
            this.BtnPrev.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnPrev.UseVisualStyleBackColor = true;
            this.BtnPrev.Click += new System.EventHandler(this.BtnPrev_Click);
            // 
            // BtnNext
            // 
            this.BtnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNext.Image = ((System.Drawing.Image)(resources.GetObject("BtnNext.Image")));
            this.BtnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnNext.Location = new System.Drawing.Point(1210, 677);
            this.BtnNext.Name = "BtnNext";
            this.BtnNext.Size = new System.Drawing.Size(35, 30);
            this.BtnNext.TabIndex = 87;
            this.BtnNext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnNext.UseVisualStyleBackColor = true;
            this.BtnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // Exportbtn
            // 
            this.Exportbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Exportbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(152)))), ((int)(((byte)(126)))));
            this.Exportbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exportbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(56)))), ((int)(((byte)(44)))));
            this.Exportbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(56)))), ((int)(((byte)(44)))));
            this.Exportbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(56)))), ((int)(((byte)(44)))));
            this.Exportbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exportbtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exportbtn.ForeColor = System.Drawing.Color.Transparent;
            this.Exportbtn.Image = ((System.Drawing.Image)(resources.GetObject("Exportbtn.Image")));
            this.Exportbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Exportbtn.Location = new System.Drawing.Point(1191, 102);
            this.Exportbtn.Name = "Exportbtn";
            this.Exportbtn.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.Exportbtn.Size = new System.Drawing.Size(133, 35);
            this.Exportbtn.TabIndex = 95;
            this.Exportbtn.Text = "Export ";
            this.Exportbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Exportbtn.UseVisualStyleBackColor = false;
            this.Exportbtn.Click += new System.EventHandler(this.Exportbtn_Click);
            // 
            // ReelID
            // 
            this.ReelID.DataPropertyName = "ReelID";
            this.ReelID.HeaderText = "Reel ID";
            this.ReelID.Name = "ReelID";
            this.ReelID.ReadOnly = true;
            this.ReelID.Width = 58;
            // 
            // Partnumber
            // 
            this.Partnumber.DataPropertyName = "Partnumber";
            this.Partnumber.HeaderText = "Partnumber";
            this.Partnumber.Name = "Partnumber";
            this.Partnumber.ReadOnly = true;
            this.Partnumber.Width = 105;
            // 
            // Location
            // 
            this.Location.DataPropertyName = "Storelocation";
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            this.Location.Width = 85;
            // 
            // Position
            // 
            this.Position.DataPropertyName = "Position";
            this.Position.HeaderText = "Upper/Lower";
            this.Position.Name = "Position";
            this.Position.ReadOnly = true;
            this.Position.Width = 112;
            // 
            // FloorLife
            // 
            this.FloorLife.DataPropertyName = "FloorLife";
            this.FloorLife.HeaderText = "FloorLife";
            this.FloorLife.Name = "FloorLife";
            this.FloorLife.ReadOnly = true;
            this.FloorLife.Width = 83;
            // 
            // Level
            // 
            this.Level.DataPropertyName = "Level";
            this.Level.HeaderText = "Level";
            this.Level.Name = "Level";
            this.Level.ReadOnly = true;
            this.Level.Width = 63;
            // 
            // LotNo
            // 
            this.LotNo.DataPropertyName = "LotNo";
            this.LotNo.HeaderText = "Lot No";
            this.LotNo.Name = "LotNo";
            this.LotNo.ReadOnly = true;
            this.LotNo.Width = 49;
            // 
            // Line
            // 
            this.Line.DataPropertyName = "Line";
            this.Line.HeaderText = "Line";
            this.Line.Name = "Line";
            this.Line.ReadOnly = true;
            this.Line.Width = 56;
            // 
            // DateIn
            // 
            this.DateIn.DataPropertyName = "DateIn";
            this.DateIn.HeaderText = "DateIn";
            this.DateIn.Name = "DateIn";
            this.DateIn.ReadOnly = true;
            this.DateIn.Width = 72;
            // 
            // TimeIn
            // 
            this.TimeIn.DataPropertyName = "TimeIn";
            this.TimeIn.HeaderText = "Time IN";
            this.TimeIn.Name = "TimeIn";
            this.TimeIn.ReadOnly = true;
            this.TimeIn.Width = 72;
            // 
            // Reel_Quantity
            // 
            this.Reel_Quantity.DataPropertyName = "Reel_Quantity";
            this.Reel_Quantity.HeaderText = "Reel Quantity";
            this.Reel_Quantity.Name = "Reel_Quantity";
            this.Reel_Quantity.ReadOnly = true;
            this.Reel_Quantity.Width = 108;
            // 
            // InputName
            // 
            this.InputName.DataPropertyName = "InputName";
            this.InputName.HeaderText = "Input Name";
            this.InputName.Name = "InputName";
            this.InputName.ReadOnly = true;
            this.InputName.Width = 97;
            // 
            // DateOut
            // 
            this.DateOut.DataPropertyName = "DateOut";
            this.DateOut.HeaderText = "Date_IN";
            this.DateOut.Name = "DateOut";
            this.DateOut.ReadOnly = true;
            this.DateOut.Width = 81;
            // 
            // TimeOut
            // 
            this.TimeOut.DataPropertyName = "TimeOut";
            this.TimeOut.HeaderText = "Time Out";
            this.TimeOut.Name = "TimeOut";
            this.TimeOut.ReadOnly = true;
            this.TimeOut.Width = 80;
            // 
            // Quantity_IN
            // 
            this.Quantity_IN.DataPropertyName = "Quantity_IN";
            this.Quantity_IN.HeaderText = "Quantity IN";
            this.Quantity_IN.Name = "Quantity_IN";
            this.Quantity_IN.ReadOnly = true;
            this.Quantity_IN.Width = 95;
            // 
            // PlanQty
            // 
            this.PlanQty.DataPropertyName = "PlanQty";
            this.PlanQty.HeaderText = "Use plan Qty";
            this.PlanQty.Name = "PlanQty";
            this.PlanQty.ReadOnly = true;
            this.PlanQty.Width = 104;
            // 
            // Input_Name
            // 
            this.Input_Name.DataPropertyName = "Input_Name";
            this.Input_Name.HeaderText = "Input Name";
            this.Input_Name.Name = "Input_Name";
            this.Input_Name.ReadOnly = true;
            this.Input_Name.Width = 97;
            // 
            // SupplierName
            // 
            this.SupplierName.DataPropertyName = "SupplierName";
            this.SupplierName.HeaderText = "SupplierName";
            this.SupplierName.Name = "SupplierName";
            this.SupplierName.ReadOnly = true;
            this.SupplierName.Width = 123;
            // 
            // Exphours
            // 
            this.Exphours.DataPropertyName = "Exphours";
            this.Exphours.HeaderText = "Expose Hours";
            this.Exphours.Name = "Exphours";
            this.Exphours.ReadOnly = true;
            this.Exphours.Width = 105;
            // 
            // TotalHours
            // 
            this.TotalHours.DataPropertyName = "TotalHours";
            this.TotalHours.HeaderText = "Total Exp hours";
            this.TotalHours.Name = "TotalHours";
            this.TotalHours.ReadOnly = true;
            this.TotalHours.Width = 116;
            // 
            // RemainLife
            // 
            this.RemainLife.DataPropertyName = "RemainLife";
            this.RemainLife.HeaderText = "Remain Life";
            this.RemainLife.Name = "RemainLife";
            this.RemainLife.ReadOnly = true;
            this.RemainLife.Width = 96;
            // 
            // RecordID
            // 
            this.RecordID.DataPropertyName = "RecordID";
            this.RecordID.HeaderText = "RecordID";
            this.RecordID.Name = "RecordID";
            this.RecordID.ReadOnly = true;
            this.RecordID.Visible = false;
            this.RecordID.Width = 89;
            // 
            // Print
            // 
            this.Print.HeaderText = "Print";
            this.Print.Image = ((System.Drawing.Image)(resources.GetObject("Print.Image")));
            this.Print.Name = "Print";
            this.Print.ReadOnly = true;
            this.Print.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Print.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Print.Width = 58;
            // 
            // MSDHIstory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 737);
            this.ControlBox = false;
            this.Controls.Add(this.Exportbtn);
            this.Controls.Add(this.BtnLast);
            this.Controls.Add(this.BtnFirst);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTotalPages);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCurrentPage);
            this.Controls.Add(this.BtnPrev);
            this.Controls.Add(this.BtnNext);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.Exitbtn);
            this.Controls.Add(this.ReelText);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.MonitorTable);
            this.Name = "MSDHIstory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MSDHIstory";
            this.Load += new System.EventHandler(this.MSDHIstory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MonitorTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ReelText;
        private System.Windows.Forms.DataGridView MonitorTable;
        private System.Windows.Forms.Button Exitbtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button BtnLast;
        private System.Windows.Forms.Button BtnFirst;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label lblTotalPages;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblCurrentPage;
        private System.Windows.Forms.Button BtnPrev;
        private System.Windows.Forms.Button BtnNext;
        private System.Windows.Forms.Button Exportbtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReelID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Partnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
        private System.Windows.Forms.DataGridViewTextBoxColumn FloorLife;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Line;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reel_Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity_IN;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlanQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Input_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Exphours;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemainLife;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordID;
        private System.Windows.Forms.DataGridViewImageColumn Print;
    }
}