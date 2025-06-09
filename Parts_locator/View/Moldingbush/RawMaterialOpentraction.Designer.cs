namespace Parts_locator.View.Moldingbush
{
    partial class RawMaterialOpentraction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RawMaterialOpentraction));
            this.Type_error = new System.Windows.Forms.Label();
            this.Addbtn = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.quan = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Type_error
            // 
            this.Type_error.AutoSize = true;
            this.Type_error.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Type_error.ForeColor = System.Drawing.Color.DarkRed;
            this.Type_error.Location = new System.Drawing.Point(242, 129);
            this.Type_error.Name = "Type_error";
            this.Type_error.Size = new System.Drawing.Size(203, 16);
            this.Type_error.TabIndex = 17;
            this.Type_error.Text = "Only numeric values are allowed.";
            this.Type_error.Visible = false;
            // 
            // Addbtn
            // 
            this.Addbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(97)))), ((int)(((byte)(235)))));
            this.Addbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Addbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Addbtn.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Addbtn.ForeColor = System.Drawing.Color.White;
            this.Addbtn.Image = ((System.Drawing.Image)(resources.GetObject("Addbtn.Image")));
            this.Addbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Addbtn.Location = new System.Drawing.Point(256, 168);
            this.Addbtn.Name = "Addbtn";
            this.Addbtn.Padding = new System.Windows.Forms.Padding(50, 0, 40, 0);
            this.Addbtn.Size = new System.Drawing.Size(189, 47);
            this.Addbtn.TabIndex = 16;
            this.Addbtn.Text = "Continue";
            this.Addbtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Addbtn.UseVisualStyleBackColor = false;
            this.Addbtn.Click += new System.EventHandler(this.Addbtn_Click);
            // 
            // Button1
            // 
            this.Button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button1.Location = new System.Drawing.Point(36, 168);
            this.Button1.Name = "Button1";
            this.Button1.Padding = new System.Windows.Forms.Padding(50, 0, 50, 0);
            this.Button1.Size = new System.Drawing.Size(189, 47);
            this.Button1.TabIndex = 15;
            this.Button1.Text = "Cancel";
            this.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button1.UseVisualStyleBackColor = true;
            // 
            // quan
            // 
            this.quan.Font = new System.Drawing.Font("Century Gothic", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quan.Location = new System.Drawing.Point(36, 83);
            this.quan.Multiline = true;
            this.quan.Name = "quan";
            this.quan.Size = new System.Drawing.Size(409, 43);
            this.quan.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(33, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 18);
            this.label1.TabIndex = 13;
            this.label1.Text = "Enter an amount (numbers only) :";
            // 
            // RawMaterialOpentraction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 255);
            this.Controls.Add(this.Type_error);
            this.Controls.Add(this.Addbtn);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.quan);
            this.Controls.Add(this.label1);
            this.Name = "RawMaterialOpentraction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RawMaterialOpentraction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Type_error;
        private System.Windows.Forms.Button Addbtn;
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.TextBox quan;
        private System.Windows.Forms.Label label1;
    }
}