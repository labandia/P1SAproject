using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootWristStrapsAnalysis
{
    public partial class WaitFormDialog : Form
    {
        public WaitFormDialog()
        {
            InitializeComponent();

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
        }

        public void SetStatus(string text)
        {
            if (InvokeRequired)
                Invoke(new Action(() => lblStatus.Text = text));
            else
                lblStatus.Text = text;
        }
        public void SetProgress(int value)
        {
            value = Math.Max(0, Math.Min(100, value));

            if (InvokeRequired)
                Invoke(new Action(() => progressBar1.Value = value));
            else
                progressBar1.Value = value;
        }
       
    }
}
