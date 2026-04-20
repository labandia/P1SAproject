namespace FanTraceableSystem
{
    partial class EditPCBShop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPCBShop));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.QuanText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Shoptext = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RevText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.LotNotext = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LineText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.IssuerText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.RecordId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PCBShopOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PCBIssuer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 35;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecordId,
            this.PCBShopOrder,
            this.LotNo,
            this.Line,
            this.Quantity,
            this.Rev,
            this.PCBIssuer,
            this.isAction,
            this.Delete});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Location = new System.Drawing.Point(359, 48);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 35;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(942, 524);
            this.dataGridView1.TabIndex = 40;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(57, 577);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(224, 35);
            this.button1.TabIndex = 39;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.BackColor = System.Drawing.Color.Teal;
            this.SaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveBtn.ForeColor = System.Drawing.Color.White;
            this.SaveBtn.Location = new System.Drawing.Point(1050, 609);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(251, 42);
            this.SaveBtn.TabIndex = 38;
            this.SaveBtn.Text = "Done";
            this.SaveBtn.UseVisualStyleBackColor = false;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // QuanText
            // 
            this.QuanText.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.QuanText.Location = new System.Drawing.Point(57, 252);
            this.QuanText.Name = "QuanText";
            this.QuanText.Size = new System.Drawing.Size(224, 23);
            this.QuanText.TabIndex = 37;
            this.QuanText.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(54, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 36;
            this.label2.Text = "Quantity ";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Shoptext
            // 
            this.Shoptext.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.Shoptext.Location = new System.Drawing.Point(58, 76);
            this.Shoptext.Name = "Shoptext";
            this.Shoptext.Size = new System.Drawing.Size(223, 23);
            this.Shoptext.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(55, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 34;
            this.label1.Text = "ShopOrder";
            // 
            // RevText
            // 
            this.RevText.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.RevText.Location = new System.Drawing.Point(57, 349);
            this.RevText.Name = "RevText";
            this.RevText.Size = new System.Drawing.Size(223, 23);
            this.RevText.TabIndex = 43;
            this.RevText.TextChanged += new System.EventHandler(this.RevText_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(54, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 15);
            this.label3.TabIndex = 42;
            this.label3.Text = "Lot No :";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(797, 609);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(220, 42);
            this.button2.TabIndex = 41;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // LotNotext
            // 
            this.LotNotext.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.LotNotext.Location = new System.Drawing.Point(58, 163);
            this.LotNotext.Name = "LotNotext";
            this.LotNotext.Size = new System.Drawing.Size(224, 23);
            this.LotNotext.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(54, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 15);
            this.label4.TabIndex = 44;
            this.label4.Text = "Revision (for Circuit)";
            // 
            // LineText
            // 
            this.LineText.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.LineText.Location = new System.Drawing.Point(57, 430);
            this.LineText.Name = "LineText";
            this.LineText.Size = new System.Drawing.Size(224, 23);
            this.LineText.TabIndex = 53;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(54, 403);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 15);
            this.label7.TabIndex = 52;
            this.label7.Text = "Line :";
            // 
            // IssuerText
            // 
            this.IssuerText.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.IssuerText.Location = new System.Drawing.Point(57, 510);
            this.IssuerText.Name = "IssuerText";
            this.IssuerText.Size = new System.Drawing.Size(224, 23);
            this.IssuerText.TabIndex = 55;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(54, 477);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 15);
            this.label5.TabIndex = 54;
            this.label5.Text = "Issued By : ";
            // 
            // RecordId
            // 
            this.RecordId.DataPropertyName = "RecordId";
            this.RecordId.HeaderText = "RecordId";
            this.RecordId.Name = "RecordId";
            this.RecordId.Visible = false;
            // 
            // PCBShopOrder
            // 
            this.PCBShopOrder.DataPropertyName = "PCBShopOrder";
            this.PCBShopOrder.HeaderText = "Shop Order";
            this.PCBShopOrder.Name = "PCBShopOrder";
            // 
            // LotNo
            // 
            this.LotNo.DataPropertyName = "LotNo";
            this.LotNo.HeaderText = "LotNo";
            this.LotNo.Name = "LotNo";
            // 
            // Line
            // 
            this.Line.DataPropertyName = "Line";
            this.Line.HeaderText = "Line";
            this.Line.Name = "Line";
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            // 
            // Rev
            // 
            this.Rev.DataPropertyName = "Rev";
            this.Rev.HeaderText = "Revision";
            this.Rev.Name = "Rev";
            // 
            // PCBIssuer
            // 
            this.PCBIssuer.DataPropertyName = "PCBIssuer";
            this.PCBIssuer.HeaderText = "Issuer ";
            this.PCBIssuer.Name = "PCBIssuer";
            // 
            // isAction
            // 
            this.isAction.DataPropertyName = "isAction";
            this.isAction.HeaderText = "isAction";
            this.isAction.Name = "isAction";
            this.isAction.Visible = false;
            // 
            // Delete
            // 
            this.Delete.DataPropertyName = "Delete";
            this.Delete.HeaderText = "Delete";
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Name = "Delete";
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // EditPCBShop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 684);
            this.Controls.Add(this.IssuerText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LineText);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.LotNotext);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.QuanText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Shoptext);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RevText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Name = "EditPCBShop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditPCBShop";
            this.Load += new System.EventHandler(this.EditPCBShop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.TextBox QuanText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Shoptext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox RevText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox LotNotext;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox LineText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox IssuerText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordId;
        private System.Windows.Forms.DataGridViewTextBoxColumn PCBShopOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Line;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rev;
        private System.Windows.Forms.DataGridViewTextBoxColumn PCBIssuer;
        private System.Windows.Forms.DataGridViewTextBoxColumn isAction;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
    }
}