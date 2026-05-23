using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMS_Kabbo.Data;

namespace GMS_Kabbo
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel41_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Users obj = new Users();
            obj.Show();
            this.Hide(); 
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Enter Username and Password");
                return;
            }

            try
            {
                using (var connection = DatabaseHelper.CreateConnection())
                {
                    connection.Open();
                    using (var cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM UserTbl WHERE Uname = @Uname AND Upass = @Upass",
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@Uname", UNameTb.Text);
                        cmd.Parameters.AddWithValue("@Upass", PasswordTb.Text);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count == 1)
                        {
                            var dashboard = new Dashboard();
                            dashboard.Show();
                            Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Information");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login failed: " + ex.Message);
            }
        }
    }
}
