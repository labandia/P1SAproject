namespace POS_System.Modals
{
    partial class AddUsers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddUsers));
            this.price_error = new System.Windows.Forms.Label();
            this.prod_error = new System.Windows.Forms.Label();
            this.userText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fullnameText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CategoryLabel = new System.Windows.Forms.Label();
            this.Categorycbn = new System.Windows.Forms.ComboBox();
            this.Cancebtn = new System.Windows.Forms.Button();
            this.Savebtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // price_error
            // 
            this.price_error.AutoSize = true;
            this.price_error.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.price_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.price_error.Location = new System.Drawing.Point(281, 221);
            this.price_error.Name = "price_error";
            this.price_error.Size = new System.Drawing.Size(122, 16);
            this.price_error.TabIndex = 36;
            this.price_error.Text = "Username Required";
            this.price_error.Visible = false;
            // 
            // prod_error
            // 
            this.prod_error.AutoSize = true;
            this.prod_error.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.prod_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.prod_error.Location = new System.Drawing.Point(282, 123);
            this.prod_error.Name = "prod_error";
            this.prod_error.Size = new System.Drawing.Size(121, 16);
            this.prod_error.TabIndex = 35;
            this.prod_error.Text = "Fullname Required*";
            this.prod_error.Visible = false;
            // 
            // userText
            // 
            this.userText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.userText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userText.Location = new System.Drawing.Point(39, 251);
            this.userText.Multiline = true;
            this.userText.Name = "userText";
            this.userText.Size = new System.Drawing.Size(370, 37);
            this.userText.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(36, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 33;
            this.label2.Text = "Username";
            // 
            // fullnameText
            // 
            this.fullnameText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.fullnameText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fullnameText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fullnameText.Location = new System.Drawing.Point(39, 155);
            this.fullnameText.Multiline = true;
            this.fullnameText.Name = "fullnameText";
            this.fullnameText.Size = new System.Drawing.Size(370, 37);
            this.fullnameText.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(36, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 31;
            this.label1.Text = "Full Name :";
            // 
            // CategoryLabel
            // 
            this.CategoryLabel.AutoSize = true;
            this.CategoryLabel.BackColor = System.Drawing.Color.Transparent;
            this.CategoryLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CategoryLabel.Location = new System.Drawing.Point(36, 325);
            this.CategoryLabel.Name = "CategoryLabel";
            this.CategoryLabel.Size = new System.Drawing.Size(75, 17);
            this.CategoryLabel.TabIndex = 123;
            this.CategoryLabel.Text = "Select Role";
            // 
            // Categorycbn
            // 
            this.Categorycbn.FormattingEnabled = true;
            this.Categorycbn.Items.AddRange(new object[] {
            "-- Select Role --",
            "Admin",
            "Users"});
            this.Categorycbn.Location = new System.Drawing.Point(39, 363);
            this.Categorycbn.Name = "Categorycbn";
            this.Categorycbn.Size = new System.Drawing.Size(370, 21);
            this.Categorycbn.TabIndex = 122;
            // 
            // Cancebtn
            // 
            this.Cancebtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancebtn.Location = new System.Drawing.Point(39, 421);
            this.Cancebtn.Name = "Cancebtn";
            this.Cancebtn.Size = new System.Drawing.Size(169, 41);
            this.Cancebtn.TabIndex = 125;
            this.Cancebtn.Text = "Cancel";
            this.Cancebtn.UseVisualStyleBackColor = true;
            // 
            // Savebtn
            // 
            this.Savebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(62)))), ((int)(((byte)(185)))));
            this.Savebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Savebtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(62)))), ((int)(((byte)(185)))));
            this.Savebtn.FlatAppearance.BorderSize = 0;
            this.Savebtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Savebtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(50)))), ((int)(((byte)(141)))));
            this.Savebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Savebtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Savebtn.ForeColor = System.Drawing.Color.White;
            this.Savebtn.Image = ((System.Drawing.Image)(resources.GetObject("Savebtn.Image")));
            this.Savebtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Savebtn.Location = new System.Drawing.Point(229, 421);
            this.Savebtn.Name = "Savebtn";
            this.Savebtn.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.Savebtn.Size = new System.Drawing.Size(174, 41);
            this.Savebtn.TabIndex = 124;
            this.Savebtn.Text = "Save";
            this.Savebtn.UseVisualStyleBackColor = false;
            this.Savebtn.Click += new System.EventHandler(this.Savebtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(34, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 28);
            this.label6.TabIndex = 126;
            this.label6.Text = "Add Users";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(281, 326);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 16);
            this.label3.TabIndex = 127;
            this.label3.Text = "Select Role Required";
            this.label3.Visible = false;
            // 
            // AddUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 515);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Cancebtn);
            this.Controls.Add(this.Savebtn);
            this.Controls.Add(this.CategoryLabel);
            this.Controls.Add(this.Categorycbn);
            this.Controls.Add(this.price_error);
            this.Controls.Add(this.prod_error);
            this.Controls.Add(this.userText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fullnameText);
            this.Controls.Add(this.label1);
            this.Name = "AddUsers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddUsers";
            this.Load += new System.EventHandler(this.AddUsers_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label price_error;
        private System.Windows.Forms.Label prod_error;
        private System.Windows.Forms.TextBox userText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fullnameText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CategoryLabel;
        private System.Windows.Forms.ComboBox Categorycbn;
        private System.Windows.Forms.Button Cancebtn;
        private System.Windows.Forms.Button Savebtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
    }
}