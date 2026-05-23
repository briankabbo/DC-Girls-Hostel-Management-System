using System.Windows.Forms;
using GMS_Kabbo.Data;

namespace GMS_Kabbo.Forms
{
    public partial class Bookings : Form
    {
        public Bookings()
        {
            InitializeComponent();
            LoadBookings();
        }

        private void LoadBookings()
        {
            BookingData.DataSource = BookingRepository.GetAll();
        }

        private void FilterBooking()
        {
            if (RTypeCb.SelectedItem == null)
                return;

            BookingData.DataSource = BookingRepository.GetByRoomType(RTypeCb.SelectedItem.ToString());
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e) => LoadBookings();

        private void RTypeCb_SelectionChangeCommitted(object sender, EventArgs e) => FilterBooking();

        private void pictureBox1_Click(object sender, EventArgs e) => NavigationHelper.Open<Dashboard>(this);
        private void pictureBox3_Click(object sender, EventArgs e) => NavigationHelper.Open<Bookings>(this);
        private void pictureBox5_Click(object sender, EventArgs e) => NavigationHelper.Open<login>(this);
        private void pictureBox4_Click(object sender, EventArgs e) => NavigationHelper.Open<Users>(this);
        private void pictureBox2_Click(object sender, EventArgs e) => NavigationHelper.Open<Customers>(this);
    }
}
