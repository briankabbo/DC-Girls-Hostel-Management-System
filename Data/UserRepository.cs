using System;
using System.Data;

namespace GMS_Kabbo.Data
{
    internal static class UserRepository
    {
        public static bool ValidateCredentials(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                return false;

            var storedPassword = GetPasswordHash(username.Trim());
            if (storedPassword == null || !PasswordHasher.Verify(password, storedPassword))
                return false;

            if (!PasswordHasher.IsHashed(storedPassword))
                SetPasswordHash(username.Trim(), PasswordHasher.Hash(password));

            return true;
        }

        public static string GetPasswordHash(string username)
        {
            var result = DatabaseHelper.Scalar(
                "SELECT Upass FROM UserTbl WHERE Uname = @Uname",
                cmd => cmd.Parameters.AddWithValue("@Uname", username));
            return result == null || result == DBNull.Value ? null : result.ToString();
        }

        public static void SetPasswordHash(string username, string passwordHash)
        {
            DatabaseHelper.Execute(
                "UPDATE UserTbl SET Upass = @Upass WHERE Uname = @Uname",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@Upass", passwordHash);
                    cmd.Parameters.AddWithValue("@Uname", username);
                });
        }

        public static DataTable GetList()
        {
            return DatabaseHelper.FillTable("SELECT UId, Uname, Uphone FROM UserTbl");
        }

        public static void Insert(string name, string phone, string passwordHash)
        {
            DatabaseHelper.Execute(
                "INSERT INTO UserTbl (Uname, Uphone, Upass) VALUES (@UN, @UP, @UPA)",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@UN", name);
                    cmd.Parameters.AddWithValue("@UP", phone);
                    cmd.Parameters.AddWithValue("@UPA", passwordHash);
                });
        }

        public static void Update(int userId, string name, string phone, string passwordHashOrNull)
        {
            if (string.IsNullOrEmpty(passwordHashOrNull))
            {
                DatabaseHelper.Execute(
                    "UPDATE UserTbl SET Uname = @UN, Uphone = @UP WHERE UId = @Ukey",
                    cmd =>
                    {
                        cmd.Parameters.AddWithValue("@UN", name);
                        cmd.Parameters.AddWithValue("@UP", phone);
                        cmd.Parameters.AddWithValue("@Ukey", userId);
                    });
            }
            else
            {
                DatabaseHelper.Execute(
                    "UPDATE UserTbl SET Uname = @UN, Uphone = @UP, Upass = @UPA WHERE UId = @Ukey",
                    cmd =>
                    {
                        cmd.Parameters.AddWithValue("@UN", name);
                        cmd.Parameters.AddWithValue("@UP", phone);
                        cmd.Parameters.AddWithValue("@UPA", passwordHashOrNull);
                        cmd.Parameters.AddWithValue("@Ukey", userId);
                    });
            }
        }

        public static void Delete(int userId)
        {
            DatabaseHelper.Execute(
                "DELETE FROM UserTbl WHERE UId = @Ukey",
                cmd => cmd.Parameters.AddWithValue("@Ukey", userId));
        }
    }
}
