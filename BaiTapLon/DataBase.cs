using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon
{
    internal class DataBase
    {
        private static SqlConnection conn;
        public static void OpenConnect()
        {
            if (conn == null)
            {
                conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=HeThongQuanLyDiem;Integrated Security=True");
            }

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public static void CloseConnect()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        public static SqlConnection GetConnection()
        {
            return conn;
        }

        public bool InsertData(string query, SqlParameter[] parameters)
        {
            OpenConnect();
            using (var command = new SqlCommand(query, GetConnection()))
            {
                command.Parameters.AddRange(parameters);
                bool result = command.ExecuteNonQuery() > 0;
                CloseConnect();
                return result;
            }
        }

        public static DataTable GetData(string query, SqlParameter[] parameters = null)
        {
            OpenConnect();
            using (var command = new SqlCommand(query, GetConnection()))
            using (var adapter = new SqlDataAdapter(command))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                CloseConnect();
                return dataTable;
            }
        }

        public bool UpdateData(string query, SqlParameter[] parameters)
        {
            OpenConnect();
            using (var command = new SqlCommand(query, GetConnection()))
            {
                command.Parameters.AddRange(parameters);
                bool result = command.ExecuteNonQuery() > 0;
                CloseConnect();
                return result;
            }
        }
        public bool DeleteData(string query, SqlParameter[] parameters)
        {
            OpenConnect();
            using (var command = new SqlCommand(query, GetConnection()))
            {
                command.Parameters.AddRange(parameters);
                bool result = command.ExecuteNonQuery() > 0;
                CloseConnect();
                return result;
            }
        }
    }
}
