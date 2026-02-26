using POS_System.Modals;
using POS_System.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System.Pages
{
    public partial class UserManagement : Form
    {
        private readonly UserServices userService = new UserServices();
        private List<UsersModel> usersData = new List<UsersModel>();

        public UserManagement()
        {
            InitializeComponent();

            SetupGrid();
        }
        // ================= GRID SETUP =================
        private void SetupGrid()
        {
            usersTable.AutoGenerateColumns = false;
            usersTable.Columns.Clear();

            // ID
            usersTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UserID",
                HeaderText = "ID",
                ReadOnly = true
            });

            // Full Name
            usersTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Full Name"
            });

            // Username
            usersTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Username",
                HeaderText = "Username",
                ReadOnly = true
            });

            // Role (ComboBox)
            var roleColumn = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "Role",
                HeaderText = "Role"
            };

            roleColumn.Items.AddRange("Admin", "Users");
            usersTable.Columns.Add(roleColumn);

            // Active (Checkbox)
            usersTable.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsActive",
                HeaderText = "Active"
            });

            // Created
            usersTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CreatedAt",
                HeaderText = "Created",
                ReadOnly = true,
                DefaultCellStyle = { Format = "yyyy-MM-dd" }
            });

            // Prevent ComboBox crash
            usersTable.DataError += (s, e) => { e.ThrowException = false; };
        }

        // ================= LOAD USERS =================
        private async Task LoadUsers()
        {
            usersData = await userService.GetAllUsers();
            usersTable.DataSource = usersData;
        }

        private async void UserManagement_Load(object sender, EventArgs e)
        {
           await LoadUsers();
        }

        private async void FinalPaymentbtn_Click(object sender, EventArgs e)
        {
            using (var addProduct = new AddUsers(this))
            {
                if (addProduct.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Add Users successful.");
                    await LoadUsers();
                }
            }
        }
    }
}
