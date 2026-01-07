namespace FootWristStrapsAnalysis
{
    partial class Startup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Startup));
            this.AnalysisTable = new System.Windows.Forms.DataGridView();
            this.RecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TestDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComprehensiveResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftFootResistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftFootResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightFootResistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightFootResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WristStrapResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConductivityEvaluation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LowerEvaluationLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpperEvaluationLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EvaluationBuzzer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EvaluationExternalOutput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FG470 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prevbtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CountTable = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.folderText = new System.Windows.Forms.TextBox();
            this.btnOpenfolder = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.selecteditem = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AnalysisTable)).BeginInit();
            this.SuspendLayout();
            // 
            // AnalysisTable
            // 
            this.AnalysisTable.AllowUserToAddRows = false;
            this.AnalysisTable.AllowUserToDeleteRows = false;
            this.AnalysisTable.AllowUserToResizeColumns = false;
            this.AnalysisTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.AnalysisTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.AnalysisTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AnalysisTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.AnalysisTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AnalysisTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.AnalysisTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AnalysisTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecordID,
            this.Select,
            this.TestDate,
            this.TestTime,
            this.EmployeeID,
            this.EmployeeName,
            this.ComprehensiveResult,
            this.LeftFootResistance,
            this.LeftFootResult,
            this.RightFootResistance,
            this.RightFootResult,
            this.WristStrapResult,
            this.ConductivityEvaluation,
            this.LowerEvaluationLimit,
            this.UpperEvaluationLimit,
            this.EvaluationBuzzer,
            this.EvaluationExternalOutput,
            this.FG470,
            this.Note});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.AnalysisTable.DefaultCellStyle = dataGridViewCellStyle18;
            this.AnalysisTable.Location = new System.Drawing.Point(21, 112);
            this.AnalysisTable.Name = "AnalysisTable";
            this.AnalysisTable.ReadOnly = true;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AnalysisTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.AnalysisTable.RowHeadersVisible = false;
            this.AnalysisTable.RowTemplate.Height = 35;
            this.AnalysisTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AnalysisTable.Size = new System.Drawing.Size(1098, 574);
            this.AnalysisTable.TabIndex = 0;
            this.AnalysisTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AnalysisTable_CellClick);
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
            // Select
            // 
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.ReadOnly = true;
            this.Select.Width = 60;
            // 
            // TestDate
            // 
            this.TestDate.DataPropertyName = "TestDate";
            this.TestDate.HeaderText = "Test Date";
            this.TestDate.Name = "TestDate";
            this.TestDate.ReadOnly = true;
            this.TestDate.Visible = false;
            this.TestDate.Width = 91;
            // 
            // TestTime
            // 
            this.TestTime.DataPropertyName = "TestTime";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TestTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.TestTime.HeaderText = "Test Time";
            this.TestTime.Name = "TestTime";
            this.TestTime.ReadOnly = true;
            this.TestTime.Width = 91;
            // 
            // EmployeeID
            // 
            this.EmployeeID.DataPropertyName = "EmployeeID";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmployeeID.DefaultCellStyle = dataGridViewCellStyle4;
            this.EmployeeID.HeaderText = "Employee ID";
            this.EmployeeID.Name = "EmployeeID";
            this.EmployeeID.ReadOnly = true;
            this.EmployeeID.Width = 106;
            // 
            // EmployeeName
            // 
            this.EmployeeName.DataPropertyName = "EmployeeName";
            this.EmployeeName.HeaderText = "Employee Name";
            this.EmployeeName.Name = "EmployeeName";
            this.EmployeeName.ReadOnly = true;
            this.EmployeeName.Width = 124;
            // 
            // ComprehensiveResult
            // 
            this.ComprehensiveResult.DataPropertyName = "ComprehensiveResult";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ComprehensiveResult.DefaultCellStyle = dataGridViewCellStyle5;
            this.ComprehensiveResult.HeaderText = "Comprehensive Result";
            this.ComprehensiveResult.Name = "ComprehensiveResult";
            this.ComprehensiveResult.ReadOnly = true;
            this.ComprehensiveResult.Width = 155;
            // 
            // LeftFootResistance
            // 
            this.LeftFootResistance.DataPropertyName = "LeftFootResistance";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.LeftFootResistance.DefaultCellStyle = dataGridViewCellStyle6;
            this.LeftFootResistance.HeaderText = "Left Foot Resistance";
            this.LeftFootResistance.Name = "LeftFootResistance";
            this.LeftFootResistance.ReadOnly = true;
            this.LeftFootResistance.Width = 145;
            // 
            // LeftFootResult
            // 
            this.LeftFootResult.DataPropertyName = "LeftFootResult";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.LeftFootResult.DefaultCellStyle = dataGridViewCellStyle7;
            this.LeftFootResult.HeaderText = "Left Foot Result";
            this.LeftFootResult.Name = "LeftFootResult";
            this.LeftFootResult.ReadOnly = true;
            this.LeftFootResult.Width = 123;
            // 
            // RightFootResistance
            // 
            this.RightFootResistance.DataPropertyName = "RightFootResistance";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RightFootResistance.DefaultCellStyle = dataGridViewCellStyle8;
            this.RightFootResistance.HeaderText = "Right Foot Resistance";
            this.RightFootResistance.Name = "RightFootResistance";
            this.RightFootResistance.ReadOnly = true;
            this.RightFootResistance.Width = 151;
            // 
            // RightFootResult
            // 
            this.RightFootResult.DataPropertyName = "RightFootResult";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RightFootResult.DefaultCellStyle = dataGridViewCellStyle9;
            this.RightFootResult.HeaderText = "Right Foot Result";
            this.RightFootResult.Name = "RightFootResult";
            this.RightFootResult.ReadOnly = true;
            this.RightFootResult.Width = 129;
            // 
            // WristStrapResult
            // 
            this.WristStrapResult.DataPropertyName = "WristStrapResult";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.WristStrapResult.DefaultCellStyle = dataGridViewCellStyle10;
            this.WristStrapResult.HeaderText = "Wrist Strap Result";
            this.WristStrapResult.Name = "WristStrapResult";
            this.WristStrapResult.ReadOnly = true;
            this.WristStrapResult.Width = 134;
            // 
            // ConductivityEvaluation
            // 
            this.ConductivityEvaluation.DataPropertyName = "ConductivityEvaluation";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ConductivityEvaluation.DefaultCellStyle = dataGridViewCellStyle11;
            this.ConductivityEvaluation.HeaderText = "Conductivity Evaluation";
            this.ConductivityEvaluation.Name = "ConductivityEvaluation";
            this.ConductivityEvaluation.ReadOnly = true;
            this.ConductivityEvaluation.Width = 160;
            // 
            // LowerEvaluationLimit
            // 
            this.LowerEvaluationLimit.DataPropertyName = "LowerEvaluationLimit";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.LowerEvaluationLimit.DefaultCellStyle = dataGridViewCellStyle12;
            this.LowerEvaluationLimit.HeaderText = "Lower Evaluation Limit";
            this.LowerEvaluationLimit.Name = "LowerEvaluationLimit";
            this.LowerEvaluationLimit.ReadOnly = true;
            this.LowerEvaluationLimit.Width = 156;
            // 
            // UpperEvaluationLimit
            // 
            this.UpperEvaluationLimit.DataPropertyName = "UpperEvaluationLimit";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.UpperEvaluationLimit.DefaultCellStyle = dataGridViewCellStyle13;
            this.UpperEvaluationLimit.HeaderText = "Upper Evaluation Limit";
            this.UpperEvaluationLimit.Name = "UpperEvaluationLimit";
            this.UpperEvaluationLimit.ReadOnly = true;
            this.UpperEvaluationLimit.Width = 156;
            // 
            // EvaluationBuzzer
            // 
            this.EvaluationBuzzer.DataPropertyName = "EvaluationBuzzer";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.EvaluationBuzzer.DefaultCellStyle = dataGridViewCellStyle14;
            this.EvaluationBuzzer.HeaderText = "Evaluation Buzzer";
            this.EvaluationBuzzer.Name = "EvaluationBuzzer";
            this.EvaluationBuzzer.ReadOnly = true;
            this.EvaluationBuzzer.Width = 132;
            // 
            // EvaluationExternalOutput
            // 
            this.EvaluationExternalOutput.DataPropertyName = "EvaluationExternalOutput";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.EvaluationExternalOutput.DefaultCellStyle = dataGridViewCellStyle15;
            this.EvaluationExternalOutput.HeaderText = "Evaluation External Output";
            this.EvaluationExternalOutput.Name = "EvaluationExternalOutput";
            this.EvaluationExternalOutput.ReadOnly = true;
            this.EvaluationExternalOutput.Width = 141;
            // 
            // FG470
            // 
            this.FG470.DataPropertyName = "FG470";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FG470.DefaultCellStyle = dataGridViewCellStyle16;
            this.FG470.HeaderText = "FG470";
            this.FG470.Name = "FG470";
            this.FG470.ReadOnly = true;
            this.FG470.Width = 80;
            // 
            // Note
            // 
            this.Note.DataPropertyName = "Note";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Note.DefaultCellStyle = dataGridViewCellStyle17;
            this.Note.HeaderText = "Note";
            this.Note.Name = "Note";
            this.Note.ReadOnly = true;
            this.Note.Width = 72;
            // 
            // prevbtn
            // 
            this.prevbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.prevbtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prevbtn.Image = ((System.Drawing.Image)(resources.GetObject("prevbtn.Image")));
            this.prevbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.prevbtn.Location = new System.Drawing.Point(793, 38);
            this.prevbtn.Name = "prevbtn";
            this.prevbtn.Padding = new System.Windows.Forms.Padding(15, 0, 10, 0);
            this.prevbtn.Size = new System.Drawing.Size(153, 40);
            this.prevbtn.TabIndex = 1;
            this.prevbtn.Text = "Loads Previous";
            this.prevbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.prevbtn.UseVisualStyleBackColor = true;
            this.prevbtn.Click += new System.EventHandler(this.prevbtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(134, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(179, 22);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(21, 50);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(93, 22);
            this.dateTimePicker1.TabIndex = 3;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(966, 38);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(15, 0, 10, 0);
            this.button1.Size = new System.Drawing.Size(153, 40);
            this.button1.TabIndex = 4;
            this.button1.Text = "Export Analysis";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(973, 689);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 94;
            this.label1.Text = "Total Records : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // CountTable
            // 
            this.CountTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CountTable.AutoSize = true;
            this.CountTable.BackColor = System.Drawing.Color.Transparent;
            this.CountTable.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CountTable.Location = new System.Drawing.Point(1093, 689);
            this.CountTable.Name = "CountTable";
            this.CountTable.Size = new System.Drawing.Size(13, 17);
            this.CountTable.TabIndex = 98;
            this.CountTable.Text = "-";
            this.CountTable.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(20, 698);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 101;
            this.label2.Text = "Load Folder path : ";
            // 
            // folderText
            // 
            this.folderText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.folderText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.folderText.Location = new System.Drawing.Point(151, 697);
            this.folderText.Name = "folderText";
            this.folderText.Size = new System.Drawing.Size(179, 22);
            this.folderText.TabIndex = 102;
            // 
            // btnOpenfolder
            // 
            this.btnOpenfolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenfolder.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnOpenfolder.FlatAppearance.BorderSize = 2;
            this.btnOpenfolder.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenfolder.Location = new System.Drawing.Point(345, 692);
            this.btnOpenfolder.Name = "btnOpenfolder";
            this.btnOpenfolder.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnOpenfolder.Size = new System.Drawing.Size(102, 29);
            this.btnOpenfolder.TabIndex = 103;
            this.btnOpenfolder.Text = "Select folder";
            this.btnOpenfolder.UseVisualStyleBackColor = true;
            this.btnOpenfolder.Click += new System.EventHandler(this.btnOpenfolder_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.button4.FlatAppearance.BorderSize = 2;
            this.button4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(453, 691);
            this.button4.Name = "button4";
            this.button4.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.button4.Size = new System.Drawing.Size(110, 29);
            this.button4.TabIndex = 104;
            this.button4.Text = "Save the path";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(20, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 17);
            this.label3.TabIndex = 105;
            this.label3.Text = "Selected Item : ";
            // 
            // selecteditem
            // 
            this.selecteditem.AutoSize = true;
            this.selecteditem.BackColor = System.Drawing.Color.Transparent;
            this.selecteditem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selecteditem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.selecteditem.Location = new System.Drawing.Point(124, 88);
            this.selecteditem.Name = "selecteditem";
            this.selecteditem.Size = new System.Drawing.Size(43, 17);
            this.selecteditem.TabIndex = 106;
            this.selecteditem.Text = "0 / 10";
            // 
            // Startup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 749);
            this.Controls.Add(this.selecteditem);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnOpenfolder);
            this.Controls.Add(this.folderText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CountTable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.prevbtn);
            this.Controls.Add(this.AnalysisTable);
            this.Name = "Startup";
            this.Text = "Foot Wrist Analysis";
            this.Load += new System.EventHandler(this.Startup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AnalysisTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView AnalysisTable;
        private System.Windows.Forms.Button prevbtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComprehensiveResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftFootResistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftFootResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightFootResistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightFootResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn WristStrapResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConductivityEvaluation;
        private System.Windows.Forms.DataGridViewTextBoxColumn LowerEvaluationLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpperEvaluationLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn EvaluationBuzzer;
        private System.Windows.Forms.DataGridViewTextBoxColumn EvaluationExternalOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn FG470;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note;
        private System.Windows.Forms.Label CountTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox folderText;
        private System.Windows.Forms.Button btnOpenfolder;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label selecteditem;
    }
}

