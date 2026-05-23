using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using GMS_Kabbo.Data;

namespace GMS_Kabbo
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            ShowCustomer();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void Customers_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void ShowCustomer()
        {
            using (var connection = DatabaseHelper.CreateConnection())
            {
                connection.Open();
                var sda = new SqlDataAdapter("SELECT * FROM CustomerTbl", connection);
                var ds = new DataSet();
                sda.Fill(ds);
                CustomerData.DataSource = ds.Tables[0];
            }
        }
        private void Reset()
        {
            CusNameTb.Text = "";
            CusPhoneTb.Text = "";
            CusMsCb.SelectedIndex = -1;
            Key = 0;
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CusNameTb.Text == "" || CusPhoneTb.Text == "" || CusMsCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    using (var connection = DatabaseHelper.CreateConnection())
                    {
                        connection.Open();
                        using (var cmd = new SqlCommand(
                            "INSERT INTO CustomerTbl (CusName, CusPhone, CusMs, CusDOB, CusRoom, CusProf) VALUES (@CN, @CP, @CMS, @CD, @Room, @Prof)",
                            connection))
                        {
                            cmd.Parameters.AddWithValue("@CN", CusNameTb.Text);
                            cmd.Parameters.AddWithValue("@CP", CusPhoneTb.Text);
                            cmd.Parameters.AddWithValue("@CMS", CusMsCb.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@CD", CusDOB.Value.Date);
                            cmd.Parameters.AddWithValue("@Room", CusRoomCb.Text);
                            cmd.Parameters.AddWithValue("@Prof", CusProfCb.SelectedItem.ToString());
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("User Saved");
                    ShowCustomer();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        int Key = 0;
        private void CustomerData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = CustomerData.Rows[e.RowIndex];
            CusNameTb.Text = selectedRow.Cells["CusName"].Value.ToString();
            CusPhoneTb.Text = selectedRow.Cells["CusPhone"].Value.ToString();
            CusMsCb.Text = selectedRow.Cells["CusMs"].Value.ToString();
            CusDOB.Value = Convert.ToDateTime(selectedRow.Cells["CusDOB"].Value.ToString());
            CusProfCb.Text = selectedRow.Cells["CusProf"].Value.ToString();
            CusRoomCb.Text = selectedRow.Cells["CusRoom"].Value.ToString();
            Key = Convert.ToInt32(selectedRow.Cells["CusId"].Value.ToString());
        }

        private void CusRoomCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
                if (Key == 0)
                {
                    MessageBox.Show("Please select a customer to delete");
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this customer?", "Delete Customer", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            using (var connection = DatabaseHelper.CreateConnection())
                            {
                                connection.Open();
                                using (var cmd = new SqlCommand(
                                    "DELETE FROM CustomerTbl WHERE CusId = @CusId", connection))
                                {
                                    cmd.Parameters.AddWithValue("@CusId", Key);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            MessageBox.Show("Customer deleted successfully");
                            ShowCustomer();
                            Reset();
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show("Error: " + Ex.Message);
                        }
                    }
                }
            }

        private void EditBtn_Click(object sender, EventArgs e)
            {
                if (Key == 0)
                {
                    MessageBox.Show("Please select a customer to edit");
                }
                else
                {
                    if (CusNameTb.Text == "" || CusPhoneTb.Text == "" || CusMsCb.SelectedIndex == -1)
                    {
                        MessageBox.Show("Missing Information");
                    }
                    else
                    {
                        try
                        {
                            using (var connection = DatabaseHelper.CreateConnection())
                            {
                                connection.Open();
                                using (var cmd = new SqlCommand(
                                    "UPDATE CustomerTbl SET CusName = @CN, CusPhone = @CP, CusMs = @CMS, CusDOB = @CD, CusRoom = @Room, CusProf = @Prof WHERE CusId = @CusId",
                                    connection))
                                {
                                    cmd.Parameters.AddWithValue("@CN", CusNameTb.Text);
                                    cmd.Parameters.AddWithValue("@CP", CusPhoneTb.Text);
                                    cmd.Parameters.AddWithValue("@CMS", CusMsCb.SelectedItem.ToString());
                                    cmd.Parameters.AddWithValue("@CD", CusDOB.Value.Date);
                                    cmd.Parameters.AddWithValue("@Room", CusRoomCb.Text);
                                    cmd.Parameters.AddWithValue("@Prof", CusProfCb.SelectedItem.ToString());
                                    cmd.Parameters.AddWithValue("@CusId", Key);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            MessageBox.Show("Customer updated successfully");
                            ShowCustomer();
                            Reset();
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show("Error: " + Ex.Message);
                        }
                    }
                }
            }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Bookings obj = new Bookings();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Users obj = new Users();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            login obj = new login();
            obj.Show();
            this.Hide();
        }
    }
    }