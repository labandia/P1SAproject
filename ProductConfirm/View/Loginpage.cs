
using Microsoft.Extensions.DependencyInjection;
using ProductConfirm.Models;
using ProductConfirm.View.Modals;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ProductConfirm
{
    public partial class Loginpage : Form
    {
        private readonly IUsers _user;
        private readonly IServiceProvider _serviceProvider;

        public Loginpage(IServiceProvider serviceProvider, IUsers user)
        {
            InitializeComponent();
            _user = user;
            _serviceProvider = serviceProvider;
        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            loginfunction();
        }


        public bool validateform()
        {
            bool check = true;
            string user = String.IsNullOrEmpty(username.Text) ? "" : username.Text;
            string pass = String.IsNullOrEmpty(password.Text) ? "" : password.Text;

            if (user == "")
            {
                user_error.Visible = true;
                check = false;
            }
            else if (pass == "")
            {
                pass_error.Visible = true;
                check = false;
            }
            else
            {
                pass_error.Visible = false;
                user_error.Visible = false;
            }

            return check;
        }

        

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginfunction();
            }
        }



        public async void loginfunction()
        {

            if (validateform())
            {
                // Get the data first 
                var users  = await _user.Getusernameinfo(username.Text.Trim());
                // Get only one Rows data
                var data = users.FirstOrDefault();

                if (users.Count() > 0)
                {

                    if (data.password == password.Text)
                    {
                        MessageBox.Show("Login success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var mainpage = _serviceProvider.GetRequiredService<Mainpage>();

                        // Assign properties
                        mainpage.userid = data.Account_ID;
                        mainpage.Fullname = data.Fullname;
                        mainpage.roleId = data.role_type;

                        mainpage.Show();
                        this.Hide();
     
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Username and password is Incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void Loginpage_Load(object sender, EventArgs e)
        {
            username.Focus();
        }

        private void password_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the key pressed is the Enter key (which may include carriage return or newline)
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)13 || e.KeyChar == (char)10)
            {
                e.Handled = true;

                string scannedCode = password.Text.Trim();
                password.Text = scannedCode;
 
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Registermodal r = new Registermodal();  
            r.Show();
        }
    }
}
