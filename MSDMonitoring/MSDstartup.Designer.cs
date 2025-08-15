namespace MSDMonitoring
{
    partial class MSDstartup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MSDstartup));
            this.Exitbtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FloorLifeText = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Ambassador = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.QtyIn = new System.Windows.Forms.TextBox();
            this.InputIn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LotText = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.NewEntryBtn = new System.Windows.Forms.Button();
            this.LineText = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.ReelID_error = new System.Windows.Forms.Label();
            this.QuanError = new System.Windows.Forms.Label();
            this.NameError = new System.Windows.Forms.Label();
            this.LineError = new System.Windows.Forms.Label();
            this.Historybtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Exitbtn
            // 
            this.Exitbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Exitbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(23)))), ((int)(((byte)(30)))));
            this.Exitbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exitbtn.FlatAppearance.BorderSize = 0;
            this.Exitbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exitbtn.Image = ((System.Drawing.Image)(resources.GetObject("Exitbtn.Image")));
            this.Exitbtn.Location = new System.Drawing.Point(1259, 8);
            this.Exitbtn.Name = "Exitbtn";
            this.Exitbtn.Size = new System.Drawing.Size(55, 46);
            this.Exitbtn.TabIndex = 49;
            this.Exitbtn.UseVisualStyleBackColor = false;
            this.Exitbtn.Click += new System.EventHandler(this.Exitbtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.FloorLifeText);
            this.panel1.Location = new System.Drawing.Point(305, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(119, 27);
            this.panel1.TabIndex = 55;
            // 
            // FloorLifeText
            // 
            this.FloorLifeText.AutoSize = true;
            this.FloorLifeText.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FloorLifeText.Location = new System.Drawing.Point(5, 3);
            this.FloorLifeText.Name = "FloorLifeText";
            this.FloorLifeText.Size = new System.Drawing.Size(42, 21);
            this.FloorLifeText.TabIndex = 1;
            this.FloorLifeText.Text = "N/A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(23)))), ((int)(((byte)(30)))));
            this.label6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(25, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(226, 19);
            this.label6.TabIndex = 44;
            this.label6.Text = " MOISTURE SENSITIVE DEVICES";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(22, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 16);
            this.label5.TabIndex = 42;
            this.label5.Text = "Enter Reel ID :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(449, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 16);
            this.label2.TabIndex = 38;
            this.label2.Text = "Reel Quantity :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(301, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 37;
            this.label1.Text = "Floor Life :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(23)))), ((int)(((byte)(30)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1344, 59);
            this.pictureBox1.TabIndex = 43;
            this.pictureBox1.TabStop = false;
            // 
            // Ambassador
            // 
            this.Ambassador.BackColor = System.Drawing.Color.Gainsboro;
            this.Ambassador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Ambassador.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ambassador.Location = new System.Drawing.Point(25, 48);
            this.Ambassador.Name = "Ambassador";
            this.Ambassador.Size = new System.Drawing.Size(260, 27);
            this.Ambassador.TabIndex = 33;
            this.Ambassador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ambassador_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(592, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 16);
            this.label12.TabIndex = 60;
            this.label12.Text = "Name : ";
            // 
            // QtyIn
            // 
            this.QtyIn.BackColor = System.Drawing.Color.Gainsboro;
            this.QtyIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.QtyIn.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QtyIn.Location = new System.Drawing.Point(452, 48);
            this.QtyIn.Name = "QtyIn";
            this.QtyIn.Size = new System.Drawing.Size(119, 27);
            this.QtyIn.TabIndex = 64;
            this.QtyIn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.QtyIn_KeyPress);
            // 
            // InputIn
            // 
            this.InputIn.BackColor = System.Drawing.Color.Gainsboro;
            this.InputIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InputIn.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputIn.Location = new System.Drawing.Point(598, 48);
            this.InputIn.Name = "InputIn";
            this.InputIn.Size = new System.Drawing.Size(119, 27);
            this.InputIn.TabIndex = 65;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(875, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 16);
            this.label3.TabIndex = 77;
            this.label3.Text = "Lot No :";
            // 
            // LotText
            // 
            this.LotText.BackColor = System.Drawing.Color.Gainsboro;
            this.LotText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LotText.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LotText.Location = new System.Drawing.Point(878, 48);
            this.LotText.Name = "LotText";
            this.LotText.Size = new System.Drawing.Size(178, 27);
            this.LotText.TabIndex = 67;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(26, 189);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1288, 535);
            this.flowLayoutPanel1.TabIndex = 80;
            // 
            // NewEntryBtn
            // 
            this.NewEntryBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewEntryBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(152)))), ((int)(((byte)(126)))));
            this.NewEntryBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NewEntryBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(152)))), ((int)(((byte)(126)))));
            this.NewEntryBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(56)))), ((int)(((byte)(44)))));
            this.NewEntryBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(56)))), ((int)(((byte)(44)))));
            this.NewEntryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewEntryBtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewEntryBtn.ForeColor = System.Drawing.Color.White;
            this.NewEntryBtn.Image = ((System.Drawing.Image)(resources.GetObject("NewEntryBtn.Image")));
            this.NewEntryBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NewEntryBtn.Location = new System.Drawing.Point(1080, 37);
            this.NewEntryBtn.Name = "NewEntryBtn";
            this.NewEntryBtn.Padding = new System.Windows.Forms.Padding(50, 0, 50, 0);
            this.NewEntryBtn.Size = new System.Drawing.Size(186, 39);
            this.NewEntryBtn.TabIndex = 68;
            this.NewEntryBtn.Text = "Add";
            this.NewEntryBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewEntryBtn.UseVisualStyleBackColor = false;
            this.NewEntryBtn.Click += new System.EventHandler(this.NewEntryBtn_Click);
            // 
            // LineText
            // 
            this.LineText.BackColor = System.Drawing.Color.Gainsboro;
            this.LineText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LineText.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LineText.Location = new System.Drawing.Point(737, 48);
            this.LineText.Name = "LineText";
            this.LineText.Size = new System.Drawing.Size(119, 27);
            this.LineText.TabIndex = 66;
            this.LineText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LineText_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(734, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 16);
            this.label14.TabIndex = 82;
            this.label14.Text = "Line :";
            // 
            // ReelID_error
            // 
            this.ReelID_error.AutoSize = true;
            this.ReelID_error.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReelID_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ReelID_error.Location = new System.Drawing.Point(120, 27);
            this.ReelID_error.Name = "ReelID_error";
            this.ReelID_error.Size = new System.Drawing.Size(13, 16);
            this.ReelID_error.TabIndex = 84;
            this.ReelID_error.Text = "*";
            this.ReelID_error.Visible = false;
            // 
            // QuanError
            // 
            this.QuanError.AutoSize = true;
            this.QuanError.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuanError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.QuanError.Location = new System.Drawing.Point(559, 27);
            this.QuanError.Name = "QuanError";
            this.QuanError.Size = new System.Drawing.Size(13, 16);
            this.QuanError.TabIndex = 85;
            this.QuanError.Text = "*";
            this.QuanError.Visible = false;
            // 
            // NameError
            // 
            this.NameError.AutoSize = true;
            this.NameError.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.NameError.Location = new System.Drawing.Point(649, 27);
            this.NameError.Name = "NameError";
            this.NameError.Size = new System.Drawing.Size(13, 16);
            this.NameError.TabIndex = 86;
            this.NameError.Text = "*";
            this.NameError.Visible = false;
            // 
            // LineError
            // 
            this.LineError.AutoSize = true;
            this.LineError.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LineError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LineError.Location = new System.Drawing.Point(784, 27);
            this.LineError.Name = "LineError";
            this.LineError.Size = new System.Drawing.Size(13, 16);
            this.LineError.TabIndex = 87;
            this.LineError.Text = "*";
            this.LineError.Visible = false;
            // 
            // Historybtn
            // 
            this.Historybtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Historybtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(23)))), ((int)(((byte)(30)))));
            this.Historybtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Historybtn.FlatAppearance.BorderSize = 0;
            this.Historybtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Historybtn.Image = ((System.Drawing.Image)(resources.GetObject("Historybtn.Image")));
            this.Historybtn.Location = new System.Drawing.Point(1197, 8);
            this.Historybtn.Name = "Historybtn";
            this.Historybtn.Size = new System.Drawing.Size(55, 46);
            this.Historybtn.TabIndex = 88;
            this.Historybtn.UseVisualStyleBackColor = false;
            this.Historybtn.Click += new System.EventHandler(this.Historybtn_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.NewEntryBtn);
            this.panel2.Controls.Add(this.Ambassador);
            this.panel2.Controls.Add(this.LineError);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.NameError);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.QuanError);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.ReelID_error);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.LineText);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.QtyIn);
            this.panel2.Controls.Add(this.InputIn);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.LotText);
            this.panel2.Location = new System.Drawing.Point(26, 77);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1288, 100);
            this.panel2.TabIndex = 89;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(23)))), ((int)(((byte)(30)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(1136, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 46);
            this.button1.TabIndex = 90;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MSDstartup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 750);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Historybtn);
            this.Controls.Add(this.Exitbtn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "MSDstartup";
            this.Text = "MSDstartup";
            this.Load += new System.EventHandler(this.MSDstartup_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Exitbtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label FloorLifeText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox Ambassador;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox QtyIn;
        private System.Windows.Forms.TextBox InputIn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox LotText;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button NewEntryBtn;
        private System.Windows.Forms.TextBox LineText;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label ReelID_error;
        private System.Windows.Forms.Label QuanError;
        private System.Windows.Forms.Label NameError;
        private System.Windows.Forms.Label LineError;
        private System.Windows.Forms.Button Historybtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
    }
}