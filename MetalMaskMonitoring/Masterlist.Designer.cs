namespace MetalMaskMonitoring
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Masterlist));
            this.MetalMaskTable = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.CountTable = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.PartnumText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Modelselect = new System.Windows.Forms.ComboBox();
            this.RecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Partnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AREA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Alternate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Side = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Thickness = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Blocks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Condition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.MetalMaskTable)).BeginInit();
            this.SuspendLayout();
            // 
            // MetalMaskTable
            // 
            this.MetalMaskTable.AllowUserToAddRows = false;
            this.MetalMaskTable.AllowUserToDeleteRows = false;
            this.MetalMaskTable.AllowUserToResizeColumns = false;
            this.MetalMaskTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MetalMaskTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.MetalMaskTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MetalMaskTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.MetalMaskTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MetalMaskTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.MetalMaskTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MetalMaskTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecordID,
            this.Partnumber,
            this.AREA,
            this.Alternate,
            this.Side,
            this.Thickness,
            this.Blocks,
            this.Condition,
            this.Remarks,
            this.ModelType});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MetalMaskTable.DefaultCellStyle = dataGridViewCellStyle3;
            this.MetalMaskTable.Location = new System.Drawing.Point(30, 115);
            this.MetalMaskTable.Name = "MetalMaskTable";
            this.MetalMaskTable.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MetalMaskTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.MetalMaskTable.RowHeadersVisible = false;
            this.MetalMaskTable.RowTemplate.Height = 35;
            this.MetalMaskTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MetalMaskTable.Size = new System.Drawing.Size(1279, 579);
            this.MetalMaskTable.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(1156, 45);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(15, 0, 10, 0);
            this.button1.Size = new System.Drawing.Size(153, 40);
            this.button1.TabIndex = 5;
            this.button1.Text = "Export Analysis";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // CountTable
            // 
            this.CountTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CountTable.AutoSize = true;
            this.CountTable.BackColor = System.Drawing.Color.Transparent;
            this.CountTable.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CountTable.Location = new System.Drawing.Point(1289, 708);
            this.CountTable.Name = "CountTable";
            this.CountTable.Size = new System.Drawing.Size(13, 17);
            this.CountTable.TabIndex = 100;
            this.CountTable.Text = "-";
            this.CountTable.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(1169, 708);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 99;
            this.label1.Text = "Total Records : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label8.Location = new System.Drawing.Point(27, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 17);
            this.label8.TabIndex = 118;
            this.label8.Text = "Search by Partnum: ";
            // 
            // PartnumText
            // 
            this.PartnumText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartnumText.Location = new System.Drawing.Point(27, 65);
            this.PartnumText.Name = "PartnumText";
            this.PartnumText.Size = new System.Drawing.Size(179, 22);
            this.PartnumText.TabIndex = 117;
            this.PartnumText.TextChanged += new System.EventHandler(this.PartnumText_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(228, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 17);
            this.label6.TabIndex = 120;
            this.label6.Text = "Filter by Model";
            // 
            // Modelselect
            // 
            this.Modelselect.FormattingEnabled = true;
            this.Modelselect.Items.AddRange(new object[] {
            "UPS ",
            "FAN",
            "SERVO"});
            this.Modelselect.Location = new System.Drawing.Point(231, 65);
            this.Modelselect.Name = "Modelselect";
            this.Modelselect.Size = new System.Drawing.Size(121, 21);
            this.Modelselect.TabIndex = 119;
            this.Modelselect.SelectedIndexChanged += new System.EventHandler(this.Modelselect_SelectedIndexChanged);
            // 
            // RecordID
            // 
            this.RecordID.DataPropertyName = "RecordID";
            this.RecordID.HeaderText = "RecordID";
            this.RecordID.Name = "RecordID";
            this.RecordID.ReadOnly = true;
            this.RecordID.Visible = false;
            // 
            // Partnumber
            // 
            this.Partnumber.DataPropertyName = "Partnumber";
            this.Partnumber.HeaderText = "Partnumber";
            this.Partnumber.Name = "Partnumber";
            this.Partnumber.ReadOnly = true;
            // 
            // AREA
            // 
            this.AREA.DataPropertyName = "AREA";
            this.AREA.HeaderText = "AREA";
            this.AREA.Name = "AREA";
            this.AREA.ReadOnly = true;
            // 
            // Alternate
            // 
            this.Alternate.DataPropertyName = "Alternate";
            this.Alternate.HeaderText = "Alternate";
            this.Alternate.Name = "Alternate";
            this.Alternate.ReadOnly = true;
            // 
            // Side
            // 
            this.Side.DataPropertyName = "Side";
            this.Side.HeaderText = "Side";
            this.Side.Name = "Side";
            this.Side.ReadOnly = true;
            // 
            // Thickness
            // 
            this.Thickness.DataPropertyName = "Thickness";
            this.Thickness.HeaderText = "Thickness";
            this.Thickness.Name = "Thickness";
            this.Thickness.ReadOnly = true;
            // 
            // Blocks
            // 
            this.Blocks.DataPropertyName = "Blocks";
            this.Blocks.HeaderText = "Blocks";
            this.Blocks.Name = "Blocks";
            this.Blocks.ReadOnly = true;
            // 
            // Condition
            // 
            this.Condition.DataPropertyName = "Condition";
            this.Condition.HeaderText = "Condition";
            this.Condition.Name = "Condition";
            this.Condition.ReadOnly = true;
            // 
            // Remarks
            // 
            this.Remarks.DataPropertyName = "Remarks";
            this.Remarks.HeaderText = "Remarks";
            this.Remarks.Name = "Remarks";
            this.Remarks.ReadOnly = true;
            // 
            // ModelType
            // 
            this.ModelType.DataPropertyName = "ModelType";
            this.ModelType.HeaderText = "ModelType";
            this.ModelType.Name = "ModelType";
            this.ModelType.ReadOnly = true;
            this.ModelType.Visible = false;
            // 
            // Masterlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1341, 753);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Modelselect);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.PartnumText);
            this.Controls.Add(this.CountTable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.MetalMaskTable);
            this.Name = "Masterlist";
            this.Text = "Masterlist";
            this.Load += new System.EventHandler(this.Masterlist_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MetalMaskTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView MetalMaskTable;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label CountTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox PartnumText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox Modelselect;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Partnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn AREA;
        private System.Windows.Forms.DataGridViewTextBoxColumn Alternate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Side;
        private System.Windows.Forms.DataGridViewTextBoxColumn Thickness;
        private System.Windows.Forms.DataGridViewTextBoxColumn Blocks;
        private System.Windows.Forms.DataGridViewTextBoxColumn Condition;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelType;
    }
}