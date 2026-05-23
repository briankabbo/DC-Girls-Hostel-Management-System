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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            ShowUsers();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void ShowUsers()
        {
            using (var connection = DatabaseHelper.CreateConnection())
            {
                connection.Open();
                var sda = new SqlDataAdapter("SELECT * FROM UserTbl", connection);
                var ds = new DataSet();
                sda.Fill(ds);
                UsersData.DataSource = ds.Tables[0];
            }
        }
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || UphoneTb.Text == "" || UpasswordTb.Text == "")
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
                            "INSERT INTO UserTbl (Uname, Uphone, Upass) VALUES (@UN, @UP, @UPA)",
                            connection))
                        {
                            cmd.Parameters.AddWithValue("@UN", UnameTb.Text);
                            cmd.Parameters.AddWithValue("@UP", UphoneTb.Text);
                            cmd.Parameters.AddWithValue("@UPA", UpasswordTb.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("User Saved");
                    ShowUsers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Key = 0;
        private void UsersData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UnameTb.Text = UsersData.SelectedRows[0].Cells[1].Value.ToString();
            UphoneTb.Text = UsersData.SelectedRows[0].Cells[2].Value.ToString();
            UpasswordTb.Text = UsersData.SelectedRows[0].Cells[3].Value.ToString();
            if (UnameTb.Text == "")
            {
                Key = 0;

            }
            else
            {
                Key = Convert.ToInt32(UsersData.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || UphoneTb.Text == "" || UpasswordTb.Text == "")
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
                            "UPDATE UserTbl SET Uname = @UN, Uphone = @UP, Upass = @UPA WHERE UId = @Ukey",
                            connection))
                        {
                            cmd.Parameters.AddWithValue("@UN", UnameTb.Text);
                            cmd.Parameters.AddWithValue("@UP", UphoneTb.Text);
                            cmd.Parameters.AddWithValue("@UPA", UpasswordTb.Text);
                            cmd.Parameters.AddWithValue("@Ukey", Key);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("User Updated");
                    ShowUsers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void Reset()
        {
            UnameTb.Text = "";
            UphoneTb.Text = "";
            UpasswordTb.Text = "";
            Key = 0;
        }
        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select User");
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
                                "DELETE FROM UserTbl WHERE UId = @Ukey", connection))
                            {
                                cmd.Parameters.AddWithValue("@Ukey", Key);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("User Deleted");
                        ShowUsers();
                        Reset();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            login obj = new login();
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Bookings obj = new Bookings();
            obj.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }
    }
}
