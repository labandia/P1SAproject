namespace ZebraPrinterLabel
{
    partial class AddMasterlist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddMasterlist));
            this.Ware_error = new System.Windows.Forms.Label();
            this.Partnum_error = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Quantity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Warehouse = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Partnum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Cancebtn = new System.Windows.Forms.Button();
            this.Savebtn = new System.Windows.Forms.Button();
            this.QuanError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Ware_error
            // 
            this.Ware_error.AutoSize = true;
            this.Ware_error.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.Ware_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Ware_error.Location = new System.Drawing.Point(219, 214);
            this.Ware_error.Name = "Ware_error";
            this.Ware_error.Size = new System.Drawing.Size(198, 16);
            this.Ware_error.TabIndex = 46;
            this.Ware_error.Text = "Warehouse Location is required *";
            this.Ware_error.Visible = false;
            // 
            // Partnum_error
            // 
            this.Partnum_error.AutoSize = true;
            this.Partnum_error.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.Partnum_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Partnum_error.Location = new System.Drawing.Point(278, 126);
            this.Partnum_error.Name = "Partnum_error";
            this.Partnum_error.Size = new System.Drawing.Size(149, 16);
            this.Partnum_error.TabIndex = 45;
            this.Partnum_error.Text = "Partnumber  is required *";
            this.Partnum_error.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(43, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 28);
            this.label6.TabIndex = 44;
            this.label6.Text = "Add new Data";
            // 
            // Quantity
            // 
            this.Quantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Quantity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Quantity.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Quantity.Location = new System.Drawing.Point(47, 323);
            this.Quantity.Multiline = true;
            this.Quantity.Name = "Quantity";
            this.Quantity.Size = new System.Drawing.Size(370, 37);
            this.Quantity.TabIndex = 39;
            this.Quantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Quantity_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(44, 299);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 38;
            this.label3.Text = "Quantity : ";
            // 
            // Warehouse
            // 
            this.Warehouse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Warehouse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Warehouse.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Warehouse.Location = new System.Drawing.Point(47, 236);
            this.Warehouse.Multiline = true;
            this.Warehouse.Name = "Warehouse";
            this.Warehouse.Size = new System.Drawing.Size(370, 37);
            this.Warehouse.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(44, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 16);
            this.label2.TabIndex = 36;
            this.label2.Text = "Warehouse Location :";
            // 
            // Partnum
            // 
            this.Partnum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Partnum.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Partnum.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Partnum.Location = new System.Drawing.Point(47, 150);
            this.Partnum.Multiline = true;
            this.Partnum.Name = "Partnum";
            this.Partnum.Size = new System.Drawing.Size(370, 37);
            this.Partnum.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(44, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 16);
            this.label1.TabIndex = 34;
            this.label1.Text = "Part number :";
            // 
            // Cancebtn
            // 
            this.Cancebtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancebtn.Location = new System.Drawing.Point(49, 403);
            this.Cancebtn.Name = "Cancebtn";
            this.Cancebtn.Size = new System.Drawing.Size(169, 46);
            this.Cancebtn.TabIndex = 33;
            this.Cancebtn.Text = "Cancel";
            this.Cancebtn.UseVisualStyleBackColor = true;
            this.Cancebtn.Click += new System.EventHandler(this.Cancebtn_Click);
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
            this.Savebtn.Location = new System.Drawing.Point(243, 403);
            this.Savebtn.Name = "Savebtn";
            this.Savebtn.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.Savebtn.Size = new System.Drawing.Size(174, 46);
            this.Savebtn.TabIndex = 32;
            this.Savebtn.Text = "Save";
            this.Savebtn.UseVisualStyleBackColor = false;
            this.Savebtn.Click += new System.EventHandler(this.Savebtn_Click);
            // 
            // QuanError
            // 
            this.QuanError.AutoSize = true;
            this.QuanError.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.QuanError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.QuanError.Location = new System.Drawing.Point(290, 299);
            this.QuanError.Name = "QuanError";
            this.QuanError.Size = new System.Drawing.Size(127, 16);
            this.QuanError.TabIndex = 47;
            this.QuanError.Text = "Quantity is required *";
            this.QuanError.Visible = false;
            // 
            // AddMasterlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 525);
            this.ControlBox = false;
            this.Controls.Add(this.QuanError);
            this.Controls.Add(this.Ware_error);
            this.Controls.Add(this.Partnum_error);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Quantity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Warehouse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Partnum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancebtn);
            this.Controls.Add(this.Savebtn);
            this.Name = "AddMasterlist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddMasterlist";
            this.Load += new System.EventHandler(this.AddMasterlist_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Ware_error;
        private System.Windows.Forms.Label Partnum_error;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Quantity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Warehouse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Partnum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancebtn;
        private System.Windows.Forms.Button Savebtn;
        private System.Windows.Forms.Label QuanError;
    }
}