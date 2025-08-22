namespace Parts_locator.View.Rotor
{
    partial class RotorPartsLocator
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
            this.PartnumDisplay = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PartnumDisplay
            // 
            this.PartnumDisplay.AutoSize = true;
            this.PartnumDisplay.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartnumDisplay.Location = new System.Drawing.Point(43, 69);
            this.PartnumDisplay.Name = "PartnumDisplay";
            this.PartnumDisplay.Size = new System.Drawing.Size(84, 28);
            this.PartnumDisplay.TabIndex = 5;
            this.PartnumDisplay.Text = "label3";
            // 
            // RotorPartsLocator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PartnumDisplay);
            this.Name = "RotorPartsLocator";
            this.Text = "RotorPartsLocator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label PartnumDisplay;
    }
}