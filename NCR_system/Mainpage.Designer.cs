namespace NCR_system
{
    partial class Mainpage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainpage));
            this.panelContainer = new System.Windows.Forms.Panel();
            this.Headerpanel = new System.Windows.Forms.Panel();
            this.Shipmentbtn = new System.Windows.Forms.Button();
            this.Rejectedbtn = new System.Windows.Forms.Button();
            this.ncrbtn = new System.Windows.Forms.Button();
            this.Customerbtn = new System.Windows.Forms.Button();
            this.processbtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.projectitle = new System.Windows.Forms.Label();
            this.Headerpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 80);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1331, 638);
            this.panelContainer.TabIndex = 3;
            this.panelContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContainer_Paint);
            // 
            // Headerpanel
            // 
            this.Headerpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(4)))), ((int)(((byte)(39)))));
            this.Headerpanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Headerpanel.BackgroundImage")));
            this.Headerpanel.Controls.Add(this.Shipmentbtn);
            this.Headerpanel.Controls.Add(this.Rejectedbtn);
            this.Headerpanel.Controls.Add(this.ncrbtn);
            this.Headerpanel.Controls.Add(this.Customerbtn);
            this.Headerpanel.Controls.Add(this.processbtn);
            this.Headerpanel.Controls.Add(this.panel1);
            this.Headerpanel.Controls.Add(this.projectitle);
            this.Headerpanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Headerpanel.Location = new System.Drawing.Point(0, 0);
            this.Headerpanel.Name = "Headerpanel";
            this.Headerpanel.Size = new System.Drawing.Size(1331, 80);
            this.Headerpanel.TabIndex = 2;
            // 
            // Shipmentbtn
            // 
            this.Shipmentbtn.BackColor = System.Drawing.Color.Transparent;
            this.Shipmentbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Shipmentbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(4)))), ((int)(((byte)(39)))));
            this.Shipmentbtn.FlatAppearance.BorderSize = 0;
            this.Shipmentbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.Shipmentbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.Shipmentbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Shipmentbtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Shipmentbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(176)))), ((int)(((byte)(192)))));
            this.Shipmentbtn.Location = new System.Drawing.Point(937, 0);
            this.Shipmentbtn.Name = "Shipmentbtn";
            this.Shipmentbtn.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.Shipmentbtn.Size = new System.Drawing.Size(132, 64);
            this.Shipmentbtn.TabIndex = 15;
            this.Shipmentbtn.Text = "Shipment";
            this.Shipmentbtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Shipmentbtn.UseVisualStyleBackColor = false;
            this.Shipmentbtn.Click += new System.EventHandler(this.Shipmentbtn_Click);
            // 
            // Rejectedbtn
            // 
            this.Rejectedbtn.BackColor = System.Drawing.Color.Transparent;
            this.Rejectedbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Rejectedbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(4)))), ((int)(((byte)(39)))));
            this.Rejectedbtn.FlatAppearance.BorderSize = 0;
            this.Rejectedbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.Rejectedbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.Rejectedbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Rejectedbtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rejectedbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(176)))), ((int)(((byte)(192)))));
            this.Rejectedbtn.Location = new System.Drawing.Point(768, 0);
            this.Rejectedbtn.Name = "Rejectedbtn";
            this.Rejectedbtn.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.Rejectedbtn.Size = new System.Drawing.Size(132, 64);
            this.Rejectedbtn.TabIndex = 14;
            this.Rejectedbtn.Text = "Rejected Lot";
            this.Rejectedbtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Rejectedbtn.UseVisualStyleBackColor = false;
            this.Rejectedbtn.Click += new System.EventHandler(this.Rejectedbtn_Click);
            // 
            // ncrbtn
            // 
            this.ncrbtn.BackColor = System.Drawing.Color.Transparent;
            this.ncrbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ncrbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(4)))), ((int)(((byte)(39)))));
            this.ncrbtn.FlatAppearance.BorderSize = 0;
            this.ncrbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.ncrbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.ncrbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ncrbtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ncrbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(176)))), ((int)(((byte)(192)))));
            this.ncrbtn.Location = new System.Drawing.Point(617, 0);
            this.ncrbtn.Name = "ncrbtn";
            this.ncrbtn.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.ncrbtn.Size = new System.Drawing.Size(132, 64);
            this.ncrbtn.TabIndex = 13;
            this.ncrbtn.Text = "NCR";
            this.ncrbtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ncrbtn.UseVisualStyleBackColor = false;
            this.ncrbtn.Click += new System.EventHandler(this.ncrbtn_Click);
            // 
            // Customerbtn
            // 
            this.Customerbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.Customerbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Customerbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(4)))), ((int)(((byte)(39)))));
            this.Customerbtn.FlatAppearance.BorderSize = 0;
            this.Customerbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.Customerbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.Customerbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Customerbtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Customerbtn.ForeColor = System.Drawing.Color.White;
            this.Customerbtn.Location = new System.Drawing.Point(289, 0);
            this.Customerbtn.Name = "Customerbtn";
            this.Customerbtn.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.Customerbtn.Size = new System.Drawing.Size(184, 64);
            this.Customerbtn.TabIndex = 10;
            this.Customerbtn.Text = "Customer Complaint";
            this.Customerbtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Customerbtn.UseVisualStyleBackColor = false;
            this.Customerbtn.Click += new System.EventHandler(this.Customerbtn_Click);
            // 
            // processbtn
            // 
            this.processbtn.BackColor = System.Drawing.Color.Transparent;
            this.processbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.processbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(4)))), ((int)(((byte)(39)))));
            this.processbtn.FlatAppearance.BorderSize = 0;
            this.processbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.processbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.processbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.processbtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(176)))), ((int)(((byte)(192)))));
            this.processbtn.Location = new System.Drawing.Point(479, 0);
            this.processbtn.Name = "processbtn";
            this.processbtn.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.processbtn.Size = new System.Drawing.Size(132, 64);
            this.processbtn.TabIndex = 9;
            this.processbtn.Text = "In Process";
            this.processbtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.processbtn.UseVisualStyleBackColor = false;
            this.processbtn.Click += new System.EventHandler(this.processbtn_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.panel1.Location = new System.Drawing.Point(0, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1331, 2);
            this.panel1.TabIndex = 12;
            // 
            // projectitle
            // 
            this.projectitle.AutoSize = true;
            this.projectitle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectitle.ForeColor = System.Drawing.SystemColors.Window;
            this.projectitle.Location = new System.Drawing.Point(35, 29);
            this.projectitle.Name = "projectitle";
            this.projectitle.Size = new System.Drawing.Size(151, 23);
            this.projectitle.TabIndex = 1;
            this.projectitle.Text = "Process Control";
            // 
            // Mainpage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 718);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.Headerpanel);
            this.Name = "Mainpage";
            this.Text = "Mainpage";
            this.Load += new System.EventHandler(this.Mainpage_Load);
            this.Headerpanel.ResumeLayout(false);
            this.Headerpanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel Headerpanel;
        private System.Windows.Forms.Button ncrbtn;
        private System.Windows.Forms.Button Customerbtn;
        private System.Windows.Forms.Button processbtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label projectitle;
        private System.Windows.Forms.Button Shipmentbtn;
        private System.Windows.Forms.Button Rejectedbtn;
    }
}