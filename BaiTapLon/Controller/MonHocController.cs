using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon.Controller
{
    internal class MonHocController
    {
        // Lấy tất cả môn học
        public static DataTable GetAllMonHoc()
        {
            string query = "SELECT MaMon, TenMon, SoTinChi, lm.LoaiMon, TongSoBuoiHoc FROM MonHoc mh LEFT JOIN LoaiMon lm ON lm.IDLoaiMon = mh.IDLoaiMon WHERE mh.TrangThai = 'Initialize'";
            return DataBase.GetData(query);
        }

        // Thêm môn học
        public static bool AddMonHoc(string tenMon, int soTinChi, string idLoaiMon, int tongSoBuoiHoc)
        {
            string query = "INSERT INTO MonHoc (TenMon, SoTinChi, IDLoaiMon, TongSoBuoiHoc, TrangThai) VALUES (@TenMon, @SoTinChi, @IDLoaiMon, @TongSoBuoiHoc, 'Initialize')";
            SqlParameter[] parameters = {
                new SqlParameter("@TenMon", tenMon),
                new SqlParameter("@SoTinChi", soTinChi),
                new SqlParameter("@IDLoaiMon", idLoaiMon),
                new SqlParameter("@TongSoBuoiHoc", tongSoBuoiHoc)
            };
            return new DataBase().UpdateData(query, parameters);
        }

        // Sửa môn học
        public static bool UpdateMonHoc(string maMon, string tenMon, int soTinChi, string idLoaiMon, int tongSoBuoiHoc)
        {
            string query = "UPDATE MonHoc SET TenMon = @TenMon, SoTinChi = @SoTinChi, IDLoaiMon = @IDLoaiMon, TongSoBuoiHoc = @TongSoBuoiHoc WHERE MaMon = @MaMon";
            SqlParameter[] parameters = {
                new SqlParameter("@MaMon", maMon),
                new SqlParameter("@TenMon", tenMon),
                new SqlParameter("@SoTinChi", soTinChi),
                new SqlParameter("@IDLoaiMon", idLoaiMon),
                new SqlParameter("@TongSoBuoiHoc", tongSoBuoiHoc)
            };
            return new DataBase().UpdateData(query, parameters);
        }

        // Xóa môn học (cập nhật trạng thái)
        public static bool DeleteMonHoc(string maMon)
        {
            string query = "UPDATE MonHoc SET TrangThai = 'Deleted' WHERE MaMon = @MaMon";
            SqlParameter[] parameters = {
                new SqlParameter("@MaMon", maMon)
            };
            return new DataBase().UpdateData(query, parameters);
        }
    }
}
