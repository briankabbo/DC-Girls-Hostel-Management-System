using System.Data;

namespace GMS_Kabbo.Data
{
    internal static class BookingRepository
    {
        public static DataTable GetAll()
        {
            return DatabaseHelper.FillTable("SELECT * FROM BookingTbl");
        }

        public static DataTable GetByRoomType(string roomType)
        {
            return DatabaseHelper.FillTable(
                "SELECT * FROM BookingTbl WHERE RType = @RType",
                cmd => cmd.Parameters.AddWithValue("@RType", roomType));
        }

        public static int GetCount()
        {
            return (int)DatabaseHelper.Scalar("SELECT COUNT(*) FROM BookingTbl");
        }

        public static void Insert(int customerId, string customerName, int roomId, string roomType, int cost)
        {
            DatabaseHelper.Execute(
                "INSERT INTO BookingTbl (CusId, CusName, RId, RNum, RType, BCost) VALUES (@CI, @CN, @RI, @RN, @RT, @RC)",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@CI", customerId);
                    cmd.Parameters.AddWithValue("@CN", customerName);
                    cmd.Parameters.AddWithValue("@RI", roomId);
                    cmd.Parameters.AddWithValue("@RN", roomId);
                    cmd.Parameters.AddWithValue("@RT", roomType);
                    cmd.Parameters.AddWithValue("@RC", cost);
                });
        }
    }
}
