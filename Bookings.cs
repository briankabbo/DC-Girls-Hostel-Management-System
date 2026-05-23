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
    public partial class Bookings : Form
    {
        public Bookings()
        {
            InitializeComponent();
            ShowBookings();
        }

        private void ShowBookings()
        {
            using (var connection = DatabaseHelper.CreateConnection())
            {
                connection.Open();
                var sda = new SqlDataAdapter("SELECT * FROM BookingTbl", connection);
                var ds = new DataSet();
                sda.Fill(ds);
                BookingData.DataSource = ds.Tables[0];
            }
        }

        private void FilterBooking()
        {
            if (RTypeCb.SelectedItem == null) return;

            using (var connection = DatabaseHelper.CreateConnection())
            {
                connection.Open();
                using (var cmd = new SqlCommand("SELECT * FROM BookingTbl WHERE RType = @RType", connection))
                {
                    cmd.Parameters.AddWithValue("@RType", RTypeCb.SelectedItem.ToString());
                    var sda = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    BookingData.DataSource = ds.Tables[0];
                }
            }
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
