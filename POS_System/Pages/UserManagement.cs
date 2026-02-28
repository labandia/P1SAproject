using POS_System.Modals;
using POS_System.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System.Pages
{
    public partial class UserManagement : Form
    {
        private readonly UserServices userService = new UserServices();
        private BindingList<UsersModel> usersData;

        public UserManagement()
        {
            InitializeComponent();

            SetupGrid();

            usersTable.CurrentCellDirtyStateChanged += usersTable_CurrentCellDirtyStateChanged;
            usersTable.CellValueChanged += usersTable_CellValueChanged;
            usersTable.EditingControlShowing += usersTable_EditingControlShowing; 
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
                ReadOnly = true, 
                Visible = false
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
                Name = "Role",               
                DataPropertyName = "Role",
                HeaderText = "Role"
            };

            roleColumn.Items.AddRange("Admin", "Users");
            usersTable.Columns.Add(roleColumn);

            // Active (Checkbox)
            usersTable.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsActive",
                HeaderText = "Active",
                TrueValue = "True",
                FalseValue = "False",
                DataPropertyName = "IsActive"
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
            var list = await userService.GetAllUsers();
            usersData = new BindingList<UsersModel>(list);
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

        private async void usersTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var user = (UsersModel)usersTable.Rows[e.RowIndex].DataBoundItem;
            if (usersTable.Columns[e.ColumnIndex].Name == "IsActive")
            {
                
                bool newValue = (bool)usersTable.Rows[e.RowIndex].Cells["IsActive"].Value;

                // 🚫 Prevent Admin from being disabled
                if (user.Role == "Admin" && newValue == false)
                {
                    MessageBox.Show("Admin account cannot be deactivated.",
                                    "Action Denied",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    // Revert checkbox
                    usersTable.Rows[e.RowIndex].Cells["IsActive"].Value = true;
                    return;
                }

                // ⚠️ Ask confirmation before disabling
                if (newValue == false)
                {
                    var confirm = MessageBox.Show(
                        $"Are you sure you want to deactivate {user.FullName}?",
                        "Confirm Deactivation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirm == DialogResult.No)
                    {
                        // Revert checkbox
                        usersTable.Rows[e.RowIndex].Cells["IsActive"].Value = true;
                        return;
                    }
                }



                // 💾 Auto update database
                await userService.UpdateUserStatusAsync(user.UserID, newValue);

                MessageBox.Show("User status updated successfully.",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }


            // ===== ROLE COMBOBOX =====
            if (usersTable.Columns[e.ColumnIndex].DataPropertyName == "Role")
            {
                string newRole = usersTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

                await userService.UpdateUserAsync(user.UserID, user.FullName, newRole, user.IsActive);

                MessageBox.Show("User status updated successfully.",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void usersTable_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (usersTable.IsCurrentCellDirty)
            {
                usersTable.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void usersTable_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (usersTable.CurrentCell.ColumnIndex == usersTable.Columns["Role"].Index)
            {
                if (e.Control is ComboBox combo)
                {
                    combo.SelectionChangeCommitted -= ComboBox_SelectionChangeCommitted;
                    combo.SelectionChangeCommitted += ComboBox_SelectionChangeCommitted;
                }
            }
        }

        private void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            usersTable.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
