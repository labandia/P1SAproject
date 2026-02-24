namespace POS_System
{
    partial class AddSalesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSalesForm));
            this.FinalPaymentbtn = new System.Windows.Forms.Button();
            this.TotalText = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.AmountText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ChangeText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FinalPaymentbtn
            // 
            this.FinalPaymentbtn.BackColor = System.Drawing.Color.Teal;
            this.FinalPaymentbtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinalPaymentbtn.ForeColor = System.Drawing.Color.White;
            this.FinalPaymentbtn.Image = ((System.Drawing.Image)(resources.GetObject("FinalPaymentbtn.Image")));
            this.FinalPaymentbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FinalPaymentbtn.Location = new System.Drawing.Point(260, 249);
            this.FinalPaymentbtn.Name = "FinalPaymentbtn";
            this.FinalPaymentbtn.Padding = new System.Windows.Forms.Padding(40, 0, 50, 0);
            this.FinalPaymentbtn.Size = new System.Drawing.Size(203, 64);
            this.FinalPaymentbtn.TabIndex = 9;
            this.FinalPaymentbtn.Text = "Confirm";
            this.FinalPaymentbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.FinalPaymentbtn.UseVisualStyleBackColor = false;
            this.FinalPaymentbtn.Click += new System.EventHandler(this.FinalPaymentbtn_Click);
            // 
            // TotalText
            // 
            this.TotalText.AutoSize = true;
            this.TotalText.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalText.Location = new System.Drawing.Point(341, 46);
            this.TotalText.Name = "TotalText";
            this.TotalText.Size = new System.Drawing.Size(59, 30);
            this.TotalText.TabIndex = 11;
            this.TotalText.Text = "0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(51, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 28);
            this.label4.TabIndex = 10;
            this.label4.Text = "Total :";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(30, 249);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(50, 0, 50, 0);
            this.button1.Size = new System.Drawing.Size(203, 64);
            this.button1.TabIndex = 12;
            this.button1.Text = "Cancel";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 28);
            this.label1.TabIndex = 13;
            this.label1.Text = "Pay :";
            // 
            // AmountText
            // 
            this.AmountText.Font = new System.Drawing.Font("Century Gothic", 18F);
            this.AmountText.Location = new System.Drawing.Point(284, 124);
            this.AmountText.Multiline = true;
            this.AmountText.Name = "AmountText";
            this.AmountText.Size = new System.Drawing.Size(167, 31);
            this.AmountText.TabIndex = 14;
            this.AmountText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.AmountText.TextChanged += new System.EventHandler(this.AmountText_TextChanged);
            this.AmountText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AmountText_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(51, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 28);
            this.label2.TabIndex = 15;
            this.label2.Text = "Change :";
            // 
            // ChangeText
            // 
            this.ChangeText.AutoSize = true;
            this.ChangeText.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeText.Location = new System.Drawing.Point(341, 188);
            this.ChangeText.Name = "ChangeText";
            this.ChangeText.Size = new System.Drawing.Size(59, 30);
            this.ChangeText.TabIndex = 16;
            this.ChangeText.Text = "0.00";
            // 
            // AddSalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 337);
            this.ControlBox = false;
            this.Controls.Add(this.ChangeText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AmountText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FinalPaymentbtn);
            this.Controls.Add(this.TotalText);
            this.Controls.Add(this.label4);
            this.Name = "AddSalesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "-";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FinalPaymentbtn;
        private System.Windows.Forms.Label TotalText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AmountText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ChangeText;
    }
}