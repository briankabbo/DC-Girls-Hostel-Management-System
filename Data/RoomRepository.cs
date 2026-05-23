using System.Data;

namespace GMS_Kabbo.Data
{
    internal static class RoomRepository
    {
        public const int TotalRooms = 20;
        public const string BookedStatus = "Booked";

        public static int CountByStatus(string status)
        {
            return (int)DatabaseHelper.Scalar(
                "SELECT COUNT(*) FROM RoomTbl WHERE RStatus = @status",
                cmd => cmd.Parameters.AddWithValue("@status", status));
        }

        public static void SetStatus(int roomId, string status)
        {
            DatabaseHelper.Execute(
                "UPDATE RoomTbl SET RStatus = @RS WHERE RId = @RKey",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@RS", status);
                    cmd.Parameters.AddWithValue("@RKey", roomId);
                });
        }

        public static DataRow GetRoom(int roomId)
        {
            var table = DatabaseHelper.FillTable(
                "SELECT RType, RCost FROM RoomTbl WHERE RId = @RoomNumber",
                cmd => cmd.Parameters.AddWithValue("@RoomNumber", roomId));
            return table.Rows.Count > 0 ? table.Rows[0] : null;
        }
    }
}
