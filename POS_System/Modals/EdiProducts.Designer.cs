namespace POS_System.Modals
{
    partial class EdiProducts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EdiProducts));
            this.Name_error = new System.Windows.Forms.Label();
            this.Emp_error = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ProdUnit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ProdPrice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ProdName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Cancebtn = new System.Windows.Forms.Button();
            this.Savebtn = new System.Windows.Forms.Button();
            this.prodStocks = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Name_error
            // 
            this.Name_error.AutoSize = true;
            this.Name_error.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.Name_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Name_error.Location = new System.Drawing.Point(238, 227);
            this.Name_error.Name = "Name_error";
            this.Name_error.Size = new System.Drawing.Size(177, 16);
            this.Name_error.TabIndex = 41;
            this.Name_error.Text = "Employee Name is required *";
            this.Name_error.Visible = false;
            // 
            // Emp_error
            // 
            this.Emp_error.AutoSize = true;
            this.Emp_error.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.Emp_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Emp_error.Location = new System.Drawing.Point(276, 139);
            this.Emp_error.Name = "Emp_error";
            this.Emp_error.Size = new System.Drawing.Size(139, 16);
            this.Emp_error.TabIndex = 40;
            this.Emp_error.Text = "ID number is required *";
            this.Emp_error.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(42, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 28);
            this.label6.TabIndex = 39;
            this.label6.Text = "Edit Products";
            // 
            // ProdUnit
            // 
            this.ProdUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ProdUnit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProdUnit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProdUnit.Location = new System.Drawing.Point(47, 362);
            this.ProdUnit.Multiline = true;
            this.ProdUnit.Name = "ProdUnit";
            this.ProdUnit.Size = new System.Drawing.Size(370, 37);
            this.ProdUnit.TabIndex = 38;
            this.ProdUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProdUnit_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(44, 330);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 16);
            this.label3.TabIndex = 37;
            this.label3.Text = "Unit Cost";
            // 
            // ProdPrice
            // 
            this.ProdPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ProdPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProdPrice.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProdPrice.Location = new System.Drawing.Point(45, 257);
            this.ProdPrice.Multiline = true;
            this.ProdPrice.Name = "ProdPrice";
            this.ProdPrice.Size = new System.Drawing.Size(370, 37);
            this.ProdPrice.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(42, 227);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 35;
            this.label2.Text = "Price";
            // 
            // ProdName
            // 
            this.ProdName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ProdName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProdName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProdName.Location = new System.Drawing.Point(45, 171);
            this.ProdName.Multiline = true;
            this.ProdName.Name = "ProdName";
            this.ProdName.Size = new System.Drawing.Size(370, 37);
            this.ProdName.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(42, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 33;
            this.label1.Text = "Product Name :";
            // 
            // Cancebtn
            // 
            this.Cancebtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancebtn.Location = new System.Drawing.Point(36, 548);
            this.Cancebtn.Name = "Cancebtn";
            this.Cancebtn.Size = new System.Drawing.Size(169, 41);
            this.Cancebtn.TabIndex = 32;
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
            this.Savebtn.Location = new System.Drawing.Point(230, 548);
            this.Savebtn.Name = "Savebtn";
            this.Savebtn.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.Savebtn.Size = new System.Drawing.Size(174, 41);
            this.Savebtn.TabIndex = 31;
            this.Savebtn.Text = "Save";
            this.Savebtn.UseVisualStyleBackColor = false;
            this.Savebtn.Click += new System.EventHandler(this.Savebtn_Click);
            // 
            // prodStocks
            // 
            this.prodStocks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.prodStocks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.prodStocks.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prodStocks.Location = new System.Drawing.Point(45, 466);
            this.prodStocks.Multiline = true;
            this.prodStocks.Name = "prodStocks";
            this.prodStocks.Size = new System.Drawing.Size(370, 37);
            this.prodStocks.TabIndex = 43;
            this.prodStocks.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.prodStocks_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(42, 434);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 16);
            this.label4.TabIndex = 42;
            this.label4.Text = "Stocks Quantity : ";
            // 
            // EdiProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 629);
            this.Controls.Add(this.prodStocks);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Name_error);
            this.Controls.Add(this.Emp_error);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ProdUnit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ProdPrice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ProdName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancebtn);
            this.Controls.Add(this.Savebtn);
            this.Name = "EdiProducts";
            this.Text = "EdiProducts";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Name_error;
        private System.Windows.Forms.Label Emp_error;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ProdUnit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ProdPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ProdName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancebtn;
        private System.Windows.Forms.Button Savebtn;
        private System.Windows.Forms.TextBox prodStocks;
        private System.Windows.Forms.Label label4;
    }
}