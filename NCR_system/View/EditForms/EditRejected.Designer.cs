namespace NCR_system.View.EditForms
{
    partial class EditRejected
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditRejected));
            this.StatsText = new System.Windows.Forms.ComboBox();
            this.Save_btn = new System.Windows.Forms.Button();
            this.ContentText = new System.Windows.Forms.RichTextBox();
            this.DateRegText = new System.Windows.Forms.DateTimePicker();
            this.QuanText = new System.Windows.Forms.TextBox();
            this.Issuedbox = new System.Windows.Forms.ComboBox();
            this.sectionbox = new System.Windows.Forms.ComboBox();
            this.ModelText = new System.Windows.Forms.TextBox();
            this.DateissuedText = new System.Windows.Forms.DateTimePicker();
            this.RegNoText = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button12 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // StatsText
            // 
            this.StatsText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.StatsText.FormattingEnabled = true;
            this.StatsText.Items.AddRange(new object[] {
            "OPEN",
            "CLOSED"});
            this.StatsText.Location = new System.Drawing.Point(115, 505);
            this.StatsText.Name = "StatsText";
            this.StatsText.Size = new System.Drawing.Size(267, 29);
            this.StatsText.TabIndex = 137;
            // 
            // Save_btn
            // 
            this.Save_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.Save_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Save_btn.FlatAppearance.BorderSize = 0;
            this.Save_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save_btn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Save_btn.ForeColor = System.Drawing.Color.White;
            this.Save_btn.Image = ((System.Drawing.Image)(resources.GetObject("Save_btn.Image")));
            this.Save_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Save_btn.Location = new System.Drawing.Point(370, 619);
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.Padding = new System.Windows.Forms.Padding(45, 0, 50, 0);
            this.Save_btn.Size = new System.Drawing.Size(180, 43);
            this.Save_btn.TabIndex = 134;
            this.Save_btn.Text = "Save";
            this.Save_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Save_btn.UseVisualStyleBackColor = false;
            this.Save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // ContentText
            // 
            this.ContentText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ContentText.Location = new System.Drawing.Point(50, 521);
            this.ContentText.Name = "ContentText";
            this.ContentText.Size = new System.Drawing.Size(607, 106);
            this.ContentText.TabIndex = 132;
            this.ContentText.Text = "";
            // 
            // DateRegText
            // 
            this.DateRegText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.DateRegText.Location = new System.Drawing.Point(49, 340);
            this.DateRegText.Name = "DateRegText";
            this.DateRegText.Size = new System.Drawing.Size(267, 29);
            this.DateRegText.TabIndex = 129;
            // 
            // QuanText
            // 
            this.QuanText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.QuanText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.QuanText.Location = new System.Drawing.Point(50, 433);
            this.QuanText.Name = "QuanText";
            this.QuanText.Size = new System.Drawing.Size(267, 29);
            this.QuanText.TabIndex = 128;
            // 
            // Issuedbox
            // 
            this.Issuedbox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Issuedbox.FormattingEnabled = true;
            this.Issuedbox.Items.AddRange(new object[] {
            "QA1-P",
            "P1FA- FA",
            "QA-S"});
            this.Issuedbox.Location = new System.Drawing.Point(49, 248);
            this.Issuedbox.Name = "Issuedbox";
            this.Issuedbox.Size = new System.Drawing.Size(267, 29);
            this.Issuedbox.TabIndex = 126;
            // 
            // sectionbox
            // 
            this.sectionbox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.sectionbox.FormattingEnabled = true;
            this.sectionbox.Items.AddRange(new object[] {
            "Molding",
            "Press",
            "Rotor",
            "Winding",
            "Circuit"});
            this.sectionbox.Location = new System.Drawing.Point(363, 248);
            this.sectionbox.Name = "sectionbox";
            this.sectionbox.Size = new System.Drawing.Size(267, 29);
            this.sectionbox.TabIndex = 124;
            // 
            // ModelText
            // 
            this.ModelText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ModelText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ModelText.Location = new System.Drawing.Point(364, 155);
            this.ModelText.Name = "ModelText";
            this.ModelText.Size = new System.Drawing.Size(267, 29);
            this.ModelText.TabIndex = 122;
            // 
            // DateissuedText
            // 
            this.DateissuedText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.DateissuedText.Location = new System.Drawing.Point(363, 340);
            this.DateissuedText.Name = "DateissuedText";
            this.DateissuedText.Size = new System.Drawing.Size(267, 29);
            this.DateissuedText.TabIndex = 119;
            // 
            // RegNoText
            // 
            this.RegNoText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.RegNoText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.RegNoText.Location = new System.Drawing.Point(49, 155);
            this.RegNoText.Name = "RegNoText";
            this.RegNoText.Size = new System.Drawing.Size(267, 29);
            this.RegNoText.TabIndex = 118;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.button12);
            this.panel1.Controls.Add(this.StatsText);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.Save_btn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(716, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(650, 729);
            this.panel1.TabIndex = 138;
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button12.FlatAppearance.BorderSize = 0;
            this.button12.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button12.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.Image = ((System.Drawing.Image)(resources.GetObject("button12.Image")));
            this.button12.Location = new System.Drawing.Point(512, 44);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(65, 62);
            this.button12.TabIndex = 223;
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(90, 142);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(477, 302);
            this.pictureBox1.TabIndex = 147;
            this.pictureBox1.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label19.Location = new System.Drawing.Point(45, 44);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(233, 30);
            this.label19.TabIndex = 140;
            this.label19.Text = "Details of Rejected Lot";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkGray;
            this.label9.Location = new System.Drawing.Point(49, 78);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(175, 17);
            this.label9.TabIndex = 139;
            this.label9.Text = "Update Rejected lot  Details";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(112, 468);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 17);
            this.label11.TabIndex = 149;
            this.label11.Text = "STATUS :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(46, 489);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 17);
            this.label12.TabIndex = 148;
            this.label12.Text = "CONTENTS : ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(47, 306);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(127, 17);
            this.label13.TabIndex = 147;
            this.label13.Text = "DATE REGISTERED : ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(47, 402);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(83, 17);
            this.label14.TabIndex = 146;
            this.label14.Text = "QUANTITY : ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(47, 217);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(122, 17);
            this.label15.TabIndex = 145;
            this.label15.Text = "ISSUING SECTION :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(360, 217);
            this.label16.Name = "label16";
            this.label16.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label16.Size = new System.Drawing.Size(145, 17);
            this.label16.TabIndex = 144;
            this.label16.Text = "SECTION IN CHARGE : ";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(361, 125);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(144, 17);
            this.label17.TabIndex = 143;
            this.label17.Text = "MOLD NO / PART NO :";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(361, 306);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(93, 17);
            this.label18.TabIndex = 142;
            this.label18.Text = "DATE ISSUED :";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(46, 125);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(128, 17);
            this.label20.TabIndex = 141;
            this.label20.Text = "REGISTRATION NO :";
            // 
            // EditRejected
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1366, 729);
            this.ControlBox = false;
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ContentText);
            this.Controls.Add(this.DateRegText);
            this.Controls.Add(this.QuanText);
            this.Controls.Add(this.Issuedbox);
            this.Controls.Add(this.sectionbox);
            this.Controls.Add(this.ModelText);
            this.Controls.Add(this.DateissuedText);
            this.Controls.Add(this.RegNoText);
            this.Name = "EditRejected";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "-";
            this.Load += new System.EventHandler(this.EditRejected_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox StatsText;
        private System.Windows.Forms.Button Save_btn;
        private System.Windows.Forms.RichTextBox ContentText;
        private System.Windows.Forms.DateTimePicker DateRegText;
        private System.Windows.Forms.TextBox QuanText;
        private System.Windows.Forms.ComboBox Issuedbox;
        private System.Windows.Forms.ComboBox sectionbox;
        private System.Windows.Forms.TextBox ModelText;
        private System.Windows.Forms.DateTimePicker DateissuedText;
        private System.Windows.Forms.TextBox RegNoText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
    }
}