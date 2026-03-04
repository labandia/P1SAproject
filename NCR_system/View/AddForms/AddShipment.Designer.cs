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
            this.label9 = new System.Windows.Forms.Label();
            this.StatsText = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button12 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // RegNoText
            // 
            this.RegNoText.BackColor = System.Drawing.Color.Gainsboro;
            this.RegNoText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RegNoText.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegNoText.Location = new System.Drawing.Point(65, 168);
            this.RegNoText.Multiline = true;
            this.RegNoText.Name = "RegNoText";
            this.RegNoText.Size = new System.Drawing.Size(267, 41);
            this.RegNoText.TabIndex = 72;
            this.RegNoText.TabStop = false;
            this.RegNoText.Text = "asda";
            this.RegNoText.Click += new System.EventHandler(this.RegNoText_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(64, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 17);
            this.label5.TabIndex = 71;
            this.label5.Text = "REGISTRATION NO :";
            // 
            // DateissuedText
            // 
            this.DateissuedText.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.DateissuedText.Location = new System.Drawing.Point(366, 337);
            this.DateissuedText.Name = "DateissuedText";
            this.DateissuedText.Size = new System.Drawing.Size(267, 27);
            this.DateissuedText.TabIndex = 73;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(363, 305);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 74;
            this.label1.Text = "DATE ISSUED : ";
            // 
            // ModelText
            // 
            this.ModelText.BackColor = System.Drawing.Color.Gainsboro;
            this.ModelText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModelText.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModelText.Location = new System.Drawing.Point(366, 168);
            this.ModelText.Multiline = true;
            this.ModelText.Name = "ModelText";
            this.ModelText.Size = new System.Drawing.Size(267, 41);
            this.ModelText.TabIndex = 76;
            this.ModelText.Click += new System.EventHandler(this.ModelText_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(363, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 17);
            this.label2.TabIndex = 75;
            this.label2.Text = "MODEL NO / PART NO :";
            // 
            // sectionbox
            // 
            this.sectionbox.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.sectionbox.FormattingEnabled = true;
            this.sectionbox.Items.AddRange(new object[] {
            "Molding",
            "Press",
            "Rotor",
            "Winding",
            "Circuit"});
            this.sectionbox.Location = new System.Drawing.Point(366, 259);
            this.sectionbox.Name = "sectionbox";
            this.sectionbox.Size = new System.Drawing.Size(267, 29);
            this.sectionbox.TabIndex = 82;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(363, 231);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(137, 17);
            this.label3.TabIndex = 81;
            this.label3.Text = "SECTION INCHARGE :";
            // 
            // Issuedbox
            // 
            this.Issuedbox.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.Issuedbox.FormattingEnabled = true;
            this.Issuedbox.IntegralHeight = false;
            this.Issuedbox.Items.AddRange(new object[] {
            "PCI-PLANNING",
            "PC1",
            "P1FA"});
            this.Issuedbox.Location = new System.Drawing.Point(65, 259);
            this.Issuedbox.Name = "Issuedbox";
            this.Issuedbox.Size = new System.Drawing.Size(267, 29);
            this.Issuedbox.TabIndex = 84;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(62, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 17);
            this.label4.TabIndex = 83;
            this.label4.Text = "ISSUING SECTION : ";
            // 
            // QuanText
            // 
            this.QuanText.BackColor = System.Drawing.Color.Gainsboro;
            this.QuanText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.QuanText.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuanText.Location = new System.Drawing.Point(67, 417);
            this.QuanText.Multiline = true;
            this.QuanText.Name = "QuanText";
            this.QuanText.Size = new System.Drawing.Size(267, 41);
            this.QuanText.TabIndex = 86;
            this.QuanText.Click += new System.EventHandler(this.QuanText_Click);
            this.QuanText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.QuanText_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(64, 389);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 17);
            this.label6.TabIndex = 85;
            this.label6.Text = "QUANTITY : ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(62, 305);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 17);
            this.label7.TabIndex = 88;
            this.label7.Text = "DATE REGISTERED : ";
            // 
            // DateRegText
            // 
            this.DateRegText.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.DateRegText.Location = new System.Drawing.Point(65, 337);
            this.DateRegText.Name = "DateRegText";
            this.DateRegText.Size = new System.Drawing.Size(267, 27);
            this.DateRegText.TabIndex = 87;
            // 
            // ContentText
            // 
            this.ContentText.BackColor = System.Drawing.Color.Gainsboro;
            this.ContentText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContentText.Location = new System.Drawing.Point(67, 513);
            this.ContentText.Name = "ContentText";
            this.ContentText.Size = new System.Drawing.Size(566, 106);
            this.ContentText.TabIndex = 90;
            this.ContentText.Text = "";
            this.ContentText.Click += new System.EventHandler(this.ContentText_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(64, 484);
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
            this.Save_btn.Location = new System.Drawing.Point(808, 565);
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.Padding = new System.Windows.Forms.Padding(200, 0, 170, 0);
            this.Save_btn.Size = new System.Drawing.Size(452, 54);
            this.Save_btn.TabIndex = 92;
            this.Save_btn.Text = "Save";
            this.Save_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Save_btn.UseVisualStyleBackColor = false;
            this.Save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(60, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(247, 25);
            this.label9.TabIndex = 93;
            this.label9.Text = "Create Shipment Delay";
            // 
            // StatsText
            // 
            this.StatsText.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.StatsText.FormattingEnabled = true;
            this.StatsText.Items.AddRange(new object[] {
            "OPEN",
            "CLOSED",
            "Report OK",
            "For Circulation"});
            this.StatsText.Location = new System.Drawing.Point(364, 417);
            this.StatsText.Name = "StatsText";
            this.StatsText.Size = new System.Drawing.Size(267, 29);
            this.StatsText.TabIndex = 95;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(361, 389);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 17);
            this.label10.TabIndex = 94;
            this.label10.Text = "STATUS : ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(697, 140);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 17);
            this.label11.TabIndex = 98;
            this.label11.Text = "UPLOAD IMAGES";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(71)))));
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.button12);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1366, 86);
            this.panel1.TabIndex = 99;
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button12.FlatAppearance.BorderSize = 0;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.Image = ((System.Drawing.Image)(resources.GetObject("button12.Image")));
            this.button12.Location = new System.Drawing.Point(1245, 21);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(55, 46);
            this.button12.TabIndex = 156;
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label12.Location = new System.Drawing.Point(65, 50);
            this.label12.Name = "label12";
            this.label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label12.Size = new System.Drawing.Size(248, 17);
            this.label12.TabIndex = 100;
            this.label12.Text = "Provide details about the Shipment Delay";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(700, 166);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(120, 0, 100, 0);
            this.button1.Size = new System.Drawing.Size(392, 101);
            this.button1.TabIndex = 148;
            this.button1.Text = "UPLOAD IMAGES";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(1117, 140);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(115, 17);
            this.label13.TabIndex = 160;
            this.label13.Text = "PREVIEW IMAGE :";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.ErrorImage")));
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(1111, 166);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(172, 101);
            this.pictureBox2.TabIndex = 159;
            this.pictureBox2.TabStop = false;
            // 
            // AddShipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1366, 729);
            this.ControlBox = false;
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.StatsText);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Save_btn);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox StatsText;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}