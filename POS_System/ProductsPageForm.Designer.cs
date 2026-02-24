namespace POS_System
{
    partial class ProductsPageForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.productsTable = new System.Windows.Forms.DataGridView();
            this.FinalPaymentbtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.CategoryLabel = new System.Windows.Forms.Label();
            this.Categorycbn = new System.Windows.Forms.ComboBox();
            this.SearchText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.productsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // productsTable
            // 
            this.productsTable.AllowUserToAddRows = false;
            this.productsTable.AllowUserToDeleteRows = false;
            this.productsTable.AllowUserToResizeColumns = false;
            this.productsTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.productsTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.productsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.productsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.productsTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.productsTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.productsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.productsTable.DefaultCellStyle = dataGridViewCellStyle7;
            this.productsTable.Location = new System.Drawing.Point(12, 93);
            this.productsTable.Name = "productsTable";
            this.productsTable.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.productsTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.productsTable.RowHeadersVisible = false;
            this.productsTable.RowTemplate.Height = 35;
            this.productsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.productsTable.Size = new System.Drawing.Size(1208, 621);
            this.productsTable.TabIndex = 1;
            this.productsTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.productsTable_CellContentClick);
            // 
            // FinalPaymentbtn
            // 
            this.FinalPaymentbtn.BackColor = System.Drawing.Color.Teal;
            this.FinalPaymentbtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinalPaymentbtn.ForeColor = System.Drawing.Color.White;
            this.FinalPaymentbtn.Location = new System.Drawing.Point(1010, 27);
            this.FinalPaymentbtn.Name = "FinalPaymentbtn";
            this.FinalPaymentbtn.Size = new System.Drawing.Size(201, 42);
            this.FinalPaymentbtn.TabIndex = 10;
            this.FinalPaymentbtn.Text = "Add Items";
            this.FinalPaymentbtn.UseVisualStyleBackColor = false;
            this.FinalPaymentbtn.Click += new System.EventHandler(this.FinalPaymentbtn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label8.Location = new System.Drawing.Point(12, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 17);
            this.label8.TabIndex = 120;
            this.label8.Text = "Search Item Name:";
            // 
            // CategoryLabel
            // 
            this.CategoryLabel.AutoSize = true;
            this.CategoryLabel.BackColor = System.Drawing.Color.Transparent;
            this.CategoryLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CategoryLabel.Location = new System.Drawing.Point(208, 27);
            this.CategoryLabel.Name = "CategoryLabel";
            this.CategoryLabel.Size = new System.Drawing.Size(119, 17);
            this.CategoryLabel.TabIndex = 119;
            this.CategoryLabel.Text = "Filter by Category";
            // 
            // Categorycbn
            // 
            this.Categorycbn.FormattingEnabled = true;
            this.Categorycbn.Items.AddRange(new object[] {
            "ALL",
            "PASS",
            "FAIL"});
            this.Categorycbn.Location = new System.Drawing.Point(211, 47);
            this.Categorycbn.Name = "Categorycbn";
            this.Categorycbn.Size = new System.Drawing.Size(121, 21);
            this.Categorycbn.TabIndex = 118;
            this.Categorycbn.SelectedIndexChanged += new System.EventHandler(this.Categorycbn_SelectedIndexChanged);
            // 
            // SearchText
            // 
            this.SearchText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchText.Location = new System.Drawing.Point(15, 47);
            this.SearchText.Name = "SearchText";
            this.SearchText.Size = new System.Drawing.Size(179, 22);
            this.SearchText.TabIndex = 117;
            this.SearchText.TextChanged += new System.EventHandler(this.SearchText_TextChanged);
            // 
            // ProductsPageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 749);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.CategoryLabel);
            this.Controls.Add(this.Categorycbn);
            this.Controls.Add(this.SearchText);
            this.Controls.Add(this.FinalPaymentbtn);
            this.Controls.Add(this.productsTable);
            this.Name = "ProductsPageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProductsPageForm";
            this.Load += new System.EventHandler(this.ProductsPageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.productsTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView productsTable;
        private System.Windows.Forms.Button FinalPaymentbtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label CategoryLabel;
        private System.Windows.Forms.ComboBox Categorycbn;
        private System.Windows.Forms.TextBox SearchText;
    }
}