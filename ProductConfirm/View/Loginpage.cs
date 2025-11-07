
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductConfirm.Models;
using ProductConfirm.View.Modals;
using ProgramPartListWeb.Helper;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
            string user = username.Text?.Trim();
            string pass = password.Text?.Trim();

            bool IsuserEmpty = string.IsNullOrEmpty(user);
            bool IspassEmpty = string.IsNullOrEmpty(pass);

            user_error.Visible = IsuserEmpty;
            pass_error.Visible = IspassEmpty;

            return !(IsuserEmpty || IspassEmpty);
        }

        

        private async void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) loginfunction();      
        }



        public async Task loginfunction()
        {
            var logs = new AuthModel
            {
                Username = username.Text.Trim(),
                Password = password.Text
            };

            var validator = new AuthValidation();
            var result = validator.Validate(logs);


            if (!result.IsValid)
            {
                foreach(var failure in result.Errors)
                {
                    if (failure.PropertyName == nameof(logs.Username))
                    {
                        user_error.Text = failure.ErrorMessage;
                        user_error.Visible = true;
                    }
                    if (failure.PropertyName == nameof(logs.Password))
                    {
                        pass_error.Text = failure.ErrorMessage;
                        pass_error.Visible = true;
                    }
                }

                return; // Stop the logic code
            }




            if (!validateform()) return;

            var user = (await _user.LoginCredentials(username.Text.Trim())).FirstOrDefault();
            if (user == null)
            {
                MessageBox.Show("Invalid credentials / Username Doesnt Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // CHECKS THE PASSWORD IF IS CORRECT
            if (!PasswordHasher.VerifyPassword(user.Password, password.Text.Trim()))
            {
                MessageBox.Show("Invalid credentials / Password is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            MessageBox.Show("Login success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var mainpage = _serviceProvider.GetRequiredService<Mainpage>();

            mainpage.userid = user.User_ID;
            mainpage.Fullname = user.Fullname;
            mainpage.roleId = user.Role_ID;

            mainpage.Show();
            this.Hide();
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

        private void username_Leave(object sender, EventArgs e)
        {
            var validator = new AuthValidation();
            var result = validator.Validate(new AuthModel { Username = username.Text });

            var error = result.Errors.FirstOrDefault(x => x.PropertyName == nameof(AuthModel.Username));
            user_error.Text = error?.ErrorMessage ?? "";
        }
    }
}
