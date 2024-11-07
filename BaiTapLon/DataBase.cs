using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon
{
    internal class DataBase
    {
        //Biến conn dùng để kết nối
        private static SqlConnection conn;
        //Mở kết nối 
        public static void OpenConnect()
        {
            if (conn == null)
            {
                // Pull về từ git thì sửa lại đi, cái này sau này fix cứng db vào hệ thống sau
                conn = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Application.StartupPath}\HeThongQuanLyDiem.mdf;Integrated Security=True;Connect Timeout=30");
            }

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        //Đóng kết nối
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
        /*
        Ví dụ cách dùng:
        string query = "INSERT INTO nhanvien (Hotennha nvien,NgaySinh, DiaChi, DienThoai) VALUES (@Hotennhanvien, @NgaySinh, @DiaChi, @DienThoai)";
        SqlParameter[] parameters = {
            new SqlParameter("@Hotennhanvien", txtHoTen.Text),
            new SqlParameter("@NgaySinh", dateTimePickerNgaySinh.Value),
            new SqlParameter("@DiaChi", txtDiaChi.Text),
            new SqlParameter("@DienThoai", txtDienThoai.Text),
        };
        int result = new DataBase().UpdateData(query, parameters); NHỚ TRUYỀN ĐỦ 2 THAM SỐ (query, parameters)
          */
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
    }
}
