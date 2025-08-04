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
            this.components = new System.ComponentModel.Container();
            this.Ambassador = new System.Windows.Forms.TextBox();
            this.WarehouseText = new System.Windows.Forms.Label();
            this.QuantityText = new System.Windows.Forms.Label();
            this.Printbtn = new System.Windows.Forms.Button();
            this.PrintCount = new System.Windows.Forms.NumericUpDown();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.panelPreview = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Preview = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PrintCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // Ambassador
            // 
            this.Ambassador.Location = new System.Drawing.Point(53, 90);
            this.Ambassador.Name = "Ambassador";
            this.Ambassador.Size = new System.Drawing.Size(262, 20);
            this.Ambassador.TabIndex = 0;
            this.Ambassador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchAmbassador);
            // 
            // WarehouseText
            // 
            this.WarehouseText.AutoSize = true;
            this.WarehouseText.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WarehouseText.Location = new System.Drawing.Point(50, 177);
            this.WarehouseText.Name = "WarehouseText";
            this.WarehouseText.Size = new System.Drawing.Size(88, 21);
            this.WarehouseText.TabIndex = 1;
            this.WarehouseText.Text = "-asdassda";
            // 
            // QuantityText
            // 
            this.QuantityText.AutoSize = true;
            this.QuantityText.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityText.Location = new System.Drawing.Point(50, 254);
            this.QuantityText.Name = "QuantityText";
            this.QuantityText.Size = new System.Drawing.Size(71, 21);
            this.QuantityText.TabIndex = 2;
            this.QuantityText.Text = "-asdasd";
            // 
            // Printbtn
            // 
            this.Printbtn.Location = new System.Drawing.Point(961, 39);
            this.Printbtn.Name = "Printbtn";
            this.Printbtn.Size = new System.Drawing.Size(248, 58);
            this.Printbtn.TabIndex = 3;
            this.Printbtn.Text = "Print";
            this.Printbtn.UseVisualStyleBackColor = true;
            this.Printbtn.Click += new System.EventHandler(this.Printbtn_Click);
            // 
            // PrintCount
            // 
            this.PrintCount.Location = new System.Drawing.Point(56, 456);
            this.PrintCount.Name = "PrintCount";
            this.PrintCount.Size = new System.Drawing.Size(120, 20);
            this.PrintCount.TabIndex = 4;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(56, 352);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(114, 20);
            this.dateTimePicker1.TabIndex = 6;
            // 
            // panelPreview
            // 
            this.panelPreview.Location = new System.Drawing.Point(494, 126);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(469, 399);
            this.panelPreview.TabIndex = 7;
            this.panelPreview.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 23);
            this.label1.TabIndex = 9;
            this.label1.Text = "Warehouse Location :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 23);
            this.label2.TabIndex = 10;
            this.label2.Text = "Quantity :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(49, 412);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 23);
            this.label3.TabIndex = 11;
            this.label3.Text = "Number of Prints :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(52, 311);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 23);
            this.label4.TabIndex = 12;
            this.label4.Text = "Set Date :";
            // 
            // Preview
            // 
            this.Preview.Location = new System.Drawing.Point(53, 507);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(248, 58);
            this.Preview.TabIndex = 13;
            this.Preview.Text = "Check";
            this.Preview.UseVisualStyleBackColor = true;
            this.Preview.Click += new System.EventHandler(this.Preview_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(52, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 19);
            this.label5.TabIndex = 15;
            this.label5.Text = "Enter Partnum:";
            // 
            // ZebraPrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1444, 682);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Preview);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.PrintCount);
            this.Controls.Add(this.Printbtn);
            this.Controls.Add(this.QuantityText);
            this.Controls.Add(this.WarehouseText);
            this.Controls.Add(this.Ambassador);
            this.Name = "ZebraPrinter";
            this.Text = "ZebraPrinter";
            this.Load += new System.EventHandler(this.ZebraPrinter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PrintCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Ambassador;
        private System.Windows.Forms.Label WarehouseText;
        private System.Windows.Forms.Label QuantityText;
        private System.Windows.Forms.Button Printbtn;
        private System.Windows.Forms.NumericUpDown PrintCount;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.PictureBox panelPreview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Preview;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer1;
    }
}