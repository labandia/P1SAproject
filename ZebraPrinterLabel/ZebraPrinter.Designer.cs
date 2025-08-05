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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZebraPrinter));
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.printerStats = new System.Windows.Forms.Label();
            this.Cancelbtn = new System.Windows.Forms.Button();
            this.part_error = new System.Windows.Forms.Label();
            this.Numb_error = new System.Windows.Forms.Label();
            this.Exitbtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.FinalRealIDText = new System.Windows.Forms.Label();
            this.flowLayoutPanelPreview = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.PrintCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanelPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // Ambassador
            // 
            this.Ambassador.BackColor = System.Drawing.Color.Gainsboro;
            this.Ambassador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Ambassador.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ambassador.Location = new System.Drawing.Point(45, 144);
            this.Ambassador.Multiline = true;
            this.Ambassador.Name = "Ambassador";
            this.Ambassador.Size = new System.Drawing.Size(371, 41);
            this.Ambassador.TabIndex = 0;
            this.Ambassador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchAmbassador);
            // 
            // WarehouseText
            // 
            this.WarehouseText.AutoSize = true;
            this.WarehouseText.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WarehouseText.Location = new System.Drawing.Point(42, 241);
            this.WarehouseText.Name = "WarehouseText";
            this.WarehouseText.Size = new System.Drawing.Size(42, 21);
            this.WarehouseText.TabIndex = 1;
            this.WarehouseText.Text = "N/A";
            // 
            // QuantityText
            // 
            this.QuantityText.AutoSize = true;
            this.QuantityText.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityText.Location = new System.Drawing.Point(42, 319);
            this.QuantityText.Name = "QuantityText";
            this.QuantityText.Size = new System.Drawing.Size(42, 21);
            this.QuantityText.TabIndex = 2;
            this.QuantityText.Text = "N/A";
            // 
            // Printbtn
            // 
            this.Printbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(152)))), ((int)(((byte)(126)))));
            this.Printbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Printbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(152)))), ((int)(((byte)(126)))));
            this.Printbtn.FlatAppearance.BorderSize = 0;
            this.Printbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Printbtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Printbtn.ForeColor = System.Drawing.Color.White;
            this.Printbtn.Image = ((System.Drawing.Image)(resources.GetObject("Printbtn.Image")));
            this.Printbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Printbtn.Location = new System.Drawing.Point(249, 629);
            this.Printbtn.Name = "Printbtn";
            this.Printbtn.Padding = new System.Windows.Forms.Padding(35, 0, 40, 0);
            this.Printbtn.Size = new System.Drawing.Size(160, 50);
            this.Printbtn.TabIndex = 3;
            this.Printbtn.Text = "Print";
            this.Printbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Printbtn.UseVisualStyleBackColor = false;
            this.Printbtn.Visible = false;
            this.Printbtn.Click += new System.EventHandler(this.Printbtn_Click);
            // 
            // PrintCount
            // 
            this.PrintCount.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintCount.Location = new System.Drawing.Point(48, 500);
            this.PrintCount.Name = "PrintCount";
            this.PrintCount.Size = new System.Drawing.Size(361, 27);
            this.PrintCount.TabIndex = 4;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(48, 403);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(361, 27);
            this.dateTimePicker1.TabIndex = 6;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // panelPreview
            // 
            this.panelPreview.BackColor = System.Drawing.Color.Silver;
            this.panelPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPreview.Location = new System.Drawing.Point(0, 0);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(439, 599);
            this.panelPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.panelPreview.TabIndex = 7;
            this.panelPreview.TabStop = false;
            this.panelPreview.Click += new System.EventHandler(this.panelPreview_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(41, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Warehouse Location :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(42, 293);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Quantity :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(44, 463);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Number of Prints :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(44, 371);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Set Date :";
            // 
            // Preview
            // 
            this.Preview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Preview.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Preview.Image = ((System.Drawing.Image)(resources.GetObject("Preview.Image")));
            this.Preview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Preview.Location = new System.Drawing.Point(56, 605);
            this.Preview.Name = "Preview";
            this.Preview.Padding = new System.Windows.Forms.Padding(120, 0, 130, 0);
            this.Preview.Size = new System.Drawing.Size(362, 50);
            this.Preview.TabIndex = 13;
            this.Preview.Text = "Print view";
            this.Preview.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Preview.UseVisualStyleBackColor = true;
            this.Preview.Click += new System.EventHandler(this.Preview_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(41, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "Enter Partnum:";
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(23)))), ((int)(((byte)(30)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1018, 75);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(23)))), ((int)(((byte)(30)))));
            this.label6.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(41, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(280, 23);
            this.label6.TabIndex = 17;
            this.label6.Text = "Program PartList Printer  Label";
            // 
            // printerStats
            // 
            this.printerStats.AutoSize = true;
            this.printerStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(23)))), ((int)(((byte)(30)))));
            this.printerStats.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printerStats.ForeColor = System.Drawing.Color.White;
            this.printerStats.Location = new System.Drawing.Point(703, 29);
            this.printerStats.Name = "printerStats";
            this.printerStats.Size = new System.Drawing.Size(220, 20);
            this.printerStats.TabIndex = 18;
            this.printerStats.Text = "Checking the Printer Status ...";
            // 
            // Cancelbtn
            // 
            this.Cancelbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cancelbtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancelbtn.Image = ((System.Drawing.Image)(resources.GetObject("Cancelbtn.Image")));
            this.Cancelbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Cancelbtn.Location = new System.Drawing.Point(48, 629);
            this.Cancelbtn.Name = "Cancelbtn";
            this.Cancelbtn.Padding = new System.Windows.Forms.Padding(20, 0, 25, 0);
            this.Cancelbtn.Size = new System.Drawing.Size(160, 50);
            this.Cancelbtn.TabIndex = 19;
            this.Cancelbtn.Text = "Cancel";
            this.Cancelbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cancelbtn.UseVisualStyleBackColor = true;
            this.Cancelbtn.Visible = false;
            this.Cancelbtn.Click += new System.EventHandler(this.Cancelbtn_Click);
            // 
            // part_error
            // 
            this.part_error.AutoSize = true;
            this.part_error.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.part_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.part_error.Location = new System.Drawing.Point(239, 115);
            this.part_error.Name = "part_error";
            this.part_error.Size = new System.Drawing.Size(179, 15);
            this.part_error.TabIndex = 20;
            this.part_error.Text = "Part number input is required *";
            this.part_error.Visible = false;
            // 
            // Numb_error
            // 
            this.Numb_error.AutoSize = true;
            this.Numb_error.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Numb_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Numb_error.Location = new System.Drawing.Point(255, 465);
            this.Numb_error.Name = "Numb_error";
            this.Numb_error.Size = new System.Drawing.Size(155, 15);
            this.Numb_error.TabIndex = 21;
            this.Numb_error.Text = "Print Quantity is required *";
            this.Numb_error.Visible = false;
            // 
            // Exitbtn
            // 
            this.Exitbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Exitbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(23)))), ((int)(((byte)(30)))));
            this.Exitbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exitbtn.FlatAppearance.BorderSize = 0;
            this.Exitbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exitbtn.Image = ((System.Drawing.Image)(resources.GetObject("Exitbtn.Image")));
            this.Exitbtn.Location = new System.Drawing.Point(936, 14);
            this.Exitbtn.Name = "Exitbtn";
            this.Exitbtn.Size = new System.Drawing.Size(55, 46);
            this.Exitbtn.TabIndex = 22;
            this.Exitbtn.UseVisualStyleBackColor = false;
            this.Exitbtn.Click += new System.EventHandler(this.Exitbtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(44, 555);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 20);
            this.label7.TabIndex = 24;
            this.label7.Text = "Reel ID:";
            // 
            // FinalRealIDText
            // 
            this.FinalRealIDText.AutoSize = true;
            this.FinalRealIDText.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinalRealIDText.Location = new System.Drawing.Point(44, 581);
            this.FinalRealIDText.Name = "FinalRealIDText";
            this.FinalRealIDText.Size = new System.Drawing.Size(42, 21);
            this.FinalRealIDText.TabIndex = 23;
            this.FinalRealIDText.Text = "N/A";
            // 
            // flowLayoutPanelPreview
            // 
            this.flowLayoutPanelPreview.AutoScroll = true;
            this.flowLayoutPanelPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelPreview.Controls.Add(this.panelPreview);
            this.flowLayoutPanelPreview.Location = new System.Drawing.Point(498, 93);
            this.flowLayoutPanelPreview.Name = "flowLayoutPanelPreview";
            this.flowLayoutPanelPreview.Size = new System.Drawing.Size(441, 601);
            this.flowLayoutPanelPreview.TabIndex = 25;
            // 
            // ZebraPrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1018, 728);
            this.Controls.Add(this.flowLayoutPanelPreview);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.FinalRealIDText);
            this.Controls.Add(this.Exitbtn);
            this.Controls.Add(this.Numb_error);
            this.Controls.Add(this.part_error);
            this.Controls.Add(this.Preview);
            this.Controls.Add(this.Cancelbtn);
            this.Controls.Add(this.printerStats);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.PrintCount);
            this.Controls.Add(this.Printbtn);
            this.Controls.Add(this.QuantityText);
            this.Controls.Add(this.WarehouseText);
            this.Controls.Add(this.Ambassador);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ZebraPrinter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZebraPrinter";
            this.Load += new System.EventHandler(this.ZebraPrinter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PrintCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanelPreview.ResumeLayout(false);
            this.flowLayoutPanelPreview.PerformLayout();
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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label printerStats;
        private System.Windows.Forms.Button Cancelbtn;
        private System.Windows.Forms.Label part_error;
        private System.Windows.Forms.Label Numb_error;
        private System.Windows.Forms.Button Exitbtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label FinalRealIDText;
        private System.Windows.Forms.Panel flowLayoutPanelPreview;
    }
}