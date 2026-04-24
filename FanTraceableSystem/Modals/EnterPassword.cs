using System;
using System.Windows.Forms;

namespace FanTraceableSystem
{
    public partial class EnterPassword : Form
    {

        public string EnteredPassword { get; private set; }

        public EnterPassword()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            EnteredPassword = txtPassword.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void submitbtn_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtPassword.Text)) return;

            EnteredPassword = txtPassword.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void EnterPassword_Load(object sender, EventArgs e)
        {

        }
    }
}
