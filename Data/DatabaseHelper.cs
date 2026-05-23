using System;
using System.Configuration;
using System.Data.SqlClient;

namespace GMS_Kabbo.Data
{
    internal static class DatabaseHelper
    {
        public static string ConnectionString
        {
            get
            {
                var cs = ConfigurationManager.ConnectionStrings["HostelDb"];
                if (cs == null || string.IsNullOrWhiteSpace(cs.ConnectionString))
                    throw new InvalidOperationException(
                        "Connection string 'HostelDb' is missing or empty in App.config.");
                return cs.ConnectionString;
            }
        }

        public static SqlConnection CreateConnection() => new SqlConnection(ConnectionString);
    }
}
