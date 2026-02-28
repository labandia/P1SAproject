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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductsPageForm));
            this.productsTable = new System.Windows.Forms.DataGridView();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Profit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SearchCache = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinalPaymentbtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.CategoryLabel = new System.Windows.Forms.Label();
            this.Categorycbn = new System.Windows.Forms.ComboBox();
            this.SearchText = new System.Windows.Forms.TextBox();
            this.TotalCount = new System.Windows.Forms.Label();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.Excelbtn = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.productsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // productsTable
            // 
            this.productsTable.AllowUserToAddRows = false;
            this.productsTable.AllowUserToDeleteRows = false;
            this.productsTable.AllowUserToResizeColumns = false;
            this.productsTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.productsTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.productsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.productsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.productsTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.productsTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.productsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.productsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemNo,
            this.Category,
            this.ItemName,
            this.UnitCost,
            this.Price,
            this.StockQty,
            this.Profit,
            this.SearchCache});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.productsTable.DefaultCellStyle = dataGridViewCellStyle11;
            this.productsTable.Location = new System.Drawing.Point(27, 110);
            this.productsTable.Name = "productsTable";
            this.productsTable.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.productsTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.productsTable.RowHeadersVisible = false;
            this.productsTable.RowTemplate.Height = 35;
            this.productsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.productsTable.Size = new System.Drawing.Size(1304, 537);
            this.productsTable.TabIndex = 1;
            this.productsTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.productsTable_CellContentClick);
            // 
            // ItemNo
            // 
            this.ItemNo.DataPropertyName = "ItemNo";
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.ReadOnly = true;
            // 
            // Category
            // 
            this.Category.DataPropertyName = "Category";
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // UnitCost
            // 
            this.UnitCost.DataPropertyName = "UnitCost";
            this.UnitCost.HeaderText = "Unit Cost";
            this.UnitCost.Name = "UnitCost";
            this.UnitCost.ReadOnly = true;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // StockQty
            // 
            this.StockQty.DataPropertyName = "StockQty";
            this.StockQty.HeaderText = "StockQty";
            this.StockQty.Name = "StockQty";
            this.StockQty.ReadOnly = true;
            // 
            // Profit
            // 
            this.Profit.DataPropertyName = "Profit";
            this.Profit.HeaderText = "Profit";
            this.Profit.Name = "Profit";
            this.Profit.ReadOnly = true;
            // 
            // SearchCache
            // 
            this.SearchCache.DataPropertyName = "SearchCache";
            this.SearchCache.HeaderText = "SearchCache";
            this.SearchCache.Name = "SearchCache";
            this.SearchCache.ReadOnly = true;
            this.SearchCache.Visible = false;
            // 
            // FinalPaymentbtn
            // 
            this.FinalPaymentbtn.BackColor = System.Drawing.Color.Teal;
            this.FinalPaymentbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FinalPaymentbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FinalPaymentbtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinalPaymentbtn.ForeColor = System.Drawing.Color.White;
            this.FinalPaymentbtn.Location = new System.Drawing.Point(1036, 34);
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
            this.label8.Location = new System.Drawing.Point(24, 34);
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
            this.CategoryLabel.Location = new System.Drawing.Point(220, 34);
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
            this.Categorycbn.Location = new System.Drawing.Point(223, 54);
            this.Categorycbn.Name = "Categorycbn";
            this.Categorycbn.Size = new System.Drawing.Size(121, 21);
            this.Categorycbn.TabIndex = 118;
            this.Categorycbn.SelectedIndexChanged += new System.EventHandler(this.Categorycbn_SelectedIndexChanged);
            // 
            // SearchText
            // 
            this.SearchText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchText.Location = new System.Drawing.Point(27, 54);
            this.SearchText.Name = "SearchText";
            this.SearchText.Size = new System.Drawing.Size(179, 22);
            this.SearchText.TabIndex = 117;
            this.SearchText.TextChanged += new System.EventHandler(this.SearchText_TextChanged);
            // 
            // TotalCount
            // 
            this.TotalCount.AutoSize = true;
            this.TotalCount.BackColor = System.Drawing.Color.Transparent;
            this.TotalCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TotalCount.Location = new System.Drawing.Point(24, 90);
            this.TotalCount.Name = "TotalCount";
            this.TotalCount.Size = new System.Drawing.Size(85, 17);
            this.TotalCount.TabIndex = 121;
            this.TotalCount.Text = "Total Items :";
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPageInfo.Location = new System.Drawing.Point(36, 681);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(52, 25);
            this.lblPageInfo.TabIndex = 122;
            this.lblPageInfo.Text = "0 / 0";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(1242, 668);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(89, 38);
            this.btnNext.TabIndex = 124;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrev.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(1120, 668);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(100, 38);
            this.btnPrev.TabIndex = 123;
            this.btnPrev.Text = "Previous";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // Excelbtn
            // 
            this.Excelbtn.BackColor = System.Drawing.Color.Teal;
            this.Excelbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Excelbtn.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Excelbtn.ForeColor = System.Drawing.Color.White;
            this.Excelbtn.Location = new System.Drawing.Point(814, 34);
            this.Excelbtn.Name = "Excelbtn";
            this.Excelbtn.Size = new System.Drawing.Size(201, 42);
            this.Excelbtn.TabIndex = 125;
            this.Excelbtn.Text = "Import Excel";
            this.Excelbtn.UseVisualStyleBackColor = false;
            this.Excelbtn.Click += new System.EventHandler(this.Excelbtn_Click);
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button12.FlatAppearance.BorderSize = 0;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.Image = ((System.Drawing.Image)(resources.GetObject("button12.Image")));
            this.button12.Location = new System.Drawing.Point(1259, 30);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(55, 46);
            this.button12.TabIndex = 126;
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(553, 683);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(278, 23);
            this.progressBar1.TabIndex = 127;
            // 
            // ProductsPageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1352, 725);
            this.ControlBox = false;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.Excelbtn);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.lblPageInfo);
            this.Controls.Add(this.TotalCount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.CategoryLabel);
            this.Controls.Add(this.Categorycbn);
            this.Controls.Add(this.SearchText);
            this.Controls.Add(this.FinalPaymentbtn);
            this.Controls.Add(this.productsTable);
            this.Name = "ProductsPageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "-";
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
        private System.Windows.Forms.Label TotalCount;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button Excelbtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Profit;
        private System.Windows.Forms.DataGridViewTextBoxColumn SearchCache;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}