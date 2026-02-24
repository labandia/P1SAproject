namespace POS_System.Modals
{
    partial class AddProductsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProductsForm));
            this.price_error = new System.Windows.Forms.Label();
            this.prod_error = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.UnitCostText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PriceText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ItemNameText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Cancebtn = new System.Windows.Forms.Button();
            this.Savebtn = new System.Windows.Forms.Button();
            this.CategoryLabel = new System.Windows.Forms.Label();
            this.Categorycbn = new System.Windows.Forms.ComboBox();
            this.Unit_error = new System.Windows.Forms.Label();
            this.selectCat_error = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // price_error
            // 
            this.price_error.AutoSize = true;
            this.price_error.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.price_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.price_error.Location = new System.Drawing.Point(336, 226);
            this.price_error.Name = "price_error";
            this.price_error.Size = new System.Drawing.Size(90, 16);
            this.price_error.TabIndex = 30;
            this.price_error.Text = "Price required";
            this.price_error.Visible = false;
            // 
            // prod_error
            // 
            this.prod_error.AutoSize = true;
            this.prod_error.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.prod_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.prod_error.Location = new System.Drawing.Point(293, 128);
            this.prod_error.Name = "prod_error";
            this.prod_error.Size = new System.Drawing.Size(152, 16);
            this.prod_error.TabIndex = 29;
            this.prod_error.Text = "Product Name Required*";
            this.prod_error.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(59, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 28);
            this.label6.TabIndex = 28;
            this.label6.Text = "Add Products";
            // 
            // UnitCostText
            // 
            this.UnitCostText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.UnitCostText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UnitCostText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UnitCostText.Location = new System.Drawing.Point(62, 356);
            this.UnitCostText.Multiline = true;
            this.UnitCostText.Name = "UnitCostText";
            this.UnitCostText.Size = new System.Drawing.Size(370, 37);
            this.UnitCostText.TabIndex = 23;
            this.UnitCostText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UnitCostText_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(59, 324);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 16);
            this.label3.TabIndex = 22;
            this.label3.Text = "Unit Cost";
            // 
            // PriceText
            // 
            this.PriceText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.PriceText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PriceText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceText.Location = new System.Drawing.Point(62, 256);
            this.PriceText.Multiline = true;
            this.PriceText.Name = "PriceText";
            this.PriceText.Size = new System.Drawing.Size(370, 37);
            this.PriceText.TabIndex = 21;
            this.PriceText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PriceText_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(59, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "Price";
            // 
            // ItemNameText
            // 
            this.ItemNameText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ItemNameText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ItemNameText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemNameText.Location = new System.Drawing.Point(62, 160);
            this.ItemNameText.Multiline = true;
            this.ItemNameText.Name = "ItemNameText";
            this.ItemNameText.Size = new System.Drawing.Size(370, 37);
            this.ItemNameText.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(59, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Product Name :";
            // 
            // Cancebtn
            // 
            this.Cancebtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancebtn.Location = new System.Drawing.Point(58, 520);
            this.Cancebtn.Name = "Cancebtn";
            this.Cancebtn.Size = new System.Drawing.Size(169, 41);
            this.Cancebtn.TabIndex = 17;
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
            this.Savebtn.Location = new System.Drawing.Point(260, 520);
            this.Savebtn.Name = "Savebtn";
            this.Savebtn.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.Savebtn.Size = new System.Drawing.Size(174, 41);
            this.Savebtn.TabIndex = 16;
            this.Savebtn.Text = "Save";
            this.Savebtn.UseVisualStyleBackColor = false;
            this.Savebtn.Click += new System.EventHandler(this.Savebtn_Click);
            // 
            // CategoryLabel
            // 
            this.CategoryLabel.AutoSize = true;
            this.CategoryLabel.BackColor = System.Drawing.Color.Transparent;
            this.CategoryLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CategoryLabel.Location = new System.Drawing.Point(61, 417);
            this.CategoryLabel.Name = "CategoryLabel";
            this.CategoryLabel.Size = new System.Drawing.Size(104, 17);
            this.CategoryLabel.TabIndex = 121;
            this.CategoryLabel.Text = "Select Category";
            // 
            // Categorycbn
            // 
            this.Categorycbn.FormattingEnabled = true;
            this.Categorycbn.Items.AddRange(new object[] {
            "ALL",
            "PASS",
            "FAIL"});
            this.Categorycbn.Location = new System.Drawing.Point(64, 455);
            this.Categorycbn.Name = "Categorycbn";
            this.Categorycbn.Size = new System.Drawing.Size(370, 21);
            this.Categorycbn.TabIndex = 120;
            // 
            // Unit_error
            // 
            this.Unit_error.AutoSize = true;
            this.Unit_error.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.Unit_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Unit_error.Location = new System.Drawing.Point(327, 324);
            this.Unit_error.Name = "Unit_error";
            this.Unit_error.Size = new System.Drawing.Size(105, 16);
            this.Unit_error.TabIndex = 122;
            this.Unit_error.Text = "Unit Cost requied";
            this.Unit_error.Visible = false;
            // 
            // selectCat_error
            // 
            this.selectCat_error.AutoSize = true;
            this.selectCat_error.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.selectCat_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.selectCat_error.Location = new System.Drawing.Point(321, 418);
            this.selectCat_error.Name = "selectCat_error";
            this.selectCat_error.Size = new System.Drawing.Size(126, 16);
            this.selectCat_error.TabIndex = 123;
            this.selectCat_error.Text = "Category is required";
            this.selectCat_error.Visible = false;
            // 
            // AddProductsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 621);
            this.Controls.Add(this.selectCat_error);
            this.Controls.Add(this.Unit_error);
            this.Controls.Add(this.CategoryLabel);
            this.Controls.Add(this.Categorycbn);
            this.Controls.Add(this.price_error);
            this.Controls.Add(this.prod_error);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.UnitCostText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PriceText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ItemNameText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancebtn);
            this.Controls.Add(this.Savebtn);
            this.Name = "AddProductsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddProductsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label price_error;
        private System.Windows.Forms.Label prod_error;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox UnitCostText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PriceText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ItemNameText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancebtn;
        private System.Windows.Forms.Button Savebtn;
        private System.Windows.Forms.Label CategoryLabel;
        private System.Windows.Forms.ComboBox Categorycbn;
        private System.Windows.Forms.Label Unit_error;
        private System.Windows.Forms.Label selectCat_error;
    }
}