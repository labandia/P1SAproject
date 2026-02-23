namespace POS_System
{
    partial class SalesHistoryPage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend10 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.productsTable = new System.Windows.Forms.DataGridView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.RevenueText = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Todaybtn = new System.Windows.Forms.Button();
            this.WeekBtn = new System.Windows.Forms.Button();
            this.monthbtn = new System.Windows.Forms.Button();
            this.cmbMonthFilter = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblAverage = new System.Windows.Forms.Label();
            this.lblUnits = new System.Windows.Forms.Label();
            this.lblRevenue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.productsTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // productsTable
            // 
            this.productsTable.AllowUserToAddRows = false;
            this.productsTable.AllowUserToDeleteRows = false;
            this.productsTable.AllowUserToResizeColumns = false;
            this.productsTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle37.BackColor = System.Drawing.Color.WhiteSmoke;
            this.productsTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle37;
            this.productsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.productsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.productsTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle38.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle38.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle38.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle38.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle38.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle38.SelectionForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.productsTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle38;
            this.productsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle39.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle39.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle39.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle39.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle39.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle39.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle39.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.productsTable.DefaultCellStyle = dataGridViewCellStyle39;
            this.productsTable.Location = new System.Drawing.Point(659, 255);
            this.productsTable.Name = "productsTable";
            this.productsTable.ReadOnly = true;
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle40.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle40.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle40.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle40.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle40.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle40.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.productsTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle40;
            this.productsTable.RowHeadersVisible = false;
            this.productsTable.RowTemplate.Height = 35;
            this.productsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.productsTable.Size = new System.Drawing.Size(537, 399);
            this.productsTable.TabIndex = 2;
            // 
            // chart1
            // 
            chartArea10.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea10);
            legend10.Name = "Legend1";
            this.chart1.Legends.Add(legend10);
            this.chart1.Location = new System.Drawing.Point(40, 255);
            this.chart1.Name = "chart1";
            series10.ChartArea = "ChartArea1";
            series10.Legend = "Legend1";
            series10.Name = "Series1";
            this.chart1.Series.Add(series10);
            this.chart1.Size = new System.Drawing.Size(595, 399);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblRevenue);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.RevenueText);
            this.panel1.Location = new System.Drawing.Point(40, 104);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(334, 109);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblAverage);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(448, 104);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(334, 109);
            this.panel2.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblUnits);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(862, 104);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(334, 109);
            this.panel3.TabIndex = 6;
            // 
            // RevenueText
            // 
            this.RevenueText.AutoSize = true;
            this.RevenueText.Location = new System.Drawing.Point(92, 73);
            this.RevenueText.Name = "RevenueText";
            this.RevenueText.Size = new System.Drawing.Size(51, 13);
            this.RevenueText.TabIndex = 0;
            this.RevenueText.Text = "Revenue";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Invoice";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(95, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "units";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(95, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Items sold";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(87, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Transactions";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(92, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Total Sales";
            // 
            // Todaybtn
            // 
            this.Todaybtn.Location = new System.Drawing.Point(786, 49);
            this.Todaybtn.Name = "Todaybtn";
            this.Todaybtn.Size = new System.Drawing.Size(75, 23);
            this.Todaybtn.TabIndex = 7;
            this.Todaybtn.Text = "Today";
            this.Todaybtn.UseVisualStyleBackColor = true;
            this.Todaybtn.Click += new System.EventHandler(this.Todaybtn_Click);
            // 
            // WeekBtn
            // 
            this.WeekBtn.Location = new System.Drawing.Point(876, 49);
            this.WeekBtn.Name = "WeekBtn";
            this.WeekBtn.Size = new System.Drawing.Size(75, 23);
            this.WeekBtn.TabIndex = 8;
            this.WeekBtn.Text = "Weekly";
            this.WeekBtn.UseVisualStyleBackColor = true;
            this.WeekBtn.Click += new System.EventHandler(this.WeekBtn_Click);
            // 
            // monthbtn
            // 
            this.monthbtn.Location = new System.Drawing.Point(974, 49);
            this.monthbtn.Name = "monthbtn";
            this.monthbtn.Size = new System.Drawing.Size(75, 23);
            this.monthbtn.TabIndex = 9;
            this.monthbtn.Text = "Monthly";
            this.monthbtn.UseVisualStyleBackColor = true;
            this.monthbtn.Click += new System.EventHandler(this.monthbtn_Click);
            // 
            // cmbMonthFilter
            // 
            this.cmbMonthFilter.FormattingEnabled = true;
            this.cmbMonthFilter.Location = new System.Drawing.Point(1065, 51);
            this.cmbMonthFilter.Name = "cmbMonthFilter";
            this.cmbMonthFilter.Size = new System.Drawing.Size(121, 21);
            this.cmbMonthFilter.TabIndex = 10;
            this.cmbMonthFilter.SelectedIndexChanged += new System.EventHandler(this.cmbMonthFilter_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(33, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(212, 37);
            this.label7.TabIndex = 6;
            this.label7.Text = "Sales Summary";
            // 
            // lblAverage
            // 
            this.lblAverage.AutoSize = true;
            this.lblAverage.Font = new System.Drawing.Font("Century Gothic", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAverage.Location = new System.Drawing.Point(83, 33);
            this.lblAverage.Name = "lblAverage";
            this.lblAverage.Size = new System.Drawing.Size(38, 41);
            this.lblAverage.TabIndex = 5;
            this.lblAverage.Text = "0";
            // 
            // lblUnits
            // 
            this.lblUnits.AutoSize = true;
            this.lblUnits.Font = new System.Drawing.Font("Century Gothic", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnits.Location = new System.Drawing.Point(91, 32);
            this.lblUnits.Name = "lblUnits";
            this.lblUnits.Size = new System.Drawing.Size(38, 41);
            this.lblUnits.TabIndex = 6;
            this.lblUnits.Text = "0";
            // 
            // lblRevenue
            // 
            this.lblRevenue.AutoSize = true;
            this.lblRevenue.Font = new System.Drawing.Font("Century Gothic", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRevenue.Location = new System.Drawing.Point(89, 32);
            this.lblRevenue.Name = "lblRevenue";
            this.lblRevenue.Size = new System.Drawing.Size(38, 41);
            this.lblRevenue.TabIndex = 6;
            this.lblRevenue.Text = "0";
            // 
            // SalesHistoryPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 749);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbMonthFilter);
            this.Controls.Add(this.monthbtn);
            this.Controls.Add(this.WeekBtn);
            this.Controls.Add(this.Todaybtn);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.productsTable);
            this.Name = "SalesHistoryPage";
            this.Text = "SalesHistoryPage";
            this.Load += new System.EventHandler(this.SalesHistoryPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.productsTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView productsTable;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label RevenueText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Todaybtn;
        private System.Windows.Forms.Button WeekBtn;
        private System.Windows.Forms.Button monthbtn;
        private System.Windows.Forms.ComboBox cmbMonthFilter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblAverage;
        private System.Windows.Forms.Label lblRevenue;
        private System.Windows.Forms.Label lblUnits;
    }
}