using BaiTapLon.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon.Controller
{
    internal class LopHocController
    {
        public static List<LopHoc> GetAllLopHoc()
        {
            string query = @"SELECT lh.MaLop, lh.TenLop, lh.KhoaHoc, lh.SoSVMax, mh.TenMon, CONCAT(hk.TenHocKy, N' - Năm ', hk.Nam) AS hocky FROM LopHoc lh LEFT JOIN MonHoc mh ON lh.MaMon = mh.MaMon LEFT JOIN HocKy hk ON lh.IDHocKy = hk.IDHocKy WHERE lh.TrangThai = 'Initialize'";
            DataTable dt = DataBase.GetData(query);
            List<LopHoc> list = new List<LopHoc>();

            foreach (DataRow row in dt.Rows)
            {
                LopHoc lopHoc = new LopHoc
                {
                    MaLop = Convert.ToInt32(row["MaLop"]),
                    TenLop = row["TenLop"].ToString(),
                    KhoaHoc = row["KhoaHoc"].ToString(),
                    SoSVMax = Convert.ToInt32(row["SoSVMax"]),
                    MaMon = row["MaMon"] != DBNull.Value ? Convert.ToInt32(row["MaMon"]) : (int?)null,
                    IDHocKy = row["IDHocKy"] != DBNull.Value ? Convert.ToInt32(row["IDHocKy"]) : (int?)null
                };
                list.Add(lopHoc);
            }

            return list;
        }
        public static bool AddLopHoc(LopHoc lopHoc)
        {
            string query = "INSERT INTO LopHoc (TenLop, KhoaHoc, SoSVMax, MaMon, TrangThai, IDHocKy) " +
                           "VALUES (@TenLop, @KhoaHoc, @SoSVMax, @MaMon, 'Initialize', @IDHocKy)";
            SqlParameter[] parameters = {
                new SqlParameter("@TenLop", lopHoc.TenLop),
                new SqlParameter("@KhoaHoc", lopHoc.KhoaHoc),
                new SqlParameter("@SoSVMax", lopHoc.SoSVMax),
                new SqlParameter("@MaMon", lopHoc.MaMon),
                new SqlParameter("@IDHocKy", lopHoc.IDHocKy)
            };
            return new DataBase().UpdateData(query, parameters);
        }
        public static bool UpdateLopHoc(LopHoc lopHoc)
        {
            string query = "UPDATE LopHoc SET TenLop = @TenLop, KhoaHoc = @KhoaHoc, SoSVMax = @SoSVMax, MaMon = @MaMon, IDHocKy = @IDHocKy " +
                           "WHERE MaLop = @MaLop";
            SqlParameter[] parameters = {
                new SqlParameter("@TenLop", lopHoc.TenLop),
                new SqlParameter("@KhoaHoc", lopHoc.KhoaHoc),
                new SqlParameter("@SoSVMax", lopHoc.SoSVMax),
                new SqlParameter("@MaMon", lopHoc.MaMon),
                new SqlParameter("@IDHocKy", lopHoc.IDHocKy),
                new SqlParameter("@MaLop", lopHoc.MaLop)
            };
            return new DataBase().UpdateData(query, parameters);
        }
        public static bool DeleteLopHoc(int maLop)
        {
            string query = "UPDATE LopHoc SET TrangThai = 'Deleted' WHERE MaLop = @MaLop";
            SqlParameter[] parameters = {
                new SqlParameter("@MaLop", maLop)
            };
            return new DataBase().UpdateData(query, parameters);
        }



    }
}
