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
        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\NOC-02\Documents\HotelManagementDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowUsers()
        {
            Connection.Open();

            string Query = "Select * from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UsersData.DataSource = ds.Tables[0];
            Connection.Close();

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
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("insert into UserTbl(UName,Uphone,Upass)values(@UN,@UP,@UPA)", Connection);
                    cmd.Parameters.AddWithValue("@UN", UnameTb.Text);
                    cmd.Parameters.AddWithValue("@UP", UphoneTb.Text);
                    cmd.Parameters.AddWithValue("@UPA", UpasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Saved");
                    Connection.Close();
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
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("Update UserTbl Set Uname=@UN,Uphone=@UP,Upass=@UPA where UId=@Ukey", Connection);
                    cmd.Parameters.AddWithValue("@UN", UnameTb.Text);
                    cmd.Parameters.AddWithValue("@UP", UphoneTb.Text);
                    cmd.Parameters.AddWithValue("@UPA", UpasswordTb.Text);
                    cmd.Parameters.AddWithValue("@Ukey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated");
                    Connection.Close();
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
                        Connection.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM UserTbl WHERE UId=@Ukey", Connection);
                        cmd.Parameters.AddWithValue("@Ukey", Key);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User Deleted");
                        Connection.Close();
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
