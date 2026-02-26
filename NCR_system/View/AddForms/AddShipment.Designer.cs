namespace NCR_system.View.AddForms
{
    partial class AddShipment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddShipment));
            this.RegNoText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DateissuedText = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.ModelText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sectionbox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Issuedbox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.QuanText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DateRegText = new System.Windows.Forms.DateTimePicker();
            this.ContentText = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Save_btn = new System.Windows.Forms.Button();
            this.Cancel_btn = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.StatsText = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.AddImagebtn = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RegNoText
            // 
            this.RegNoText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.RegNoText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RegNoText.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegNoText.Location = new System.Drawing.Point(33, 141);
            this.RegNoText.Multiline = true;
            this.RegNoText.Name = "RegNoText";
            this.RegNoText.Size = new System.Drawing.Size(347, 41);
            this.RegNoText.TabIndex = 72;
            this.RegNoText.TabStop = false;
            this.RegNoText.Click += new System.EventHandler(this.RegNoText_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(32, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 17);
            this.label5.TabIndex = 71;
            this.label5.Text = "REGISTRATION NO :";
            // 
            // DateissuedText
            // 
            this.DateissuedText.Location = new System.Drawing.Point(435, 303);
            this.DateissuedText.Name = "DateissuedText";
            this.DateissuedText.Size = new System.Drawing.Size(347, 20);
            this.DateissuedText.TabIndex = 73;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(432, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 74;
            this.label1.Text = "DATE ISSUED : ";
            // 
            // ModelText
            // 
            this.ModelText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ModelText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModelText.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModelText.Location = new System.Drawing.Point(435, 141);
            this.ModelText.Multiline = true;
            this.ModelText.Name = "ModelText";
            this.ModelText.Size = new System.Drawing.Size(347, 41);
            this.ModelText.TabIndex = 76;
            this.ModelText.Click += new System.EventHandler(this.ModelText_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(432, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 17);
            this.label2.TabIndex = 75;
            this.label2.Text = "MODEL NO / PART NO :";
            // 
            // sectionbox
            // 
            this.sectionbox.FormattingEnabled = true;
            this.sectionbox.Items.AddRange(new object[] {
            "Molding",
            "Press",
            "Rotor",
            "Winding",
            "Circuit"});
            this.sectionbox.Location = new System.Drawing.Point(435, 232);
            this.sectionbox.Name = "sectionbox";
            this.sectionbox.Size = new System.Drawing.Size(347, 21);
            this.sectionbox.TabIndex = 82;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(432, 199);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(137, 17);
            this.label3.TabIndex = 81;
            this.label3.Text = "SECTION INCHARGE :";
            // 
            // Issuedbox
            // 
            this.Issuedbox.FormattingEnabled = true;
            this.Issuedbox.IntegralHeight = false;
            this.Issuedbox.Items.AddRange(new object[] {
            "PCI-PLANNING",
            "PC1",
            "P1FA"});
            this.Issuedbox.Location = new System.Drawing.Point(33, 232);
            this.Issuedbox.Name = "Issuedbox";
            this.Issuedbox.Size = new System.Drawing.Size(347, 21);
            this.Issuedbox.TabIndex = 84;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(30, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 17);
            this.label4.TabIndex = 83;
            this.label4.Text = "ISSUING SECTION : ";
            // 
            // QuanText
            // 
            this.QuanText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.QuanText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.QuanText.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuanText.Location = new System.Drawing.Point(37, 511);
            this.QuanText.Multiline = true;
            this.QuanText.Name = "QuanText";
            this.QuanText.Size = new System.Drawing.Size(343, 41);
            this.QuanText.TabIndex = 86;
            this.QuanText.Click += new System.EventHandler(this.QuanText_Click);
            this.QuanText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.QuanText_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(34, 483);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 17);
            this.label6.TabIndex = 85;
            this.label6.Text = "QUANTITY : ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(30, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 17);
            this.label7.TabIndex = 88;
            this.label7.Text = "DATE REGISTERED : ";
            // 
            // DateRegText
            // 
            this.DateRegText.Location = new System.Drawing.Point(33, 303);
            this.DateRegText.Name = "DateRegText";
            this.DateRegText.Size = new System.Drawing.Size(347, 20);
            this.DateRegText.TabIndex = 87;
            // 
            // ContentText
            // 
            this.ContentText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContentText.Location = new System.Drawing.Point(35, 597);
            this.ContentText.Name = "ContentText";
            this.ContentText.Size = new System.Drawing.Size(747, 106);
            this.ContentText.TabIndex = 90;
            this.ContentText.Text = "";
            this.ContentText.Click += new System.EventHandler(this.ContentText_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(32, 568);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 17);
            this.label8.TabIndex = 89;
            this.label8.Text = "CONTENTS : ";
            // 
            // Save_btn
            // 
            this.Save_btn.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Save_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Save_btn.FlatAppearance.BorderSize = 0;
            this.Save_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save_btn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Save_btn.ForeColor = System.Drawing.Color.White;
            this.Save_btn.Image = ((System.Drawing.Image)(resources.GetObject("Save_btn.Image")));
            this.Save_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Save_btn.Location = new System.Drawing.Point(600, 727);
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.Padding = new System.Windows.Forms.Padding(45, 0, 50, 0);
            this.Save_btn.Size = new System.Drawing.Size(180, 43);
            this.Save_btn.TabIndex = 92;
            this.Save_btn.Text = "Save";
            this.Save_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Save_btn.UseVisualStyleBackColor = false;
            this.Save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // Cancel_btn
            // 
            this.Cancel_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cancel_btn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancel_btn.Location = new System.Drawing.Point(405, 727);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(178, 43);
            this.Cancel_btn.TabIndex = 91;
            this.Cancel_btn.Text = "Cancel";
            this.Cancel_btn.UseVisualStyleBackColor = true;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(32, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(172, 25);
            this.label9.TabIndex = 93;
            this.label9.Text = "Shipment Delay";
            // 
            // StatsText
            // 
            this.StatsText.FormattingEnabled = true;
            this.StatsText.Items.AddRange(new object[] {
            "OPEN",
            "CLOSED",
            "Report OK",
            "For Circulation"});
            this.StatsText.Location = new System.Drawing.Point(435, 511);
            this.StatsText.Name = "StatsText";
            this.StatsText.Size = new System.Drawing.Size(345, 21);
            this.StatsText.TabIndex = 95;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(432, 483);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 17);
            this.label10.TabIndex = 94;
            this.label10.Text = "STATUS : ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(435, 374);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(345, 90);
            this.pictureBox1.TabIndex = 96;
            this.pictureBox1.TabStop = false;
            // 
            // AddImagebtn
            // 
            this.AddImagebtn.Location = new System.Drawing.Point(37, 374);
            this.AddImagebtn.Name = "AddImagebtn";
            this.AddImagebtn.Size = new System.Drawing.Size(343, 90);
            this.AddImagebtn.TabIndex = 97;
            this.AddImagebtn.Text = "Add Image";
            this.AddImagebtn.UseVisualStyleBackColor = true;
            this.AddImagebtn.Click += new System.EventHandler(this.AddImagebtn_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(32, 342);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 17);
            this.label11.TabIndex = 98;
            this.label11.Text = "UPLOAD IMAGES";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(71)))));
            this.panel1.Controls.Add(this.label9);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 78);
            this.panel1.TabIndex = 99;
            // 
            // AddShipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 809);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.AddImagebtn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.StatsText);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Save_btn);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.ContentText);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DateRegText);
            this.Controls.Add(this.QuanText);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Issuedbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sectionbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ModelText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DateissuedText);
            this.Controls.Add(this.RegNoText);
            this.Controls.Add(this.label5);
            this.Name = "AddShipment";
            this.Text = "-";
            this.Load += new System.EventHandler(this.AddShipment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox RegNoText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker DateissuedText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ModelText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox sectionbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Issuedbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox QuanText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker DateRegText;
        private System.Windows.Forms.RichTextBox ContentText;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button Save_btn;
        private System.Windows.Forms.Button Cancel_btn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox StatsText;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button AddImagebtn;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
    }
}