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
        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\NOC-02\Documents\HotelManagementDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Enter Username and Password");
            } else
                try
                {
                    Connection.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) FROM UserTbl WHERE Uname ='"+UNameTb.Text+"' and Upass'" + PasswordTb.Text + "'", Connection);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if(dt.Rows[0][0].ToString() == "1")
                    {
                        Dashboard obj = new Dashboard();
                        obj.Show();
                        this.Hide();
                        Connection.Close();
                    } else
                    {
                        MessageBox.Show("Invalid Information");
                    }

                    Connection.Close();
                } catch (Exception)
                {
                    throw;
                }
        }
    }
}
