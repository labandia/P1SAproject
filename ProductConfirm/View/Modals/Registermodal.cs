using ProductConfirm.DataAccess;
using ProductConfirm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductConfirm.View.Modals
{
    public partial class Registermodal : Form
    {
        private readonly UserRespository _user;

        public Registermodal()
        {
            InitializeComponent();
            _user  = new UserRespository(); 
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {


            if (await userExists(UserText.Text))
            {
                MessageBox.Show("Account is already  Exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var newUser = new RegisterModel
                {
                    username = UserText.Text,
                    password = PassText.Text,
                    Fname = FnameText.Text,
                    lname = LastnameText.Text,
                    role_type = 1,
                    Project_ID = 3
                };

                bool result = await _user.RegisterUser(newUser);

                if (result)
                {
                    MessageBox.Show("Account is successfully created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Visible = false;
                }
                else
                {
                    MessageBox.Show("Invalid Insert Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public async Task<bool> userExists(string username)
        {
            bool result = await _user.CheckusersExist(username);
            return result;
        }




        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            Visible = false;
        }
    }
}
