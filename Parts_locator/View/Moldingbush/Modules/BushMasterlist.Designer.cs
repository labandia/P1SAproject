namespace Parts_locator.View.Moldingbush.Modules
{
    partial class BushMasterlist
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BushMasterlist));
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Bushassy = new System.Windows.Forms.TabPage();
            this.ShaftassyGridview = new System.Windows.Forms.DataGridView();
            this.InsertBush = new System.Windows.Forms.TabPage();
            this.InsertBushgrid = new System.Windows.Forms.DataGridView();
            this.PartNumberBush = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RacksBush = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BushQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EditFrame = new System.Windows.Forms.DataGridViewImageColumn();
            this.Bushtap = new System.Windows.Forms.TabPage();
            this.PartNumbershaft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RotorBush = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShaftPartnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Racks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShaftQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.tabControl1.SuspendLayout();
            this.Bushassy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShaftassyGridview)).BeginInit();
            this.InsertBush.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InsertBushgrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 28);
            this.label1.TabIndex = 19;
            this.label1.Text = "Parts locator masterlist";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Bushassy);
            this.tabControl1.Controls.Add(this.InsertBush);
            this.tabControl1.Controls.Add(this.Bushtap);
            this.tabControl1.Location = new System.Drawing.Point(53, 103);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1260, 493);
            this.tabControl1.TabIndex = 25;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // Bushassy
            // 
            this.Bushassy.Controls.Add(this.ShaftassyGridview);
            this.Bushassy.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Bushassy.Location = new System.Drawing.Point(4, 22);
            this.Bushassy.Name = "Bushassy";
            this.Bushassy.Padding = new System.Windows.Forms.Padding(3);
            this.Bushassy.Size = new System.Drawing.Size(1252, 467);
            this.Bushassy.TabIndex = 0;
            this.Bushassy.Text = "Bush shaft Assy";
            this.Bushassy.UseVisualStyleBackColor = true;
            // 
            // ShaftassyGridview
            // 
            this.ShaftassyGridview.AllowUserToAddRows = false;
            this.ShaftassyGridview.AllowUserToDeleteRows = false;
            this.ShaftassyGridview.AllowUserToResizeColumns = false;
            this.ShaftassyGridview.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Poppins", 11.25F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.ShaftassyGridview.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.ShaftassyGridview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ShaftassyGridview.BackgroundColor = System.Drawing.Color.White;
            this.ShaftassyGridview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ShaftassyGridview.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.ShaftassyGridview.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.ShaftassyGridview.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle14.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ShaftassyGridview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.ShaftassyGridview.ColumnHeadersHeight = 45;
            this.ShaftassyGridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ShaftassyGridview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PartNumbershaft,
            this.RotorBush,
            this.ShaftPartnum,
            this.Racks,
            this.ShaftQuantity,
            this.Type,
            this.Edit});
            this.ShaftassyGridview.EnableHeadersVisualStyles = false;
            this.ShaftassyGridview.Location = new System.Drawing.Point(0, 0);
            this.ShaftassyGridview.Name = "ShaftassyGridview";
            this.ShaftassyGridview.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ShaftassyGridview.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.ShaftassyGridview.RowHeadersVisible = false;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Poppins", 11.25F);
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.Black;
            this.ShaftassyGridview.RowsDefaultCellStyle = dataGridViewCellStyle19;
            this.ShaftassyGridview.RowTemplate.Height = 40;
            this.ShaftassyGridview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ShaftassyGridview.Size = new System.Drawing.Size(1246, 461);
            this.ShaftassyGridview.TabIndex = 25;
            this.ShaftassyGridview.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ShaftassyGridview_CellClick);
            this.ShaftassyGridview.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ShaftassyGridview_CellContentClick);
            // 
            // InsertBush
            // 
            this.InsertBush.Controls.Add(this.InsertBushgrid);
            this.InsertBush.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InsertBush.Location = new System.Drawing.Point(4, 22);
            this.InsertBush.Name = "InsertBush";
            this.InsertBush.Padding = new System.Windows.Forms.Padding(3);
            this.InsertBush.Size = new System.Drawing.Size(1252, 467);
            this.InsertBush.TabIndex = 1;
            this.InsertBush.Text = "Mold Frame Insert Bush";
            this.InsertBush.UseVisualStyleBackColor = true;
            // 
            // InsertBushgrid
            // 
            this.InsertBushgrid.AllowUserToAddRows = false;
            this.InsertBushgrid.AllowUserToDeleteRows = false;
            this.InsertBushgrid.AllowUserToResizeColumns = false;
            this.InsertBushgrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.InsertBushgrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle20;
            this.InsertBushgrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.InsertBushgrid.BackgroundColor = System.Drawing.Color.White;
            this.InsertBushgrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InsertBushgrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.InsertBushgrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.InsertBushgrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle21.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InsertBushgrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.InsertBushgrid.ColumnHeadersHeight = 45;
            this.InsertBushgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.InsertBushgrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PartNumberBush,
            this.ModelName,
            this.RacksBush,
            this.BushQuantity,
            this.EditFrame});
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.InsertBushgrid.DefaultCellStyle = dataGridViewCellStyle22;
            this.InsertBushgrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InsertBushgrid.EnableHeadersVisualStyles = false;
            this.InsertBushgrid.GridColor = System.Drawing.Color.White;
            this.InsertBushgrid.Location = new System.Drawing.Point(3, 3);
            this.InsertBushgrid.Name = "InsertBushgrid";
            this.InsertBushgrid.ReadOnly = true;
            this.InsertBushgrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.Color.GhostWhite;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InsertBushgrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.InsertBushgrid.RowHeadersVisible = false;
            this.InsertBushgrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.Color.Black;
            this.InsertBushgrid.RowsDefaultCellStyle = dataGridViewCellStyle24;
            this.InsertBushgrid.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.InsertBushgrid.RowTemplate.Height = 40;
            this.InsertBushgrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.InsertBushgrid.Size = new System.Drawing.Size(1246, 461);
            this.InsertBushgrid.TabIndex = 26;
            this.InsertBushgrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InsertBushgrid_CellClick);
            // 
            // PartNumberBush
            // 
            this.PartNumberBush.DataPropertyName = "PartNumber";
            this.PartNumberBush.HeaderText = "PartNumber";
            this.PartNumberBush.MinimumWidth = 2;
            this.PartNumberBush.Name = "PartNumberBush";
            this.PartNumberBush.ReadOnly = true;
            // 
            // ModelName
            // 
            this.ModelName.DataPropertyName = "ModelName";
            this.ModelName.HeaderText = "ModelName";
            this.ModelName.Name = "ModelName";
            this.ModelName.ReadOnly = true;
            // 
            // RacksBush
            // 
            this.RacksBush.DataPropertyName = "Racks";
            this.RacksBush.FillWeight = 93.81573F;
            this.RacksBush.HeaderText = "Racks";
            this.RacksBush.Name = "RacksBush";
            this.RacksBush.ReadOnly = true;
            // 
            // BushQuantity
            // 
            this.BushQuantity.DataPropertyName = "Quantity";
            this.BushQuantity.HeaderText = "Total Quantity";
            this.BushQuantity.Name = "BushQuantity";
            this.BushQuantity.ReadOnly = true;
            this.BushQuantity.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // EditFrame
            // 
            this.EditFrame.Image = ((System.Drawing.Image)(resources.GetObject("EditFrame.Image")));
            this.EditFrame.Name = "EditFrame";
            this.EditFrame.ReadOnly = true;
            // 
            // Bushtap
            // 
            this.Bushtap.Location = new System.Drawing.Point(4, 22);
            this.Bushtap.Name = "Bushtap";
            this.Bushtap.Size = new System.Drawing.Size(1252, 467);
            this.Bushtap.TabIndex = 2;
            this.Bushtap.Text = "Bush Tap";
            this.Bushtap.UseVisualStyleBackColor = true;
            // 
            // PartNumbershaft
            // 
            this.PartNumbershaft.DataPropertyName = "PartNumber";
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.MediumPurple;
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Padding = new System.Windows.Forms.Padding(10);
            this.PartNumbershaft.DefaultCellStyle = dataGridViewCellStyle15;
            this.PartNumbershaft.FillWeight = 91.64914F;
            this.PartNumbershaft.HeaderText = "PartNumber";
            this.PartNumbershaft.MinimumWidth = 2;
            this.PartNumbershaft.Name = "PartNumbershaft";
            this.PartNumbershaft.ReadOnly = true;
            // 
            // RotorBush
            // 
            this.RotorBush.DataPropertyName = "RotorBush";
            this.RotorBush.HeaderText = "Rotor Bush";
            this.RotorBush.Name = "RotorBush";
            this.RotorBush.ReadOnly = true;
            // 
            // ShaftPartnum
            // 
            this.ShaftPartnum.DataPropertyName = "ShaftPartnum";
            this.ShaftPartnum.HeaderText = "Shaft Part Number";
            this.ShaftPartnum.Name = "ShaftPartnum";
            this.ShaftPartnum.ReadOnly = true;
            // 
            // Racks
            // 
            this.Racks.DataPropertyName = "Racks";
            this.Racks.FillWeight = 93.81573F;
            this.Racks.HeaderText = "Racks";
            this.Racks.Name = "Racks";
            this.Racks.ReadOnly = true;
            // 
            // ShaftQuantity
            // 
            this.ShaftQuantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ShaftQuantity.DefaultCellStyle = dataGridViewCellStyle16;
            this.ShaftQuantity.FillWeight = 93.81573F;
            this.ShaftQuantity.HeaderText = "Total Quantity";
            this.ShaftQuantity.Name = "ShaftQuantity";
            this.ShaftQuantity.ReadOnly = true;
            this.ShaftQuantity.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            // 
            // Edit
            // 
            this.Edit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.NullValue = null;
            this.Edit.DefaultCellStyle = dataGridViewCellStyle17;
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = ((System.Drawing.Image)(resources.GetObject("Edit.Image")));
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // BushMasterlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Name = "BushMasterlist";
            this.Size = new System.Drawing.Size(1366, 641);
            this.Load += new System.EventHandler(this.BushMasterlist_Load);
            this.tabControl1.ResumeLayout(false);
            this.Bushassy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ShaftassyGridview)).EndInit();
            this.InsertBush.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InsertBushgrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Bushassy;
        private System.Windows.Forms.TabPage InsertBush;
        private System.Windows.Forms.TabPage Bushtap;
        private System.Windows.Forms.DataGridView ShaftassyGridview;
        private System.Windows.Forms.DataGridView InsertBushgrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumberBush;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RacksBush;
        private System.Windows.Forms.DataGridViewTextBoxColumn BushQuantity;
        private System.Windows.Forms.DataGridViewImageColumn EditFrame;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumbershaft;
        private System.Windows.Forms.DataGridViewTextBoxColumn RotorBush;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShaftPartnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Racks;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShaftQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
    }
}
