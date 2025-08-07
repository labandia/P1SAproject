namespace ZebraPrinterLabel.View
{
    partial class EditMasterlist
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
            this.Addbtn = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.quan = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Quan_error = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Addbtn
            // 
            this.Addbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(97)))), ((int)(((byte)(235)))));
            this.Addbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Addbtn.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Addbtn.ForeColor = System.Drawing.Color.White;
            this.Addbtn.Location = new System.Drawing.Point(249, 143);
            this.Addbtn.Name = "Addbtn";
            this.Addbtn.Size = new System.Drawing.Size(189, 47);
            this.Addbtn.TabIndex = 11;
            this.Addbtn.Text = "Continue";
            this.Addbtn.UseVisualStyleBackColor = false;
            this.Addbtn.Click += new System.EventHandler(this.Addbtn_Click);
            // 
            // Button1
            // 
            this.Button1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button1.Location = new System.Drawing.Point(29, 143);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(189, 47);
            this.Button1.TabIndex = 10;
            this.Button1.Text = "Cancel";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // quan
            // 
            this.quan.Font = new System.Drawing.Font("Century Gothic", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quan.Location = new System.Drawing.Point(29, 73);
            this.quan.Multiline = true;
            this.quan.Name = "quan";
            this.quan.Size = new System.Drawing.Size(409, 43);
            this.quan.TabIndex = 9;
            this.quan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.quan_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(26, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "Enter amount of quantity:";
            // 
            // Quan_error
            // 
            this.Quan_error.AutoSize = true;
            this.Quan_error.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.Quan_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Quan_error.Location = new System.Drawing.Point(289, 37);
            this.Quan_error.Name = "Quan_error";
            this.Quan_error.Size = new System.Drawing.Size(130, 16);
            this.Quan_error.TabIndex = 46;
            this.Quan_error.Text = "Quantity  is required *";
            this.Quan_error.Visible = false;
            // 
            // EditMasterlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 228);
            this.Controls.Add(this.Quan_error);
            this.Controls.Add(this.Addbtn);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.quan);
            this.Controls.Add(this.label1);
            this.Name = "EditMasterlist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditMasterlist";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Addbtn;
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.TextBox quan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Quan_error;
    }
}