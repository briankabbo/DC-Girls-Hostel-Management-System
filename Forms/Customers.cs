using System;
using System.Windows.Forms;
using GMS_Kabbo.Data;

namespace GMS_Kabbo.Forms
{
    public partial class Customers : Form
    {
        private int _selectedCustomerId;

        public Customers()
        {
            InitializeComponent();
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            CustomerData.DataSource = CustomerRepository.GetAll();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out var maritalStatus, out var profession))
                return;

            try
            {
                CustomerRepository.Insert(
                    CusNameTb.Text,
                    CusPhoneTb.Text,
                    maritalStatus,
                    CusDOB.Value,
                    CusRoomCb.Text,
                    profession);
                MessageBox.Show("Customer Saved");
                LoadCustomers();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CustomerData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = CustomerData.Rows[e.RowIndex];
            CusNameTb.Text = row.Cells["CusName"].Value.ToString();
            CusPhoneTb.Text = row.Cells["CusPhone"].Value.ToString();
            CusMsCb.Text = row.Cells["CusMs"].Value.ToString();
            CusDOB.Value = Convert.ToDateTime(row.Cells["CusDOB"].Value);
            CusProfCb.Text = row.Cells["CusProf"].Value.ToString();
            CusRoomCb.Text = row.Cells["CusRoom"].Value.ToString();
            _selectedCustomerId = Convert.ToInt32(row.Cells["CusId"].Value);
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (_selectedCustomerId == 0)
            {
                MessageBox.Show("Please select a customer to delete");
                return;
            }

            if (MessageBox.Show("Delete this customer?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                CustomerRepository.Delete(_selectedCustomerId);
                MessageBox.Show("Customer deleted successfully");
                LoadCustomers();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (_selectedCustomerId == 0)
            {
                MessageBox.Show("Please select a customer to edit");
                return;
            }

            if (!ValidateInput(out var maritalStatus, out var profession))
                return;

            try
            {
                CustomerRepository.Update(
                    _selectedCustomerId,
                    CusNameTb.Text,
                    CusPhoneTb.Text,
                    maritalStatus,
                    CusDOB.Value,
                    CusRoomCb.Text,
                    profession);
                MessageBox.Show("Customer updated successfully");
                LoadCustomers();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private bool ValidateInput(out string maritalStatus, out string profession)
        {
            maritalStatus = CusMsCb.SelectedItem?.ToString();
            profession = CusProfCb.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(CusNameTb.Text) || string.IsNullOrWhiteSpace(CusPhoneTb.Text) ||
                maritalStatus == null)
            {
                MessageBox.Show("Missing Information");
                return false;
            }

            return true;
        }

        private void ResetForm()
        {
            CusNameTb.Text = "";
            CusPhoneTb.Text = "";
            CusMsCb.SelectedIndex = -1;
            _selectedCustomerId = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e) => NavigationHelper.Open<Dashboard>(this);
        private void pictureBox3_Click(object sender, EventArgs e) => NavigationHelper.Open<Bookings>(this);
        private void pictureBox4_Click(object sender, EventArgs e) => NavigationHelper.Open<Users>(this);
        private void pictureBox2_Click(object sender, EventArgs e) => NavigationHelper.Open<Customers>(this);
        private void pictureBox5_Click(object sender, EventArgs e) => NavigationHelper.Open<login>(this);
    }
}
