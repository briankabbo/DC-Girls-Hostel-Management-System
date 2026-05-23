using System;
using System.Data;
using System.Windows.Forms;
using GMS_Kabbo.Data;

namespace GMS_Kabbo.Forms
{
    public partial class Dashboard : Form
    {
        private int _roomNumber;
        private string _roomType;
        private int _roomCost;

        public Dashboard()
        {
            InitializeComponent();
            RefreshStats();
            LoadCustomerIds();
        }

        private void RefreshStats()
        {
            try
            {
                int booked = RoomRepository.CountByStatus(RoomRepository.BookedStatus);
                int free = RoomRepository.TotalRooms - booked;
                int bookedPercent = (booked * 100) / RoomRepository.TotalRooms;
                int freePercent = (free * 100) / RoomRepository.TotalRooms;

                BLbl.Text = $"{booked} Booked";
                AVLbl.Text = $"{free} Available";
                AVLbl2.Text = $"{free}";
                FreeRoomsProgress.Value = freePercent;
                Bprogress.Value = bookedPercent;
                Aprogress.Value = freePercent;
                CustNumLbl.Text = $"{CustomerRepository.GetCount()}";
                BookedLbl.Text = $"{BookingRepository.GetCount()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadCustomerIds()
        {
            var table = CustomerRepository.GetIdList();
            CusIdCb.ValueMember = "CusId";
            CusIdCb.DataSource = table;
        }

        private void GetCustomerName()
        {
            if (CusIdCb.SelectedValue == null)
            {
                MessageBox.Show("Please select a customer.");
                return;
            }

            try
            {
                var name = CustomerRepository.GetNameById(CusIdCb.SelectedValue);
                CusNameTb.Text = string.IsNullOrEmpty(name) ? "Customer not found" : name;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void GetRoomType()
        {
            var row = RoomRepository.GetRoom(_roomNumber);
            if (row == null)
            {
                _roomType = "Room type not found";
                _roomCost = 0;
            }
            else
            {
                _roomType = row["RType"].ToString();
                _roomCost = Convert.ToInt32(row["RCost"]);
            }

            MessageBox.Show("Room Type: " + _roomType);
        }

        private void ResetBookingForm()
        {
            _roomType = "";
            _roomCost = 0;
            _roomNumber = 0;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CusNameTb.Text) || _roomNumber == 0)
            {
                MessageBox.Show("Select A Room");
                return;
            }

            try
            {
                GetRoomType();
                BookingRepository.Insert(
                    Convert.ToInt32(CusIdCb.SelectedValue),
                    CusNameTb.Text,
                    _roomNumber,
                    _roomType ?? "",
                    _roomCost);
                RoomRepository.SetStatus(_roomNumber, RoomRepository.BookedStatus);
                MessageBox.Show("Room Booked Successfully");
                ResetBookingForm();
                RefreshStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CusIdCb_SelectionChangeCommitted(object sender, EventArgs e) => GetCustomerName();

        private void Room_Click(object sender, EventArgs e)
        {
            var name = (sender as Control)?.Name;
            if (name != null && name.StartsWith("R") && int.TryParse(name.Substring(1), out int roomId))
                _roomNumber = roomId;
        }

        private void pictureBox2_Click(object sender, EventArgs e) => NavigationHelper.Open<Customers>(this);
        private void pictureBox3_Click(object sender, EventArgs e) => NavigationHelper.Open<Bookings>(this);
        private void pictureBox5_Click(object sender, EventArgs e) => NavigationHelper.Open<login>(this);
        private void pictureBox4_Click_1(object sender, EventArgs e) => NavigationHelper.Open<Users>(this);
        private void pictureBox1_Click_1(object sender, EventArgs e) => NavigationHelper.Open<Dashboard>(this);
    }
}
