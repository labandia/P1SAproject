namespace NCR_system.View.Details
{
    partial class CustomerDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerDetails));
            this.EditNGText = new System.Windows.Forms.TextBox();
            this.EditCustomerText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.EditRegNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.selectDepart = new System.Windows.Forms.ComboBox();
            this.EditProblemText = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.EditLotText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.EditModelText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Deletebtn = new System.Windows.Forms.Button();
            this.Finalizebtn = new System.Windows.Forms.Button();
            this.Save_btn = new System.Windows.Forms.Button();
            this.Cancel_btn = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // EditNGText
            // 
            this.EditNGText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.EditNGText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EditNGText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.EditNGText.Location = new System.Drawing.Point(357, 308);
            this.EditNGText.Multiline = true;
            this.EditNGText.Name = "EditNGText";
            this.EditNGText.Size = new System.Drawing.Size(267, 35);
            this.EditNGText.TabIndex = 172;
            this.EditNGText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditNGText_KeyPress);
            // 
            // EditCustomerText
            // 
            this.EditCustomerText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.EditCustomerText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EditCustomerText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.EditCustomerText.Location = new System.Drawing.Point(357, 121);
            this.EditCustomerText.Multiline = true;
            this.EditCustomerText.Name = "EditCustomerText";
            this.EditCustomerText.Size = new System.Drawing.Size(267, 41);
            this.EditCustomerText.TabIndex = 171;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(354, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 17);
            this.label7.TabIndex = 170;
            this.label7.Text = "Customer Name:";
            // 
            // EditRegNo
            // 
            this.EditRegNo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.EditRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EditRegNo.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.EditRegNo.Location = new System.Drawing.Point(58, 121);
            this.EditRegNo.Multiline = true;
            this.EditRegNo.Name = "EditRegNo";
            this.EditRegNo.Size = new System.Drawing.Size(267, 41);
            this.EditRegNo.TabIndex = 169;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(55, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 17);
            this.label8.TabIndex = 168;
            this.label8.Text = "Registration :";
            // 
            // selectDepart
            // 
            this.selectDepart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectDepart.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.selectDepart.FormattingEnabled = true;
            this.selectDepart.Items.AddRange(new object[] {
            "Select Section",
            "Molding",
            "Press",
            "Rotor",
            "Winding",
            "Circuit"});
            this.selectDepart.Location = new System.Drawing.Point(59, 308);
            this.selectDepart.Name = "selectDepart";
            this.selectDepart.Size = new System.Drawing.Size(267, 29);
            this.selectDepart.TabIndex = 167;
            // 
            // EditProblemText
            // 
            this.EditProblemText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.EditProblemText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EditProblemText.Location = new System.Drawing.Point(54, 392);
            this.EditProblemText.Name = "EditProblemText";
            this.EditProblemText.Size = new System.Drawing.Size(570, 131);
            this.EditProblemText.TabIndex = 166;
            this.EditProblemText.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(51, 363);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 17);
            this.label6.TabIndex = 165;
            this.label6.Text = "Details of Problem :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(354, 278);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 17);
            this.label4.TabIndex = 164;
            this.label4.Text = "NG Quantity :";
            // 
            // EditLotText
            // 
            this.EditLotText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.EditLotText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EditLotText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.EditLotText.Location = new System.Drawing.Point(58, 214);
            this.EditLotText.Multiline = true;
            this.EditLotText.Name = "EditLotText";
            this.EditLotText.Size = new System.Drawing.Size(267, 41);
            this.EditLotText.TabIndex = 163;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(55, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 162;
            this.label2.Text = "Lot No :";
            // 
            // EditModelText
            // 
            this.EditModelText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.EditModelText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EditModelText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.EditModelText.Location = new System.Drawing.Point(357, 214);
            this.EditModelText.Multiline = true;
            this.EditModelText.Name = "EditModelText";
            this.EditModelText.Size = new System.Drawing.Size(267, 41);
            this.EditModelText.TabIndex = 161;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(354, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 17);
            this.label5.TabIndex = 160;
            this.label5.Text = "Model No. / Part No.  :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(56, 278);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 17);
            this.label3.TabIndex = 159;
            this.label3.Text = "Section in Charge :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(698, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 17);
            this.label9.TabIndex = 216;
            this.label9.Text = "Preview Image :";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(1167, 83);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(120, 0, 100, 0);
            this.button2.Size = new System.Drawing.Size(141, 31);
            this.button2.TabIndex = 215;
            this.button2.Text = "UPLOAD IMAGES";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(701, 121);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(607, 232);
            this.pictureBox1.TabIndex = 214;
            this.pictureBox1.TabStop = false;
            // 
            // Deletebtn
            // 
            this.Deletebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Deletebtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Deletebtn.Location = new System.Drawing.Point(54, 575);
            this.Deletebtn.Name = "Deletebtn";
            this.Deletebtn.Size = new System.Drawing.Size(191, 43);
            this.Deletebtn.TabIndex = 223;
            this.Deletebtn.Text = "Delete this Data";
            this.Deletebtn.UseVisualStyleBackColor = true;
            this.Deletebtn.Visible = false;
            this.Deletebtn.Click += new System.EventHandler(this.Deletebtn_Click);
            // 
            // Finalizebtn
            // 
            this.Finalizebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.Finalizebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Finalizebtn.FlatAppearance.BorderSize = 0;
            this.Finalizebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Finalizebtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Finalizebtn.ForeColor = System.Drawing.Color.White;
            this.Finalizebtn.Image = ((System.Drawing.Image)(resources.GetObject("Finalizebtn.Image")));
            this.Finalizebtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Finalizebtn.Location = new System.Drawing.Point(1087, 575);
            this.Finalizebtn.Name = "Finalizebtn";
            this.Finalizebtn.Padding = new System.Windows.Forms.Padding(45, 0, 50, 0);
            this.Finalizebtn.Size = new System.Drawing.Size(221, 43);
            this.Finalizebtn.TabIndex = 220;
            this.Finalizebtn.Text = "Enable Edit";
            this.Finalizebtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Finalizebtn.UseVisualStyleBackColor = false;
            this.Finalizebtn.Click += new System.EventHandler(this.Finalizebtn_Click);
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
            this.Save_btn.Location = new System.Drawing.Point(1128, 575);
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.Padding = new System.Windows.Forms.Padding(45, 0, 50, 0);
            this.Save_btn.Size = new System.Drawing.Size(180, 43);
            this.Save_btn.TabIndex = 221;
            this.Save_btn.Text = "Save";
            this.Save_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Save_btn.UseVisualStyleBackColor = false;
            this.Save_btn.Visible = false;
            this.Save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // Cancel_btn
            // 
            this.Cancel_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cancel_btn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancel_btn.Location = new System.Drawing.Point(932, 575);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(178, 43);
            this.Cancel_btn.TabIndex = 222;
            this.Cancel_btn.Text = "Cancel";
            this.Cancel_btn.UseVisualStyleBackColor = true;
            this.Cancel_btn.Visible = false;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button12.FlatAppearance.BorderSize = 0;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.Image = ((System.Drawing.Image)(resources.GetObject("button12.Image")));
            this.button12.Location = new System.Drawing.Point(1253, 21);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(55, 46);
            this.button12.TabIndex = 224;
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // CustomerDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1383, 672);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.Deletebtn);
            this.Controls.Add(this.Finalizebtn);
            this.Controls.Add(this.Save_btn);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.EditNGText);
            this.Controls.Add(this.EditCustomerText);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.EditRegNo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.selectDepart);
            this.Controls.Add(this.EditProblemText);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EditLotText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EditModelText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Name = "CustomerDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerDetails";
            this.Load += new System.EventHandler(this.CustomerDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox EditNGText;
        private System.Windows.Forms.TextBox EditCustomerText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox EditRegNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox selectDepart;
        private System.Windows.Forms.RichTextBox EditProblemText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox EditLotText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox EditModelText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Deletebtn;
        private System.Windows.Forms.Button Finalizebtn;
        private System.Windows.Forms.Button Save_btn;
        private System.Windows.Forms.Button Cancel_btn;
        private System.Windows.Forms.Button button12;
    }
}