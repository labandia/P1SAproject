using POS_System.Pages;
using POS_System.Services;
using POS_System.Utilities;
using System;
using System.Windows.Forms;

namespace POS_System.Modals
{
    public partial class AddUsers : Form
    {
        private readonly UserServices _userservice = new UserServices();

        private readonly UserManagement _user;

        public AddUsers(UserManagement user)
        {
            InitializeComponent();
            _user = user;
        }

        private void AddUsers_Load(object sender, EventArgs e)
        {
            Categorycbn.SelectedIndex = 0;  
        }

        private async void Savebtn_Click(object sender, EventArgs e)
        {
            if (!ValidationForm())
            {
                return;
            }

            try
            {
                await _userservice.CreateUserAsync(new UsersModel
                {
                    FullName = fullnameText.Text,
                    Username = userText.Text,
                    PasswordHash = "user123",
                    Role = Categorycbn.Text
                });

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add User Error");
            }
        }


        public bool ValidationForm()
        {
            if (string.IsNullOrEmpty(userText.Text) || string.IsNullOrEmpty(fullnameText.Text) || Categorycbn.SelectedIndex == 0)
            {
                MessageBox.Show("Enter All the Required Fields.");
                return false;
            }

            if (string.IsNullOrEmpty(fullnameText.Text))
            {
                prod_error.Visible = true;
                return false;
            }
            else
            {
                prod_error.Visible = false;
            }

            if (string.IsNullOrEmpty(userText.Text))
            {
                price_error.Visible = true;
                return false;
            }
            else
            {
                prod_error.Visible = false;
            }


            if (Categorycbn.SelectedIndex == 0)
            {
                label3.Visible = true;
                return false;
            }
            else
            {
                label3.Visible = false;
            }

            prod_error.Visible = true;
            price_error.Visible = true;
            label3.Visible = true;

            return true;
        }

        private void Cancebtn_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
