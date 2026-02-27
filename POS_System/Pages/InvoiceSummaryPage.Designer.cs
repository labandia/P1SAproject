namespace POS_System
{
    partial class InvoiceSummaryPage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceSummaryPage));
            this.itemsTable = new System.Windows.Forms.DataGridView();
            this.InvoiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtyIN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtyOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoiceTable = new System.Windows.Forms.DataGridView();
            this.SalesToday = new System.Windows.Forms.Label();
            this.InvoiceText = new System.Windows.Forms.Label();
            this.TotalItemsText = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button12 = new System.Windows.Forms.Button();
            this.UserText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.itemsTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceTable)).BeginInit();
            this.SuspendLayout();
            // 
            // itemsTable
            // 
            this.itemsTable.AllowUserToAddRows = false;
            this.itemsTable.AllowUserToDeleteRows = false;
            this.itemsTable.AllowUserToResizeColumns = false;
            this.itemsTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle33.BackColor = System.Drawing.Color.WhiteSmoke;
            this.itemsTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle33;
            this.itemsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.itemsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.itemsTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle34.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle34.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle34.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle34.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle34.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.itemsTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle34;
            this.itemsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.itemsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InvoiceNo,
            this.Date,
            this.ItemNo,
            this.ItemName,
            this.Price,
            this.QtyIN,
            this.QtyOut});
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle35.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle35.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle35.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.itemsTable.DefaultCellStyle = dataGridViewCellStyle35;
            this.itemsTable.Location = new System.Drawing.Point(618, 151);
            this.itemsTable.Name = "itemsTable";
            this.itemsTable.ReadOnly = true;
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle36.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle36.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle36.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle36.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle36.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.itemsTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle36;
            this.itemsTable.RowHeadersVisible = false;
            this.itemsTable.RowTemplate.Height = 35;
            this.itemsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.itemsTable.Size = new System.Drawing.Size(702, 521);
            this.itemsTable.TabIndex = 2;
            // 
            // InvoiceNo
            // 
            this.InvoiceNo.DataPropertyName = "InvoiceNo";
            this.InvoiceNo.HeaderText = "InvoiceNo";
            this.InvoiceNo.Name = "InvoiceNo";
            this.InvoiceNo.ReadOnly = true;
            this.InvoiceNo.Visible = false;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "Date";
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Visible = false;
            // 
            // ItemNo
            // 
            this.ItemNo.DataPropertyName = "ItemNo";
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.ReadOnly = true;
            this.ItemNo.Visible = false;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "ItemName";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // QtyIN
            // 
            this.QtyIN.DataPropertyName = "QtyIN";
            this.QtyIN.HeaderText = "QtyIN";
            this.QtyIN.Name = "QtyIN";
            this.QtyIN.ReadOnly = true;
            // 
            // QtyOut
            // 
            this.QtyOut.DataPropertyName = "QtyOut";
            this.QtyOut.HeaderText = "QtyOut";
            this.QtyOut.Name = "QtyOut";
            this.QtyOut.ReadOnly = true;
            // 
            // invoiceTable
            // 
            this.invoiceTable.AllowUserToAddRows = false;
            this.invoiceTable.AllowUserToDeleteRows = false;
            this.invoiceTable.AllowUserToResizeColumns = false;
            this.invoiceTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle37.BackColor = System.Drawing.Color.WhiteSmoke;
            this.invoiceTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle37;
            this.invoiceTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.invoiceTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle38.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle38.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle38.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle38.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle38.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle38.SelectionForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.invoiceTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle38;
            this.invoiceTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle39.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle39.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle39.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle39.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle39.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle39.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle39.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.invoiceTable.DefaultCellStyle = dataGridViewCellStyle39;
            this.invoiceTable.Location = new System.Drawing.Point(34, 151);
            this.invoiceTable.Name = "invoiceTable";
            this.invoiceTable.ReadOnly = true;
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle40.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle40.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle40.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle40.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle40.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle40.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.invoiceTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle40;
            this.invoiceTable.RowHeadersVisible = false;
            this.invoiceTable.RowTemplate.Height = 35;
            this.invoiceTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.invoiceTable.Size = new System.Drawing.Size(540, 521);
            this.invoiceTable.TabIndex = 3;
            this.invoiceTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.invoiceTable_CellClick);
            // 
            // SalesToday
            // 
            this.SalesToday.AutoSize = true;
            this.SalesToday.BackColor = System.Drawing.Color.Transparent;
            this.SalesToday.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SalesToday.Location = new System.Drawing.Point(26, 47);
            this.SalesToday.Name = "SalesToday";
            this.SalesToday.Size = new System.Drawing.Size(270, 45);
            this.SalesToday.TabIndex = 15;
            this.SalesToday.Text = "Inventory report";
            // 
            // InvoiceText
            // 
            this.InvoiceText.AutoSize = true;
            this.InvoiceText.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InvoiceText.Location = new System.Drawing.Point(615, 119);
            this.InvoiceText.Name = "InvoiceText";
            this.InvoiceText.Size = new System.Drawing.Size(80, 17);
            this.InvoiceText.TabIndex = 20;
            this.InvoiceText.Text = "Invoice No :";
            // 
            // TotalItemsText
            // 
            this.TotalItemsText.AutoSize = true;
            this.TotalItemsText.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalItemsText.Location = new System.Drawing.Point(1167, 684);
            this.TotalItemsText.Name = "TotalItemsText";
            this.TotalItemsText.Size = new System.Drawing.Size(142, 17);
            this.TotalItemsText.TabIndex = 21;
            this.TotalItemsText.Text = "Total Items Purchased";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "Recent Transactions";
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button12.FlatAppearance.BorderSize = 0;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.Image = ((System.Drawing.Image)(resources.GetObject("button12.Image")));
            this.button12.Location = new System.Drawing.Point(1254, 35);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(55, 46);
            this.button12.TabIndex = 128;
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // UserText
            // 
            this.UserText.AutoSize = true;
            this.UserText.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserText.Location = new System.Drawing.Point(1131, 119);
            this.UserText.Name = "UserText";
            this.UserText.Size = new System.Drawing.Size(42, 17);
            this.UserText.TabIndex = 129;
            this.UserText.Text = "User :";
            this.UserText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // InvoiceSummaryPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1352, 725);
            this.ControlBox = false;
            this.Controls.Add(this.UserText);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TotalItemsText);
            this.Controls.Add(this.InvoiceText);
            this.Controls.Add(this.SalesToday);
            this.Controls.Add(this.invoiceTable);
            this.Controls.Add(this.itemsTable);
            this.Name = "InvoiceSummaryPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InvoiceSummaryPage";
            this.Load += new System.EventHandler(this.InvoiceSummaryPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.itemsTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView itemsTable;
        private System.Windows.Forms.DataGridView invoiceTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtyIN;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtyOut;
        private System.Windows.Forms.Label SalesToday;
        private System.Windows.Forms.Label InvoiceText;
        private System.Windows.Forms.Label TotalItemsText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Label UserText;
    }
}