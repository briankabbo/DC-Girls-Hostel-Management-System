using System;
using System.Configuration;
using System.Data;
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

        public static void WithConnection(Action<SqlConnection> action)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                action(connection);
            }
        }

        public static T WithConnection<T>(Func<SqlConnection, T> func)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return func(connection);
            }
        }

        public static DataTable FillTable(string sql, Action<SqlCommand> configure = null)
        {
            return WithConnection(connection =>
            {
                using (var cmd = new SqlCommand(sql, connection))
                {
                    configure?.Invoke(cmd);
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            });
        }

        public static int Execute(string sql, Action<SqlCommand> configure = null)
        {
            return WithConnection(connection =>
            {
                using (var cmd = new SqlCommand(sql, connection))
                {
                    configure?.Invoke(cmd);
                    return cmd.ExecuteNonQuery();
                }
            });
        }

        public static object Scalar(string sql, Action<SqlCommand> configure = null)
        {
            return WithConnection(connection =>
            {
                using (var cmd = new SqlCommand(sql, connection))
                {
                    configure?.Invoke(cmd);
                    return cmd.ExecuteScalar();
                }
            });
        }
    }
}
