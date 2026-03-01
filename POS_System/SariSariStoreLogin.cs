using POS_System.Services;
using System;
using System.Windows.Forms;

namespace POS_System
{
    public partial class SariSariStoreLogin : Form
    {
        private readonly UserServices _userServices = new UserServices();   

        public SariSariStoreLogin()
        {
            InitializeComponent();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                

                // ⏎ ENTER → Payment (Safe Logic)
                case Keys.Enter:
                    if (!string.IsNullOrWhiteSpace(userText.Text) || !string.IsNullOrWhiteSpace(passwordText.Text))
                    {
                        LoginBtn.PerformClick();
                    }
                    return true;

              
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(userText.Text) && string.IsNullOrWhiteSpace(passwordText.Text))
            {
                MessageBox.Show("Please enter both username and password.", "" +
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(userText.Text))
            {
                MessageBox.Show("Please enter username.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(passwordText.Text))
            {
                MessageBox.Show("Please enter password.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            var loginResult = _userServices.ValidateLogin(userText.Text, passwordText.Text);

            if (loginResult != null)
            {
                MessageBox.Show("Login Success.");
                Form1 frm = new Form1(loginResult);
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void userText_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                passwordText.Focus();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }
    }
}
