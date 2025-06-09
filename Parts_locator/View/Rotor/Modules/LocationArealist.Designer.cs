namespace Parts_locator.Modules
{
    partial class LocationArealist
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Letter = new System.Windows.Forms.Label();
            this.productTable = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.productTable)).BeginInit();
            this.SuspendLayout();
            // 
            // Letter
            // 
            this.Letter.AutoSize = true;
            this.Letter.Location = new System.Drawing.Point(41, 55);
            this.Letter.Name = "Letter";
            this.Letter.Size = new System.Drawing.Size(35, 13);
            this.Letter.TabIndex = 2;
            this.Letter.Text = "label1";
            // 
            // productTable
            // 
            this.productTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.productTable.Location = new System.Drawing.Point(44, 123);
            this.productTable.Name = "productTable";
            this.productTable.Size = new System.Drawing.Size(870, 373);
            this.productTable.TabIndex = 3;
            // 
            // LocationArealist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.productTable);
            this.Controls.Add(this.Letter);
            this.Name = "LocationArealist";
            this.Size = new System.Drawing.Size(959, 572);
            ((System.ComponentModel.ISupportInitialize)(this.productTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Letter;
        private System.Windows.Forms.DataGridView productTable;
    }
}
