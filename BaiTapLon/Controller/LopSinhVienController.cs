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
    internal class LopSinhVienController
    {
        public static List<LopSinhVien> GetAllLopHocSinhVien()
        {
            string query = @"SELECT lhsv.ID, lh.TenLop, CONCAT(sv.HoDem, ' ', sv.Ten,'-',sv.MaSV) AS TenSinhVien 
                             FROM LopHoc_SinhVien lhsv 
                             LEFT JOIN LopHoc lh ON lh.MaLop = lhsv.MaLop 
                             LEFT JOIN SinhVien sv ON lhsv.MaSV = sv.MaSV 
                             WHERE lhsv.TrangThai = 'Initialize'";
            DataTable dt = DataBase.GetData(query);
            List<LopSinhVien> list = new List<LopSinhVien>();

            foreach (DataRow row in dt.Rows)
            {
                LopSinhVien lopHocSinhVien = new LopSinhVien
                {
                    ID = Convert.ToInt32(row["ID"]),
                    MaLop = Convert.ToInt32(row["MaLop"]),
                  
                    MaSV = Convert.ToInt32(row["MaSV"]),
                   
                };
                list.Add(lopHocSinhVien);
            }

            return list;
        }
        public static bool AddLopHocSinhVien(LopSinhVien lopHocSinhVien)
        {
            string query = "INSERT INTO LopHoc_SinhVien (MaLop, MaSV, TrangThai) VALUES (@MaLop, @MaSV, 'Initialize')";
            SqlParameter[] parameters = {
                new SqlParameter("@MaLop", lopHocSinhVien.MaLop),
                new SqlParameter("@MaSV", lopHocSinhVien.MaSV)
            };
            return new DataBase().UpdateData(query, parameters);
        }
        public static bool UpdateLopHocSinhVien(LopSinhVien lopHocSinhVien)
        {
            string query = "UPDATE LopHoc_SinhVien SET MaLop = @MaLop, MaSV = @MaSV WHERE ID = @ID";
            SqlParameter[] parameters = {
                new SqlParameter("@MaLop", lopHocSinhVien.MaLop),
                new SqlParameter("@MaSV", lopHocSinhVien.MaSV),
                new SqlParameter("@ID", lopHocSinhVien.ID)
            };
            return new DataBase().UpdateData(query, parameters);
        }
        public static bool DeleteLopHocSinhVien(int id)
        {
            string query = "UPDATE LopHoc_SinhVien SET TrangThai = 'Deleted' WHERE ID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", id) };
            return new DataBase().UpdateData(query, parameters);
        }
    }
}
