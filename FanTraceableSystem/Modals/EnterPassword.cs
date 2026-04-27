using System;
using System.Windows.Forms;

namespace FanTraceableSystem
{
    public partial class EnterPassword : Form
    {

        public string EnteredPassword { get; private set; }
        private bool _isProcessing = false;
        private bool _isSubmitting = false;

        public EnterPassword()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || _isProcessing) return;

            txtPassword.Enabled = false; // 🚫 stop input immediately
            e.SuppressKeyPress = true;

            EnteredPassword = txtPassword.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void submitbtn_Click(object sender, EventArgs e)
        {
            if (_isSubmitting) return;

            if (string.IsNullOrWhiteSpace(txtPassword.Text)) return;

            _isSubmitting = true;                 // 🚫 prevent double submit
            submitbtn.Enabled = false;            // optional: block UI interaction

            EnteredPassword = txtPassword.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void EnterPassword_Load(object sender, EventArgs e)
        {

        }
    }
}
