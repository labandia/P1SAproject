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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle181 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle182 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle197 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle198 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle183 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle184 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle185 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle186 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle187 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle188 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle189 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle190 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle191 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle192 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle193 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle194 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle195 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle196 = new System.Windows.Forms.DataGridViewCellStyle();
            this.AnalysisTable = new System.Windows.Forms.DataGridView();
            this.prevbtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
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
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AnalysisTable)).BeginInit();
            this.SuspendLayout();
            // 
            // AnalysisTable
            // 
            this.AnalysisTable.AllowUserToAddRows = false;
            this.AnalysisTable.AllowUserToDeleteRows = false;
            this.AnalysisTable.AllowUserToResizeColumns = false;
            this.AnalysisTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle181.BackColor = System.Drawing.Color.WhiteSmoke;
            this.AnalysisTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle181;
            this.AnalysisTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AnalysisTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.AnalysisTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle182.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle182.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle182.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle182.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle182.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle182.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle182.SelectionForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle182.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AnalysisTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle182;
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
            dataGridViewCellStyle197.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle197.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle197.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle197.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle197.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle197.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle197.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle197.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.AnalysisTable.DefaultCellStyle = dataGridViewCellStyle197;
            this.AnalysisTable.Location = new System.Drawing.Point(21, 93);
            this.AnalysisTable.Name = "AnalysisTable";
            this.AnalysisTable.ReadOnly = true;
            dataGridViewCellStyle198.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle198.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle198.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle198.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle198.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle198.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle198.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AnalysisTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle198;
            this.AnalysisTable.RowHeadersVisible = false;
            this.AnalysisTable.RowTemplate.Height = 35;
            this.AnalysisTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AnalysisTable.Size = new System.Drawing.Size(1098, 608);
            this.AnalysisTable.TabIndex = 0;
            this.AnalysisTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AnalysisTable_CellDoubleClick);
            // 
            // prevbtn
            // 
            this.prevbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.prevbtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prevbtn.Location = new System.Drawing.Point(781, 38);
            this.prevbtn.Name = "prevbtn";
            this.prevbtn.Size = new System.Drawing.Size(153, 40);
            this.prevbtn.TabIndex = 1;
            this.prevbtn.Text = "Loads Previous";
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
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(21, 50);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(93, 22);
            this.dateTimePicker1.TabIndex = 3;
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
            this.Select.Width = 55;
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
            this.TestTime.HeaderText = "Test Time";
            this.TestTime.Name = "TestTime";
            this.TestTime.ReadOnly = true;
            this.TestTime.Width = 92;
            // 
            // EmployeeID
            // 
            this.EmployeeID.DataPropertyName = "EmployeeID";
            dataGridViewCellStyle183.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmployeeID.DefaultCellStyle = dataGridViewCellStyle183;
            this.EmployeeID.HeaderText = "Employee ID";
            this.EmployeeID.Name = "EmployeeID";
            this.EmployeeID.ReadOnly = true;
            this.EmployeeID.Width = 109;
            // 
            // EmployeeName
            // 
            this.EmployeeName.DataPropertyName = "EmployeeName";
            this.EmployeeName.HeaderText = "Employee Name";
            this.EmployeeName.Name = "EmployeeName";
            this.EmployeeName.ReadOnly = true;
            this.EmployeeName.Width = 119;
            // 
            // ComprehensiveResult
            // 
            this.ComprehensiveResult.DataPropertyName = "ComprehensiveResult";
            dataGridViewCellStyle184.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ComprehensiveResult.DefaultCellStyle = dataGridViewCellStyle184;
            this.ComprehensiveResult.HeaderText = "Comprehensive Result";
            this.ComprehensiveResult.Name = "ComprehensiveResult";
            this.ComprehensiveResult.ReadOnly = true;
            this.ComprehensiveResult.Width = 147;
            // 
            // LeftFootResistance
            // 
            this.LeftFootResistance.DataPropertyName = "LeftFootResistance";
            dataGridViewCellStyle185.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.LeftFootResistance.DefaultCellStyle = dataGridViewCellStyle185;
            this.LeftFootResistance.HeaderText = "Left Foot Resistance";
            this.LeftFootResistance.Name = "LeftFootResistance";
            this.LeftFootResistance.ReadOnly = true;
            this.LeftFootResistance.Width = 136;
            // 
            // LeftFootResult
            // 
            this.LeftFootResult.DataPropertyName = "LeftFootResult";
            dataGridViewCellStyle186.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.LeftFootResult.DefaultCellStyle = dataGridViewCellStyle186;
            this.LeftFootResult.HeaderText = "Left Foot Result";
            this.LeftFootResult.Name = "LeftFootResult";
            this.LeftFootResult.ReadOnly = true;
            this.LeftFootResult.Width = 117;
            // 
            // RightFootResistance
            // 
            this.RightFootResistance.DataPropertyName = "RightFootResistance";
            dataGridViewCellStyle187.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RightFootResistance.DefaultCellStyle = dataGridViewCellStyle187;
            this.RightFootResistance.HeaderText = "Right Foot Resistance";
            this.RightFootResistance.Name = "RightFootResistance";
            this.RightFootResistance.ReadOnly = true;
            this.RightFootResistance.Width = 144;
            // 
            // RightFootResult
            // 
            this.RightFootResult.DataPropertyName = "RightFootResult";
            dataGridViewCellStyle188.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RightFootResult.DefaultCellStyle = dataGridViewCellStyle188;
            this.RightFootResult.HeaderText = "Right Foot Result";
            this.RightFootResult.Name = "RightFootResult";
            this.RightFootResult.ReadOnly = true;
            this.RightFootResult.Width = 124;
            // 
            // WristStrapResult
            // 
            this.WristStrapResult.DataPropertyName = "WristStrapResult";
            dataGridViewCellStyle189.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.WristStrapResult.DefaultCellStyle = dataGridViewCellStyle189;
            this.WristStrapResult.HeaderText = "Wrist Strap Result";
            this.WristStrapResult.Name = "WristStrapResult";
            this.WristStrapResult.ReadOnly = true;
            this.WristStrapResult.Width = 126;
            // 
            // ConductivityEvaluation
            // 
            this.ConductivityEvaluation.DataPropertyName = "ConductivityEvaluation";
            dataGridViewCellStyle190.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ConductivityEvaluation.DefaultCellStyle = dataGridViewCellStyle190;
            this.ConductivityEvaluation.HeaderText = "Conductivity Evaluation";
            this.ConductivityEvaluation.Name = "ConductivityEvaluation";
            this.ConductivityEvaluation.ReadOnly = true;
            this.ConductivityEvaluation.Width = 154;
            // 
            // LowerEvaluationLimit
            // 
            this.LowerEvaluationLimit.DataPropertyName = "LowerEvaluationLimit";
            dataGridViewCellStyle191.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.LowerEvaluationLimit.DefaultCellStyle = dataGridViewCellStyle191;
            this.LowerEvaluationLimit.HeaderText = "Lower Evaluation Limit";
            this.LowerEvaluationLimit.Name = "LowerEvaluationLimit";
            this.LowerEvaluationLimit.ReadOnly = true;
            this.LowerEvaluationLimit.Width = 150;
            // 
            // UpperEvaluationLimit
            // 
            this.UpperEvaluationLimit.DataPropertyName = "UpperEvaluationLimit";
            dataGridViewCellStyle192.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.UpperEvaluationLimit.DefaultCellStyle = dataGridViewCellStyle192;
            this.UpperEvaluationLimit.HeaderText = "Upper Evaluation Limit";
            this.UpperEvaluationLimit.Name = "UpperEvaluationLimit";
            this.UpperEvaluationLimit.ReadOnly = true;
            this.UpperEvaluationLimit.Width = 150;
            // 
            // EvaluationBuzzer
            // 
            this.EvaluationBuzzer.DataPropertyName = "EvaluationBuzzer";
            dataGridViewCellStyle193.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.EvaluationBuzzer.DefaultCellStyle = dataGridViewCellStyle193;
            this.EvaluationBuzzer.HeaderText = "Evaluation Buzzer";
            this.EvaluationBuzzer.Name = "EvaluationBuzzer";
            this.EvaluationBuzzer.ReadOnly = true;
            this.EvaluationBuzzer.Width = 126;
            // 
            // EvaluationExternalOutput
            // 
            this.EvaluationExternalOutput.DataPropertyName = "EvaluationExternalOutput";
            dataGridViewCellStyle194.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.EvaluationExternalOutput.DefaultCellStyle = dataGridViewCellStyle194;
            this.EvaluationExternalOutput.HeaderText = "Evaluation External Output";
            this.EvaluationExternalOutput.Name = "EvaluationExternalOutput";
            this.EvaluationExternalOutput.ReadOnly = true;
            this.EvaluationExternalOutput.Width = 136;
            // 
            // FG470
            // 
            this.FG470.DataPropertyName = "FG470";
            dataGridViewCellStyle195.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FG470.DefaultCellStyle = dataGridViewCellStyle195;
            this.FG470.HeaderText = "FG470";
            this.FG470.Name = "FG470";
            this.FG470.ReadOnly = true;
            this.FG470.Width = 76;
            // 
            // Note
            // 
            this.Note.DataPropertyName = "Note";
            dataGridViewCellStyle196.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Note.DefaultCellStyle = dataGridViewCellStyle196;
            this.Note.HeaderText = "Note";
            this.Note.Name = "Note";
            this.Note.ReadOnly = true;
            this.Note.Width = 70;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(966, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 40);
            this.button1.TabIndex = 4;
            this.button1.Text = "Export Analysis";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Startup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 749);
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
        private System.Windows.Forms.Button button1;
    }
}

