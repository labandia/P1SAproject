namespace NCR_system.View.Details
{
    partial class SDCDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SDCDetails));
            this.NGText = new System.Windows.Forms.TextBox();
            this.selectDepart = new System.Windows.Forms.ComboBox();
            this.ProblemText = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LotText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ModelText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Deletebtn = new System.Windows.Forms.Button();
            this.Finalizebtn = new System.Windows.Forms.Button();
            this.Save_btn = new System.Windows.Forms.Button();
            this.Cancel_btn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // NGText
            // 
            this.NGText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NGText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NGText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.NGText.Location = new System.Drawing.Point(56, 188);
            this.NGText.Multiline = true;
            this.NGText.Name = "NGText";
            this.NGText.Size = new System.Drawing.Size(267, 35);
            this.NGText.TabIndex = 167;
            this.NGText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NGText_KeyPress);
            // 
            // selectDepart
            // 
            this.selectDepart.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectDepart.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.selectDepart.FormattingEnabled = true;
            this.selectDepart.Items.AddRange(new object[] {
            "-- Select Section -- ",
            "Molding",
            "Press",
            "Rotor",
            "Winding",
            "Circuit"});
            this.selectDepart.Location = new System.Drawing.Point(373, 189);
            this.selectDepart.Name = "selectDepart";
            this.selectDepart.Size = new System.Drawing.Size(267, 29);
            this.selectDepart.TabIndex = 166;
            // 
            // ProblemText
            // 
            this.ProblemText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ProblemText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProblemText.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.ProblemText.Location = new System.Drawing.Point(56, 286);
            this.ProblemText.Name = "ProblemText";
            this.ProblemText.Size = new System.Drawing.Size(582, 155);
            this.ProblemText.TabIndex = 165;
            this.ProblemText.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(54, 253);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 17);
            this.label6.TabIndex = 164;
            this.label6.Text = "Details of the problem";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(53, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 17);
            this.label4.TabIndex = 163;
            this.label4.Text = "NG Quantity :";
            // 
            // LotText
            // 
            this.LotText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LotText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LotText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.LotText.Location = new System.Drawing.Point(373, 105);
            this.LotText.Multiline = true;
            this.LotText.Name = "LotText";
            this.LotText.Size = new System.Drawing.Size(267, 35);
            this.LotText.TabIndex = 162;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(370, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 161;
            this.label2.Text = "Lot No :";
            // 
            // ModelText
            // 
            this.ModelText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ModelText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModelText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ModelText.Location = new System.Drawing.Point(57, 105);
            this.ModelText.Multiline = true;
            this.ModelText.Name = "ModelText";
            this.ModelText.Size = new System.Drawing.Size(267, 35);
            this.ModelText.TabIndex = 160;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(54, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 17);
            this.label5.TabIndex = 159;
            this.label5.Text = "Model No / Part No :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(370, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 17);
            this.label3.TabIndex = 158;
            this.label3.Text = "Section in Charge :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(694, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 17);
            this.label9.TabIndex = 213;
            this.label9.Text = "Preview Image :";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(1163, 68);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(120, 0, 100, 0);
            this.button2.Size = new System.Drawing.Size(141, 31);
            this.button2.TabIndex = 212;
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
            this.pictureBox1.Location = new System.Drawing.Point(697, 106);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(607, 232);
            this.pictureBox1.TabIndex = 211;
            this.pictureBox1.TabStop = false;
            // 
            // Deletebtn
            // 
            this.Deletebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Deletebtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Deletebtn.Location = new System.Drawing.Point(56, 488);
            this.Deletebtn.Name = "Deletebtn";
            this.Deletebtn.Size = new System.Drawing.Size(191, 43);
            this.Deletebtn.TabIndex = 219;
            this.Deletebtn.Text = "Delete this Data";
            this.Deletebtn.UseVisualStyleBackColor = true;
            this.Deletebtn.Visible = false;
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
            this.Finalizebtn.Location = new System.Drawing.Point(1020, 488);
            this.Finalizebtn.Name = "Finalizebtn";
            this.Finalizebtn.Padding = new System.Windows.Forms.Padding(45, 0, 50, 0);
            this.Finalizebtn.Size = new System.Drawing.Size(221, 43);
            this.Finalizebtn.TabIndex = 216;
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
            this.Save_btn.Location = new System.Drawing.Point(1124, 488);
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.Padding = new System.Windows.Forms.Padding(45, 0, 50, 0);
            this.Save_btn.Size = new System.Drawing.Size(180, 43);
            this.Save_btn.TabIndex = 217;
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
            this.Cancel_btn.Location = new System.Drawing.Point(928, 488);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(178, 43);
            this.Cancel_btn.TabIndex = 218;
            this.Cancel_btn.Text = "Cancel";
            this.Cancel_btn.UseVisualStyleBackColor = true;
            this.Cancel_btn.Visible = false;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // SDCDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1357, 567);
            this.Controls.Add(this.Deletebtn);
            this.Controls.Add(this.Finalizebtn);
            this.Controls.Add(this.Save_btn);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.NGText);
            this.Controls.Add(this.selectDepart);
            this.Controls.Add(this.ProblemText);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LotText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ModelText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SDCDetails";
            this.Text = "SDCDetails";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NGText;
        private System.Windows.Forms.ComboBox selectDepart;
        private System.Windows.Forms.RichTextBox ProblemText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox LotText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ModelText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Deletebtn;
        private System.Windows.Forms.Button Finalizebtn;
        private System.Windows.Forms.Button Save_btn;
        private System.Windows.Forms.Button Cancel_btn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}