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
    public partial class Bookings : Form
    {
        public Bookings()
        {
            InitializeComponent();
            ShowBookings();
        }

        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\NOC-02\Documents\HotelManagementDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowBookings()
        {
            Connection.Open();

            string Query = "Select * from BookingTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookingData.DataSource = ds.Tables[0];
            Connection.Close();

        }
        private void FilterBooking()
        {
            Connection.Open();

            string Query = $"SELECT * FROM BookingTbl WHERE RType = '{RTypeCb.SelectedItem.ToString()}'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookingData.DataSource = ds.Tables[0];
            Connection.Close();

        }
        private void Bookings_Load(object sender, EventArgs e)
        {

        }

        private void BookingDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            ShowBookings();
        }

        private void RTypeCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterBooking();
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
    }
}
