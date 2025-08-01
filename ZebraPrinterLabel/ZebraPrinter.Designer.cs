namespace ZebraPrinterLabel
{
    partial class ZebraPrinter
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
            this.Ambassador = new System.Windows.Forms.TextBox();
            this.WarehouseText = new System.Windows.Forms.Label();
            this.QuantityText = new System.Windows.Forms.Label();
            this.Printbtn = new System.Windows.Forms.Button();
            this.PrintCount = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.PrintCount)).BeginInit();
            this.SuspendLayout();
            // 
            // Ambassador
            // 
            this.Ambassador.Location = new System.Drawing.Point(23, 39);
            this.Ambassador.Name = "Ambassador";
            this.Ambassador.Size = new System.Drawing.Size(218, 20);
            this.Ambassador.TabIndex = 0;
            this.Ambassador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchAmbassador);
            // 
            // WarehouseText
            // 
            this.WarehouseText.AutoSize = true;
            this.WarehouseText.Location = new System.Drawing.Point(270, 39);
            this.WarehouseText.Name = "WarehouseText";
            this.WarehouseText.Size = new System.Drawing.Size(63, 13);
            this.WarehouseText.TabIndex = 1;
            this.WarehouseText.Text = "sasdasdasd";
            // 
            // QuantityText
            // 
            this.QuantityText.AutoSize = true;
            this.QuantityText.Location = new System.Drawing.Point(515, 39);
            this.QuantityText.Name = "QuantityText";
            this.QuantityText.Size = new System.Drawing.Size(59, 13);
            this.QuantityText.TabIndex = 2;
            this.QuantityText.Text = "adsadsada";
            // 
            // Printbtn
            // 
            this.Printbtn.Location = new System.Drawing.Point(642, 379);
            this.Printbtn.Name = "Printbtn";
            this.Printbtn.Size = new System.Drawing.Size(126, 41);
            this.Printbtn.TabIndex = 3;
            this.Printbtn.Text = "Print";
            this.Printbtn.UseVisualStyleBackColor = true;
            this.Printbtn.Click += new System.EventHandler(this.Printbtn_Click);
            // 
            // PrintCount
            // 
            this.PrintCount.Location = new System.Drawing.Point(490, 391);
            this.PrintCount.Name = "PrintCount";
            this.PrintCount.Size = new System.Drawing.Size(120, 20);
            this.PrintCount.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(23, 128);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(745, 227);
            this.panel1.TabIndex = 5;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(23, 83);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(114, 20);
            this.dateTimePicker1.TabIndex = 6;
            // 
            // ZebraPrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PrintCount);
            this.Controls.Add(this.Printbtn);
            this.Controls.Add(this.QuantityText);
            this.Controls.Add(this.WarehouseText);
            this.Controls.Add(this.Ambassador);
            this.Name = "ZebraPrinter";
            this.Text = "ZebraPrinter";
            ((System.ComponentModel.ISupportInitialize)(this.PrintCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Ambassador;
        private System.Windows.Forms.Label WarehouseText;
        private System.Windows.Forms.Label QuantityText;
        private System.Windows.Forms.Button Printbtn;
        private System.Windows.Forms.NumericUpDown PrintCount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}