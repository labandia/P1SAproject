namespace ProductConfirm.Modules
{
    partial class Masterlistpage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Masterlistpage));
            this.projectitle = new System.Windows.Forms.Label();
            this.Masterlistable = new System.Windows.Forms.DataGridView();
            this.RotorProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RotorAssy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachinePressureMinMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CaulkingDentMinMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CaulkingDentTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShaftLengthMinMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SEA_MinMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShaftPullingForce = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BushPullingForce = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MagnetHeightMinMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.searchbox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Masterlistable)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // projectitle
            // 
            this.projectitle.AutoSize = true;
            this.projectitle.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectitle.ForeColor = System.Drawing.Color.Black;
            this.projectitle.Location = new System.Drawing.Point(32, 51);
            this.projectitle.Name = "projectitle";
            this.projectitle.Size = new System.Drawing.Size(216, 28);
            this.projectitle.TabIndex = 2;
            this.projectitle.Text = "Product Masterlist";
            // 
            // Masterlistable
            // 
            this.Masterlistable.AllowUserToAddRows = false;
            this.Masterlistable.AllowUserToDeleteRows = false;
            this.Masterlistable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(15);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Masterlistable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Masterlistable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Masterlistable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Masterlistable.BackgroundColor = System.Drawing.Color.White;
            this.Masterlistable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Masterlistable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.Masterlistable.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.Masterlistable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Masterlistable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Masterlistable.ColumnHeadersHeight = 45;
            this.Masterlistable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Masterlistable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RotorProductID,
            this.RotorAssy,
            this.ProductType,
            this.MachinePressureMinMax,
            this.CaulkingDentMinMax,
            this.CaulkingDentTarget,
            this.ShaftLengthMinMax,
            this.SEA_MinMax,
            this.ShaftPullingForce,
            this.BushPullingForce,
            this.MagnetHeightMinMax,
            this.ModelType,
            this.Edit});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Masterlistable.DefaultCellStyle = dataGridViewCellStyle5;
            this.Masterlistable.EnableHeadersVisualStyles = false;
            this.Masterlistable.Location = new System.Drawing.Point(36, 114);
            this.Masterlistable.Name = "Masterlistable";
            this.Masterlistable.ReadOnly = true;
            this.Masterlistable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Masterlistable.RowHeadersVisible = false;
            this.Masterlistable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Masterlistable.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.Masterlistable.RowTemplate.Height = 40;
            this.Masterlistable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Masterlistable.Size = new System.Drawing.Size(1283, 460);
            this.Masterlistable.TabIndex = 3;
            this.Masterlistable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Masterlistable_CellClick);
            // 
            // RotorProductID
            // 
            this.RotorProductID.DataPropertyName = "RotorProductID";
            this.RotorProductID.HeaderText = "RotorProductID";
            this.RotorProductID.Name = "RotorProductID";
            this.RotorProductID.ReadOnly = true;
            this.RotorProductID.Visible = false;
            this.RotorProductID.Width = 137;
            // 
            // RotorAssy
            // 
            this.RotorAssy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.RotorAssy.DataPropertyName = "RotorAssy";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.RotorAssy.DefaultCellStyle = dataGridViewCellStyle3;
            this.RotorAssy.HeaderText = "Rotor Magnet Assy.";
            this.RotorAssy.Name = "RotorAssy";
            this.RotorAssy.ReadOnly = true;
            this.RotorAssy.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.RotorAssy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RotorAssy.Width = 150;
            // 
            // ProductType
            // 
            this.ProductType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProductType.DataPropertyName = "ProductType";
            this.ProductType.HeaderText = "Model name";
            this.ProductType.Name = "ProductType";
            this.ProductType.ReadOnly = true;
            this.ProductType.Width = 300;
            // 
            // MachinePressureMinMax
            // 
            this.MachinePressureMinMax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MachinePressureMinMax.DataPropertyName = "MachinePressureMinMax";
            this.MachinePressureMinMax.HeaderText = "Machine Pressure Setting Range (Mpa)";
            this.MachinePressureMinMax.Name = "MachinePressureMinMax";
            this.MachinePressureMinMax.ReadOnly = true;
            this.MachinePressureMinMax.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MachinePressureMinMax.Width = 200;
            // 
            // CaulkingDentMinMax
            // 
            this.CaulkingDentMinMax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CaulkingDentMinMax.DataPropertyName = "CaulkingDentMinMax";
            this.CaulkingDentMinMax.HeaderText = "Caulking Dent(mm)";
            this.CaulkingDentMinMax.Name = "CaulkingDentMinMax";
            this.CaulkingDentMinMax.ReadOnly = true;
            this.CaulkingDentMinMax.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CaulkingDentMinMax.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CaulkingDentMinMax.Width = 200;
            // 
            // CaulkingDentTarget
            // 
            this.CaulkingDentTarget.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CaulkingDentTarget.DataPropertyName = "CaulkingDentTarget";
            this.CaulkingDentTarget.HeaderText = "Target Caulking Dent Height (mm)";
            this.CaulkingDentTarget.Name = "CaulkingDentTarget";
            this.CaulkingDentTarget.ReadOnly = true;
            this.CaulkingDentTarget.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CaulkingDentTarget.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CaulkingDentTarget.Width = 200;
            // 
            // ShaftLengthMinMax
            // 
            this.ShaftLengthMinMax.DataPropertyName = "ShaftLengthMinMax";
            this.ShaftLengthMinMax.HeaderText = "Shaft Length (mm)";
            this.ShaftLengthMinMax.Name = "ShaftLengthMinMax";
            this.ShaftLengthMinMax.ReadOnly = true;
            this.ShaftLengthMinMax.Width = 144;
            // 
            // SEA_MinMax
            // 
            this.SEA_MinMax.DataPropertyName = "SEA_MinMax";
            this.SEA_MinMax.HeaderText = "S.E.A (mm)";
            this.SEA_MinMax.Name = "SEA_MinMax";
            this.SEA_MinMax.ReadOnly = true;
            this.SEA_MinMax.Width = 102;
            // 
            // ShaftPullingForce
            // 
            this.ShaftPullingForce.DataPropertyName = "ShaftPullingForce";
            this.ShaftPullingForce.HeaderText = "Shaft Pulling Force";
            this.ShaftPullingForce.Name = "ShaftPullingForce";
            this.ShaftPullingForce.ReadOnly = true;
            this.ShaftPullingForce.Width = 146;
            // 
            // BushPullingForce
            // 
            this.BushPullingForce.DataPropertyName = "BushPullingForce";
            this.BushPullingForce.HeaderText = "Bush Pulling Force";
            this.BushPullingForce.Name = "BushPullingForce";
            this.BushPullingForce.ReadOnly = true;
            this.BushPullingForce.Width = 144;
            // 
            // MagnetHeightMinMax
            // 
            this.MagnetHeightMinMax.DataPropertyName = "MagnetHeightMinMax";
            this.MagnetHeightMinMax.HeaderText = "MagnetHeightMinMax";
            this.MagnetHeightMinMax.Name = "MagnetHeightMinMax";
            this.MagnetHeightMinMax.ReadOnly = true;
            this.MagnetHeightMinMax.Width = 184;
            // 
            // ModelType
            // 
            this.ModelType.DataPropertyName = "ModelType";
            this.ModelType.HeaderText = "ModelType";
            this.ModelType.Name = "ModelType";
            this.ModelType.ReadOnly = true;
            this.ModelType.Visible = false;
            this.ModelType.Width = 114;
            // 
            // Edit
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle4.NullValue")));
            this.Edit.DefaultCellStyle = dataGridViewCellStyle4;
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = ((System.Drawing.Image)(resources.GetObject("Edit.Image")));
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Width = 45;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(27)))), ((int)(((byte)(156)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(27)))), ((int)(((byte)(156)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(884, 48);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.button1.Size = new System.Drawing.Size(164, 42);
            this.button1.TabIndex = 6;
            this.button1.Text = "Add Product";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(43, 587);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 18);
            this.label1.TabIndex = 50;
            this.label1.Text = "Total Masterlist :";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.searchbox);
            this.panel1.Location = new System.Drawing.Point(1064, 49);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(254, 40);
            this.panel1.TabIndex = 51;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Transparent;
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.Location = new System.Drawing.Point(6, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 24);
            this.label6.TabIndex = 34;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // searchbox
            // 
            this.searchbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchbox.BackColor = System.Drawing.Color.White;
            this.searchbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchbox.Font = new System.Drawing.Font("Century Gothic", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchbox.Location = new System.Drawing.Point(46, 7);
            this.searchbox.MaximumSize = new System.Drawing.Size(198, 30);
            this.searchbox.MaxLength = 11;
            this.searchbox.MinimumSize = new System.Drawing.Size(198, 30);
            this.searchbox.Name = "searchbox";
            this.searchbox.Size = new System.Drawing.Size(198, 21);
            this.searchbox.TabIndex = 32;
            this.searchbox.WordWrap = false;
            this.searchbox.TextChanged += new System.EventHandler(this.searchbox_TextChanged);
            // 
            // Masterlistpage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Masterlistable);
            this.Controls.Add(this.projectitle);
            this.Name = "Masterlistpage";
            this.Size = new System.Drawing.Size(1350, 626);
            ((System.ComponentModel.ISupportInitialize)(this.Masterlistable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label projectitle;
        private System.Windows.Forms.DataGridView Masterlistable;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox searchbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn RotorProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RotorAssy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductType;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachinePressureMinMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn CaulkingDentMinMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn CaulkingDentTarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShaftLengthMinMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEA_MinMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShaftPullingForce;
        private System.Windows.Forms.DataGridViewTextBoxColumn BushPullingForce;
        private System.Windows.Forms.DataGridViewTextBoxColumn MagnetHeightMinMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelType;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
    }
}
