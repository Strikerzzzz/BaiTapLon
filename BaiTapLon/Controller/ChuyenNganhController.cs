using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaiTapLon.Model;

namespace BaiTapLon.Controller
{
    internal class ChuyenNganhController
    {
        // Lấy danh sách chuyên ngành
        public static List<ChuyenNganh> GetAllChuyenNganh()
        {
            string query = "SELECT MaChuyenNganh, TenChuyenNganh FROM ChuyenNganh WHERE TrangThai = 'Initialize'";
            DataTable dt = DataBase.GetData(query);
            List<ChuyenNganh> list = new List<ChuyenNganh>();

            foreach (DataRow row in dt.Rows)
            {
                ChuyenNganh chuyenNganh = new ChuyenNganh
                {
                    MaChuyenNganh = row["MaChuyenNganh"].ToString(),
                    TenChuyenNganh = row["TenChuyenNganh"].ToString()
                };
                list.Add(chuyenNganh);
            }

            return list;
        }

        // Thêm chuyên ngành
        public static bool AddChuyenNganh(ChuyenNganh chuyenNganh)
        {
            string query = "INSERT INTO ChuyenNganh (TenChuyenNganh, TrangThai) VALUES (@TenChuyenNganh, 'Initialize')";
            SqlParameter[] parameters = {
                new SqlParameter("@TenChuyenNganh", chuyenNganh.TenChuyenNganh)
            };
            return new DataBase().UpdateData(query, parameters);
        }

        // Sửa chuyên ngành
        public static bool UpdateChuyenNganh(ChuyenNganh chuyenNganh)
        {
            string query = "UPDATE ChuyenNganh SET TenChuyenNganh = @TenChuyenNganh WHERE MaChuyenNganh = @MaChuyenNganh";
            SqlParameter[] parameters = {
                new SqlParameter("@TenChuyenNganh", chuyenNganh.TenChuyenNganh),
                new SqlParameter("@MaChuyenNganh", chuyenNganh.MaChuyenNganh)
            };
            return new DataBase().UpdateData(query, parameters);
        }
        // Xóa chuyên ngành (cập nhật trạng thái)
        public static bool DeleteChuyenNganh(string maChuyenNganh)
        {
            string query = "UPDATE ChuyenNganh SET TrangThai = 'Deleted' WHERE MaChuyenNganh = @MaChuyenNganh";
            SqlParameter[] parameters = {
                new SqlParameter("@MaChuyenNganh", maChuyenNganh)
            };
            return new DataBase().UpdateData(query, parameters);
        }
    }
}
