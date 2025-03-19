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
        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\NOC-02\Documents\HotelManagementDB.mdf;Integrated Security=True;Connect Timeout=30");
        int free, Booked;
        int bper, freeper;
        private void CountBooked()
        {
            string status = "Booked";
            int totalRooms = 20;

            try
            {
                Connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM RoomTbl WHERE RStatus = @status", Connection);
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }

        private void CountCustomer()
        {
            try
            {
                Connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM CustomerTbl", Connection);

                int customerCount = Convert.ToInt32(cmd.ExecuteScalar());
                CustNumLbl.Text = $"{customerCount}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }

        private void CountBookings()
        {
            try
            {
                Connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM BookingTbl", Connection);

                int BookingCount = Convert.ToInt32(cmd.ExecuteScalar());
                BookedLbl.Text = $"{BookingCount}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Connection.Close();
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
            Connection.Open();
            SqlCommand cmd = new SqlCommand("Select CusID FROM CustomerTbl",Connection);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CusId", typeof(int));
            dt.Load(rdr);
            CusIdCb.ValueMember = "CusId";
            CusIdCb.DataSource = dt;
            Connection.Close();
        }

        int RoomNumber = 0;
        private void GetCustomerName()
        {
            try
            {
                Connection.Open();
                string query = "SELECT CusName FROM CustomerTbl WHERE CusId = @CusId";

                using (SqlCommand cmd = new SqlCommand(query, Connection))
                {
                    if (CusIdCb.SelectedValue == null)
                    {
                        MessageBox.Show("Please select a customer.");
                        return;
                    }
                    cmd.Parameters.AddWithValue("@CusId", CusIdCb.SelectedValue);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            CusNameTb.Text = dt.Rows[0]["CusName"].ToString();
                        }
                        else
                        {
                            CusNameTb.Text = "Customer not found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        string RType;
        int RC;
        private void GetRoomType()
        {
            try
            {
                Connection.Open();
                string query = "SELECT RType, RCost FROM RoomTbl WHERE RId = @RoomNumber";

                using (SqlCommand cmd = new SqlCommand(query, Connection))
                {
                    cmd.Parameters.AddWithValue("@RoomNumber", RoomNumber);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
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

                MessageBox.Show("Room Type: " + RType);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
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
                Connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE RoomTbl SET RStatus = @RS WHERE RId = @RKey", Connection);
                cmd.Parameters.AddWithValue("@RS", Status);
                cmd.Parameters.AddWithValue("@RKey", RoomNumber);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Room updated successfully");
                Connection.Close();
                Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error: " + Ex.Message);
            }
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CusNameTb.Text == ""|| RoomNumber == 0)
            {
                MessageBox.Show("Select A Room");
            } else
            {
                try
                {
                    GetRoomType();
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("insert into BookingTbl(CusId,CusName,RId,RNum,RType,BCost) values(@CI,@CN,@RI,@RN,@RT,@RC)", Connection);
                    cmd.Parameters.AddWithValue("@CI", CusIdCb.SelectedValue?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@CN", CusNameTb.Text);
                    cmd.Parameters.AddWithValue("@RI", RoomNumber);
                    cmd.Parameters.AddWithValue("@RN", RoomNumber);
                    cmd.Parameters.AddWithValue("@RT", RType);
                    cmd.Parameters.AddWithValue("@RC", RC);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Booked Successfully");
                    Reset();
                    Connection.Close();
                    UpdateRoom();
                } catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
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
