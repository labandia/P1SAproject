using Parts_locator.View.Moldingbush;
using System;
using System.Data;
using System.Windows.Forms;

namespace Parts_locator
{
    public partial class Startup : Form
    {
        public Startup()
        {
            InitializeComponent();
        }
        private void Exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            loginfunction();
        }

        public bool validateform()
        {
            string user = username.Text;
            string pass = password.Text;

            //CHECKS THE INPUT IF IS EMPTY
            if (String.IsNullOrEmpty(user))
            {
                user_error.Visible = true;
                return false;
            }
            else
            {
                user_error.Visible = false;
            }
            if (String.IsNullOrEmpty(pass))
            {
                pass_error.Visible = true;
                return false;
            }
            else
            {
                pass_error.Visible = false;
            }

            pass_error.Visible = false;
            user_error.Visible = false;
            return true;
        }



        public void loginfunction()
        {      
            GlobalDb db = new GlobalDb();

            if (validateform())
            {
                string strsql = "SELECT Account_ID, username, password, role_type, Date_created, " +
                                "Project, Fname, Lname, Project_ID " +
                                "FROM Project_account WHERE username = '"+ username.Text + "'";
                DataTable emp = db.GetData(strsql);

                // CHECKS THE USERNAME AND PASSWORD
                if (emp.Rows.Count > 0)
                {
                    DataRow row = emp.Rows[0];

                    if (row["password"].ToString() == password.Text)
                    {
                        int proj = Convert.ToInt32(row["Project_ID"].ToString());

                        MessageBox.Show("Login success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectionMainpage(proj);
                        Visible = false;
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



        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginfunction();
            }
        }

        private void password_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the key pressed is the Enter key (which may include carriage return or newline)
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)13 || e.KeyChar == (char)10)
            {
                // Suppress the newline by setting Handled to true
                e.Handled = true;
                // Optionally, you can process the scanned value here
                string scannedCode = password.Text.Trim();
                password.Text = scannedCode;

            }
        }




        public void SelectionMainpage(int proj)
        {
            BushMain b = new BushMain();
            b.Show();

            //switch (proj)
            //{
            //    case 1:
            //Mainlayout m = new Mainlayout();
            //    case 4:
            //        BushMain b = new BushMain();
            //        b.Show();
            //        break;
            //}
        }

    }
}
