using System;
using System.Windows.Forms;
using GMS_Kabbo.Data;

namespace GMS_Kabbo.Forms
{
    public partial class Users : Form
    {
        private int _selectedUserId;

        public Users()
        {
            InitializeComponent();
            UpasswordTb.PasswordChar = '*';
            LoadUsers();
        }

        private void LoadUsers()
        {
            UsersData.DataSource = UserRepository.GetList();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UnameTb.Text) || string.IsNullOrWhiteSpace(UphoneTb.Text) ||
                string.IsNullOrWhiteSpace(UpasswordTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                UserRepository.Insert(
                    UnameTb.Text.Trim(),
                    UphoneTb.Text.Trim(),
                    PasswordHasher.Hash(UpasswordTb.Text));
                MessageBox.Show("User Saved");
                LoadUsers();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UsersData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = UsersData.SelectedRows[0];
            UnameTb.Text = row.Cells[1].Value.ToString();
            UphoneTb.Text = row.Cells[2].Value.ToString();
            UpasswordTb.Text = "";
            _selectedUserId = string.IsNullOrWhiteSpace(UnameTb.Text)
                ? 0
                : Convert.ToInt32(row.Cells[0].Value);
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == 0)
            {
                MessageBox.Show("Select a user to edit");
                return;
            }

            if (string.IsNullOrWhiteSpace(UnameTb.Text) || string.IsNullOrWhiteSpace(UphoneTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                var newHash = string.IsNullOrEmpty(UpasswordTb.Text)
                    ? null
                    : PasswordHasher.Hash(UpasswordTb.Text);
                UserRepository.Update(_selectedUserId, UnameTb.Text.Trim(), UphoneTb.Text.Trim(), newHash);
                MessageBox.Show("User Updated");
                LoadUsers();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == 0)
            {
                MessageBox.Show("Select User");
                return;
            }

            if (MessageBox.Show("Delete this user?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                UserRepository.Delete(_selectedUserId);
                MessageBox.Show("User Deleted");
                LoadUsers();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ResetForm()
        {
            UnameTb.Text = "";
            UphoneTb.Text = "";
            UpasswordTb.Text = "";
            _selectedUserId = 0;
        }

        private void pictureBox5_Click(object sender, EventArgs e) => NavigationHelper.Open<login>(this);
        private void pictureBox4_Click(object sender, EventArgs e) => NavigationHelper.Open<Users>(this);
        private void pictureBox2_Click(object sender, EventArgs e) => NavigationHelper.Open<Customers>(this);
        private void pictureBox3_Click(object sender, EventArgs e) => NavigationHelper.Open<Bookings>(this);
        private void pictureBox1_Click(object sender, EventArgs e) => NavigationHelper.Open<Dashboard>(this);
    }
}
