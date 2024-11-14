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
    internal class MonHocController
    {
        // Lấy tất cả môn học
        public static List<MonHoc> GetAllMonHoc()
        {
            List<MonHoc> monHocs = new List<MonHoc>();
            string query = "SELECT MaMon, TenMon, SoTinChi, lm.LoaiMon, TongSoBuoiHoc FROM MonHoc mh LEFT JOIN LoaiMon lm ON lm.IDLoaiMon = mh.IDLoaiMon WHERE mh.TrangThai = 'Initialize'";
            DataTable dataTable = DataBase.GetData(query);

            foreach (DataRow row in dataTable.Rows)
            {
                MonHoc monHoc = new MonHoc(
                    row["MaMon"].ToString(),
                    row["TenMon"].ToString(),
                    Convert.ToInt32(row["SoTinChi"]),
                    row["LoaiMon"].ToString(),
                    Convert.ToInt32(row["TongSoBuoiHoc"])
                );
                monHocs.Add(monHoc);
            }

            return monHocs;
        }

        // Thêm môn học
        public static bool AddMonHoc(MonHoc monHoc)
        {
            string query = "INSERT INTO MonHoc (TenMon, SoTinChi, IDLoaiMon, TongSoBuoiHoc, TrangThai) VALUES (@TenMon, @SoTinChi, @IDLoaiMon, @TongSoBuoiHoc, 'Initialize')";
            SqlParameter[] parameters = {
                new SqlParameter("@TenMon", monHoc.TenMon),
                new SqlParameter("@SoTinChi", monHoc.SoTinChi),
                new SqlParameter("@IDLoaiMon", monHoc.IDLoaiMon),
                new SqlParameter("@TongSoBuoiHoc", monHoc.TongSoBuoiHoc)
            };
            return new DataBase().UpdateData(query, parameters);
        }

        // Sửa môn học
        public static bool UpdateMonHoc(MonHoc monHoc)
        {
            string query = "UPDATE MonHoc SET TenMon = @TenMon, SoTinChi = @SoTinChi, IDLoaiMon = @IDLoaiMon, TongSoBuoiHoc = @TongSoBuoiHoc WHERE MaMon = @MaMon";
            SqlParameter[] parameters = {
                new SqlParameter("@MaMon", monHoc.MaMon),
                new SqlParameter("@TenMon", monHoc.TenMon),
                new SqlParameter("@SoTinChi", monHoc.SoTinChi),
                new SqlParameter("@IDLoaiMon", monHoc.IDLoaiMon),
                new SqlParameter("@TongSoBuoiHoc", monHoc.TongSoBuoiHoc)
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
