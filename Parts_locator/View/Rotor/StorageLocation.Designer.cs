namespace Parts_locator.Modals
{
    partial class StorageLocation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StorageLocation));
            this.letter = new System.Windows.Forms.Label();
            this.Storagetablelist = new System.Windows.Forms.DataGridView();
            this.PartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PalletName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PalletID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Storagetablelist)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // letter
            // 
            this.letter.AutoSize = true;
            this.letter.Font = new System.Drawing.Font("Century Gothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter.ForeColor = System.Drawing.Color.White;
            this.letter.Location = new System.Drawing.Point(39, 39);
            this.letter.Margin = new System.Windows.Forms.Padding(0);
            this.letter.Name = "letter";
            this.letter.Size = new System.Drawing.Size(80, 77);
            this.letter.TabIndex = 3;
            this.letter.Text = "A";
            this.letter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Storagetablelist
            // 
            this.Storagetablelist.AllowUserToAddRows = false;
            this.Storagetablelist.AllowUserToDeleteRows = false;
            this.Storagetablelist.AllowUserToResizeColumns = false;
            this.Storagetablelist.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.Storagetablelist.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Storagetablelist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Storagetablelist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Storagetablelist.BackgroundColor = System.Drawing.Color.White;
            this.Storagetablelist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Storagetablelist.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.Storagetablelist.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(97)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(97)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Storagetablelist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Storagetablelist.ColumnHeadersHeight = 45;
            this.Storagetablelist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Storagetablelist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PartNumber,
            this.ModelName,
            this.PalletName,
            this.Quantity,
            this.PalletID});
            this.Storagetablelist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Storagetablelist.EnableHeadersVisualStyles = false;
            this.Storagetablelist.Location = new System.Drawing.Point(55, 186);
            this.Storagetablelist.Name = "Storagetablelist";
            this.Storagetablelist.ReadOnly = true;
            this.Storagetablelist.RowHeadersVisible = false;
            this.Storagetablelist.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.Storagetablelist.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.Storagetablelist.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.Storagetablelist.RowTemplate.Height = 40;
            this.Storagetablelist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Storagetablelist.Size = new System.Drawing.Size(1231, 460);
            this.Storagetablelist.TabIndex = 1;
            this.Storagetablelist.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Storagetablelist_CellDoubleClick);
            // 
            // PartNumber
            // 
            this.PartNumber.DataPropertyName = "PartNumber";
            this.PartNumber.HeaderText = "Part number";
            this.PartNumber.Name = "PartNumber";
            this.PartNumber.ReadOnly = true;
            // 
            // ModelName
            // 
            this.ModelName.DataPropertyName = "ModelName";
            this.ModelName.HeaderText = "Model name";
            this.ModelName.Name = "ModelName";
            this.ModelName.ReadOnly = true;
            // 
            // PalletName
            // 
            this.PalletName.DataPropertyName = "PalletName";
            this.PalletName.HeaderText = "Pallet ";
            this.PalletName.Name = "PalletName";
            this.PalletName.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // PalletID
            // 
            this.PalletID.DataPropertyName = "PalletID";
            this.PalletID.HeaderText = "Pallet ID";
            this.PalletID.Name = "PalletID";
            this.PalletID.ReadOnly = true;
            this.PalletID.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(1231, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 46);
            this.button1.TabIndex = 9;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(97)))), ((int)(((byte)(235)))));
            this.panel1.Controls.Add(this.letter);
            this.panel1.Location = new System.Drawing.Point(55, -4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(154, 150);
            this.panel1.TabIndex = 10;
            // 
            // StorageLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 703);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Storagetablelist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "StorageLocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "-";
            this.Load += new System.EventHandler(this.StorageLocation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Storagetablelist)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView Storagetablelist;
        private System.Windows.Forms.Label letter;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PalletName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn PalletID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
    }
}