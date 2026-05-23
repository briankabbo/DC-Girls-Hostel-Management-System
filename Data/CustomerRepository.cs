using System;
using System.Data;

namespace GMS_Kabbo.Data
{
    internal static class CustomerRepository
    {
        public static DataTable GetAll()
        {
            return DatabaseHelper.FillTable("SELECT * FROM CustomerTbl");
        }

        public static int GetCount()
        {
            return (int)DatabaseHelper.Scalar("SELECT COUNT(*) FROM CustomerTbl");
        }

        public static void Insert(string name, string phone, string maritalStatus, DateTime dob, string room, string profession)
        {
            DatabaseHelper.Execute(
                "INSERT INTO CustomerTbl (CusName, CusPhone, CusMs, CusDOB, CusRoom, CusProf) VALUES (@CN, @CP, @CMS, @CD, @Room, @Prof)",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@CN", name);
                    cmd.Parameters.AddWithValue("@CP", phone);
                    cmd.Parameters.AddWithValue("@CMS", maritalStatus);
                    cmd.Parameters.AddWithValue("@CD", dob.Date);
                    cmd.Parameters.AddWithValue("@Room", room);
                    cmd.Parameters.AddWithValue("@Prof", profession);
                });
        }

        public static void Update(int customerId, string name, string phone, string maritalStatus, DateTime dob, string room, string profession)
        {
            DatabaseHelper.Execute(
                "UPDATE CustomerTbl SET CusName = @CN, CusPhone = @CP, CusMs = @CMS, CusDOB = @CD, CusRoom = @Room, CusProf = @Prof WHERE CusId = @CusId",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@CN", name);
                    cmd.Parameters.AddWithValue("@CP", phone);
                    cmd.Parameters.AddWithValue("@CMS", maritalStatus);
                    cmd.Parameters.AddWithValue("@CD", dob.Date);
                    cmd.Parameters.AddWithValue("@Room", room);
                    cmd.Parameters.AddWithValue("@Prof", profession);
                    cmd.Parameters.AddWithValue("@CusId", customerId);
                });
        }

        public static void Delete(int customerId)
        {
            DatabaseHelper.Execute(
                "DELETE FROM CustomerTbl WHERE CusId = @CusId",
                cmd => cmd.Parameters.AddWithValue("@CusId", customerId));
        }

        public static string GetNameById(object customerId)
        {
            var result = DatabaseHelper.Scalar(
                "SELECT CusName FROM CustomerTbl WHERE CusId = @CusId",
                cmd => cmd.Parameters.AddWithValue("@CusId", customerId));
            return result?.ToString();
        }

        public static DataTable GetIdList()
        {
            return DatabaseHelper.FillTable("SELECT CusId FROM CustomerTbl");
        }
    }
}
