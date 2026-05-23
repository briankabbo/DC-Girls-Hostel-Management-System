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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountBooked();
            CountCustomer();
            CountBookings();
            GetCustomer();
            GetCustomerName();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar2_ValueChanged(object sender, EventArgs e)
        {

        }
        int free, Booked;
        int bper, freeper;
        private void CountBooked()
        {
            string status = "Booked";
            int totalRooms = 20;

            try
            {
                using (var connection = DatabaseHelper.CreateConnection())
                {
                    connection.Open();
                    using (var cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM RoomTbl WHERE RStatus = @status", connection))
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                        int bookedCount = Convert.ToInt32(cmd.ExecuteScalar());
                        free = totalRooms - bookedCount;
                        Booked = bookedCount;
                        bper = (Booked * 100) / totalRooms;
                        freeper = (free * 100) / totalRooms;
                        BLbl.Text = $"{Booked} Booked";
                        AVLbl.Text = $"{free} Available";
                        AVLbl2.Text = $"{free}";
                        FreeRoomsProgress.Value = freeper;
                        Bprogress.Value = bper;
                        Aprogress.Value = freeper;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void CountCustomer()
        {
            try
            {
                using (var connection = DatabaseHelper.CreateConnection())
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("SELECT COUNT(*) FROM CustomerTbl", connection))
                    {
                        int customerCount = Convert.ToInt32(cmd.ExecuteScalar());
                        CustNumLbl.Text = $"{customerCount}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void CountBookings()
        {
            try
            {
                using (var connection = DatabaseHelper.CreateConnection())
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("SELECT COUNT(*) FROM BookingTbl", connection))
                    {
                        int bookingCount = Convert.ToInt32(cmd.ExecuteScalar());
                        BookedLbl.Text = $"{bookingCount}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void GetCustomer()
        {
            using (var connection = DatabaseHelper.CreateConnection())
            {
                connection.Open();
                using (var cmd = new SqlCommand("SELECT CusId FROM CustomerTbl", connection))
                using (var rdr = cmd.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Columns.Add("CusId", typeof(int));
                    dt.Load(rdr);
                    CusIdCb.ValueMember = "CusId";
                    CusIdCb.DataSource = dt;
                }
            }
        }

        int RoomNumber = 0;
        private void GetCustomerName()
        {
            try
            {
                if (CusIdCb.SelectedValue == null)
                {
                    MessageBox.Show("Please select a customer.");
                    return;
                }

                using (var connection = DatabaseHelper.CreateConnection())
                {
                    connection.Open();
                    using (var cmd = new SqlCommand(
                        "SELECT CusName FROM CustomerTbl WHERE CusId = @CusId", connection))
                    {
                        cmd.Parameters.AddWithValue("@CusId", CusIdCb.SelectedValue);
                        using (var sda = new SqlDataAdapter(cmd))
                        {
                            var dt = new DataTable();
                            sda.Fill(dt);
                            CusNameTb.Text = dt.Rows.Count > 0
                                ? dt.Rows[0]["CusName"].ToString()
                                : "Customer not found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        string RType;
        int RC;
        private void GetRoomType()
        {
            try
            {
                using (var connection = DatabaseHelper.CreateConnection())
                {
                    connection.Open();
                    using (var cmd = new SqlCommand(
                        "SELECT RType, RCost FROM RoomTbl WHERE RId = @RoomNumber", connection))
                    {
                        cmd.Parameters.AddWithValue("@RoomNumber", RoomNumber);
                        using (var sda = new SqlDataAdapter(cmd))
                        {
                            var dt = new DataTable();
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                RType = dt.Rows[0]["RType"].ToString();
                                RC = Convert.ToInt32(dt.Rows[0]["RCost"].ToString());
                            }
                            else
                            {
                                RType = "Room type not found";
                            }
                        }
                    }
                }

                MessageBox.Show("Room Type: " + RType);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private void Reset()
        {
            RType = "";
            RC = 0;
            RoomNumber = 0;

        }

        String Status = "Booked"; 
        private void UpdateRoom()
        {
            try
            {
                using (var connection = DatabaseHelper.CreateConnection())
                {
                    connection.Open();
                    using (var cmd = new SqlCommand(
                        "UPDATE RoomTbl SET RStatus = @RS WHERE RId = @RKey", connection))
                    {
                        cmd.Parameters.AddWithValue("@RS", Status);
                        cmd.Parameters.AddWithValue("@RKey", RoomNumber);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Room updated successfully");
                Reset();
                CountBooked();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CusNameTb.Text == "" || RoomNumber == 0)
            {
                MessageBox.Show("Select A Room");
            }
            else
            {
                try
                {
                    GetRoomType();
                    using (var connection = DatabaseHelper.CreateConnection())
                    {
                        connection.Open();
                        using (var cmd = new SqlCommand(
                            "INSERT INTO BookingTbl (CusId, CusName, RId, RNum, RType, BCost) VALUES (@CI, @CN, @RI, @RN, @RT, @RC)",
                            connection))
                        {
                            cmd.Parameters.AddWithValue("@CI", CusIdCb.SelectedValue ?? 0);
                            cmd.Parameters.AddWithValue("@CN", CusNameTb.Text);
                            cmd.Parameters.AddWithValue("@RI", RoomNumber);
                            cmd.Parameters.AddWithValue("@RN", RoomNumber);
                            cmd.Parameters.AddWithValue("@RT", RType ?? "");
                            cmd.Parameters.AddWithValue("@RC", RC);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Room Booked Successfully");
                    UpdateRoom();
                    CountBookings();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CusIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustomerName();
        }

        private void R1_Paint(object sender, PaintEventArgs e)
        {
      
        }

        private void label10_Click_1(object sender, EventArgs e)
        {

        }

        private void R2_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R3_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R4_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R5_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R6_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R7_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R8_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R9_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R10_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R11_Paint(object sender, PaintEventArgs e)
        {
         
        }

        private void R12_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R13_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R14_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R15_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R16_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R17_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R18_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R19_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void R20_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void label9_Click_1(object sender, EventArgs e)
        {

        }

        private void R1_Click(object sender, EventArgs e)
        {
            RoomNumber = 1;
        }

        private void R2_Click(object sender, EventArgs e)
        {
            RoomNumber = 2;
        }

        private void R3_Click(object sender, EventArgs e)
        {
            RoomNumber = 3;
        }

        private void R4_Click(object sender, EventArgs e)
        {
            RoomNumber = 4;
        }

        private void R5_Click(object sender, EventArgs e)
        {
            RoomNumber = 5;
        }

        private void R6_Click(object sender, EventArgs e)
        {
            RoomNumber = 6;
        }

        private void R7_Click(object sender, EventArgs e)
        {
            RoomNumber = 7;
        }

        private void R8_Click(object sender, EventArgs e)
        {
            RoomNumber = 8;
        }

        private void R9_Click(object sender, EventArgs e)
        {
            RoomNumber = 9;
        }

        private void R10_Click(object sender, EventArgs e)
        {
            RoomNumber = 10;
        }

        private void R11_Click(object sender, EventArgs e)
        {
            RoomNumber = 11;
        }

        private void R12_Click(object sender, EventArgs e)
        {
            RoomNumber = 12;
        }

        private void R13_Click(object sender, EventArgs e)
        {
            RoomNumber = 13;
        }

        private void R14_Click(object sender, EventArgs e)
        {
            RoomNumber = 14;
        }

        private void R15_Click(object sender, EventArgs e)
        {
            RoomNumber = 15;
        }

        private void R16_Click(object sender, EventArgs e)
        {
            RoomNumber = 16;
        }

        private void R17_Click(object sender, EventArgs e)
        {
            RoomNumber = 17;
        }

        private void R18_Click(object sender, EventArgs e)
        {
            RoomNumber = 18;
        }

        private void R19_Click(object sender, EventArgs e)
        {
            RoomNumber = 19;
        }

        private void R20_Click(object sender, EventArgs e)
        {
            RoomNumber = 20;
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            login obj = new login();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            Users obj = new Users();
            obj.Show();
            this.Hide();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
