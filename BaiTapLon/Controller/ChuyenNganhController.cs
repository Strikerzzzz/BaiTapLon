﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon.Controller
{
    internal class ChuyenNganhController
    {
        // Lấy danh sách chuyên ngành
        public static DataTable GetAllChuyenNganh()
        {
            string query = "SELECT MaChuyenNganh, TenChuyenNganh FROM ChuyenNganh WHERE TrangThai = 'Initialize'";
            return DataBase.GetData(query);
        }

        // Thêm chuyên ngành
        public static bool AddChuyenNganh(string tenChuyenNganh)
        {
            string query = "INSERT INTO ChuyenNganh (TenChuyenNganh, TrangThai) VALUES (@TenChuyenNganh, 'Initialize')";
            SqlParameter[] parameters = {
                new SqlParameter("@TenChuyenNganh", tenChuyenNganh)
            };
            return new DataBase().UpdateData(query, parameters);
        }

        // Sửa chuyên ngành
        public static bool UpdateChuyenNganh(string maChuyenNganh, string tenChuyenNganh)
        {
            string query = "UPDATE ChuyenNganh SET TenChuyenNganh = @TenChuyenNganh WHERE MaChuyenNganh = @MaChuyenNganh";
            SqlParameter[] parameters = {
                new SqlParameter("@TenChuyenNganh", tenChuyenNganh),
                new SqlParameter("@MaChuyenNganh", maChuyenNganh)
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
