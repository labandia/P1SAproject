namespace NCR_system.View.Module
{
    partial class FirstBatchCharts
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.ShipmentChart = new LiveCharts.WinForms.CartesianChart();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rejectedChart = new LiveCharts.WinForms.CartesianChart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.customerChart = new LiveCharts.WinForms.CartesianChart();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.inprocessChart = new LiveCharts.WinForms.CartesianChart();
            this.cartesianChart5 = new LiveCharts.WinForms.CartesianChart();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ShipmentChart);
            this.panel4.Location = new System.Drawing.Point(670, 308);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(541, 234);
            this.panel4.TabIndex = 54;
            // 
            // ShipmentChart
            // 
            this.ShipmentChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShipmentChart.Location = new System.Drawing.Point(0, 0);
            this.ShipmentChart.Name = "ShipmentChart";
            this.ShipmentChart.Size = new System.Drawing.Size(541, 234);
            this.ShipmentChart.TabIndex = 0;
            this.ShipmentChart.Text = "cartesianChart2";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rejectedChart);
            this.panel3.Location = new System.Drawing.Point(56, 308);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(541, 234);
            this.panel3.TabIndex = 53;
            // 
            // rejectedChart
            // 
            this.rejectedChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rejectedChart.Location = new System.Drawing.Point(0, 0);
            this.rejectedChart.Name = "rejectedChart";
            this.rejectedChart.Size = new System.Drawing.Size(541, 234);
            this.rejectedChart.TabIndex = 0;
            this.rejectedChart.Text = "cartesianChart2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.customerChart);
            this.panel1.Controls.Add(this.cartesianChart1);
            this.panel1.Location = new System.Drawing.Point(58, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(562, 203);
            this.panel1.TabIndex = 49;
            // 
            // customerChart
            // 
            this.customerChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customerChart.Location = new System.Drawing.Point(0, 0);
            this.customerChart.Name = "customerChart";
            this.customerChart.Size = new System.Drawing.Size(562, 203);
            this.customerChart.TabIndex = 1;
            this.customerChart.Text = "cartesianChart3";
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartesianChart1.Location = new System.Drawing.Point(0, 0);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(562, 203);
            this.cartesianChart1.TabIndex = 0;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(666, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 21);
            this.label5.TabIndex = 48;
            this.label5.Text = "INPROCESS DEFECT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 21);
            this.label1.TabIndex = 47;
            this.label1.Text = "CUSTOMER COMPLAINT";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.inprocessChart);
            this.panel2.Controls.Add(this.cartesianChart5);
            this.panel2.Location = new System.Drawing.Point(661, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(562, 203);
            this.panel2.TabIndex = 50;
            // 
            // inprocessChart
            // 
            this.inprocessChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inprocessChart.Location = new System.Drawing.Point(0, 0);
            this.inprocessChart.Name = "inprocessChart";
            this.inprocessChart.Size = new System.Drawing.Size(562, 203);
            this.inprocessChart.TabIndex = 1;
            this.inprocessChart.Text = "cartesianChart4";
            // 
            // cartesianChart5
            // 
            this.cartesianChart5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartesianChart5.Location = new System.Drawing.Point(0, 0);
            this.cartesianChart5.Name = "cartesianChart5";
            this.cartesianChart5.Size = new System.Drawing.Size(562, 203);
            this.cartesianChart5.TabIndex = 0;
            this.cartesianChart5.Text = "cartesianChart5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(643, 263);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 21);
            this.label4.TabIndex = 52;
            this.label4.Text = "SHIPMENT DELAY";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(52, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 21);
            this.label3.TabIndex = 51;
            this.label3.Text = "REJECTED LOT";
            // 
            // FirstBatchCharts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "FirstBatchCharts";
            this.Size = new System.Drawing.Size(1366, 591);
            this.Load += new System.EventHandler(this.FirstBatchCharts_Load);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private LiveCharts.WinForms.CartesianChart ShipmentChart;
        private System.Windows.Forms.Panel panel3;
        private LiveCharts.WinForms.CartesianChart rejectedChart;
        private System.Windows.Forms.Panel panel1;
        private LiveCharts.WinForms.CartesianChart customerChart;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private LiveCharts.WinForms.CartesianChart inprocessChart;
        private LiveCharts.WinForms.CartesianChart cartesianChart5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}
