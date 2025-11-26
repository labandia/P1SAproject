namespace FootWristStrapsAnalysis
{
    partial class Startup
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
            this.AnalysisTable = new System.Windows.Forms.DataGridView();
            this.prevbtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AnalysisTable)).BeginInit();
            this.SuspendLayout();
            // 
            // AnalysisTable
            // 
            this.AnalysisTable.AllowUserToAddRows = false;
            this.AnalysisTable.AllowUserToDeleteRows = false;
            this.AnalysisTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AnalysisTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AnalysisTable.Location = new System.Drawing.Point(21, 141);
            this.AnalysisTable.Name = "AnalysisTable";
            this.AnalysisTable.ReadOnly = true;
            this.AnalysisTable.Size = new System.Drawing.Size(1073, 554);
            this.AnalysisTable.TabIndex = 0;
            // 
            // prevbtn
            // 
            this.prevbtn.Location = new System.Drawing.Point(928, 101);
            this.prevbtn.Name = "prevbtn";
            this.prevbtn.Size = new System.Drawing.Size(75, 23);
            this.prevbtn.TabIndex = 1;
            this.prevbtn.Text = "button1";
            this.prevbtn.UseVisualStyleBackColor = true;
            this.prevbtn.Click += new System.EventHandler(this.prevbtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(21, 101);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(179, 20);
            this.textBox1.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(236, 98);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(559, 67);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 40);
            this.button1.TabIndex = 4;
            this.button1.Text = "Add Data";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Startup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 749);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.prevbtn);
            this.Controls.Add(this.AnalysisTable);
            this.Name = "Startup";
            this.Text = "Foot Wrist Analysis";
            this.Load += new System.EventHandler(this.Startup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AnalysisTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView AnalysisTable;
        private System.Windows.Forms.Button prevbtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
    }
}

