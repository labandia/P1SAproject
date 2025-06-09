namespace Parts_locator.Modules
{
    partial class Masterlist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Masterlist));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Partnumtext = new System.Windows.Forms.TextBox();
            this.Exportbtn = new System.Windows.Forms.Button();
            this.GtotalText = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MasterlistTable = new System.Windows.Forms.DataGridView();
            this.PartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PalletName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Result = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MasterlistTable)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(44, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Official list of parts locator system";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(41, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "Parts locator masterlist";
            // 
            // Partnumtext
            // 
            this.Partnumtext.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Partnumtext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Partnumtext.Dock = System.Windows.Forms.DockStyle.Left;
            this.Partnumtext.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Partnumtext.Location = new System.Drawing.Point(5, 5);
            this.Partnumtext.Multiline = true;
            this.Partnumtext.Name = "Partnumtext";
            this.Partnumtext.Size = new System.Drawing.Size(130, 28);
            this.Partnumtext.TabIndex = 5;
            this.Partnumtext.TextChanged += new System.EventHandler(this.Partnumtext_TextChanged);
            // 
            // Exportbtn
            // 
            this.Exportbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Exportbtn.BackColor = System.Drawing.Color.White;
            this.Exportbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exportbtn.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.Exportbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exportbtn.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exportbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Exportbtn.Image = ((System.Drawing.Image)(resources.GetObject("Exportbtn.Image")));
            this.Exportbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Exportbtn.Location = new System.Drawing.Point(758, 131);
            this.Exportbtn.Name = "Exportbtn";
            this.Exportbtn.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.Exportbtn.Size = new System.Drawing.Size(157, 38);
            this.Exportbtn.TabIndex = 7;
            this.Exportbtn.Text = "Import Data";
            this.Exportbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Exportbtn.UseVisualStyleBackColor = false;
            this.Exportbtn.Click += new System.EventHandler(this.Exportbtn_Click);
            // 
            // GtotalText
            // 
            this.GtotalText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GtotalText.Font = new System.Drawing.Font("Century Gothic", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GtotalText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(97)))), ((int)(((byte)(235)))));
            this.GtotalText.Location = new System.Drawing.Point(729, 60);
            this.GtotalText.Name = "GtotalText";
            this.GtotalText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GtotalText.Size = new System.Drawing.Size(196, 47);
            this.GtotalText.TabIndex = 8;
            this.GtotalText.Text = "3,461,696";
            this.GtotalText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.label4.Location = new System.Drawing.Point(814, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Total Quantity ";
            // 
            // MasterlistTable
            // 
            this.MasterlistTable.AllowUserToAddRows = false;
            this.MasterlistTable.AllowUserToDeleteRows = false;
            this.MasterlistTable.AllowUserToResizeColumns = false;
            this.MasterlistTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle41.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle41.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle41.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle41.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle41.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MasterlistTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle41;
            this.MasterlistTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MasterlistTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.MasterlistTable.BackgroundColor = System.Drawing.Color.White;
            this.MasterlistTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MasterlistTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.MasterlistTable.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.MasterlistTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle42.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle42.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            dataGridViewCellStyle42.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle42.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle42.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MasterlistTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle42;
            this.MasterlistTable.ColumnHeadersHeight = 45;
            this.MasterlistTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.MasterlistTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PartNumber,
            this.ModelName,
            this.PalletName,
            this.Quantity,
            this.Edit});
            dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle46.BackColor = System.Drawing.Color.SandyBrown;
            dataGridViewCellStyle46.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle46.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            dataGridViewCellStyle46.SelectionBackColor = System.Drawing.Color.Thistle;
            dataGridViewCellStyle46.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle46.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MasterlistTable.DefaultCellStyle = dataGridViewCellStyle46;
            this.MasterlistTable.EnableHeadersVisualStyles = false;
            this.MasterlistTable.GridColor = System.Drawing.Color.DarkGray;
            this.MasterlistTable.Location = new System.Drawing.Point(46, 198);
            this.MasterlistTable.Name = "MasterlistTable";
            this.MasterlistTable.ReadOnly = true;
            this.MasterlistTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle47.BackColor = System.Drawing.Color.MediumPurple;
            dataGridViewCellStyle47.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle47.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle47.SelectionBackColor = System.Drawing.Color.MediumPurple;
            dataGridViewCellStyle47.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle47.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MasterlistTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle47;
            this.MasterlistTable.RowHeadersVisible = false;
            this.MasterlistTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle48.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle48.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle48.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle48.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            dataGridViewCellStyle48.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle48.SelectionForeColor = System.Drawing.Color.Black;
            this.MasterlistTable.RowsDefaultCellStyle = dataGridViewCellStyle48;
            this.MasterlistTable.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.MasterlistTable.RowTemplate.Height = 50;
            this.MasterlistTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MasterlistTable.Size = new System.Drawing.Size(869, 324);
            this.MasterlistTable.TabIndex = 10;
            this.MasterlistTable.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.MasterlistTable_CellFormatting);
            // 
            // PartNumber
            // 
            this.PartNumber.DataPropertyName = "PartNumber";
            dataGridViewCellStyle43.BackColor = System.Drawing.Color.MediumPurple;
            dataGridViewCellStyle43.ForeColor = System.Drawing.Color.White;
            this.PartNumber.DefaultCellStyle = dataGridViewCellStyle43;
            this.PartNumber.FillWeight = 91.64914F;
            this.PartNumber.HeaderText = "PartNumber";
            this.PartNumber.MinimumWidth = 2;
            this.PartNumber.Name = "PartNumber";
            this.PartNumber.ReadOnly = true;
            // 
            // ModelName
            // 
            this.ModelName.DataPropertyName = "ModelName";
            this.ModelName.FillWeight = 93.81573F;
            this.ModelName.HeaderText = "Model Name";
            this.ModelName.Name = "ModelName";
            this.ModelName.ReadOnly = true;
            // 
            // PalletName
            // 
            this.PalletName.DataPropertyName = "PalletName";
            this.PalletName.FillWeight = 93.81573F;
            this.PalletName.HeaderText = "Location";
            this.PalletName.Name = "PalletName";
            this.PalletName.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle44;
            this.Quantity.FillWeight = 93.81573F;
            this.Quantity.HeaderText = "Total Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Edit
            // 
            this.Edit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle45.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle45.NullValue")));
            this.Edit.DefaultCellStyle = dataGridViewCellStyle45;
            this.Edit.FillWeight = 200.9035F;
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = ((System.Drawing.Image)(resources.GetObject("Edit.Image")));
            this.Edit.MinimumWidth = 80;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Edit.ToolTipText = "Edit";
            this.Edit.Width = 80;
            // 
            // Result
            // 
            this.Result.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Result.AutoSize = true;
            this.Result.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Result.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Result.Location = new System.Drawing.Point(44, 548);
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(102, 16);
            this.Result.TabIndex = 11;
            this.Result.Text = "Total Records :";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Controls.Add(this.Partnumtext);
            this.panel1.Controls.Add(this.label6);
            this.panel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(47, 131);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(187, 38);
            this.panel1.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Transparent;
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.Location = new System.Drawing.Point(148, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 39);
            this.label6.TabIndex = 16;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(536, 131);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.button1.Size = new System.Drawing.Size(198, 38);
            this.button1.TabIndex = 17;
            this.button1.Text = "Download template";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // Masterlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.MasterlistTable);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.GtotalText);
            this.Controls.Add(this.Exportbtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Masterlist";
            this.Size = new System.Drawing.Size(959, 601);
            this.Load += new System.EventHandler(this.Masterlist_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MasterlistTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Partnumtext;
        private System.Windows.Forms.Button Exportbtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView MasterlistTable;
        private System.Windows.Forms.Label Result;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label GtotalText;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PalletName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
    }
}
