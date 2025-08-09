namespace ZebraPrinterLabel
{
    partial class Printer_History
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
            this.label6 = new System.Windows.Forms.Label();
            this.HistoryGrid = new System.Windows.Forms.DataGridView();
            this.DateInput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Partnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WarehouseLocal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(40, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(165, 28);
            this.label6.TabIndex = 45;
            this.label6.Text = "Printer history";
            // 
            // HistoryGrid
            // 
            this.HistoryGrid.AllowUserToAddRows = false;
            this.HistoryGrid.AllowUserToDeleteRows = false;
            this.HistoryGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HistoryGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.HistoryGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DateInput,
            this.ReelID,
            this.Partnum,
            this.WarehouseLocal,
            this.Qty});
            this.HistoryGrid.Location = new System.Drawing.Point(44, 96);
            this.HistoryGrid.Name = "HistoryGrid";
            this.HistoryGrid.ReadOnly = true;
            this.HistoryGrid.RowHeadersVisible = false;
            this.HistoryGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.HistoryGrid.Size = new System.Drawing.Size(989, 582);
            this.HistoryGrid.TabIndex = 46;
            // 
            // DateInput
            // 
            this.DateInput.DataPropertyName = "DateInput";
            this.DateInput.HeaderText = "Date Print";
            this.DateInput.Name = "DateInput";
            this.DateInput.ReadOnly = true;
            // 
            // ReelID
            // 
            this.ReelID.DataPropertyName = "ReelID";
            this.ReelID.HeaderText = "ReelID";
            this.ReelID.Name = "ReelID";
            this.ReelID.ReadOnly = true;
            this.ReelID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ReelID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Partnum
            // 
            this.Partnum.DataPropertyName = "Partnum";
            this.Partnum.HeaderText = "Part number";
            this.Partnum.Name = "Partnum";
            this.Partnum.ReadOnly = true;
            // 
            // WarehouseLocal
            // 
            this.WarehouseLocal.DataPropertyName = "WarehouseLocal";
            this.WarehouseLocal.HeaderText = "Warehouse Location";
            this.WarehouseLocal.Name = "WarehouseLocal";
            this.WarehouseLocal.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            this.Qty.HeaderText = "Quantity";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            // 
            // Printer_History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 745);
            this.Controls.Add(this.HistoryGrid);
            this.Controls.Add(this.label6);
            this.Name = "Printer_History";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Printer_History";
            this.Load += new System.EventHandler(this.Printer_History_Load);
            ((System.ComponentModel.ISupportInitialize)(this.HistoryGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView HistoryGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateInput;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReelID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Partnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn WarehouseLocal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
    }
}